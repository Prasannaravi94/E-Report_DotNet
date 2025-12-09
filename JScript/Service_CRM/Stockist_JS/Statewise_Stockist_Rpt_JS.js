$(document)['ready'](function () {
    var _0x32f6x1 = new Date(),
        _0x32f6x2 = _0x32f6x1['getMonth'](),
        _0x32f6x3 = _0x32f6x1['getFullYear']();
    $('#ddlMonth option:eq(' + _0x32f6x2 + ')')['prop']('selected', true);
    BindYear_DDL();
    $('#btnGo')['click'](function () {
        var _0x32f6x4 = $('#ddlMonth')['val']();
        var _0x32f6x5 = $('#ddlYear')['val']();
        var _0x32f6x6 = _0x32f6x4 + '^' + _0x32f6x5;
        StockistDetail(_0x32f6x6)
    })
});

function BindYear_DDL() {
    $['ajax']({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: 'webservice/Stockist_WebService.asmx/FillYear',
        data: '{}',
        dataType: 'json',
        success: function (_0x32f6x8) {
            $('#ddlYear')['empty']();
            var _0x32f6x9 = _0x32f6x8['d'][0]['Year'];
            var _0x32f6xa = new Date()['getFullYear']();
            for (var _0x32f6xb = parseInt(_0x32f6x9) ; _0x32f6xb <= _0x32f6xa; _0x32f6xb++) {
                $('#ddlYear')['append']($('<option></option>')['val'](_0x32f6xb)['html'](_0x32f6xb))
            };
            $('#ddlYear option:contains(\'' + _0x32f6xa + '\')')['attr']('selected', 'selected')
        },
        error: function _0x32f6xc(_0x32f6xd) {
            alert('Error')
        }
    })
}

function StockistDetail(_0x32f6x6) {
    var _0x32f6xf = _0x32f6x6['split']('^');
    var _0x32f6x10 = '<tr>';
    _0x32f6x10 += '<th>#</th>';
    _0x32f6x10 += '<th style="display:none;">State Code</th>';
    _0x32f6x10 += '<th>State Name</th>';
    _0x32f6x10 += '<th>No of Stockist Available</th>';
    _0x32f6x10 += '<th>Stockist Done</th>';
    _0x32f6x10 += '<th>No of Remain Stock</th>';
    _0x32f6x10 += '</tr>';
    $['ajax']({
        type: 'POST',
        url: 'webservice/Stockist_WebService.asmx/GetStockist_List',
        data: '{objStock:' + JSON['stringify'](_0x32f6x6) + '}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (_0x32f6x8) {
            if (_0x32f6x8['d']['length'] > 0) {
                var _0x32f6x11 = 0;
                var _0x32f6x12 = 0;
                var _0x32f6x13 = 0;
                var _0x32f6x14 = 0;
                for (var _0x32f6xb = 0; _0x32f6xb < _0x32f6x8['d']['length']; _0x32f6xb++) {
                    _0x32f6x10 += '<tr>';
                    _0x32f6x10 += '<td>' + _0x32f6x8['d'][_0x32f6xb]['SNo'] + '</td>';
                    _0x32f6x10 += '<td style="display:none;">' + _0x32f6x8['d'][_0x32f6xb]['StateCode'] + '</td>';
                    _0x32f6x10 += '<td>' + _0x32f6x8['d'][_0x32f6xb]['StateName'] + '</td>';
                    _0x32f6x10 += '<td>' + _0x32f6x8['d'][_0x32f6xb]['AvaStockist'] + '</td>';
                    _0x32f6x10 += '<td>' + _0x32f6x8['d'][_0x32f6xb]['StockistDone'] + '</td>';
                    _0x32f6x10 += '<td><a id="lnkStock_' + _0x32f6xb + '" href="#" title="RemainStock" class="LnkStockist" onclick="showModalPopUp(' + _0x32f6xf[0] + ',' + _0x32f6xf[1] + ',' + _0x32f6x8['d'][_0x32f6xb]['StateCode'] + ')">' + _0x32f6x8['d'][_0x32f6xb]['RemainStock'] + '</a></td>';
                    _0x32f6x11 = _0x32f6x11 + parseInt(_0x32f6x8['d'][_0x32f6xb].RemainStock);
                    _0x32f6x10 += '</tr>';
                    _0x32f6x12 = _0x32f6x12 + parseInt(_0x32f6x8['d'][_0x32f6xb].AvaStockist);
                    _0x32f6x13 = _0x32f6x13 + parseInt(_0x32f6x8['d'][_0x32f6xb].StockistDone);
                    _0x32f6x14 = _0x32f6x14 + parseInt(_0x32f6x8['d'][_0x32f6xb].RemainStock)
                };
                var _0x32f6x15 = '10001';
                _0x32f6x10 += '<tr>';
                _0x32f6x10 += '<td colSpan="2">Total</td>';
                _0x32f6x10 += '<td>' + _0x32f6x12 + '</td>';
                _0x32f6x10 += '<td>' + _0x32f6x13 + '</td>';
                _0x32f6x10 += '<td><a id="Total" href="#" title="Total" onclick="showModalPopUp(' + _0x32f6xf[0] + ',' + _0x32f6xf[1] + ',' + _0x32f6x15 + ')">' + _0x32f6x11 + '</a></td>';
                _0x32f6x10 += '</tr>';
                var _0x32f6x16 = _0x32f6x12 + _0x32f6x13 + _0x32f6x14;
                $('#tblStockist')['html'](_0x32f6x10)
            }
        },
        error: function (_0x32f6x17) { }
    })
}