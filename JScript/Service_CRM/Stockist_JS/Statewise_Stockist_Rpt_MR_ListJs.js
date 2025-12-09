$(document)['ready'](function () {
    $('#divload')['hide']();
    var _0x9862x1 = GetParameterValues('Month');
    var _0x9862x2 = GetParameterValues('Year');
    var _0x9862x3 = GetParameterValues('State');
    $('#hdnState')['val'](_0x9862x3);
    var _0x9862x4 = _0x9862x1 + '^' + _0x9862x2 + '^' + _0x9862x3;
    Stock_Detail(_0x9862x4)
});

function StockistDetail(_0x9862x4) {
    var _0x9862x6 = _0x9862x4['split']('^');
    var _0x9862x7 = '<tr>';
    _0x9862x7 += '<th>S.No</th>';
    _0x9862x7 += '<th>Stockist Name</th>';
    _0x9862x7 += '<th>HQ Name</th>';
    _0x9862x7 += '<th>MR Name</th>';
    _0x9862x7 += '</tr>';
    $['ajax']({
        type: 'POST',
        url: 'webservice/Stockist_WebService.asmx/Get_Statewise_Stockist_MRDet',
        data: '{objStock:' + JSON['stringify'](_0x9862x4) + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (_0x9862x8) {
            if (_0x9862x8['d']['length'] > 0) {
                var _0x9862x9;
                var _0x9862xa = 0;
                for (var _0x9862xb = 0; _0x9862xb < _0x9862x8['d']['length']; _0x9862xb++) {
                    _0x9862x9 = _0x9862x8['d'][_0x9862xb]['StateName'];
                    _0x9862xa = _0x9862xa + 1;
                    _0x9862x7 += '<tr>';
                    _0x9862x7 += '<td>' + _0x9862xa + '</td>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['StockName'] + '</td>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['HqName'] + '</td>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['SfCode'] + '</td>';
                    _0x9862x7 += '</tr>'
                };
                if ($('#hdnState')['val']() == '10001') {
                    $('#lblStateName')['html']('-- All State --')
                } else {
                    $('#lblStateName')['html'](_0x9862x9)
                };
                $('#tblStockList')['html'](_0x9862x7)
            }
        },
        error: function (_0x9862xc) { }
    })
}

function Stock_Detail(_0x9862x4) {
    var _0x9862xe = [];
    var _0x9862x6 = _0x9862x4['split']('^');
    var _0x9862x7;
    var _0x9862xf;
    $('#divload')['show']();
    $('#tblStockList')['hide']();
    $('#tblStockEnter')['hide']();
    $['ajax']({
        type: 'POST',
        url: 'webservice/Stockist_WebService.asmx/Get_Statewise_Stockist_MRDet',
        data: '{objStock:' + JSON['stringify'](_0x9862x4) + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (_0x9862x8) {
            if (_0x9862x8['d']['length'] > 0) {
                var _0x9862x9;
                var _0x9862xa = 0;
                for (var _0x9862xb = 0; _0x9862xb < _0x9862x8['d']['length']; _0x9862xb++) {
                    if (_0x9862xb == 0) {
                        var _0x9862x10 = _0x9862x8['d'][_0x9862xb]['ArrStock_1'];
                        var _0x9862x11 = _0x9862x8['d'][_0x9862xb]['ArrStock_2'];
                        if (_0x9862x10['length'] > 0) {
                            $('#tblStockList')['show']();
                            var _0x9862xa = 0;
                            for (var _0x9862x12 = 0; _0x9862x12 < _0x9862x10['length']; _0x9862x12++) {
                                var _0x9862x13 = _0x9862x10[_0x9862x12];
                                var _0x9862x14 = _0x9862x13['split']('^');
                                _0x9862x9 = _0x9862x14[3];
                                _0x9862xa = _0x9862xa + 1;
                                _0x9862x7 += '<tr>';
                                _0x9862x7 += '<td>' + _0x9862xa + '</td>';
                                _0x9862x7 += '<td>' + _0x9862x14[0] + '</td>';
                                _0x9862x7 += '<td>' + _0x9862x14[1] + '</td>';
                                _0x9862x7 += '<td>' + _0x9862x14[2] + '</td>';
                                _0x9862x7 += '</tr>';
                                _0x9862xe['push'](_0x9862x14[1])
                            };
                            if ($('#hdnState')['val']() == '10001') {
                                $('#lblStateName')['html']('-- All State --')
                            } else {
                                $('#lblStateName')['html'](_0x9862x9)
                            };
                            $('#tblStockList')['append'](_0x9862x7);
                            _0x9862xe = unique(_0x9862xe);
                            if (_0x9862xe['length'] > 0) {
                                $('.HQCss')['append']($('<option></option>')['val']('All')['html']('--All--'));
                                for (var _0x9862x15 = 0; _0x9862x15 < _0x9862xe['length']; _0x9862x15++) {
                                    if (_0x9862xe[_0x9862x15] != _0x9862xe[_0x9862x15 + 1]) {
                                        $('.HQCss')['append']($('<option></option>')['val'](_0x9862xe[_0x9862x15])['html'](_0x9862xe[_0x9862x15]))
                                    }
                                }
                            }
                        }
                    } else {
                        var _0x9862x16 = _0x9862x8['d'][_0x9862xb]['ArrStock_2'];
                        if (_0x9862x16['length'] > 0) {
                            $('#tblStockEnter')['show']();
                            var _0x9862xa = 0;
                            for (var _0x9862x12 = 0; _0x9862x12 < _0x9862x16['length']; _0x9862x12++) {
                                var _0x9862x13 = _0x9862x16[_0x9862x12];
                                var _0x9862x14 = _0x9862x13['split']('^');
                                _0x9862x9 = _0x9862x14[3];
                                _0x9862xa = _0x9862xa + 1;
                                _0x9862xf += '<tr>';
                                _0x9862xf += '<td>' + _0x9862xa + '</td>';
                                _0x9862xf += '<td>' + _0x9862x14[0] + '</td>';
                                _0x9862xf += '<td>' + _0x9862x14[1] + '</td>';
                                _0x9862xf += '<td>' + _0x9862x14[2] + '</td>';
                                _0x9862xf += '<td>' + _0x9862x14[4] + '</td>';
                                _0x9862xf += '</tr>';
                                _0x9862xe['push'](_0x9862x14[1])
                            };
                            if ($('#hdnState')['val']() == '10001') {
                                $('#lblStateName')['html']('-- All State --')
                            } else {
                                $('#lblStateName')['html'](_0x9862x9)
                            };
                            $('#tblStockEnter')['append'](_0x9862xf)
                        }
                    }
                }
            };
            $('#divload')['hide']()
        },
        error: function (_0x9862xc) {
            $('#divload')['hide']()
        }
    })
}

function myFunction() {
    var _0x9862x18, _0x9862x19, _0x9862x1a, _0x9862x1b, _0x9862x1c, _0x9862xb;
    _0x9862x18 = document['getElementById']('ddlHq');
    _0x9862x19 = _0x9862x18['value']['toUpperCase']();
    _0x9862x1a = document['getElementById']('tblStockList');
    _0x9862x1b = _0x9862x1a['getElementsByTagName']('tr');
    var _0x9862xa = 0;
    if ($('#ddlHq')['val']() == 'All') {
        for (_0x9862xb = 0; _0x9862xb < _0x9862x1b['length']; _0x9862xb++) {
            _0x9862x1c = _0x9862x1b[_0x9862xb]['getElementsByTagName']('td')[2];
            var _0x9862x1d = _0x9862x1b[_0x9862xb]['getElementsByTagName']('td')[0];
            if (_0x9862x1c) {
                _0x9862x1b[_0x9862xb]['style']['display'] = '';
                _0x9862xa += 1;
                _0x9862x1d['innerHTML'] = _0x9862xa
            }
        }
    } else {
        for (_0x9862xb = 0; _0x9862xb < _0x9862x1b['length']; _0x9862xb++) {
            _0x9862x1c = _0x9862x1b[_0x9862xb]['getElementsByTagName']('td')[2];
            var _0x9862x1d = _0x9862x1b[_0x9862xb]['getElementsByTagName']('td')[0];
            if (_0x9862x1c) {
                if (_0x9862x1c['innerHTML']['toUpperCase']()['indexOf'](_0x9862x19) > -1) {
                    _0x9862x1b[_0x9862xb]['style']['display'] = '';
                    _0x9862xa += 1;
                    _0x9862x1d['innerHTML'] = _0x9862xa
                } else {
                    _0x9862x1b[_0x9862xb]['style']['display'] = 'none'
                }
            }
        }
    }
}

function GetParameterValues(_0x9862x1f) {
    var _0x9862x20 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0x9862xb = 0; _0x9862xb < _0x9862x20['length']; _0x9862xb++) {
        var _0x9862x21 = _0x9862x20[_0x9862xb]['split']('=');
        if (_0x9862x21[0] == _0x9862x1f) {
            return _0x9862x21[1]
        }
    }
}

function StockistDetail(_0x9862x4) {
    var _0x9862x7 = '<tr>';
    _0x9862x7 += '<th>S.No</th>';
    _0x9862x7 += '<th style="display:none;">State Code</th>';
    _0x9862x7 += '<th>State Name</th>';
    _0x9862x7 += '<th>No of Stockist Available</th>';
    _0x9862x7 += '<th>Stockist Done</th>';
    _0x9862x7 += '<th>No of Remain Stock</th>';
    _0x9862x7 += '</tr>';
    $['ajax']({
        type: 'POST',
        url: 'webservice/Stockist_WebService.asmx/GetStockist_List',
        data: '{objStock:' + JSON['stringify'](_0x9862x4) + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (_0x9862x8) {
            if (_0x9862x8['d']['length'] > 0) {
                var _0x9862x22 = 0;
                for (var _0x9862xb = 0; _0x9862xb < _0x9862x8['d']['length']; _0x9862xb++) {
                    _0x9862x7 += '<tr>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['SNo'] + '</td>';
                    _0x9862x7 += '<td style="display:none;">' + _0x9862x8['d'][_0x9862xb]['StateCode'] + '</td>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['StateName'] + '</td>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['AvaStockist'] + '</td>';
                    _0x9862x7 += '<td>' + _0x9862x8['d'][_0x9862xb]['StockistDone'] + '</td>';
                    _0x9862x7 += '<td><a id="lnkStock_' + _0x9862xb + '" href="#"  title="RemainStock">' + _0x9862x8['d'][_0x9862xb]['RemainStock'] + '</a></td>';
                    _0x9862x22 = _0x9862x22 + parseInt(_0x9862x8['d'][_0x9862xb].RemainStock);
                    _0x9862x7 += '</tr>'
                };
                _0x9862x7 += '<tr>';
                _0x9862x7 += '<td colSpan="4">Total</td>';
                _0x9862x7 += '<td><a id="Total" href="#" title="Total">' + _0x9862x22 + '</a></td>';
                _0x9862x7 += '</tr>';
                $('#tblStockList')['html'](_0x9862x7)
            }
        },
        error: function (_0x9862xc) { }
    })
}

function unique(_0x9862x24) {
    var _0x9862x25 = [];
    for (i = 0; i < _0x9862x24['length']; i++) {
        if (_0x9862x25['indexOf'](_0x9862x24[i]) == -1) {
            _0x9862x25['push'](_0x9862x24[i])
        }
    };
    return _0x9862x25
}