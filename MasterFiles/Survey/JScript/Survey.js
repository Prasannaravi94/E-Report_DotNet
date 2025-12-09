
var _0x6da7 = ["4", "val", "#txtNoOfOption", '<div id="OptDiv" style="width:400px;height:45px;float:left;margin-left:3%;margin-top:1%;"><div style="width:10%;height:43px;float:left;margin-top:4%"><input type="radio" id="RbtnOption_', '" name="radio_name" class="radiogroup" value="', '" onchange=check(); /></div><div style="width:30%;height:43px;float:left"><textarea name="textarea" id="txtOption_', '" style="width:300px;height:43px;"></textarea></div></div>', "append", "#OptionDiv", "", "html",
"keyup", "textarea", "find", "siblings", "parent", "&", "includes", "&amp;", "replace", "***", "each", "#tbl_outside table.tbl_Inside input[type=radio]", "#ddlQuestionType", "#txtQuestionText", "#txtAns", "QuestionTypeId", "QuestionText", "InputOption", "CorrectAnswer", "POST", "../webservice/Quiz_QuestionWS.asmx/AddQuestionData", "{QueData:", "stringify", "}", "application/json; charset=utf-8", "json", "Question has been added successfully", "Error", "ajax", "preventDefault", "click", "#btnAddQuestion",
"ready", "id", "attr", "checked", "removeAttr", "#", "Please Enter Answer Option", "#tbl_outside table.tbl_Inside input:radio:checked", "Please Select Question Type", "Please Enter Question Text", "Please Enter No Of Option", "Please Enter Answer Choices and Select One Option"];
$(document)[_0x6da7[43]](function () {
    var _0xc7cdx1 = $(_0x6da7[2])[_0x6da7[1]](_0x6da7[0]);
    i = 0;
    for (; i < 4; i++) {
        $(_0x6da7[8])[_0x6da7[7]](_0x6da7[3] + i + _0x6da7[4] + i + _0x6da7[5] + i + _0x6da7[6]);
    }
    $(_0x6da7[2])[_0x6da7[11]](function () {
        $(_0x6da7[8])[_0x6da7[10]](_0x6da7[9]);
        var cell_amount = $(_0x6da7[2])[_0x6da7[1]]();
        i = 0;
        for (; i < cell_amount; i++) {
            $(_0x6da7[8])[_0x6da7[7]](_0x6da7[3] + i + _0x6da7[4] + i + _0x6da7[5] + i + _0x6da7[6]);
        }
    });
    $(_0x6da7[42])[_0x6da7[41]](function (canCreateDiscussions) {
        if (Validation() === true) {
            var meanText = _0x6da7[9];
            $(_0x6da7[22])[_0x6da7[21]](function (mmCoreSplitViewBlock) {
                var SUVtext = $(this)[_0x6da7[15]]()[_0x6da7[14]](mmCoreSplitViewBlock)[_0x6da7[13]](_0x6da7[12])[_0x6da7[1]]();
                if (SUVtext[_0x6da7[17]](_0x6da7[16])) {
                    SUVtext = SUVtext[_0x6da7[19]](_0x6da7[16], _0x6da7[18]);
                }
                meanText = meanText + (SUVtext + _0x6da7[20]);
            });
            var lta = meanText;
            var bCell = $(_0x6da7[23])[_0x6da7[1]]();
            var lampState = $(_0x6da7[24])[_0x6da7[1]]();
            var responseTasks = $(_0x6da7[25])[_0x6da7[1]]();
            var data = {};
            data[_0x6da7[26]] = bCell;
            data[_0x6da7[27]] = lampState;
            data[_0x6da7[28]] = lta;
            data[_0x6da7[29]] = responseTasks;
            $[_0x6da7[39]]({
                type: _0x6da7[30],
                url: _0x6da7[31],
                data: _0x6da7[32] + JSON[_0x6da7[33]](data) + _0x6da7[34],
                contentType: _0x6da7[35],
                dataType: _0x6da7[36],
                success: function iterator(indicesStatusTimestamp) {
                    var txt = _0x6da7[37];
                    createCustomAlert(txt);
                    Clear();
                },
                error: function error(deleted_model) {
                    var txt = _0x6da7[38];
                    createCustomAlert(txt);
                }
            });
        }
        canCreateDiscussions[_0x6da7[40]]();
    });
});
function check() {
    $(_0x6da7[50])[_0x6da7[21]](function () {
        var conid = $(this)[_0x6da7[45]](_0x6da7[44]);
        var artistTrack = $(this)[_0x6da7[15]]()[_0x6da7[14]]()[_0x6da7[13]](_0x6da7[12])[_0x6da7[1]]();
        if (artistTrack == _0x6da7[9]) {
            $(_0x6da7[48] + conid)[_0x6da7[47]](_0x6da7[46]);
            $(_0x6da7[25])[_0x6da7[1]](_0x6da7[9]);
            createCustomAlert(_0x6da7[49]);
            return false;
        } else {
            $(_0x6da7[25])[_0x6da7[1]](artistTrack);
            return true;
        }
    });
}
function Validation() {
    if ($(_0x6da7[23])[_0x6da7[1]]() == 0) {
        createCustomAlert(_0x6da7[51]);
        return false;
    }
    if ($(_0x6da7[24])[_0x6da7[1]]() == _0x6da7[9]) {
        createCustomAlert(_0x6da7[52]);
        return false;
    }
    if ($(_0x6da7[2])[_0x6da7[1]]() == _0x6da7[9]) {
        createCustomAlert(_0x6da7[53]);
        return false;
    }
    if ($(_0x6da7[25])[_0x6da7[1]]() == _0x6da7[9]) {
        createCustomAlert(_0x6da7[54]);
        return false;
    } else {
        return true;
    }
}
function Clear() {
    $(_0x6da7[24])[_0x6da7[1]](_0x6da7[9]);
    var _0xc7cdx1 = $(_0x6da7[2])[_0x6da7[1]](_0x6da7[0]);
    $(_0x6da7[8])[_0x6da7[10]](_0x6da7[9]);
    i = 0;
    for (; i < 4; i++) {
        $(_0x6da7[8])[_0x6da7[7]](_0x6da7[3] + i + _0x6da7[4] + i + _0x6da7[5] + i + _0x6da7[6]);
    }
    $(_0x6da7[25])[_0x6da7[1]](_0x6da7[9]);
}
;