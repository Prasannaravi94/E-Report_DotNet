var ArrHQDet = {};
var dataTableSt;
var ColMonth = '';
var ArrSale = {};
var ArrMonth = [];
var Prd_Type = '';
var ArrBrand = [];
var prd_SecSale = '';
var strMsg = '';
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
    var _0xf5dexd = $('#SS_DivCode')['val']();
    var _0xf5dexe = GetParameterValues('sfcode');
    var _0xf5dexf = GetParameterValues('Sf_Name');
    var _0xf5dex10 = GetParameterValues('FMonth');
    var _0xf5dex11 = GetParameterValues('FYear');
    var _0xf5dex12 = GetParameterValues('TMonth');
    var _0xf5dex13 = GetParameterValues('TYear');
    DDl_Opt = '2';

    function _0xf5dex14() {
        var _0xf5dex15 = window['location']['search'];
        var _0xf5dex16 = /([^?&=]*)=([^&]*)/g;
        var _0xf5dex17 = {};
        var _0xf5dex18 = null;
        while (_0xf5dex18 = _0xf5dex16['exec'](_0xf5dex15)) {
            _0xf5dex17[_0xf5dex18[1]] = decodeURIComponent(_0xf5dex18[2])
        };
        return _0xf5dex17
    }
    var _0xf5dex17 = _0xf5dex14();
    try {
        var _0xf5dex19 = JSON['parse'](_0xf5dex17.PrdParam);
        ArrBrand = _0xf5dex19;
        var _0xf5dex1a = [];
        _0xf5dex1a = ArrBrand[0]['split']('^');
        if ($['trim'](_0xf5dex1a[0]) == 'chkBrand') {
            Prd_Type = 'B';
            prd_SecSale = _0xf5dex1a[1];
            strMsg = 'Brand Wise'
        } else {
            if ($['trim'](_0xf5dex1a[0]) == 'chkGroup') {
                Prd_Type = 'G';
                prd_SecSale = _0xf5dex1a[1];
                strMsg = 'Group Wise'
            } else {
                if ($['trim'](_0xf5dex1a[0]) == 'chkCategory') {
                    Prd_Type = 'C';
                    prd_SecSale = _0xf5dex1a[1];
                    strMsg = 'Category Wise'
                }
            }
        }
    } catch (err) { };
    var _0xf5dex1b = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var _0xf5dex1c = parseInt(_0xf5dex10 - 1);
    var _0xf5dex1d = parseInt(_0xf5dex12 - 1);
    var _0xf5dex1e = _0xf5dex1b[_0xf5dex1c];
    var _0xf5dex1f = _0xf5dex1b[_0xf5dex1d];
    if (_0xf5dex10 != _0xf5dex12) {
        ColMonth = _0xf5dex1e + '-' + _0xf5dex1f
    } else {
        ColMonth = _0xf5dex1e + '-' + _0xf5dex1f
    };
    var _0xf5dex20 = '' + strMsg + ' Sale Report From ' + _0xf5dex1e + ' ' + _0xf5dex11 + ' to ' + ' ' + _0xf5dex1f + ' ' + ' ' + _0xf5dex13;
    var _0xf5dex21 = '<span style=\'color:#0077ff\'>Field Force Name :</span><span style=\'color:#c17111\'> ' + _0xf5dexf + '</span>';
    $('#lblStock')['html'](_0xf5dex20);
    $('#lblField')['html'](_0xf5dex21);
    var _0xf5dex22 = (parseInt(_0xf5dex13) - parseInt(_0xf5dex11)) * 12 + parseInt(_0xf5dex12) - parseInt(_0xf5dex10);
    var _0xf5dex23 = parseInt(_0xf5dex10);
    var _0xf5dex24 = parseInt(_0xf5dex11);
    if (_0xf5dex22 >= 0) {
        for (var _0xf5dex25 = 1; _0xf5dex25 <= _0xf5dex22 + 1; _0xf5dex25++) {
            var _0xf5dex1c = parseInt(_0xf5dex23 - 1);
            var _0xf5dex1e = _0xf5dex1b[_0xf5dex1c];
            var _0xf5dex26 = _0xf5dex1e;
            ArrMonth['push'](_0xf5dex26);
            _0xf5dex23 = _0xf5dex23 + 1;
            if (_0xf5dex23 == 13) {
                _0xf5dex23 = 1;
                _0xf5dex24 = _0xf5dex24 + 1
            }
        }
    }
});

function GetHQ_Det() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_All_ParamField',
        data: '{}',
        dataType: 'json',
        success: function (_0xf5dex19) {
            ArrHQDet = _0xf5dex19['d'];
            GetSaleclose()
        },
        error: function (_0xf5dex28) { }
    })
}

function GetSaleclose() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/GetSaleClose_Field',
        data: '{}',
        dataType: 'json',
        success: function (_0xf5dex19) {
            ArrSale = _0xf5dex19['d'];
            GetRecordDet(Prd_Type)
        },
        error: function (_0xf5dex28) { }
    })
}

function GetRecordDet(Prd_Type) {
    var _0xf5dex2b = {};
    var _0xf5dex2c = '';
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Brandwise_Prd_SaleAndClosing',
        data: '{objPrdType:' + JSON['stringify'](Prd_Type) + ',objSS_Code:' + JSON['stringify'](prd_SecSale) + '}',
        dataType: 'json',
        success: function (_0xf5dex19) {
            dataTableSt = _0xf5dex19['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                var _0xf5dex2d = ['Prod_Name', 'Pack', 'Brand_Name', 'Product_ERP_Code'];
                var _0xf5dex2e = getPivotArray_Test(dataTableSt, _0xf5dex2d, 'VST', 'Value');
                var _0xf5dex2f = [];
                _0xf5dex2f = prd_SecSale['split'](',');
                var _0xf5dex30 = '<table id="tblSecsale" class="table" style="min-width:95%;max-width:100%">';
                _0xf5dex30 += '<tr>';
                _0xf5dex30 += '<th rowspan="3" >#</th>';
                if (Prd_Type == 'B') {
                    _0xf5dex30 += '<th rowspan="3" >Brand Name</th>'
                } else {
                    if (Prd_Type == 'G') {
                        _0xf5dex30 += '<th rowspan="3" >Group Name</th>'
                    } else {
                        if (Prd_Type == 'C') {
                            _0xf5dex30 += '<th rowspan="3" >Category Name</th>'
                        }
                    }
                };
                _0xf5dex30 += '<th rowspan="3" >Product Name</th>';
                _0xf5dex30 += '<th rowspan="3" >Product ERP Code</th>';
                _0xf5dex30 += '<th rowspan="3" >Pack</th>';
                var _0xf5dex31 = _0xf5dex2f['length'];
                var _0xf5dex32 = parseInt(_0xf5dex31) * 2;
                for (var _0xf5dex33 = 0; _0xf5dex33 < ArrMonth['length']; _0xf5dex33++) {
                    _0xf5dex30 += '<th colspan="' + _0xf5dex32 + '" >' + ArrMonth[_0xf5dex33] + '</th>'
                };
                _0xf5dex30 += '<th colspan="' + _0xf5dex32 + '" >Total</th>';
                _0xf5dex30 += '</tr>';
                _0xf5dex30 += '<tr>';
                for (var _0xf5dex33 = 0; _0xf5dex33 < ArrMonth['length']; _0xf5dex33++) {
                    for (var _0xf5dex34 = 0; _0xf5dex34 < ArrHQDet['length']; _0xf5dex34++) {
                        var _0xf5dex35 = ArrHQDet[_0xf5dex34]['Sec_Sale_Code'];
                        var _0xf5dex36 = '';
                        for (var _0xf5dex37 = 0; _0xf5dex37 < _0xf5dex2f['length']; _0xf5dex37++) {
                            var _0xf5dex38 = _0xf5dex2f[_0xf5dex37];
                            if ($['trim'](_0xf5dex35) == $['trim'](_0xf5dex38)) {
                                _0xf5dex36 = '2';
                                _0xf5dex30 += '<th   colspan="' + _0xf5dex36 + '" >' + ArrHQDet[_0xf5dex34]['Sec_Sale_Name'] + '</th>';
                                break
                            }
                        }
                    }
                };
                for (var _0xf5dex34 = 0; _0xf5dex34 < ArrHQDet['length']; _0xf5dex34++) {
                    var _0xf5dex35 = ArrHQDet[_0xf5dex34]['Sec_Sale_Code'];
                    var _0xf5dex36 = '';
                    for (var _0xf5dex37 = 0; _0xf5dex37 < _0xf5dex2f['length']; _0xf5dex37++) {
                        var _0xf5dex38 = _0xf5dex2f[_0xf5dex37];
                        if ($['trim'](_0xf5dex35) == $['trim'](_0xf5dex38)) {
                            _0xf5dex36 = '2';
                            _0xf5dex30 += '<th   colspan="' + _0xf5dex36 + '" >' + ArrHQDet[_0xf5dex34]['Sec_Sale_Name'] + '</th>';
                            break
                        }
                    }
                };
                _0xf5dex30 += '</tr>';
                _0xf5dex30 += '<tr>';
                for (var _0xf5dex33 = 0; _0xf5dex33 < ArrMonth['length']; _0xf5dex33++) {
                    for (var _0xf5dex34 = 0; _0xf5dex34 < ArrHQDet['length']; _0xf5dex34++) {
                        var _0xf5dex35 = ArrHQDet[_0xf5dex34]['Sec_Sale_Code'];
                        var _0xf5dex39 = false;
                        for (var _0xf5dex18 = 0; _0xf5dex18 < _0xf5dex2f['length'] && !_0xf5dex39; _0xf5dex18++) {
                            if (_0xf5dex2f[_0xf5dex18] === _0xf5dex35) {
                                _0xf5dex39 = true;
                                break
                            }
                        };
                        if (_0xf5dex39 == true) {
                            _0xf5dex30 += '<th >Qty </th>';
                            _0xf5dex30 += '<th >Value</th>'
                        }
                    }
                };
                for (var _0xf5dex34 = 0; _0xf5dex34 < ArrHQDet['length']; _0xf5dex34++) {
                    var _0xf5dex35 = ArrHQDet[_0xf5dex34]['Sec_Sale_Code'];
                    var _0xf5dex39 = false;
                    for (var _0xf5dex18 = 0; _0xf5dex18 < _0xf5dex2f['length'] && !_0xf5dex39; _0xf5dex18++) {
                        if (_0xf5dex2f[_0xf5dex18] === _0xf5dex35) {
                            _0xf5dex39 = true;
                            break
                        }
                    };
                    if (_0xf5dex39 == true) {
                        _0xf5dex30 += '<th >Qty </th>';
                        _0xf5dex30 += '<th >Value</th>'
                    }
                };
                _0xf5dex30 += '</tr>';
                for (var _0xf5dex18 = 3; _0xf5dex18 < _0xf5dex2e['length']; _0xf5dex18++) {
                    _0xf5dex30 += '<tr>';
                    var _0xf5dex3a = 0;
                    for (var _0xf5dex25 = 0; _0xf5dex25 < _0xf5dex2e[_0xf5dex18]['length']; _0xf5dex25++) {
                        var _0xf5dex3b = _0xf5dex2e[_0xf5dex18][_0xf5dex25];
                        var _0xf5dex3c = '';
                        var _0xf5dex3d = _0xf5dex2e[_0xf5dex18]['length'] - parseInt(_0xf5dex32);
                        if (_0xf5dex3d <= _0xf5dex25) {
                            _0xf5dex3c = 'color:#DC143C;font-size:12px;font-weight:bold;background-color:#F1F5F8;'
                        } else {
                            _0xf5dex3c = 'color:#636d73;'
                        };
                        if (_0xf5dex25 == 1) {
                            var _0xf5dex3e = 'Subtotal';
                            if (_0xf5dex3b['indexOf'](_0xf5dex3e) > -1) {
                                _0xf5dex30 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#F1F5F8;>Total</td>';
                                _0xf5dex3a = 1
                            } else {
                                _0xf5dex30 += '<td style=' + _0xf5dex3c + '>' + _0xf5dex2e[_0xf5dex18][_0xf5dex25] + '</td>'
                            }
                        } else {
                            if (_0xf5dex25 == 2) {
                                if (_0xf5dex3b['indexOf'](_0xf5dex3e) > -1) {
                                    _0xf5dex30 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#F1F5F8;></td>'
                                } else {
                                    _0xf5dex30 += '<td style=' + _0xf5dex3c + '>' + _0xf5dex2e[_0xf5dex18][_0xf5dex25] + '</td>'
                                }
                            } else {
                                if (_0xf5dex3a == 1) {
                                    _0xf5dex30 += '<td style=color:#000;font-size:12px;font-weight:bold;background-color:#F1F5F8;>' + _0xf5dex2e[_0xf5dex18][_0xf5dex25] + '</td>'
                                } else {
                                    _0xf5dex30 += '<td style=' + _0xf5dex3c + '>' + _0xf5dex2e[_0xf5dex18][_0xf5dex25] + '</td>'
                                }
                            }
                        }
                    };
                    _0xf5dex30 += '</tr>'
                };
                _0xf5dex30 += '</table>';
                $('#divpnl')['append'](_0xf5dex30);
                TotalBg();
                TotalDeActivate();
                mergeCells(1)
            };
            $('.modal')['hide']()
        },
        error: function (_0xf5dex28) {
            $('.modal')['hide']()
        }
    })
}

function TotalBg() {
    var _0xf5dex40 = $('#tblSecsale tr:last td')['length'] - 4;
    $('#tblSecsale tr:last td')['each'](function (_0xf5dex18) {
        var _0xf5dex41 = $(this)['html']();
        $(this)['attr']('style', 'color:#c17111;font-size:12px;font-weight:bold;background-color:#F1F5F8;');
        if (_0xf5dex18 >= _0xf5dex40) {
            Active_Val['push'](_0xf5dex41)
        }
    })
}

function GetParameterValues(_0xf5dex43) {
    var _0xf5dex44 = window['location']['href']['slice'](window['location']['href']['indexOf']('?') + 1)['split']('&');
    for (var _0xf5dex18 = 0; _0xf5dex18 < _0xf5dex44['length']; _0xf5dex18++) {
        var _0xf5dex45 = _0xf5dex44[_0xf5dex18]['split']('=');
        if (_0xf5dex45[0] == _0xf5dex43) {
            return decodeURIComponent(_0xf5dex45[1])
        }
    }
}

function getPivotArray_Test(_0xf5dex47, _0xf5dex48, _0xf5dex49, _0xf5dex4a) {
    var _0xf5dex2b = {},
        _0xf5dex4b = [];
    var _0xf5dex4c = [];
    var _0xf5dex4d = [];
    var _0xf5dex4e = [];
    var _0xf5dex4f = [];
    var ArrBrand = [];
    var _0xf5dex50 = [];
    var _0xf5dex51 = [];
    var _0xf5dex52 = [];
    var _0xf5dex53 = [];
    for (var _0xf5dex18 = 0; _0xf5dex18 < _0xf5dex47['length']; _0xf5dex18++) {
        if (!_0xf5dex2b[_0xf5dex47[_0xf5dex18][_0xf5dex48[0]]]) {
            _0xf5dex2b[_0xf5dex47[_0xf5dex18][_0xf5dex48[0]]] = {};
            _0xf5dex4d['push'](_0xf5dex47[_0xf5dex18][_0xf5dex48[1]]);
            _0xf5dex4e['push'](_0xf5dex47[_0xf5dex18][_0xf5dex48[0]]);
            ArrBrand['push'](_0xf5dex47[_0xf5dex18][_0xf5dex48[2]]);
            _0xf5dex4f['push'](_0xf5dex47[_0xf5dex18][_0xf5dex48[3]])
        };
        _0xf5dex2b[_0xf5dex47[_0xf5dex18][_0xf5dex48[0]]][_0xf5dex47[_0xf5dex18][_0xf5dex49]] = _0xf5dex47[_0xf5dex18][_0xf5dex4a];
        if (_0xf5dex4c['indexOf'](_0xf5dex47[_0xf5dex18][_0xf5dex49]) == -1) {
            if (_0xf5dex47[_0xf5dex18][_0xf5dex49] != null) {
                _0xf5dex4c['push'](_0xf5dex47[_0xf5dex18][_0xf5dex49])
            }
        }
    };
    var _0xf5dex54 = [];
    _0xf5dex54['push']('Sl_No');
    _0xf5dex54['push']('Brand');
    _0xf5dex54['push']('Item');
    _0xf5dex54['push']('ERP');
    _0xf5dex54['push']('Pack');
    _0xf5dex54['push']['apply'](_0xf5dex54, _0xf5dex4c);
    _0xf5dex4b['push'](_0xf5dex54);
    var _0xf5dex3a = 0;
    var _0xf5dex55 = 0;
    for (var _0xf5dex56 in _0xf5dex2b) {
        if (_0xf5dex3a < 2) {
            _0xf5dex55 = 0
        } else {
            _0xf5dex55 += 1
        };
        _0xf5dex54 = [];
        _0xf5dex54['push'](_0xf5dex55);
        _0xf5dex54['push'](ArrBrand[_0xf5dex3a]);
        _0xf5dex54['push'](_0xf5dex56);
        _0xf5dex54['push'](_0xf5dex4f[_0xf5dex3a]);
        _0xf5dex54['push'](_0xf5dex4d[_0xf5dex3a]);
        for (var _0xf5dex18 = 0; _0xf5dex18 < _0xf5dex4c['length']; _0xf5dex18++) {
            _0xf5dex54['push'](_0xf5dex2b[_0xf5dex56][_0xf5dex4c[_0xf5dex18]] || ' ')
        };
        _0xf5dex4b['push'](_0xf5dex54);
        _0xf5dex3a += 1
    };
    return _0xf5dex4b
}

function mergeCells(_0xf5dex58) {
    try {
        var _0xf5dex59 = document['getElementsByTagName']('table')[0];
        var _0xf5dex5a = _0xf5dex59['rows'][1];
        var _0xf5dex5b = _0xf5dex5a['nextSibling'];
        while (true) {
            if (_0xf5dex5b['nodeType'] == 3) {
                break
            };
            if (_0xf5dex5a['cells'][_0xf5dex58]['innerHTML'] == _0xf5dex5b['cells'][_0xf5dex58]['innerHTML']) {
                _0xf5dex5a['cells'][_0xf5dex58]['rowSpan'] = 1 + parseInt(_0xf5dex5a['cells'][_0xf5dex58]['rowSpan']);
                _0xf5dex5b['cells'][_0xf5dex58]['style']['display'] = 'none'
            } else {
                _0xf5dex5a = _0xf5dex5b
            };
            _0xf5dex5b = _0xf5dex5b['nextSibling']
        }
    } catch (Exception) {
        $('.modal')['hide']()
    }
}

function TotalDeActivate() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: '../webservice/Stockist_WebService.asmx/Get_Prod_Stockist_DeActive',
        data: '{ModeType:' + JSON['stringify'](DDl_Opt) + '}',
        dataType: 'json',
        success: function (_0xf5dex19) {
            dataTableSt = _0xf5dex19['d']['dtStock'];
            if (dataTableSt['length'] > 0) {
                storeUserDataInSession(dataTableSt);
                var _0xf5dex25 = dataTableSt['length'] - 1;
                var _0xf5dex5d = dataTableSt[_0xf5dex25];
                var _0xf5dex30 = '<table id=\'tblStkDe\' class=\'table table-bordered table-striped\' style=\'min-width:50%;max-width:80%\'>';
                _0xf5dex30 += '<tr>';
                _0xf5dex30 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0xf5dex30 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Sale</th>';
                _0xf5dex30 += '<th colspan=2 style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Closing</th>';
                _0xf5dex30 += '</tr>';
                _0xf5dex30 += '<tr>';
                _0xf5dex30 += '<th style=background-color:#9affb0;Color:black;font-size:11px;></th>';
                _0xf5dex30 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0xf5dex30 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0xf5dex30 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>Active</th>';
                _0xf5dex30 += '<th style=background-color:#9affb0;Color:black;font-size:11px;font-weight:bold>DeActive</th>';
                _0xf5dex30 += '</tr>';
                _0xf5dex30 += '<tr>';
                _0xf5dex30 += '<td style=background-color:#d4d4d4;>';
                _0xf5dex30 += '<span class=\'Service\' style=\'display: inline;float:right\'> Deactivate Sales Value --> (Stockist + Product) (0) :</span>';
                _0xf5dex30 += '</td>';
                var _0xf5dex5e = 0;
                var _0xf5dex3a = 0;
                var _0xf5dex5f = 0;
                $['each'](_0xf5dex5d, function (_0xf5dex56, _0xf5dex60) {
                    var _0xf5dex61 = _0xf5dex56;
                    if (_0xf5dex60 == null || _0xf5dex60 == '0') {
                        _0xf5dex60 = ''
                    };
                    if (_0xf5dex61['includes']('_ABT') || _0xf5dex61['includes']('_ACT')) {
                        var _0xf5dex62 = parseFloat(_0xf5dex60)['toFixed'](2);
                        DeAct_Val = _0xf5dex62;
                        if (_0xf5dex3a == 0) {
                            DeAct_Val = _0xf5dex62;
                            _0xf5dex5e = parseFloat(Active_Val[1]) + parseFloat(DeAct_Val);
                            _0xf5dex30 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[1]) + '</td>';
                            _0xf5dex30 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        } else {
                            DeAct_Val = _0xf5dex62;
                            _0xf5dex5f = parseFloat(Active_Val[3]) + parseFloat(DeAct_Val);
                            _0xf5dex30 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + parseFloat(Active_Val[3]) + '</td>';
                            _0xf5dex30 += '<td class="lblDEStVal" style=color:#890eaf;font-size:13px;font-weight:bold;font-family:Cambria;background-color:#d4d4d4;>' + DeAct_Val + '</td>'
                        };
                        _0xf5dex3a += 1
                    }
                });
                _0xf5dex30 += '</tr>';
                _0xf5dex30 += '<tr>';
                _0xf5dex30 += '<td style=background-color:#d4d4d4;>';
                _0xf5dex30 += '<span style=color:#0000CD;font-size:14px;font-weight:bold;font-family:Calibri;float:right;>Net Total :</span>';
                _0xf5dex30 += '<td colspan=2  style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0xf5dex5e + '</span></td>';
                _0xf5dex30 += '<td colspan=2  style=background-color:#d4d4d4;><span class="NetCSS" style=color:#205a09;font-size:13px;font-weight:bold;font-family:Cambria;>' + _0xf5dex5f + '</span></td>';
                _0xf5dex30 += '</td>';
                _0xf5dex30 += '</tr>';
                _0xf5dex30 += '</table>';
                $('#div_DeAct')['append'](_0xf5dex30)
            };
            $('.modal')['hide']()
        },
        error: function (_0xf5dex28) {
            $('.modal')['hide']()
        }
    })
}

function storeUserDataInSession(_0xf5dex64) {
    sessionStorage['clear']();
    var _0xf5dex65 = JSON['stringify'](_0xf5dex64);
    window['sessionStorage']['setItem']('userObject', _0xf5dex65)
}