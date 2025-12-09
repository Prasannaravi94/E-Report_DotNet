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
var OptType = '';
var DDl_Opt = '';
var DeAct_Val = 0;
var Active_Val = [];
$(document)['ready'](function () {
    $('.modal')['ajaxStart'](function () {
        $(this)['show']()
    })['ajaxStop'](function () {
        $(this)['hide']()
    });
    GetHQ_Det();
    var _0x8041xa = $('#SS_DivCode')['val']();
    var _0x8041xb = GetParameterValues('sfcode');
    var _0x8041xc = GetParameterValues('Sf_Name');
    var _0x8041xd = GetParameterValues('FMonth');
    var _0x8041xe = GetParameterValues('FYear');
    var _0x8041xf = GetParameterValues('TMonth');
    var _0x8041x10 = GetParameterValues('TYear');
    OptType = GetParameterValues('OptType');
    var _0x8041x11 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0x8041x12 = parseInt(_0x8041xd - 1);
    var _0x8041x13 = parseInt(_0x8041xf - 1);
    var _0x8041x14 = _0x8041x11[_0x8041x12];
    var _0x8041x15 = _0x8041x11[_0x8041x13];
    if (_0x8041xd != _0x8041xf) {
        ColMonth = _0x8041x14 + '-' + _0x8041x15
    } else {
        ColMonth = _0x8041x14 + '-' + _0x8041x15
    };
    var _0x8041x16 = '';
    if (OptType == 'Sale') {
        _0x8041x16 = 'Sale';
        DDl_Opt = '1'
    } else {
        if (OptType == 'Closing') {
            _0x8041x16 = 'Closing';
            DDl_Opt = '3'
        } else {
            if (OptType == 'Both') {
                _0x8041x16 = 'Sale & CB';
                DDl_Opt = '2'
            }
        }
    };
    var _0x8041x17 = 'Product - State wise Stockist (' + _0x8041x16 + ') From ' + _0x8041x14 + ' ' + _0x8041xe + ' to ' + ' ' + _0x8041x15 + ' ' + ' ' + _0x8041x10;
    var _0x8041x18 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0x8041xc + '</span>';
    $('#lblStock')['html'](_0x8041x17);
    $('#lblField')['html'](_0x8041x18);
    var _0x8041x19 = (parseInt(_0x8041x10) - parseInt(_0x8041xe)) * 12 + parseInt(_0x8041xf) - parseInt(_0x8041xd);
    var _0x8041x1a = parseInt(_0x8041xd);
    var _0x8041x1b = parseInt(_0x8041xe);
    if (_0x8041x19 >= 0) {
        for (var _0x8041x1c = 1; _0x8041x1c <= _0x8041x19 + 1; _0x8041x1c++) {
            var _0x8041x12 = parseInt(_0x8041x1a - 1);
            var _0x8041x14 = _0x8041x11[_0x8041x12];
            var _0x8041x1d = _0x8041x14;
            ArrMonth['push'](_0x8041x1d);
            _0x8041x1a = _0x8041x1a + 1;
            if (_0x8041x1a == 13) {
                _0x8041x1a = 1;
                _0x8041x1b = _0x8041x1b + 1
            }
        }
    }
});

function GetParameterValues(_0x8041x1f) {
    var _0x8041x20 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x8041x21 = 0; _0x8041x21 < _0x8041x20['length']; _0x8041x21++) {
        var _0x8041x22 = _0x8041x20[_0x8041x21]['split']('=');
        if (_0x8041x22[0] == _0x8041x1f) {
            return decodeURIComponent(_0x8041x22[1])
        }
    }
}

function Get_SaleCls_Field() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetType_Rpt_Field',
        data: '{}',
        dataType: 'json',
        success: function (_0x8041x24) {
            ArrSale = _0x8041x24['d'];
            GetRecord_Detail()
        },
        error: function (_0x8041x25) { }
    })
}

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Parameter',
        data: '{}',
        dataType: 'json',
        success: function (_0x8041x24) {
            ArrHQDet = _0x8041x24['d'];
            Get_SaleCls_Field()
        },
        error: function (_0x8041x25) { }
    })
}

function GetRecord_Detail() {
    var _0x8041x28 = {};
    var _0x8041x29 = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_StateHQ_SaleAndClosing',
        data: '{}',
        dataType: 'json',
        success: function (_0x8041x24) {
            dataTableSt = _0x8041x24['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0x8041x2a = '<table id="tblSecsale" class="table" style="min-width:95%;max-width:100%">';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<th rowspan="3" >#</th>';
                _0x8041x2a += '<th rowspan="3" >State</th>';
                _0x8041x2a += '<th rowspan="3" >HQ</th>';
                _0x8041x2a += '<th rowspan="3" >Stockist Name</th>';
                _0x8041x2a += '<th rowspan="3" >Product Name</th>';
                _0x8041x2a += '<th rowspan="3" >Product ERP Code</th>';
                _0x8041x2a += '<th rowspan="3" >Pack</th>';
                var _0x8041x2b = ArrSale['length'];
                var _0x8041x2c;
                if (OptType == 'Sale' || OptType == 'Closing') {
                    _0x8041x2c = parseInt(_0x8041x2b) + 1
                } else {
                    if (OptType == 'Both') {
                        _0x8041x2c = parseInt(_0x8041x2b) * 2
                    }
                };
                for (var _0x8041x2d = 0; _0x8041x2d < ArrMonth['length']; _0x8041x2d++) {
                    _0x8041x2a += '<th colspan="' + _0x8041x2c + '" >' + ArrMonth[_0x8041x2d] + '</th>'
                };
                _0x8041x2a += '<th colspan="' + _0x8041x2c + '" >Total</th>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                for (var _0x8041x2d = 0; _0x8041x2d < ArrMonth['length']; _0x8041x2d++) {
                    for (var _0x8041x2e = 0; _0x8041x2e < ArrSale['length']; _0x8041x2e++) {
                        var _0x8041x2f = ArrSale[_0x8041x2e]['Sec_Sale_Code'];
                        for (var _0x8041x30 = 0; _0x8041x30 < ArrHQDet['length']; _0x8041x30++) {
                            var _0x8041x31 = ArrHQDet[_0x8041x30]['Sec_Sale_Code'];
                            if ($['trim'](_0x8041x31) == $['trim'](_0x8041x2f)) {
                                _0x8041x2a += '<th   colspan="2" >' + ArrHQDet[_0x8041x30]['Sec_Sale_Name'] + '</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0x8041x30 = 0; _0x8041x30 < ArrHQDet['length']; _0x8041x30++) {
                    var _0x8041x31 = ArrHQDet[_0x8041x30]['Sec_Sale_Code'];
                    var _0x8041x32 = '';
                    for (var _0x8041x2e = 0; _0x8041x2e < ArrSale['length']; _0x8041x2e++) {
                        var _0x8041x2f = ArrSale[_0x8041x2e]['Sec_Sale_Code'];
                        if ($['trim'](_0x8041x31) == $['trim'](_0x8041x2f)) {
                            _0x8041x32 = '2';
                            _0x8041x2a += '<th   colspan="' + _0x8041x32 + '" >' + ArrHQDet[_0x8041x30]['Sec_Sale_Name'] + '</th>';
                            break
                        }
                    }
                };
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                for (var _0x8041x2d = 0; _0x8041x2d < ArrMonth['length']; _0x8041x2d++) {
                    for (var _0x8041x2e = 0; _0x8041x2e < ArrSale['length']; _0x8041x2e++) {
                        var _0x8041x2f = ArrSale[_0x8041x2e]['Sec_Sale_Code'];
                        for (var _0x8041x30 = 0; _0x8041x30 < ArrHQDet['length']; _0x8041x30++) {
                            var _0x8041x31 = ArrHQDet[_0x8041x30]['Sec_Sale_Code'];
                            if ($['trim'](_0x8041x31) == $['trim'](_0x8041x2f)) {
                                _0x8041x2a += '<th >Qty</th>';
                                _0x8041x2a += '<th >Value</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0x8041x30 = 0; _0x8041x30 < ArrHQDet['length']; _0x8041x30++) {
                    var _0x8041x31 = ArrHQDet[_0x8041x30]['Sec_Sale_Code'];
                    var _0x8041x33 = false;
                    for (var _0x8041x21 = 0; _0x8041x21 < ArrSale['length'] && !_0x8041x33; _0x8041x21++) {
                        if (ArrSale[_0x8041x21]['Sec_Sale_Code'] === _0x8041x31) {
                            _0x8041x33 = true;
                            break
                        }
                    };
                    if (_0x8041x33 == true) {
                        _0x8041x2a += '<th >Qty </th>';
                        _0x8041x2a += '<th >Value</th>'
                    }
                };
                _0x8041x2a += '</tr>';
                var _0x8041x34 = 0;
                for (var _0x8041x1c = 0; _0x8041x1c < dataTableSt['length']; _0x8041x1c++) {
                    var _0x8041x35 = dataTableSt[_0x8041x1c];
                    var _0x8041x36 = 0;
                    var _0x8041x37 = '';
                    var _0x8041x38 = _0x8041x35['length'];
                    _0x8041x34 = _0x8041x34 + 1;
                    _0x8041x2a += '<tr>';
                    $['each'](_0x8041x35, function (_0x8041x39, _0x8041x3a) {
                        var _0x8041x3b = _0x8041x39;
                        if (_0x8041x3a == null || _0x8041x3a == '0') {
                            _0x8041x3a = ''
                        };
                        if (_0x8041x3b == 'SNO' || _0x8041x3b == 'State' || _0x8041x3b == 'HQ' || _0x8041x3b == 'Stockist_Name' || _0x8041x3b == 'Prod_Name' || _0x8041x3b == 'Product_ERP_Code' || _0x8041x3b == 'Pack') {
                            if (_0x8041x3b == 'State') {
                                _0x8041x2a += '<td class="two">' + _0x8041x3a + '</td>'
                            } else {
                                if (_0x8041x3b == 'SNO') {
                                    _0x8041x2a += '<td>' + _0x8041x34 + '</td>'
                                } else {
                                    _0x8041x2a += '<td>' + _0x8041x3a + '</td>'
                                }
                            }
                        } else {
                            if (_0x8041x3b['includes']('_ABT') || _0x8041x3b['includes']('_ACT')) {
                                var _0x8041x3c = _0x8041x3b['split']('_');
                                var _0x8041x3d = _0x8041x3c[1];
                                var _0x8041x3e = _0x8041x3c[0];
                                if (_0x8041x3b['includes']('_ACT')) {
                                    if (_0x8041x3a != '') {
                                        _0x8041x3a = parseFloat(_0x8041x3a)['toFixed'](2)
                                    }
                                };
                                if (_0x8041x1c == dataTableSt['length'] - 1) {
                                    if (_0x8041x3b['includes']('_ACT')) {
                                        if (_0x8041x3a != '') {
                                            _0x8041x3a = parseFloat(_0x8041x3a)['toFixed'](2)
                                        }
                                    };
                                    _0x8041x2a += '<td>' + _0x8041x3a + '</td>'
                                } else {
                                    _0x8041x2a += '<td>' + _0x8041x3a + '</td>'
                                }
                            } else {
                                if (_0x8041x3b == 'T_SaleQty' || _0x8041x3b == 'T_SaleVal' || _0x8041x3b == 'T_ClsQty' || _0x8041x3b == 'T_ClsVal') {
                                    if (OptType == 'Sale') {
                                        if (_0x8041x3b == 'T_SaleVal' || _0x8041x3b == 'T_SaleQty') {
                                            if (_0x8041x3b == 'T_SaleVal') {
                                                if (_0x8041x3a != '') {
                                                    _0x8041x3a = parseFloat(_0x8041x3a)['toFixed'](2);
                                                    if (_0x8041x1c == dataTableSt['length'] - 1) {
                                                        Active_Val['push'](_0x8041x3a)
                                                    }
                                                }
                                            };
                                            _0x8041x2a += '<td style=color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;>' + _0x8041x3a + '</td>'
                                        }
                                    } else {
                                        if (OptType == 'Closing') {
                                            if (_0x8041x3b == 'T_ClsVal' || _0x8041x3b == 'T_ClsQty') {
                                                if (_0x8041x3b == 'T_ClsVal') {
                                                    if (_0x8041x3a != '') {
                                                        _0x8041x3a = parseFloat(_0x8041x3a)['toFixed'](2);
                                                        if (_0x8041x1c == dataTableSt['length'] - 1) {
                                                            Active_Val['push'](_0x8041x3a)
                                                        }
                                                    }
                                                };
                                                _0x8041x2a += '<td style=color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;>' + _0x8041x3a + '</td>'
                                            }
                                        } else {
                                            if (OptType == 'Both') {
                                                if (_0x8041x3b == 'T_SaleVal' || _0x8041x3b == 'T_SaleQty' || _0x8041x3b == 'T_ClsVal' || _0x8041x3b == 'T_ClsQty') {
                                                    if (_0x8041x3b == 'T_SaleVal' || _0x8041x3b == 'T_ClsVal') {
                                                        if (_0x8041x3a != '') {
                                                            _0x8041x3a = parseFloat(_0x8041x3a)['toFixed'](2);
                                                            if (_0x8041x1c == dataTableSt['length'] - 1) {
                                                                if (_0x8041x3b == 'T_SaleVal') {
                                                                    Active_Val['push'](_0x8041x3a)
                                                                } else {
                                                                    if (_0x8041x3b == 'T_ClsVal') {
                                                                        Active_Val['push'](_0x8041x3a)
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    };
                                                    _0x8041x2a += '<td style=color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;>' + _0x8041x3a + '</td>'
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
                    _0x8041x2a += '</tr>'
                };
                _0x8041x2a += '</table>';
                $('#divpnl')['append'](_0x8041x2a);
                TotalBg();
                if (OptType == 'Sale' || OptType == 'Closing') {
                    TotalDeActivate()
                } else {
                    if (OptType == 'Both') {
                        TotalDeActivate_Both()
                    }
                }
            };
            $('.modal')['hide']()
        },
        error: function (_0x8041x25) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    $('#tblSecsale tr:last td')['each'](function () {
        var _0x8041x40 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;')
    })
}

function groupTable(_0x8041x42, _0x8041x43, _0x8041x44) {
    if (_0x8041x44 === 0) {
        return
    };
    var _0x8041x21, _0x8041x45 = _0x8041x43,
        _0x8041x46 = 1,
        _0x8041x47 = [];
    var _0x8041x48 = _0x8041x42['find']('td:eq(' + _0x8041x45 + ')');
    var _0x8041x49 = $(_0x8041x48[0]);
    _0x8041x47['push'](_0x8041x42[0]);
    for (_0x8041x21 = 1; _0x8041x21 <= _0x8041x48['length']; _0x8041x21++) {
        if (_0x8041x49['text']() == $(_0x8041x48[_0x8041x21])['text']()) {
            _0x8041x46++;
            $(_0x8041x48[_0x8041x21])['addClass']('deleted');
            _0x8041x47['push'](_0x8041x42[_0x8041x21])
        } else {
            if (_0x8041x46 > 1) {
                _0x8041x49['attr']('rowspan', _0x8041x46);
                groupTable($(_0x8041x47), _0x8041x43 + 1, _0x8041x44 - 1)
            };
            _0x8041x46 = 1;
            _0x8041x47 = [];
            _0x8041x49 = $(_0x8041x48[_0x8041x21]);
            _0x8041x47['push'](_0x8041x42[_0x8041x21])
        }
    }
}

function TotalDeActivate_Both() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0x8041x24) {
            dataTableSt = _0x8041x24['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x8041x1c = dataTableSt['length'] - 1;
                var _0x8041x35 = dataTableSt[_0x8041x1c];
                var _0x8041x2a = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x8041x2a += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0x8041x2a += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<td style=background-color:#d4d4d4;>';
                _0x8041x2a += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x8041x2a += '</td>';
                var _0x8041x4b = 0;
                var _0x8041x34 = 0;
                var _0x8041x4c = 0;
                $['each'](_0x8041x35, function (_0x8041x39, _0x8041x3a) {
                    var _0x8041x3b = _0x8041x39;
                    if (_0x8041x3a == null || _0x8041x3a == '0') {
                        _0x8041x3a = ''
                    };
                    if (_0x8041x3b['includes']('_ABT') || _0x8041x3b['includes']('_ACT')) {
                        var _0x8041x4d = parseFloat(_0x8041x3a)['toFixed'](2);
                        DeAct_Val = _0x8041x4d;
                        if (_0x8041x34 == 0) {
                            DeAct_Val = _0x8041x4d;
                            _0x8041x4b = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val);
                            _0x8041x2a += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                            _0x8041x2a += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0x8041x4d;
                            _0x8041x4c = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0x8041x2a += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0x8041x2a += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0x8041x34 += 1
                    }
                });
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<td style=background-color:#d4d4d4;>';
                _0x8041x2a += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x8041x2a += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x8041x4b + '</span></td>';
                _0x8041x2a += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x8041x4c + '</span></td>';
                _0x8041x2a += '</td>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '</table>';
                $('#div_DeAct')['append'](_0x8041x2a)
            }
        },
        error: function (_0x8041x25) {
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
        success: function (_0x8041x24) {
            dataTableSt = _0x8041x24['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0x8041x1c = dataTableSt['length'] - 1;
                var _0x8041x35 = dataTableSt[_0x8041x1c];
                var _0x8041x2a = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<th  style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                if (OptType == 'Sale') {
                    _0x8041x2a += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>'
                } else {
                    if (OptType == 'Closing') {
                        _0x8041x2a += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>'
                    }
                };
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0x8041x2a += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<td style=background-color:#d4d4d4;>';
                _0x8041x2a += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0x8041x2a += '</td>';
                var _0x8041x4b = 0;
                $['each'](_0x8041x35, function (_0x8041x39, _0x8041x3a) {
                    var _0x8041x3b = _0x8041x39;
                    if (_0x8041x3a == null || _0x8041x3a == '0') {
                        _0x8041x3a = ''
                    };
                    if (_0x8041x3b['includes']('_ABT') || _0x8041x3b['includes']('_ACT')) {
                        var _0x8041x4d = parseFloat(_0x8041x3a)['toFixed'](2);
                        DeAct_Val = _0x8041x4d;
                        _0x8041x4b = parseFloat(Active_Val[0]) + parseFloat(DeAct_Val)
                    }
                });
                _0x8041x2a += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[0]) + '</td>';
                _0x8041x2a += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '<tr>';
                _0x8041x2a += '<td style=background-color:#d4d4d4;>';
                _0x8041x2a += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0x8041x2a += '<td colspan=2 style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0x8041x4b + '</span></td>';
                _0x8041x2a += '</td>';
                _0x8041x2a += '</tr>';
                _0x8041x2a += '</table>';
                $('#div_DeAct')['append'](_0x8041x2a)
            }
        },
        error: function (_0x8041x25) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0x8041x50) {
    sessionStorage['removeItem']('userObject');
    var _0x8041x51 = JSON['stringify'](_0x8041x50);
    window['sessionStorage']['setItem']('userObject', _0x8041x51)
}