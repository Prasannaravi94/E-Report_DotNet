__w = window; lckMsg = ''; hndMonth = '', hndYear = ''; var count = 1;
DDL_SEL_ = ".ddl-Drop.active";
DDL_SEL_Srch_ = DDL_SEL_ + " > .ddl-search  input";
DDL_SEL_Pad_ = DDL_SEL_ + " > .ddl-results";
DDL_SEL_List_ = DDL_SEL_Pad_ + "> .active-result";
//var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
tabs = { 'D': 'Msl' };


//d_Ses = [{ id: "M", name: "Morning" }, { id: "E", name: "Evening" }];
_lcStore = __w.localStorage;
SFDet = {};
Setup = {};
Entry = {};
_st = {};
_st_eD = {};
_st_eP = {};
__Wndw = {
    D: { fields: {}, grd_Cols: {}, prv_Cols: {}, nw: 0 },
    //C: { fields: {}, grd_Cols: {}, prv_Cols: {}, nw: 1 },
    //H: { fields: {}, grd_Cols: {}, prv_Cols: {}, nw: 1 },
    //S: { fields: {}, grd_Cols: {}, prv_Cols: {}, nw: 0 },
    //U: { fields: {}, grd_Cols: {}, prv_Cols: {}, nw: 1 },
    //R: { RwCtrl: { rem: { cap: "Remarks", ty: 'ta', id: "Rmks", h: 'fill', w: 'fill', mxl: 0 } }, prv_Cols: {} },
    P: {}
};
__Click = true;
__Alert = false;
DCRDatas = {};
__DoubleClick = false;


(function ($) {
    FormValidate = {
        addMax: function () {
            var
                maxlAttr = $(this).attr("maxlength"),
                maxAttR = $(this).attr("max"),
                x = 0,
                max = "";
            if (typeof maxlAttr !== typeof undefined && maxlAttr !== false) {
                if (typeof maxlAttr !== typeof undefined && maxlAttr !== false) {
                    while (x < maxlAttr) {
                        max += "9";
                        x++;
                    }
                    maxAttR = max;
                }
                var keys = [8, 9, 13, 46, 37, 39, 38, 40];
                $(this)
                    .attr("max", maxAttR)
                    .keydown(function (event) {
                        if ($(this).val().length == maxlAttr && $.inArray(event.which, keys) == -1 && FormValidate.isTextSelected() == false) return false;
                    });
            }
        },
        isTextSelected: function () {
            text = "";
            if (window.getSelection) {
                text = window.getSelection().toString();
            } else if (document.selection && document.selection.type != "Control") {
                text = document.selection.createRange().text;
            }
            return (text.length > 0);
        }
    };

    $.maxlengthNumber = function () {
        $("input[type=number]").each(function () { FormValidate.addMax.call(this); });
    }

})($);



(function ($) {
    alertBox = {
        BuildAlert: function () {
            $("<div class='alrt-box'><div class='alrt-cont'><div class='alrt-HD'><b>Daily Call Report</b><a class='closBt' href='javascript:okFunc();'>x</a></div><div class='MsgCont'></div><div class='altr-ftr aBtn'><a href='javascript:okFunc();' class='button'>OK</a></div><div class='altr-ftr cBtn'><a href='javascript:yFunc();' class='button button-green'>Yes</a><a href='javascript:nFunc();' class='button button-red'>No</a></div></div>").appendTo("body");
        },
        Show: function (msg, Fobj) {
            $('.MsgCont').html(msg);
            $('.alrt-box').css('display', 'block');
            $('.aBtn').css('display', 'block');
            $('.aBtn').find('.button').focus();
            $('.cBtn').css('display', 'none');
            __Alert = true;
            okFunc = function () {
                $(Fobj).focus();
                alertBox.hide();
            }
        },
        hide: function () {
            $('.MsgCont').html("");
            $('.alrt-box').css('display', 'none');
            __Alert = false;
        },
        confirm: function (msg, tFunc, fFunc) {
            alertBox.Show(msg);
            $('.cBtn').css('display', 'block');
            $('.cBtn').find('.button-green').focus();
            $('.aBtn').css('display', 'none');
            yFunc = function () {
                alertBox.hide();
                $(tFunc);
            }
            nFunc = function () {
                alertBox.hide();
                $(fFunc);
            }
        }
    }

})($);

$(document).ready(function () {
    initialize();
    $.maxlengthNumber();
    alertBox.BuildAlert();
    $(window).resize(function () { resize(true); });
    $(window).load(function () {
        resize(true);
        create_Dropdown();

        BuildMenu(0);
        $("#planer").css('display', 'none');
        $("#idWD").css('display', 'none');
        $("#DtInf").css('display', 'none');

        $(".mnu-bt").css('display', 'none');
        $(".ResetGo").css('display', 'none');
        showAlert('Select the Month,Year and Press Go.');
        
        hideMenu();

        //ClearAllData();

        //$(".plnPlholder").css('display', 'none');
        //if ($.isArray(Setup) == true) Setup = {};
        //if ($.isArray(DCRDatas) == true) DCRDatas = {};
        //if (Setup.TPBaseDCRDr == 1) {
        //    DCRDatas.SFCode = $('#hSF_Code').val();
        //    DCRDatas.EDate = $('#hDCRDt').val();
        //    DCRDatas.Rem = $("#txa_RRmks").val();

        //}
        //if (DCRDatas.SFCode != undefined && SFDet.SFCode == DCRDatas.SFCode && $('#hDCRDt').val() == DCRDatas.EDate) {
        //    $('#hSF_Code').val(DCRDatas.SFCode);
        //    $('#hDCRDt').val(DCRDatas.EDate);
        //    DCR.SFCode = DCRDatas.SFCode;
        //    DCR.EDate = DCRDatas.EDate;
        //    if (Setup.TPBaseDCRDr != 1) {
        //        $('#ddl_WorkType').val(DCRDatas.Wtyp);
        //        $('#ddl_HQ').val(DCRDatas.selHQ);
        //    }
        //    if (DCRDatas.Rem != undefined) $("#txa_RRmks").val(DCRDatas.Rem);

        //    ChangeWT();
        //    ChangeHQ();
        //}
        //else {
        //    DCRDatas.SFCode = $('#hSF_Code').val();
        //    DCRDatas.EDate = $('#hDCRDt').val();
        //    DCRDatas.Rem = $("#txa_RRmks").val();
        //}
        //DCRDatas.STime = $("#hSTime").val();
        //DCRDatas.CurrDt = $("#hCurrDt").val();

        //if (Setup.TPBaseDCRDr != 1)
        //    setData('DCRMain', DCRDatas);
        //DCR.SFCode = DCRDatas.SFCode;
        //DCR.EDate = DCRDatas.EDate;

        $("body").removeClass('loading');
        //$('#ddl_WorkType').removeAttr('disabled');
        //eDt = new Date(DCRDatas.EDate);
        //cDt = new Date(DCRDatas.CurrDt);
        //if (eDt > cDt) {
        //    showAlert("Future Date DCR Not Allowed....");
        //    $('#ddl_WorkType').attr('disabled', 'disabled');
        //    return false;
        //}
        //SFty = $("#hSFTyp").val();
        //if (SFty == 1) {
        //    d_twns = getData("d_twn_" + DCR.SFCode);
        //    $("#ddl_Ntwn").reload();
        //}
    });

    //$("#tCusRem").on("paste", function (e) {
    //    if (!e) e = event;
    //    var pastedData = e.originalEvent.clipboardData.getData('text');
    //    window.document.execCommand('insertText', false, pastedData.replace(/\'/g, "").replace(/\"/g, ""));
    //    return false;
    //});
    //$("#tCusRem").keydown(function (event) {
    //    keys = [222]; if ($.inArray(event.which, keys) > -1) return false;
    //});

    //$("#txa_RRmks").on("paste", function (e) {
    //    if (!e) e = event;
    //    var pastedData = e.originalEvent.clipboardData.getData('text');
    //    window.document.execCommand('insertText', false, pastedData.replace(/\'/g, "").replace(/\"/g, ""));
    //    return false;
    //});
    //$("#txa_RRmks").keydown(function (event) {
    //    keys = [222]; if ($.inArray(event.which, keys) > -1) return false;
    //});
    //$("#txa_RRmks").on('keyup',
    //    function (e) {
    //        DCRDatas.Rem = $(this).val();
    //        setData('DCRMain', DCRDatas);
    //    });
    $(document, this).on('click', function (e) {
        $drp = $(e.target).closest(DDL_SEL_);
        $sel = $(e.target).closest(".disp-selopt.active");
        if ($drp.length < 1 && $sel.length < 1) {
            $(DDL_SEL_).removeClass('active');
        }
        $drp = $(e.target).closest(".wind-o.active");
        $sel = $(e.target).closest(".disp-selopt.wactive");
        if ($drp.length < 1 && $sel.length < 1 && __Click == true && __Alert == false) {
            ddlWinClose()
        }

        $(DDL_SEL_List_, this).mouseover(function () {
            $(DDL_SEL_List_).removeClass("highlighted");
            $(this).addClass("highlighted");
        });
        $(DDL_SEL_List_, this).click(function () {
            selectItem($(this));
        })
        __Click = true;
    });

    $(document).keydown(function (e) {
        if (!e) e = event; var $key = (e.keyCode ? e.keyCode : e.which);
        ky = String.fromCharCode($key);
        for (k in __Menu) {
            if (ky.toUpperCase() == __Menu[k].key.toUpperCase() && e.ctrlKey == true && e.altKey == true) {
                navTag($("[data-tag=" + ky + "]"))
            };
        }
        $curr = $(DDL_SEL_List_ + ".highlighted");
        if ($curr.length == 0) {
            $cr = $(DDL_SEL_List_);
            $curr = $($cr[0]);
        } else {
            if ($key == 40) $curr = $curr.nextAll('.active-result:first');
            if ($key == 38) $curr = $curr.prevAll('.active-result:first');
        }
        if (!($key == 40 || $key == 39 || $key == 38 || $key == 37)) $(DDL_SEL_Srch_).focus();

        if ($curr != undefined) {
            $(DDL_SEL_List_).removeClass("highlighted");
            $curr.focus().addClass("highlighted");
            scrollUL($curr);
            if ($key == 13) { selectItem($curr); }
        }
    });

});
getCurrTime = function () {
    var currentdate = new Date();
    var datetime = currentdate.getFullYear() + "-"
        + (currentdate.getMonth() + 1) + "-"
        + currentdate.getDate() + "  "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    return datetime;
}
$.fn.ObjIndexOf = function (keys, val) {
    $this = $(this); key = (keys + '.').split('.');
    for (var i = 0; i < $this.length; i++) {
        $x = $this[i];
        for (j = 0; j < key.length - 1; j++) {
            $x = $x[key[j]];
        }
        if ($x == val)
            return i;
    }
    return -1;
}

$.fn.reload = function (clFlag) {
    $dSrc = $(this).data('src');
    $slVal = $(this).attr('data-value');
    $adf = $(this).data('adf');
    hndLst = $(this).find('.ddl-results');
    $dtaTF = $(this).data('tf');
    $dtaVF = $(this).data('vf');
    if (!clFlag) clFlag = 0;
    if (clFlag == 1)
        opts = [];
    else {
        try {
            opts = eval($dSrc);
        } catch (e) {
            opts = JSON.parse(_lcStore.getItem($dSrc)) || [];
        }
    }
    $dd = "";
    if ($adf != "" && $adf != undefined) {
        ao = ($adf + (($adf.indexOf(',') > -1) ? '' : ',')).split(',');
    }
    else ao = [];
    if (opts == undefined || opts.length == undefined) opts = [];
    for ($indx = 0; $indx < opts.length; $indx++) {
        $dd += "<li class='active-result" + (($slVal == opts[$indx][$dtaVF]) ? " result-selected" : "") + "' data-val='" + opts[$indx][$dtaVF] + "'";
        for (o in ao) {
            $dd += " data-" + ao[o] + "='" + opts[$indx][ao[o]] + "'";
        }
        $dd += "><label>"
        if ($(this).hasClass('multi')) {
            $dd += "<input type='checkbox' class='ckBx' autocomplete='off' tabindex='-1'>";
        }
        $dd += opts[$indx][$dtaTF];
        $dd += "</label></li>";
    }
    $dd += "<li class='noRec " + ((opts.length > 0) ? '' : 'active') + "'><i>No Record Found</i><b></b></li>";
    hndLst.html($dd);

    $(".ckBx", this).on('click', function () {
        setjoinWrk(this)
    });
}
setjoinWrk = function (x) {
    td = $(x).closest(DDL_SEL_); tx = ''; vl = '';
    td.find('.ckBx:checked').each(function () {
        tx += $(this).closest('label').text() + ', ';
        if (vl != "") vl += "$$";
        vl += $(this).closest('li').attr("data-val");
    });
    $('.disp-selopt.active').find('span').text(tx);
    $('.disp-selopt.active').closest('.ddl-Box').attr('data-text', tx);
    $('.disp-selopt.active').closest('.ddl-Box').attr('data-value', vl);
}
$.fn.setItem = function (vl, tx) {
    $(this).attr('data-value', vl);
    $(this).attr('data-text', tx);
    $(this).find('span').text((tx != '') ? tx : $(this).attr("data-dft"));
    if ($(this).hasClass('multi')) {
        $(this).find(".ckBx").prop('checked', false);
        //if (vl.indexOf('$$') == -1)
        vl = vl + '$$';
        ovl = vl.split('$$');
        for (io = 0; io < ovl.length - 1; io++)
            $(this).find("li[data-val='" + ovl[io] + "']").find(".ckBx").prop('checked', true);

    } else {
        $(this).find("li").removeClass('result-selected');
        $(this).find("li[data-val='" + vl + "']").addClass('result-selected');
    }
}

$.fn.setItem_Prod = function (vl, tx, pack, rate) {
    $(this).attr('data-value', vl);
    $(this).attr('data-text', tx);
    if (pack !== undefined) $(this).attr('data-pack', pack);
    if (rate !== undefined) $(this).attr('data-rate', rate);

    $(this).find('span').text((tx != '') ? tx : $(this).attr("data-dft"));

    if ($(this).hasClass('multi')) {
        $(this).find(".ckBx").prop('checked', false);
        vl = vl + '$$';
        ovl = vl.split('$$');
        for (io = 0; io < ovl.length - 1; io++) {
            $(this).find("li[data-val='" + ovl[io] + "']")
                .find(".ckBx").prop('checked', true);
        }
    } else {
        $(this).find("li").removeClass('result-selected');
        var selLi = $(this).find("li[data-val='" + vl + "']").addClass('result-selected');

        // If pack/rate not passed, fallback from li
        if (selLi.length > 0) {
            if (pack === undefined && selLi.data("pack") !== undefined) {
                $(this).attr("data-pack", selLi.data("pack"));
            }
            if (rate === undefined && selLi.data("rate") !== undefined) {
                $(this).attr("data-rate", selLi.data("rate"));
            }
        }
    }
};

var DCR = {
    haveMenu: false,
    edtIndex: null,
    edtCus: '',
    SFCode: '',
    EDate: '',
    curMnu: null,
    Menu: {
        create: function (a) {
            $("#mnuTab").html('');

            //sst = $('#ddl_WorkType').find('option:selected').data('etabs');
            sst = 'D,P';
            //$('.wTab').removeClass('active');

            //if (sst == undefined) {
            //    showAlert("Select The Work Type");
            //    return;
            //}
            for (ix in a) {
                if (sst.indexOf(ix) > -1 || a[ix].imp != undefined) {
                    li = $('<li>').attr('data-tag', ix);
                    ic = $('<u>').addClass('fa fa-' + a[ix].ic);
                    ke = $('<u>').addClass('sKey').text('[ Ctrl + Alt + ' + a[ix].key + ' ]');
                    //ke = "";
                    $(li).each(function () {
                        if ($(DCR.curMnu).attr('data-tag') == $(this).attr('data-tag')) {
                            navTag(this);
                        }
                    });
                    $(li).on('click', function () {
                        navTag(this);
                    })
                        .append(ic, ke, a[ix].name);
                    $("#mnuTab").append(li);
                }
            }
            li = $("#mnuTab").find('li');
            navTag($(li[0]));

            this.haveMenu = true;
        }
    },
    WrkWin: {
        create: function (a) {
            for (ix in a) {
                div = $('<div></div>').attr('id', 'idW' + ix).addClass('wTab');

                //if (a[ix].RwCtrl != undefined) {
                //    eTab = ''; eDtD = '';
                //    for (_ic in a[ix].RwCtrl) {
                //        fa = a[ix].RwCtrl[_ic];
                //        if (fa.m == undefined) fa.m = 0;
                //        if (fa.m == 0) {
                //            eDtD += '<div style="padding-bottom:4px;">';
                //            if (fa.cap != undefined) eTab += "<span style='display:block;width:" + ((fa.w == "fill") ? "98.5%" : (fa.w + "px")) + " !important;' class='GrdH'>" + fa.cap + "<span class='ntpad'>Max Length : " + fa.mxl + "</span></span>";
                //            if (fa.ty == 'ta') eDtD += '<textarea id="txa_' + ix + fa.id + '" maxlength="' + fa.mxl + '" style="width:' + ((fa.w == 'fill') ? '98.5%' : (fa.w + 'px')) + ' !important;height:' + ((fa.h == 'fill') ? '100%' : (fa.h + 'px')) + ' !important;" rows=10 />';
                //            eDtD += '</div>';
                //        }
                //    }
                //    eDtD += '</tr>';
                //    eTab += eDtD;
                //    $(div).append(eTab);
                //}
                if (a[ix].fields != undefined) {

                    a[ix].fields.cus.cap = __Menu[ix].name;
                    a[ix].fields.cus.df = "-- " + __Menu[ix].name + " --";
                    a[ix].fields.cus.src = 'd_' + __Menu[ix].eSrc;

                    eTab = '<table id="tbEnt_' + ix + '" class="fg-group"><tr>';
                    if (a[ix].fields != undefined) {
                        eDtD = '<tr>';
                        for (_ic in a[ix].fields) {

                            fa = a[ix].fields[_ic];

                            if (fa.id != 'spec' && fa.id != 'cat' && fa.id != 'sub_area' && fa.id != 'B_Val') {

                                if (fa.m == undefined) fa.m = 0;
                                eTab += "<th>" + ((fa.id == 'cus' && a[ix].nw == 1) ? '<a href="#" class="button button-orange go-bt" style="margin: -3px 3px;height: 15px;padding: 3px 8px;line-height:15px;" onclick="openWin(\'' + ix + '\')">New</a>' : '') + fa.cap + "</th>"; eDtD += '<td>';
                                if (fa.ty.indexOf('ddl') > -1) eDtD += '<div id="ddl_' + ix + fa.id + '" class="' + fa.ty + '" style="width:' + fa.w + 'px;" data-value="' + fa.val + '" data-text="' + fa.df + '" data-inwidth="' + fa.iw + 'px"  data-src="' + fa.src + '" data-adf="' + ((fa.adf != undefined) ? fa.adf : '') + '" data-vf="' + fa.vf + '" data-tf="' + fa.tf + '"></div>';
                                if (fa.ty == 'txt_Tm') eDtD += '<input type="text" id="tx_' + ix + fa.id + '" maxlength="' + fa.mxl + '" class="tmr" style="width:' + fa.w + 'px;"   />';
                                if (fa.ty == 'txt_Dc') eDtD += '<input type="number" id="tx_' + ix + fa.id + '" maxlength="' + fa.mxl + '" min="0" class="' + fa.ty + '" style="width:' + fa.w + 'px;"   />';
                                if (fa.ty.indexOf('button') > -1) eDtD += '<a href="#" class="button go-bt" onclick="DCR.insEntry(\'' + ix + '\')">GO</a>';
                                eDtD += '</td>';
                            }
                        }
                        eDtD += '</tr>';
                    }
                    eTab += eDtD + '</table><br>';
                    $(div).append(eTab);
                }
                if (a[ix].grd_Cols != undefined) {
                    eTab = '<table id="tbDta_' + ix + '" class="fg-group"><tr><th class="rwHd"></th>'; eDtD = '<tr>';
                    for (_ic in a[ix].grd_Cols) {
                        fa = a[ix].fields[_ic];
                        if (fa.id != 'prd') {                      
                            eTab += "<th style='min-width:" + (fa.gw || fa.iw || fa.w) + "px !important;'>" + fa.cap + "</th>";
                        }
                    }
                    eTab += '</table><div id="dWD_' + ix + '"></div>';
                    $(div).append(eTab);
                }
                $("#working-Area").append(div);
            }
        }
    },
    validSel: function (id) {
        $o = $('#' + id);
        if ($o.attr('data-value') == "") {
            alertBox.Show('Select the ' + $o.attr('data-dft').replace(/-/g, ''), $o.find('.disp-selopt'));
            return false;
        }
    },
    ValidTime: function (ox) {
        sTime = $(ox).val();
        if (sTime != '' && sTime != undefined) {
            if (sTime.length != 8) { alertBox.Show('Please enter date in hh:mm:ss format', $(ox)); return false; }
            c1 = sTime.charAt(2); c2 = sTime.charAt(5);
            h = parseInt(sTime.charAt(0) + sTime.charAt(1)); m = parseInt(sTime.charAt(3) + sTime.charAt(4)); s = parseInt(sTime.charAt(6) + sTime.charAt(7));

            var msg = "";
            if (c1 != ':' && c2 != ':') { alertBox.Show('Please enter date in hh:mm:ss format!', $(ox)); return false; }
            msg += (h >= 0 && h <= 23) ? '' : 'Please enter valid hours\n';
            msg += (m <= 59 && m >= 0) ? '' : 'Please enter valid minutes\n';
            msg += (s <= 59 && s >= 0) ? '' : 'Please enter valid seconds\n';
            if (msg != "") { alertBox.Show(msg, $(ox)); return false; }
        }
        return true;
    },
    validEntry: function (typ) {
        $o = $('#ddl_' + typ + 'cus');
        //MxCnt = (typ == "D") ? Setup.NoOfDrs : (typ == "C") ? Setup.NoOfChm : (typ == "S") ? Setup.NoOfStk : (typ == "U") ? Setup.NoOfUdr : Setup.NoOfHos;
        setData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ, data);

        //if (data.length >= MxCnt && this.edtIndex == null) {
        //    alertBox.Show("You are reached limit of " + $o.attr('data-dft').replace(/-/g, ''));
        //    return false;
        //}
        if (this.validSel('ddl_' + typ + 'cus') == false) return false;
        if ($(data).ObjIndexOf('cus.val', $o.attr('data-value')) > -1 && this.edtCus != $o.attr('data-value')) {
            alertBox.Show("This " + $o.attr('data-dft').replace(/-/g, '') + " Already Selected...", $o.find('.disp-selopt'));
            return false
        }

        //if (Setup.SesNeed == 1 && Setup.SesMand == 1) if (this.validSel('ddl_' + typ + 'ses') == false) return false;
        //$o = $('#tx_' + typ + 'tm');
        //if (Setup.TmNeed == 1) {
        //    if ($o.val() == "" && Setup.TmMand == 1) {
        //        alertBox.Show('Enter the Time', $o);
        //        return false;
        //    }
        //    if ($o.val() != "") { if (this.ValidTime($o) == false) return false; }
        //}

        //if ($("#tx_" + typ + "pob").val() == '' && ((Setup.CPOBM == 1 && typ == 'C') || (Setup.DPOBM == 1 && typ == 'D'))) {
        //    alertBox.Show("Enter the POB", $("#tx_" + typ + "pob"));
        //    return false
        //}

        pvl = $("#ddl_Dprd").attr("data-value");
        //if (typ == "D" && Setup.ProdMand > 0 && pvl == '') {
        if (typ == "D" && pvl == '') {
            ddlWinOpen($("#ddl_Dprd"), $("#ddl_Dprd").find('.disp-selopt'), $("#wProd"));
            return false;
        }

        //srem = $('#ddl_' + typ + 'rem').attr('data-value') || "";

        //_st = JSON.parse(Setup.SDCRE);
        //sfedbk = $('#ddl_fedBk').attr('data-text') || "";
        //if (typ == "D" && _st.rem.D > 0 && Setup.DrRem > 0 && (srem == '' && sfedbk == '')) {
        //    ddlWinOpen($("#ddl_Drem"), $("#ddl_Drem").find('.disp-selopt'), $("#wRem"));
        //    return false;
        //}
    },
    insEntry: function (typ) {

        if (__DoubleClick) {
            return; // Exit if another click is being processed
        }

        __DoubleClick = true;
        hideAlert();

        $('#btn-ins').attr('disabled', 'disabled');

        __Click = false;
        cust = { sf: {}, twn: {} };
        if (this.validEntry(typ) == false) {
            $('#btn-ins').removeAttr('disabled');
            __DoubleClick = false;
            return false;
        }

        updState(1);
        for (wky in _C) {
            //if (wky == "rem") {
            //    cust.rem = $('#ddl_' + typ + 'rem').attr('data-value') || "";
            //    cust.fedbk = {};
            //    cust.fedbk.val = $('#ddl_fedBk').attr('data-value') || "";
            //    cust.fedbk.txt = $('#ddl_fedBk').attr('data-text') || "";
            //}
            //else


            if (wky != "go") {
                ty = _C[wky].ty;
                cust[wky] = {};
                if (ty.indexOf('ddl') > -1) {
                    var a = $('#ddl_' + typ + wky).attr('data-value');
                    var b = $('#ddl_' + typ + wky).attr('data-text');

                    cust[wky].val = $('#ddl_' + typ + wky).attr('data-value') || "";
                    cust[wky].txt = $('#ddl_' + typ + wky).attr('data-text') || "";
                }
                else {
                    var txval = $('#tx_' + typ + wky).val() || "";
                    cust[wky].val = txval;
                    cust[wky].txt = txval;
                }
            }
        }
        with ($('#ddl_' + typ + 'cus').find("li[data-val='" + cust.cus.val + "']")) {
            cust.twn.val = data('tcd') || "",
                cust.twn.txt = data('tnm') || "",

                cust.sub_area.val = data('tcd') || "",
                cust.sub_area.txt = data('tnm') || "",

                cust.spec.val = data('d_spec') || "",
                cust.spec.txt = data('d_spec') || "",

                cust.cat.val = data('d_cat') || "",
                cust.cat.txt = data('d_cat') || "",

                cust.sf.val = data('sf') || "";
        }



        cust.B_Val.val = $('#hTtl_Value').val();
        cust.B_Val.txt = $('#hTtl_Value').val();
        $('#hTtl_Value').val('');
        //cust.B_Val.val = data('tcd') || "",

        //if (Worked_With_Name != "") Worked_With_Name += "$$";
        //Worked_With_Name += cust.jw.val;



        DCRDta_F = {};


        //DCRDta_F = getData("DCRMain");

        //DCRDta_F.SFTyp = $("#hSFTyp").val();
        DCRDta_F.Month = hndMonth;
        DCRDta_F.Year = hndYear;


        //SFDet = getData("SFDet")

        for (tab in tabs) {
            //if (otabs.indexOf(tab) > -1) {
            if (tab == typ) {
                //dta = getData('dta_' + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_') + '_' + tab)
                DCRDta_F[tabs[tab]] = [cust];
            }
            else {
                DCRDta_F[tabs[tab]] = [];
            }
        }


        //sSetup = JSON.stringify(getData("Setup"));
        SFData = JSON.stringify(SFDet);

        //var sf_codee = $("#hSF_Code").val();


        //alert(JSON.stringify(DCRDta_F));

        //data = getData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ);
        //if (this.edtIndex != null) {
        //    data.splice(this.edtIndex, 1, cust);
        //} else
        //    data.push(cust);
        //setData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ, data);

        //var successcount = 0;


        asyncState = false;
        PageMethods.Save_Business_Entry_Single_Data(JSON.stringify(SFDet), JSON.stringify(DCRDta_F), function (data) {

            result = JSON.parse(data);

            if (result.success == true) {

                //data = getData('dta_' + DCR.SFCode + '_' + DCRDta_F.EDate.replace(/\//g, '_') + '_' + typ);
                //data = getData('dta_' + DCR.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);
                data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);

                if (DCR.edtIndex != null) {
                    data.splice(DCR.edtIndex, 1, cust);
                } else
                    data.push(cust);
                setData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ, data);

                DCR.clrEntry(typ);
                DCR.genGrid(typ);

                $('#ddl_WorkType').attr('disabled', 'disabled');

                $('#ddl_' + typ + 'cus').addClass("ddl-Box");
                $(".btnEdt").css('display', 'none');

            }



            __DoubleClick = false;
            $('#btn-ins').removeAttr('disabled');
            count = 1;
        }
            , function (error) {
                showAlert(error.get_message());
                $(".ProcessMsg").css('display', 'none');
                $('#btn-ins').removeAttr('disabled');
                __DoubleClick = false;
            }

        );

    },
    edtEntry: function (typ, indx) {

        if (count == 0) {
            alertBox.Show('Kindly Save the Edited Value...');
            return false;

        }

        $('#ddl_' + typ + 'cus').removeClass("ddl-Box");

        //SFDet = getData("SFDet")


        //data = getData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ);

        data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);


        if (data.length > 0) {
            cust = data[indx];
            //$('#ddl_HQ').val(cust.sf.val);
            //BuildMenu(1); getMasterList(); srtCusTwbs($("#ddl_SDP"));
            for (wky in __Wndw[typ].fields) {
                if (wky == "rem") {
                    tx = cust.fedbk.txt;
                    $('#ddl_' + typ + 'rem').setItem(cust.rem, cust.rem);
                    $('#ddl_' + typ + 'rem').find('span').text(tx + ', ' + cust.rem);
                    $('#tCusRem').val(cust.rem);
                    $('#ddl_fedBk').setItem(cust.fedbk.val, tx);
                }
                else if (wky != "go") {
                    ty = __Wndw[typ].fields[wky].ty;
                    if (ty.indexOf('ddl') > -1) {
                        tx = cust[wky].txt;
                        vl = cust[wky].val;
                        $('#ddl_' + typ + wky).setItem(vl, tx);
                    }
                    else
                        $('#tx_' + typ + wky).val(cust[wky].val);





                    $('#hTtl_Value').val(cust.B_Val.val);


                }
            }


            this.edtIndex = indx;
            this.edtCus = cust.cus.val;
            tr = $('#dWD_' + typ).find("TR");
            $(tr).removeClass("selector green");
            $(tr[indx]).addClass("selector green");


            count = 0;

        }
    },
    //delEntry: function (typ, index) {

    //    if (count == 0) {
    //        alertBox.Show('Kindly Save the Edited Value...');
    //        return false;

    //    }



    //    //data = getData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ);
    //    data = getData('dta_' + this.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);

    //    if (data.length > 0) {

    //        var indx = data.findIndex(img => img.cus.val === index);
    //        Dta = data[indx];
    //        //Dta = data[indx];
    //        data.splice(indx, 1);
    //        setData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ, data);

    //        data = getData('nCus_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_'));
    //        nDta = $.grep(data, function (a) { return (a.id == Dta.cus.val) });
    //        if (nDta.length > 0) {
    //            data.splice(data.indexOf(nDta[0]), 1);
    //            setData('nCus_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_'), data);
    //            ty = tabs[typ].toLowerCase();
    //            data = getData('d_' + ty + '_' + Dta.sf.val);
    //            nDta = $.grep(data, function (a) { return (a.id == Dta.cus.val) });
    //            if (nDta.length > 0) {
    //                data.splice(data.indexOf(nDta[0]), 1);
    //                setData('d_' + ty + '_' + Dta.sf.val, data);
    //            }
    //        }
    //    }
    //    updState(1);
    //    this.genGrid(typ);


    //},

    delEntry_New: function (typ, index, Dr_Code) {

        if (count == 0) {
            alertBox.Show('Kindly Save the Edited Value...');
            return false;

        }
        var sf_codee = $("#hSF_Code").val();
        var div_codee = $('#hDiv').val();
        asyncState = false;
        PageMethods.Delete_Business_Details(sf_codee, div_codee, Dr_Code, hndMonth, hndYear, function (data) {
            Dresult = JSON.parse(data);
            if (Dresult.success == true) {

                //data = getData('dta_' + DCR.SFCode + '_' + DCR.EDate.replace(/\//g, '_') + '_' + typ);
                data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);

                if (data.length > 0) {

                    var indx = data.findIndex(img => img.cus.val === Dr_Code);
                    Dta = data[indx];
                    //Dta = data[indx];
                    data.splice(indx, 1);
                    //setData('dta_' + DCR.SFCode + '_' + DCR.EDate.replace(/\//g, '_') + '_' + typ, data);
                    setData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ, data);

                    //data = getData('nCus_' + DCR.SFCode + '_' + DCR.EDate.replace(/\//g, '_'));

                    data = getData('nCus_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear);


                    nDta = $.grep(data, function (a) { return (a.id == Dta.cus.val) });
                    if (nDta.length > 0) {
                        data.splice(data.indexOf(nDta[0]), 1);
                        //setData('nCus_' + DCR.SFCode + '_' + DCR.EDate.replace(/\//g, '_'), data);
                        setData('nCus_' + DCR.SFCode + '_' + hndMonth + '_' + hndYear, data);

                        ty = tabs[typ].toLowerCase();
                        data = getData('d_' + ty + '_' + Dta.sf.val);
                        nDta = $.grep(data, function (a) { return (a.id == Dta.cus.val) });
                        if (nDta.length > 0) {
                            data.splice(data.indexOf(nDta[0]), 1);
                            setData('d_' + ty + '_' + Dta.sf.val, data);
                        }
                    }
                }

                updState(1);
                DCR.genGrid(typ);
                alertBox.Show('Deleted Successfully...');
            }
            else {
                /*  alert('Error: ' + Mobj.message);*/
                //alert('Try Again...');
                alert('Try Again...Error: ' + Dresult.message);
            }
        });

    },
    clrEntry: function (typ) {
        for (wky in __Wndw[typ].fields) {
            fld = __Wndw[typ].fields[wky];

            if (wky == "rem") {
                $('#ddl_' + typ + 'rem').setItem('', '');
                $('#tCusRem').val('');
                $('#ddl_fedBk').setItem('', '');
            }
            else if (wky != "go") {
                if (fld.ty.indexOf('ddl') > -1)
                    if (wky == "jw") {
                        /* $('#ddl_' + typ + wky).setItem(SFDet.SFCode, "SELF,");
                        $('#ddl_' + typ + wky).find("li[data-val='" + SFDet.SFCode + "']").find(".ckBx").prop('checked', true);*/
                    }
                    else
                        $('#ddl_' + typ + wky).setItem('', '');
                else
                    $('#tx_' + typ + wky).val('');
            }
        }
        this.edtIndex = null;
        this.edtCus = '';
    },


    genGrid: function (typ) {
        //data = getData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ);

        //SFDet = getData("SFDet")

        $("#planer").css('display', 'block');
        $("#idWD").css('display', 'block');

        data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);

        sgrd = "<table class='fg-group'>";
        for (ij = 0; ij < data.length; ij++) {
            gRW = data[ij];
            sgrd += "<tr ondblclick='DCR.edtEntry(\"" + typ + "\",\"" + ij + "\")'><th class='rwHd'> " + (ij + 1) + "</th>";
            for (wky in __Wndw[typ].grd_Cols) {


                fa = __Wndw[typ].fields[wky];
                w = (fa.gw || fa.iw || fa.w)
             
                    sgrd += "<td style='border:1px solid #D2D2D2;min-width:" + w + "px !important;max-width:" + w + "px !important;width:" + w + "px !important;";               

                if (wky == "go") {
                    sgrd += "padding:2px 6px;'><a href='#' class='button button-red go-bt' style='padding:5px 8px' onclick='DCR.delEntry_New(\"" + typ + "\",\"" + ij + "\",\"" + gRW["cus"].val + "\")'>D</a><a href='#' class='button button-blue go-bt' style='padding:5px 8px' onclick='DCR.edtEntry(\"" + typ + "\",\"" + ij + "\")'>E</a>"
                } else {
                    //sgrd += "padding-left: 10px;'>" + ((wky == "rem") ? gRW.fedbk.txt + ', ' + gRW[wky] : ((gRW[wky] == undefined) ? "" : (gRW[wky].txt == undefined) ? gRW[wky] : gRW[wky].txt))
                    if (wky == "prd") {
                        sgrd += "padding-left: 10px;display:none;'>" + ((wky == "rem") ? gRW[wky] : ((gRW[wky] == undefined) ? "" : (gRW[wky].txt == undefined) ? gRW[wky] : gRW[wky].txt))
                    }
                    else {
                        sgrd += "padding-left: 10px;'>" + ((wky == "rem") ? gRW[wky] : ((gRW[wky] == undefined) ? "" : (gRW[wky].txt == undefined) ? gRW[wky] : gRW[wky].txt))
                    }
                }
                sgrd += "</td>";


            }

            sgrd += "</tr>";
        }
        sgrd += "</table>";
        $('#dWD_' + typ).html(sgrd);

        tr = $('#dWD_' + typ).find("TR");
        $(tr).removeClass("selector green");
        if (this.edtIndex > -1) $(tr[this.edtIndex]).addClass("selector green");
    },



    //ClearData: function () {
    //    alertBox.confirm("Do you want to <b style='color:red'>Clear Data(s)</b> for this <b>" + $("#DtInf").html() + "</b> Date ?", function () {
    //        alertBox.confirm("Are you sure to clear data(s)?", function () {
    //            for (i = localStorage.length - 1; i > -1; i--) {
    //                key = localStorage.key(i);
    //                if (key.indexOf('dta_' + DCR.SFCode + '_' + hndMonth + '_' + hndYear + '_') > -1) delData(key);
    //            }
    //            DCR.genPrev();
    //            alertBox.Show("Data(s) are cleared...");
    //        })
    //    })
    //    updState(0);
    //},


    genEditRpt: function () {

        $(".ProcessMsg").css('display', 'inline');
        $(".txtMsg").text('Loading.Please Wait...');
        sgrd = "<div style='padding:50px;'><div class='txhead'>Listed Dr-Business ValueWise</div><br>";
        //slopt = $('#ddl_WorkType').find('option:selected')
        //sst = slopt.data('etabs') + ',';
        //typs = sst.split(',');
        $("#planer").css('display', 'none');
        $("#idWD").css('display', 'none');

        asyncState = false;
        PageMethods.Get_AllReport(SFDet.SFCode, hndMonth, hndYear, function (data) {
            //alert(data);
            EditRpt = JSON.parse(data);



            //data = getData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ);
            sgrd += "<table id='tblRoute' class='fg-group' style='width:100%;'>";
            //for (ij = 0; ij < EditRpt.length; ij++) {
            //gRW = data[ij];
            sgrd += "<tr><th class='rwHd'>Sl.No</th><th class='rwHd'>Listed Doctor</th><th class='rwHd' style='display:none;'>Product</th><th class='rwHd'>Specialty</th><th class='rwHd'>Category</th><th class='rwHd'>Sub Area</th><th class='rwHd'>Value</th>";
            sgrd += "</tr>";

            let totalVal = 0;

            for (wky in EditRpt) {


                sgrd += "<tr>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + parseInt(parseInt(wky, 0) + 1) + "</td>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + EditRpt[wky].ListedDr_Name + "</td>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;display:none;'>" + EditRpt[wky].Product_Detail_Name + "</td>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + EditRpt[wky].Speciality_Name + "</td>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + EditRpt[wky].Category_Name + "</td>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + EditRpt[wky].Territory_Name + "</td>";
                sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + EditRpt[wky].TotalValue + "</td>";
                //sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + EditRpt[wky].Type + "</td>";
                //sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'>" + Type_Name + "</td>";
                //sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;display:none;'>" + EditRpt[wky].Route_Tbl_SlNo + "</td>";
                //sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;display:none;'>" + EditRpt[wky].Row_No + "</td>";
                ////sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;display:none;'>" + (EditRpt[wky].Type == "D" ? 'Lst Doctor' : EditRpt[wky].Type == "C" ? 'Chemist' : 'Unlst Doctor') + "</td>";
                //sgrd += "<td style='border:1px solid #D2D2D2;padding-left: 10px;'><a href='#' class='button button-red go-bt' style='padding:5px 8px;display:none;' onclick='DCR.delEntry_New(\"" + EditRpt[wky].Type + "\",\"" + EditRpt[wky].Code + "\",\"" + parseInt(parseInt(wky, 0) + 1) + "\",\"" + EditRpt[wky].Route_Tbl_SlNo + "\",\"" + EditRpt[wky].Row_No + "\",\"" + EditRpt[wky].SDP + "\")'>D</a></td>";
              
                sgrd += "</tr>";
                //if (wky == "go") {
                //    sgrd += "padding:2px 6px;'><a href='#' class='button button-red go-bt' style='padding:5px 8px' onclick='DCR.delEntry(\"" + typ + "\",\"" + ij + "\")'>D</a><a href='#' class='button button-blue go-bt' style='padding:5px 8px' onclick='DCR.edtEntry(\"" + typ + "\",\"" + ij + "\")'>E</a>"
                //} else {
                //    sgrd += "padding-left: 10px;'>" + ((wky == "rem") ? gRW.fedbk.txt + ', ' + gRW[wky] : ((gRW[wky] == undefined) ? "" : (gRW[wky].txt == undefined) ? gRW[wky] : gRW[wky].txt))
                //}
                //sgrd += "</td>";
                //alert(wky);
                //alert(EditRpt[0].Trans_Detail_Name);
                //alert(EditRpt[wky].Trans_Detail_Name);
                //alert(EditRpt[wky].Mode);
                let rowVal = parseFloat(EditRpt[wky].TotalValue) || 0;
                totalVal += rowVal;
            }


            if (EditRpt.length > 0) {
                sgrd += "<tr style='font-weight:bold;background:#f5f5f5;color:red;'>";
                sgrd += "<td colspan='5' style='border:1px solid #D2D2D2;padding:5px;text-align:right;'>Total</td>";

                // clickable link for total value
                sgrd += "<td style='border:1px solid #D2D2D2;padding:5px;text-align:right;'>" +
                    "<a href='rptDR_Business_Valuewise_Detail.aspx?sf_code=" + SFDet.SFCode + "&month=" + hndMonth + "&year=" + hndYear + "' target='_blank' style='color:red;text-decoration:underline;'>" +
                    totalVal.toFixed(2) +
                    "</a>" +
                    "</td>";

                sgrd += "</tr>";
            }



            //}
            sgrd += "</table>";

            sgrd += "</div>";

            //$('#tblRoute tr:last').find('td:last-child').css('display', 'block');
            //$('#tblRoute tbody tr td:last-child').css('display', 'block');
            $('#idWP').html(sgrd);
            //$('#tblRoute.fg-group tr:last td:last-child').find('a.button.button-red.go-bt').css('display', 'block');
            //$('#tblRoute tr:last td:last-child').find('a').css('display', 'block');
            //if (EditRpt.length > 0)
            //    $(".clsDy").css('display', 'none');
            //if (DayStatus == '2')
            //    $(".clsDy").css('display', 'none');

            //var cusval = EditRpt.findIndex(x => x.cus.val === Code);
            //EditRpt.splice(cusval, 1);
            $(".ProcessMsg").css('display', 'none');
         

        }, function (error) {
            showAlert(error.get_message());
            $(".ProcessMsg").css('display', 'none');

        });
    },


    //genPrev: function () {
    //    sgrd = "<div style='padding:15px;'><a href='javascript:DCR.ClearData()' class='button button-orange' style='margin: -15px;'>Clear Data</a><div class='txhead'>Daily Work Entry - Preview</div><br>";
    //    //slopt = $('#ddl_WorkType').find('option:selected')
    //    //sst = slopt.data('etabs') + ',';
    //    sst = 'D,';
    //    sgrd += "<b style='color:blue;font-weight:bold;'>Work Type : </b><br><br>";
    //    typs = sst.split(',');
    //    for (ik = 0; ik < typs.length - 1; ik++) {
    //        typ = typs[ik];
    //        //data = getData('dta_' + this.SFCode + '_' + this.EDate.replace(/\//g, '_') + '_' + typ);
    //        data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);
    //        if (data.length > 0) {
    //            sgrd += "<table class='fg-group'><tr><th class='rwHd'></th>";
    //            for (_ic in __Wndw[typ].prv_Cols) {
    //                fa = __Wndw[typ].fields[_ic];
    //                if (_ic != "go") sgrd += "<th style='min-width:" + (fa.gw || fa.iw || fa.w) + "px !important;'>" + fa.cap + "</th>";

    //            }
    //            sgrd += "</tr>";
    //            for (ij = 0; ij < data.length; ij++) {
    //                gRW = data[ij];
    //                sgrd += "<tr><th class='rwHd'>" + (ij + 1) + "</th>";
    //                for (wky in __Wndw[typ].prv_Cols) {
    //                    fa = __Wndw[typ].fields[wky];
    //                    w = (fa.gw || fa.iw || fa.w)
    //                    if (wky != "go") {
    //                        sgrd += "<td style='border:1px solid #D2D2D2;min-width:" + w + "px !important;max-width:" + w + "px !important;width:" + w + "px !important;padding-left: 10px;'>" + ((wky == "rem") ? gRW.fedbk.txt + ', ' + gRW[wky] : ((gRW[wky] == undefined) ? "" : (gRW[wky].txt == undefined) ? gRW[wky] : gRW[wky].txt)) + "</td>";
    //                    }

    //                }

    //                sgrd += "</tr>";
    //            }
    //            sgrd += "</table><br>";
    //        }
    //    }
    //    sgrd += "<br><b style='color:blue;font-weight:bold;'>Remarks :- </b>" + $("#txa_RRmks").val();

    //    sgrd += "</div>";
    //    $('#idWP').html(sgrd);
    //},
    //validDCR: function () {
    //    sFWFl = $('#ddl_WorkType').find('option:selected').attr('data-fwflg');
    //    if (sFWFl == 'L') { $('#ddl_WorkType').val(''); return false; }
    //    $('#ddl_WorkType').removeAttr('disabled');
    //    eDt = new Date(DCRDatas.EDate);
    //    cDt = new Date(DCRDatas.CurrDt);
    //    if (eDt > cDt) {
    //        showAlert("Future Date DCR Not Allowed....");
    //        $('#ddl_WorkType').attr('disabled', 'disabled');
    //        return false;
    //    }
    //    $o = $("#ddl_WorkType");
    //    sopt = $o.find('option:selected');
    //    otabs = sopt.data('etabs');
    //    sWt = $o.val();
    //    if (sWt == '') {
    //        alertBox.Show('Select the Worktype', $("#ddl_WorkType"));
    //        return false
    //    }
    //    if (sopt.attr('data-fwflg') == "F") {
    //        if ($("#ddl_HQ").val() == '') {
    //            alertBox.Show('Select the Headquater', $("#ddl_HQ"));
    //            return false
    //        }

    //        flag = false;
    //        for (atab in tabs) {
    //            if (otabs.indexOf(atab) > -1) {
    //                dtd = getData('dta_' + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_') + '_' + atab)
    //                if (dtd.length > 0) {
    //                    flag = true;
    //                }
    //            }
    //        }

    //        dtd = getData('dta_' + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_') + '_D')
    //        if (dtd.length < 1 && Setup.DCRDrMand == 1) {
    //            alertBox.Show('Doctor Entry not found for submit DCR');
    //            return false;
    //        }


    //        if (flag == false) {
    //            alertBox.Show('Entry not found for submit DCR');
    //            return false;
    //        }
    //    }





    //    sfty = $('#hSFTyp').val();//Newly Added-2025
    //    if (sfty != 1) {
    //        HQ = $('#ddl_HQ').find('option:selected').val();
    //        if (HQ == '' && sopt.attr('data-fwflg') != "F" && sopt.attr('data-fwflg') != "W" && sopt.attr('data-fwflg') != "L" && sopt.attr('data-fwflg') != "H") {
    //            alertBox.Show('Select the Headquarter', $("#ddl_SDP"));
    //            return false
    //        }
    //    }
    //    Terri = $('#ddl_SDP').find('option:selected').val();
    //    if (Terri == '' && sopt.attr('data-fwflg') != "F" && sopt.attr('data-fwflg') != "W" && sopt.attr('data-fwflg') != "L" && sopt.attr('data-fwflg') != "H") {

    //        alertBox.Show('Select the Territory', $("#ddl_SDP"));
    //        return false
    //    }
    //    /////////////////////////////////////////////







    //    navTag($("[data-tag=P]"));
    //    alertBox.confirm("Do want to submit the DCR ? ",
    //        function () {
    //            alertBox.confirm("Are you sure to submit ? ", function () {
    //                $(".Process").css('display', 'block');
    //                setTimeout(function () { DCR.svSaveDCR() }, 10);
    //            });
    //        });
    //},
    //svSaveDCR: function () {

    //    SFDet = getData("SFDet")
    //    sopt = $('#ddl_WorkType').find('option:selected');
    //    otabs = sopt.data('etabs');

    //    DCRDta = getData("DCRMain");
    //    DCRDta.SFTyp = $("#hSFTyp").val();
    //    DCRDta.DCRType = $("#hDtTyp").val();
    //    DCRDta.Wtyp = $("#ddl_WorkType").val(); var d = new Date();
    //    DCRDta.ETime = d.toISOString();
    //    DCRDta.SysIP = SFDet.SysIP;
    //    DCRDta.ETyp = SFDet.ETyp;
    //    DCRDta.FWFlg = sopt.attr('data-fwflg');
    //    DCRDta.ETabs = otabs;

    //    DCRDta.STime = $("#hSTime").val();


    //    if (sopt.attr('data-fwflg') == 'F') {//Newly Added-2025
    //        DCRDta.TwnCd = "";
    //        //  DCRDta.terri_name = "";
    //    }
    //    else {
    //        DCRDta.TwnCd = $("#ddl_SDP option:selected").val();
    //        // DCRDta.terri_name = $("#ddl_SDP option:selected").text();
    //    }



    //    if (sopt.attr('data-fwflg') == 'F') {
    //        flag = false;
    //        for (tab in tabs) {
    //            if (otabs.indexOf(tab) > -1) {
    //                dta = getData('dta_' + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_') + '_' + tab)
    //                DCRDta[tabs[tab]] = dta;
    //            }
    //            else { DCRDta[tabs[tab]] = []; }
    //        }
    //    }
    //    DCRData = JSON.stringify(DCRDta);
    //    sSetup = JSON.stringify(getData("Setup"));
    //    SFData = JSON.stringify(SFDet);
    //    nlst = getData('nCus_' + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_'));






    //    PageMethods.SaveDCR(SFData, DCRData, sSetup, nlst, function (data) {
    //        result = JSON.parse(data);
    //        if (result.success == true) {
    //            updState(0);
    //            delData("dta_" + SFDet.SFCode + "_" + DCRDta.EDate.replace(/\//g, '_') + "_D");
    //            delData("dta_" + SFDet.SFCode + "_" + DCRDta.EDate.replace(/\//g, '_') + "_C");
    //            delData("dta_" + SFDet.SFCode + "_" + DCRDta.EDate.replace(/\//g, '_') + "_S");
    //            delData("dta_" + SFDet.SFCode + "_" + DCRDta.EDate.replace(/\//g, '_') + "_H");
    //            delData("dta_" + SFDet.SFCode + "_" + DCRDta.EDate.replace(/\//g, '_') + "_U");
    //            delData("nCus_" + SFDet.SFCode + "_" + DCRDta.EDate.replace(/\//g, '_'));
    //            DtDet = result.NextDat;
    //            ClNCus = result.newCus;
    //            ExData = JSON.stringify(result.exDta);
    //            for (ncs in ClNCus) {
    //                changeMasCode(ClNCus[ncs].cus);
    //            }
    //            getMasterList()
    //            dt = new Date(DtDet.DCR_Date);
    //            dy = dt.getDate(); mm = (dt.getMonth() + 1); yy = dt.getFullYear();
    //            DtDet.DCR_Date = ((mm < 10) ? '0' : '') + mm + '/' + ((dy < 10) ? '0' : '') + dy + '/' + yy;
    //            DCRDatas.EDate = DtDet.DCR_Date;
    //            DCRDatas.Rem = '';
    //            DCRDatas.Wtyp = '';
    //            lckMsg = DtDet.DtMsg; showHideBlockMsg();
    //            $('#hDCRDt').val(DtDet.DCR_Date);
    //            $("#txa_RRmks").val('');
    //            $("#hDtTyp").val(DtDet.Type);
    //            $('#ddl_WorkType').val('');

    //            $('#ddl_SDP').val('');//Newly Added---2025
    //            $('#ddl_HQ').val();



    //            var d = new Date();
    //            DCRDatas.STime = d.toISOString();
    //            setData("DCRMain", DCRDatas);

    //            DCR.haveMenu = false;
    //            DCR.edtIndex = null;
    //            DCR.edtCus = '';
    //            DCR.EDate = DtDet.DCR_Date;
    //            DCR.curMnu = null;
    //            BuildMenu(0);

    //            $("#DtInf").html(((dy < 10) ? '0' : '') + dy + '/' + ((mm < 10) ? '0' : '') + mm + '/' + yy + " - " + days[dt.getDay()] + " " + ((DtDet.DTRem != "") ? " -  <span class='stat" + DtDet.Type + "'>" + DtDet.DTRem + "</span>" : ""));
    //            //if(ExData!="{}"){setExistData(ExData);}
    //            $(".Process").css('display', 'none');
    //            alertBox.Show("DCR Saved Successfully...");

    //            $('#ddl_WorkType').removeAttr('disabled');
    //            eDt = new Date(DtDet.DCR_Date);
    //            cDt = new Date(DtDet.CurrDt);
    //            if (eDt > cDt) {
    //                showAlert("Future Date DCR Not Allowed....");
    //                $('#ddl_WorkType').attr('disabled', 'disabled');
    //            }
    //        }
    //    }, function (error) {
    //        showAlert(error.get_message());
    //        $(".Process").css('display', 'none');
    //    });
    //},
    ShowCusDt: function () {

        $(".ProcessMsg").css('display', 'inline');
        $(".txtMsg").text('Loading.Please Wait...');
        showAlert('Select the Territory and Press Go.');
        count = 1;

        $("#planer").css('display', 'block');
        $("#idWD").css('display', 'inline');
        $("#DtInf").css('display', 'inline');
        $(".clsMonth").css('display', 'none');
        $(".ResetGo").css('display', 'inline');

        hndMonth = $('#ddlMonth').val();
        hndYear = $('#ddlYear').val();

        $("#DtInf").html("Selected Month/Year : " + $("#ddlMonth option:selected").text() + " - " + hndYear);

        $(".mnu-bt").css('display', 'inline');
        showMenu();

        ClearAllData();

        $(".ProcessMsg").css('display', 'none');
    },
    Reset: function () {

        $(".ProcessMsg").css('display', 'inline');
        $(".txtMsg").text('Loading.Please Wait...');
        count = 1;
        ClearAllData();
        navTag($("[data-tag=D]"));
        $("#planer").css('display', 'none');
        $("#idWD").css('display', 'none');
        $("#DtInf").css('display', 'none');
        $(".clsMonth").css('display', 'inline');

        $(".mnu-bt").css('display', 'none');
        $(".ResetGo").css('display', 'none');
       

        hideMenu();

      
        //document['location']['href'] = 'Doctor_Business_Entry.aspx';
        showAlert('Select the Territory and Press Go.');
        $(".ProcessMsg").css('display', 'none');
    },
    ShowDr: function () {
        count = 1;
        hideAlert();
        getBusiness_Details_TerritoryWise($("#ddl_SDP option:selected").val());
        srtCusTwbs($("#ddl_SDP"));
       

        //
    }
};
setHome = function () { $("#alHome").attr("href", $("#aHome").attr("href")) }
showHideBlockMsg = function () {
    $('#lckMsg').css('display', ((lckMsg != '') ? 'block' : 'none'));
    $('#frm').css('display', ((lckMsg != '') ? 'none' : 'block'));
    $('#lckMsg').html("<div class='alert alert-danger'><strong>" + lckMsg + "</strong></div><a id='alHome'  onclick='setHome()' class='button home-bt'>Home</a>")
}
openWin = function (typ, indx) {

    idv = $('#ddl_' + typ + "cus").attr('data-value') || "";

    if (!(idv == "" || idv.indexOf("N-") > -1)) {
        return false;
    }
    sfty = $('#hSFTyp').val();
    sf = $('#ddl_HQ').val();
    if (sf == '' && sfty != 1) { alertBox.Show("select the Headquarter", $("#ddl_HQ")); return false; }

    $(".drE").css("display", "none")
    if (typ == "D" || typ == "U") $(".drE").css("display", "table-row")
    if (idv != "") {
        dNCus = getData("nCus_" + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_')) || [];
        nCus = $.grep(dNCus, function (a) { return (a.id == idv); })[0];

        $('#tNwCus').val(nCus.name);
        $("#tNwAdd").val(nCus.addr);
        $('#ddl_Ntwn').setItem(nCus.twn.val, nCus.twn.txt);
        $('#ddl_NCat').setItem(nCus.Cat.val, nCus.Cat.txt);
        $('#ddl_NSpc').setItem(nCus.Spc.val, nCus.Spc.txt);
        $('#ddl_NCla').setItem(nCus.Cla.val, nCus.Cla.txt);
        $('#ddl_NQua').setItem(nCus.Qua.val, nCus.Qua.txt);
    }

    $o = $('#ddl_' + typ + 'cus');
    $("#wNCus").css("display", "block");
    $("#wNCus").attr('data-wTyp', typ);
    $(".wCap").html("New " + $o.attr('data-dft').replace(/-/g, '') + "");
    //$(".lblSDP").html(Setup.SDPCap);
}
updState = function (v) {
    Entry.UModi = v;
    //setData("Entry_" + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_'), Entry);
    //setData("Entry_" + DCRDatas.SFCode + '_2025_2_', Entry);
    setData("Entry_" + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_', Entry);
}
CloseWin = function () {
    $('#tNwCus').val('');
    $("#tNwAdd").val('');
    $('#ddl_Ntwn').setItem('', '');
    $('#ddl_NCat').setItem('', '');
    $('#ddl_NSpc').setItem('', '');
    $('#ddl_NCla').setItem('', '');
    $('#ddl_NQua').setItem('', '');

    $("#wNCus").css("display", "none");
    $(".wCap").html("New " + $o.attr('data-dft').replace(/-/g, '') + "")
}

//AddtoListCus = function () {
//    typ = $('#wNCus').attr('data-wTyp');
//    sfty = $('#hSFTyp').val();
//    sf = $('#ddl_HQ').val();
//    Esf = $('#hSF_Code').val();
//    if (sfty == 1) sf = Esf;
//    if ($("#tNwCus").val() == '') { alertBox.Show("Enter the Name", $("#tNwCus")); return false; }
//    dNCus = getData("nCus_" + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_')) || [];
//    nCus = {}
//    dt = new Date();
//    nCus.id = $('#ddl_' + typ + "cus").attr('data-value') || "";
//    ef = true;
//    if (nCus.id == "") {
//        nCus.id = "N-" + typ + DCRDatas.SFCode + dt.getTime();
//        ef = false;
//    }


//    nCus.name = $("#tNwCus").val();
//    nCus.addr = $("#tNwAdd").val();
//    nCus.twn = {};
//    nCus.twn.val = $('#ddl_Ntwn').attr('data-value') || "";
//    nCus.twn.txt = $('#ddl_Ntwn').attr('data-text') || "";
//    if (nCus.twn.val == '') { alertBox.Show("Select the " + Setup.SDPCap, $("#ddl_Ntwn").find(".disp-selopt")); return false; }
//    nCus.Cat = {};
//    nCus.Cat.val = $('#ddl_NCat').attr('data-value') || "";
//    nCus.Cat.txt = $('#ddl_NCat').attr('data-text') || "";
//    if (nCus.Cat.val == '' && (typ == "D" || typ == "U")) { alertBox.Show("Select the Category", $("#ddl_NCat").find(".disp-selopt")); return false; }
//    nCus.Spc = {};
//    nCus.Spc.val = $('#ddl_NSpc').attr('data-value') || "";
//    nCus.Spc.txt = $('#ddl_NSpc').attr('data-text') || "";
//    if (nCus.Spc.val == '' && (typ == "D" || typ == "U")) { alertBox.Show("Select the Specialty", $("#ddl_NSpc").find(".disp-selopt")); return false; }
//    nCus.Cla = {};
//    nCus.Cla.val = $('#ddl_NCla').attr('data-value') || "";
//    nCus.Cla.txt = $('#ddl_NCla').attr('data-text') || "";
//    if (nCus.Cla.val == '' && (typ == "D" || typ == "U")) { alertBox.Show("Select the Class", $("#ddl_NCla").find(".disp-selopt")); return false; }
//    nCus.Qua = {};
//    nCus.Qua.val = $('#ddl_NQua').attr('data-value') || "";
//    nCus.Qua.txt = $('#ddl_NQua').attr('data-text') || "";
//    if (nCus.Qua.val == '' && (typ == "D" || typ == "U")) { alertBox.Show("Select the Qualification", $("#ddl_NQua").find(".disp-selopt")); return false; }
//    nCus.typ = typ;
//    nCus.sf = sf;
//    if (ef == true) {
//        idx = getNewCusIdx(nCus.id);
//        dNCus.splice(idx, 1, nCus);
//    } else
//        dNCus.push(nCus);
//    setData("nCus_" + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_'), dNCus);

//    dty = (typ == "D") ? "drs" : (typ == "C") ? "chm" : (typ == "s") ? "stk" : (typ == "H") ? "hos" : (typ == "U") ? "udr" : '';
//    dLCus = getData("d_" + dty + "_" + sf) || [];
//    Cus = {}
//    Cus.id = nCus.id;
//    Cus.name = nCus.name;
//    Cus.TCd = nCus.twn.val;
//    Cus.TNm = nCus.twn.txt;
//    Cus.sf = nCus.sf;

//    if (ef == true) {
//        idx = dLCus.indexOf(Cus);
//        dLCus.splice(idx, 1, Cus);
//    } else
//        dLCus.push(Cus);

//    setData("d_" + dty + "_" + sf, dLCus);
//    eval("d_" + dty + "=getData('d_" + dty + "_" + sf + "');");
//    reloadDatas(typ);

//    vl = nCus.id;
//    tx = nCus.name;
//    $('#ddl_' + typ + "cus").setItem(vl, tx);


//    CloseWin();
//}
//getNewCusIdx = function (id) {
//    dNCus = getData("nCus_" + DCRDatas.SFCode + '_' + DCRDatas.EDate.replace(/\//g, '_')) || [];
//    for (lix = 0; lix < dNCus.length; lix++) { if (dNCus[lix].id == id) { return lix; } } return -1;
//}
create_Dropdown = function () {
    $('.ddl-Box').each(function () {
        $dtaVal = $(this).data('value');
        $dtaText = $(this).data('text');
        $dtaSrc = $(this).data('src');
        $(this).attr('data-dfv', $dtaVal);
        $(this).attr('data-dft', $dtaText);
        $(this).attr('data-text', '');
        $dtaTF = $(this).data('tf');
        $dtaVF = $(this).data('vf');
        if ($dtaVF == "undefined") $dtaVF = "id";
        if ($dtaTF == "undefined") $dtaTF = "name";


        try {
            optDta = eval($dtaSrc);
        } catch (e) {
            optDta = JSON.parse(_lcStore.getItem($dtaSrc)) || [];
        }
        $dd = "<a href='#' class='disp-selopt'><span>" + (($dtaText == '') ? '&nbsp;' : $dtaText) + "</span></a>";
        if (!$(this).hasClass('wind')) {
            $dd += "<div class='ddl-Drop' style='" + (($(this).hasClass('multi')) ? "padding-top:4px;" : "") + (($(this).data('inwidth') != undefined) ? "width:" + $(this).data('inwidth') + " !important;" : "") + "'>";
            if ($(this).hasClass('search')) {
                $dd += "<div class='ddl-search'><input type='text' autocomplete='off' tabindex='-1' onkeyup='searchItems(event,this)'></div>";
            }
            $dd += "<ul class='ddl-results'>"; $(this).data('vf', $dtaVF);
            $(this).data('tf', $dtaTF);
            for ($indx = 0; $indx < optDta.length; $indx++) {


                if ($dtaSrc == "d_Prod")
                    $dd += "<li class='active-result' data-val='" + optDta[$indx][$dtaVF] + "'  data-pack='" + optDta[$indx]["Product_Sale_Unit"] + "' data-rate='" + optDta[$indx]["SetupBased_Price"] + "' data-retailor='" + optDta[$indx]["Retailor_Price"] + "' data-mrp='" + optDta[$indx]["MRP_Price"] + "' data-distributor='" + optDta[$indx]["Distributor_Price"] + "' data-nsr='" + optDta[$indx]["NSR_Price"] + "' data-target='" + optDta[$indx]["Target_Price"] + "'><label>"
                else
                    $dd += "<li class='active-result' data-val='" + optDta[$indx][$dtaVF] + "'><label>"




                if ($(this).hasClass('multi')) {
                    $dd += "<input type='checkbox' class='ckBx' autocomplete='off' tabindex='-1'>";
                }
                $dd += optDta[$indx][$dtaTF];
                $dd += "</label></li>";
            }
            $dd += "<li class='noRec " + ((optDta.length > 0) ? '' : 'active') + "'><i>No Record Found</i><b></b></li>";
            $dd += "</ul>";
        }
        $dd += "</div>";
        $(this).html($dd);
        $(".disp-selopt", this).on('click', function () {
            $dtaSrc = $(this).parent().data('src');
            $dVal = $(this).parent().attr('data-value');
            if ($(this).parent().find(".ddl-Drop")) {
                $drp = $(this).parent().find(".ddl-Drop");

                $(".disp-selopt").removeClass('active');
                if ($drp.hasClass('active')) {
                    $drp.removeClass('active');
                } else {
                    $drp.find("li").removeClass("result-selected");
                    $drp.find("li[data-val='" + $dVal + "']").addClass('result-selected');
                    $(DDL_SEL_).removeClass("active");
                    $drp
                        .css('top', ($(this).closest('.ddl-Box').position().top + $(this).closest('.ddl-Box').height()))
                        .css('left', (($(this).closest('.ddl-Box').position().left - $('#' + $dtaSrc).width())))
                        .removeClass('active');
                    if ($(this).closest('.ddl-Box').attr('data-inwidth') == undefined) {
                        $drp
                            .css('min-width', $(this).closest('.ddl-Box').width())
                            .css('max-width', $(this).closest('.ddl-Box').width())
                            .css('width', $(this).closest('.ddl-Box').width())
                    }
                    $drp.addClass('active');
                    if ($(this).parent().hasClass('search')) {
                        $(DDL_SEL_Srch_).val('');
                        searchItem($(DDL_SEL_Srch_));
                    }
                    $(DDL_SEL_List_).removeClass("highlighted");
                    $(DDL_SEL_List_ + '.result-selected').addClass('highlighted');
                    $(this).addClass('active');
                    selItem = $(DDL_SEL_List_ + '.highlighted');
                    if (selItem.length < 1) selItem = $(DDL_SEL_List_ + ':first');
                    scrollUL(selItem);
                }
            }
            if ($(this).parent().hasClass('wind')) {
                ddl_wind = $('#' + $dtaSrc);
                $(this).removeClass('active');
                if (ddl_wind.hasClass('active')) {
                    ddl_wind.removeClass('active');
                    $(this).removeClass('wactive');
                }
                else {
                    ddlWinClose();
                    ddlWinOpen($(this).parent(), $(this), ddl_wind);
                }
            }
        });
    });

    searchItems = function (e, schBx) {
        if (!e) e = event; var $key = (e.keyCode ? e.keyCode : e.which);
        if (!($key == 40 || $key == 39 || $key == 38 || $key == 37)) {
            searchItem(schBx)
        }
    }
    searchItem = function (schBx) {
        scTx = $(schBx).val() || "";
        Lis = $(DDL_SEL_Pad_).find("li").each(function () {
            if (!$(this).hasClass('noRec')) {
                $(this).removeClass('active-result');
                $(this).removeClass('highlighted');
                if ($(this).find('label').text().toLowerCase().indexOf(scTx.toLowerCase()) > -1) $(this).addClass('active-result');
            }
        });
        NREC = $(DDL_SEL_Pad_ + ' > .noRec');
        NREC.removeClass('highlighted');
        NREC.removeClass('active');
        scTx = (scTx != "") ? '"' + scTx + '"' : "";
        if ($(DDL_SEL_List_).length < 1) NREC.addClass('active').find('b').html(scTx);
    }
}
ddlWinClose = function () {
    $('.disp-selopt').removeClass('wactive');
    $('.wind-o').removeClass('active');
}
ddlWinOpen = function (ddl, $x, ddlw) {
    $('.wind-o').removeClass('active');
    ddlw.addClass('active');
    $x.addClass('wactive');
    $(ddlw).find(".TBH").each(function () {
        tb = $(this);
        $(tb).find('TH').each(function () {
            $w = ($(this).attr("w"));
            $(this).css('min-width', $w + 'px');
            td = ddlw.find(".TBD").find("TD:nth-child(" + ($(this).index() + 1) + ")");
            td.css({
                'min-width': (parseFloat($w) + 9) + 'px',
                'max-width': (parseFloat($w) + 9) + 'px',
                'width': (parseFloat($w) + 9) + 'px'
            });

        });
    });
    //if (Setup.DRxQty == 0 || $('.wTab.active').attr('id') != 'idWD') {
    //    $(ddlw).find('.TPVH').css('display', 'none');
    //    $(ddlw).find('.tPV').parent().css('display', 'none');
    //}
    //if (Setup.DRxFed == 0 || $('.wTab.active').attr('id') != 'idWD') {
    //    $(ddlw).find('.PRxH').css('display', 'none');
    //    $(ddlw).find('.PRx').parent().css('display', 'none');
    //}
    setWinVal(ddl, ddlw);
    ddlw
        .css('top', (ddl.position().top + ddl.height()) + 10)
        .css('left', ((ddl.position().left - ddlw.width()) + ddl.width()) - 10)
}
selectItem = function (item) {
    if ($(item).find('.ckBx').length < 1) {
        $('.disp-selopt.active').find('span').text($(item).find('label').text());
        $('.disp-selopt.active').closest('.ddl-Box').attr('data-value', $(item).data('val'));
        $('.disp-selopt.active').closest('.ddl-Box').attr('data-text', $(item).find('label').text());

        $('.disp-selopt.active').closest('.ddl-Box').attr('data-rate', $(item).data('rate'));//Newly Added
        $('.disp-selopt.active').closest('.ddl-Box').attr('data-pack', $(item).data('pack'));

        $(item).closest('tr').find('.Pack_Inp').text($(item).data('pack'));
        $(item).closest('tr').find('.Rate_Inp').text($(item).data('rate'));


        $(item).closest('tr').find('.tBV').val($(item).data('rate') * $(item).closest('tr').find('.tBQ').val());//Newly Added --Need to change




        $(DDL_SEL_List_)
            .removeClass("result-selected")
            .removeClass("highlighted");
        $(item).addClass('result-selected');
        $(DDL_SEL_).removeClass("active");
        $(".disp-selopt").removeClass('active');
    }
    else {
        //console.log($(item).find('label').text());
    }
}
function scrollUL(li) {
    var ul = li.parent();
    var fudge = 4;
    var lbottom = ul.height();
    var ltop = $(ul).find('.active-result').length;
    if (li.position() == undefined) return;
    fudge = $(ul).find('.active-result').index(li);
    if (li.position().top <= 0) {
        ul.animate({
            scrollTop: (li.outerHeight() * $(ul).find('.active-result').index(li))
        }, 'fast')
    } else if (li.position().top >= lbottom) {
        ul.animate({ scrollTop: ((li.outerHeight() * $(ul).find('.active-result').index(li)) - ul.height()) + li.outerHeight() }, 'fast')
    }
};
getData = function (ky) { return JSON.parse(_lcStore.getItem(ky)) || []; }
setData = function (ky, Data) { _lcStore.setItem(ky, JSON.stringify(Data)); }
delData = function (ky) { _lcStore.removeItem(ky) }





resize = function (c) {
    var h = $(window).innerHeight() - $('.pad.HDBg').outerHeight();
    var w = $(window).innerWidth() - (($('.aside').hasClass('active')) ? $('.aside').width() : 0);
    $('.aside').css('height', h);
    $('.sidebar').css('height', h - 10);
    $('.Work-Area').css('height', h);
    $('.Work-Area').css('width', w);
    //$('#working-Area').css('height', (h - $('#planer').height()) - 10);


    var t = $(window).height();
    var headerH = $('#planer').outerHeight(true); // includes padding/margin
    $('#working-Area').css('height', (t - headerH - 10) + 'px');

    $('#working-Area').css('width', w - 10);
    setDataHeight();
    // $('.note').css('left', (w - 10) - ($('.note').width()*2));
    //if (c == true) {
    //    if ($(window).width() <= 800) hideMenu();
    //    //else if (!$('.aside').hasClass('active')) showMenu();
    //}
}



setDataHeight = function () {
    $("div[id*='dWD_']").each(function () {
        $w = $(this).closest('.wTab');
        $(this).css('height', $w.outerHeight() - ($(this).position().top - 60));
    })
}
showMenu = function () {
    $('.aside').addClass('active');
    $('.Work-Area').addClass('active'); //.css('margin-left', '220px');
    $('.submit-bt1').css('display', 'none');
    resize(false);
}

hideMenu = function () {
    $('.aside').removeClass('active');
    $('.Work-Area').removeClass('active'); //.css('margin-left', '0px');
    $('.submit-bt1').css('display', 'block');
    resize(false);
}
toggleMenu = function () { if ($('.aside').hasClass('active')) hideMenu(); else showMenu(); }



navTag = function (mnu) {
    aTab = $(mnu).data("tag");
    $('#mnuTab > li.select').removeClass('select');
    $(mnu).addClass('select');
    pTab = $('.wTab.active').attr('id');

    DCR.curMnu = $(mnu);
    $('.wTab').removeClass('active');
    $('#idW' + aTab).addClass('active');
    if (pTab != undefined) {
        pTab = pTab.replace(/idW/g, '');
        DCR.clrEntry(pTab);
    }
    count = 1;
    if (aTab == 'P') {
        //DCR.genPrev();
        hideAlert();
        DCR.genEditRpt();
    } else {
        //showAlert('Select the Territory and Press Go.');
        DCR.genGrid(aTab);
    }
    setDataHeight();
}
showAlert = function (msg) {
    $('.alert-box').html(msg);
    $('.alert-box').css('display', 'block');
}
hideAlert = function () {
    $('.alert-box').css('display', 'none');
}
setWinVal = function (opnr, win) {
    lv = ''; tx = '';
    tb = $(win).find(".TBD");
    vals = opnr.attr('data-value').replace(/[$]/g, '~');
    txts = opnr.attr('data-text').replace(/ [(] /g, '~').replace(/ [)]/g, '').replace(/, /g, ',');
    delAllRw(tb);
    tid = tb.attr('id');
    if (vals != '') {
        spvs = vals.split('#'); spts = txts.split(',');
        for (il = 0; il < spvs.length - 1; il++) {
            spv = spvs[il].split('~'); spt = spts[il].split('~');
            if (il > 0) addRow(tid);

            r = $(tb).find('tr')[il];
            if (tid == "tProd") {
                ddl = $(r).find('.Prod');
                //$(ddl).setItem(spv[0], spt[0]);

                $(ddl).setItem_Prod(spv[0], spt[0], spv[3], spv[4]);
             


                $(r).find('.tBQ').val(spv[1]);
                $(r).find('.tBV').val(spv[2]);
                $(r).find('.Pack_Inp').text(spv[3]);
                $(r).find('.Rate_Inp').text(spv[4]);



                //ddl = $(r).find('.PRx');
                //$(ddl).setItem(spv[3], spt[3]);
                //$(r).find('.tBV').val(spv[2]);
            }
            else {
                ddl = $(r).find('.ddl-Box');
                $(ddl).setItem(spv[0], spt[0]);
                $(r).find('.tGQ').val(spv[1]);

            }
        }
    }
    else {
        var rwCnt = 1;
        //if (tid == "tProd") rwCnt = Setup.ProdMand;
        for (il = 1; il < rwCnt; il++) addRow(tid);
    }
}
svWinVal = function (e) {
    if (!e) e = event; var $se = (e.target ? e.target : e.srcElement);
    vld = true;
    lv = ''; tx = '';
    var lvTotal_Val = 0;
    win = $($se).closest(".wind-o");
    tb = $(win).find(".TBD");
    if (tb.attr("id") == "tProd") {
        $("#tProd").find(".Prod").each(function () {
            $r = $(this).closest('TR');
            if ($(this).attr('data-value') == "") {
                alertBox.Show('Select the Product', $r.find(".disp-selopt")); vld = false;
                return false;
            }
            //if (Setup.PQtyZro == 1 && $r.find('.tPQ').val() == "") $r.find('.tPQ').val('0');


            if ($r.find('.tBQ').val() == "") {
                alertBox.Show('Enter the Business Qty', $r.find(".tBQ")); vld = false;
                return false;
            }
            if ($r.find('.tBQ').val() == "0") {
                alertBox.Show('Business Qty Should not be 0.', $r.find(".tBQ")); vld = false;
                return false;
            }
            if (('#' + lv).indexOf('#' + $(this).attr('data-value') + '~') > -1) {
                alertBox.Show('This Product Already Selected...', $r.find(".disp-selopt")); vld = false;
                return false;
            }

            //lv += $(this).attr('data-value') + '~' + $r.find('.tBQ').val() + '$' + $r.find('.tBV').val() + "#";
            lv += $(this).attr('data-value') + '~' + $r.find('.tBQ').val() + '$' + $r.find('.tBV').val() + '$' + $r.find('.Pack_Inp').text() + '$' + $r.find('.Rate_Inp').text() + "#";
            tx += $(this).attr('data-text');
            tx += ', ';
         
            lvTotal_Val += parseFloat($r.find('.tBV').val()) || 0;


        })
    }


    if (vld == true) {
        dl = $('.disp-selopt.wactive').closest('.ddl-Box');
        $('.disp-selopt.wactive').find('span').text((tx != '') ? tx : $(dl).attr('data-dft'));
        $(dl).attr('data-value', lv);
        $(dl).attr('data-text', tx);
        $(".wind-o.active").removeClass('active');
        $(".disp-selopt.wactive").removeClass('wactive');


        $('#hTtl_Value').val(lvTotal_Val.toFixed(2));
    }
}
addRow = function (t) {
    t = $('#' + t);
    r = t.find('TR').last().clone(true);
    t.append(r);
    clearRow(r);

    //row.find('input, select, textarea').first().focus();

    //let container = table.closest(".wScroll");
    //container.scrollTop(container.prop("scrollHeight"));

}
clearRow = function (r) {
    $(r).find('.ddl-Box').each(function () {
        txt = $(this).data('dft');
        $(this).find('span').text(txt);
        $(this).attr('data-value', $(this).data('dfv'));
        $(this).attr('data-text', txt);
    });
    $(r).find('input').val('');
    $(r).find('.lbl').text('');

}
delRW = function (rw) {
    r = $(rw);
    if (r.parent().children().length < 2)
        clearRow(r);
    else
        r.remove();
    __Click = false;
}
delRow = function (o) {
    r = $(o).closest('tr'); delRW(r);
}
delAllRw = function (tb) {
    rws = $(tb).find('tr');
    for (i = rws.length - 1; i >= 0; i--)
        delRW(rws[i]);
}

initialize = function () {

    Setup = getData('Setup');
    SFDet = getData('SFDet');
    DCRDatas = getData('DCRMain');
    //_st = JSON.parse(Setup.SDCRE);
    //_st_eD = JSON.parse(Setup.SDCRV);
    for (wky in __Wndw) {
        for (key in _C) {
            if (__Wndw[wky].fields != undefined) {
                //stv = (_st[key] == undefined || _st[key][wky] == undefined) ? 1 : _st[key][wky];
                //if (stv == 1)

                __Wndw[wky].fields[key] = JSON.parse(JSON.stringify(_C[key]));
            }
            if (__Wndw[wky].grd_Cols != undefined) {
                //stg = (_st_eD[key] == undefined || _st_eD[key][wky] == undefined) ? 1 : _st_eD[key][wky];
                //if (stv == 1 && stg == 1)

                __Wndw[wky].grd_Cols[key] = JSON.parse(JSON.stringify(_C[key]));
            }
            if (__Wndw[wky].prv_Cols != undefined) {
                //stp = (_st_eP[key] == undefined || _st_eP[key][wky] == undefined) ? 1 : _st_eP[key][wky];
                //if (stv == 1 && stp == 1)

                __Wndw[wky].prv_Cols[key] = JSON.parse(JSON.stringify(_C[key]));
            }
        }
        //addAccs = 0
        //if (wky == "U") addAccs = Setup.NUdr
        //if ((",C,H,").indexOf(wky) > -1) addAccs = Setup.NChm
        //__Wndw[wky].nw = addAccs;
    }








    //__Wndw.R.RwCtrl.rem.mxl = Setup.RemLen;

    var _d = DCR;
    _d.WrkWin.create(__Wndw);

    //$('.tmr').timeEntry({ unlimitedHours: true, showSeconds: true, spinnerImage: 'ui/img/spinnerOrange.png' }); /*,spinnerImage: ''*/
    //$('.timeEntry-control').css('margin-left', '-24px');
    //$('.timeEntry-control').css('margin-top', '-2px');
    //$('.timeEntry-control').css('position', 'relative');
    //if ($('#ddl_WorkType').val() == '') showAlert('Select the Worktype');
}
//ChangeHQ = function () {
//    if (Setup.TPBaseDCRDr != 1) {
//        $('#ddl_SDP').val('');
//    }
//    DCRDatas = getData('DCRMain');
//    DCRDatas.selHQ = $("#ddl_HQ").val();
//    setData('DCRMain', DCRDatas);
//    BuildMenu(1);
//}
//ChangeWT = function () {
//    sFWFl = $('#ddl_WorkType').find('option:selected').attr('data-fwflg');
//    if (Setup.TPBaseDCRDr != 1)
//        DCRDatas = getData('DCRMain');

//    $('.FSubmit').removeAttr('disabled');
//    if (sFWFl == 'L') {
//        $(".FSubmit").attr('disabled', 'disabled');
//        DCRDatas.Wtyp = '';
//        sfty = $('#hSFTyp').val()
//        if (sfty != 1)
//            document.location.href = '../MasterFiles/MGR/Leave_Form_Mgr.aspx?LeaveFrom=' + $("#hDCRDt").val();
//        else
//            document.location.href = '../MasterFiles/MR/LeaveForm.aspx?LeaveFrom=' + $("#hDCRDt").val();
//        return false;
//    } else
//        DCRDatas.Wtyp = $("#ddl_WorkType").val();
//    setData('DCRMain', DCRDatas);

//    BuildMenu(0);

//}
BuildMenu = function (flg) {
    hideAlert();
    if (flg == 0) {
        $("#mnuTab").html('');
        $('.wTab').addClass('active');
    }
    sFWFl = $('#ddl_WorkType').find('option:selected').attr('data-fwflg');

    sfty = $('#hSFTyp').val();
    //$('#ddl_HQ').removeAttr('disabled');

    //$(".plnPlholder").css('display', 'none');

    //if (sFWFl == 'H' || sFWFl == 'W' || sFWFl == 'L' || sFWFl == '' || sFWFl == undefined) {//Newly Added --2025
    //    $(".plnPlholder").css('display', 'none');
    //}
    //else {
    //    $(".plnPlholder").css('display', 'inline-block');
    //}

    $(".plnPlholder").css('display', 'inline-block');



    //if (sFWFl != 'F' && sFWFl != 'N') {  //Newly Added --2025
    //    hSF = (sfty != 1) ? '' : $('#hSF_Code').val();
    //    $('#ddl_HQ').val(hSF);
    //    $('#ddl_HQ').attr('disabled', 'disabled');
    //} else
    //    $(".plnPlholder").css('display', 'inline-block');





    //sHQ = $('#ddl_HQ').val();
    //SDP = $('#ddl_SDP').val();
    //if (SDP == null) SDP = '';

    //if (sfty != 1 && (sFWFl == 'F' || sFWFl == 'N') && sHQ == '') { //Newly Added --2025
    //    showAlert('Select the Headquarter Name');
    //}
    //if (sfty != 1 && (sFWFl == 'F' || sFWFl == 'N') && sHQ != '') { //Newly Added --2025
    //    sf = $('#ddl_HQ').val();
    //    $('#ddl_SDP').empty();
    //    $('#ddl_SDP').append('<option value="">Loading...</option>');
    //    loadClusters(sf);
    //    //if (SDP == '') showAlert('Select the ' + Setup.SDPCap +' Name');
    //}

    //if (sFWFl != 'F' || (sFWFl == 'F' && sHQ != '')) { // && SDP != ''
    //if (flg == 0)


    DCR.Menu.create(__Menu);
    //if (sFWFl == 'F' && ((sfty == 1 && flg == 0) || sfty != 1 && flg == 1))



    getMasterList();
    $('.wTab').addClass('active');
    //}    
}
//loadClusters = function (sf) {
//    d_twns = getData("d_twn_" + sf);
//    if (d_twns.length < 1) {
//        PageMethods.GetClusterList(sf, function (data) {
//            d_twns = JSON.parse(data) || [];
//            setData("d_twn_" + sf, d_twns);

//            reloadTwns(sf)
//        }, function (e) {
//            $('#ddl_SDP').empty();
//        });
//    } else reloadTwns(sf);
//}
//reloadTwns = function (sf) {
//    d_twns = getData("d_twn_" + sf);
//    $('#ddl_SDP').empty();
//    $('#ddl_SDP').append('<option value="">-- Select the ' + Setup.SDPCap + ' Name --</option>');
//    for (var i = 0; i < d_twns.length; i++) {
//        $('#ddl_SDP').append('<option value="' + d_twns[i].id + '">' + d_twns[i].name + '</option>');
//    }
//    d_twns = getData("d_twn_" + sf);
//    $("#ddl_Ntwn").reload();
//}
SortByName = function (a, b) {
    var aName = a.name.toLowerCase();
    var bName = b.name.toLowerCase();
    return ((aName < bName) ? -1 : ((aName > bName) ? 1 : 0));
}

srtCusTwbs = function (x) {

    $(".ProcessMsg").css('display', 'block');
    $(".txtMsg").text('Loading.Please Wait...');

    //sst = $('#ddl_WorkType').find('option:selected').data('etabs');
    sst = 'D,';
    if (sst != undefined) {

        //mlti = Setup.MultiDr;
        mlti = 0;
        t = (sst + ',').split(',');
        for (i = 0; i < t.length - 1; i++) {
            $dSrc = $('#ddl_' + t[i] + 'cus').data('src');
            $dVal = $('#ddl_' + t[i] + 'cus').attr('data-value');
            vl = $(x).val();
            opts = eval($dSrc);

            topts = opts;
            fDa = $.grep(opts, function (a) { return ((',' + a.TCd + ',').indexOf(',' + vl + ',') > -1); });
            Da = $.grep(opts, function (a) { return ((',' + a.TCd + ',').indexOf(',' + vl + ',') < 0); });
            //if (Setup.ShowPatchOnly == 1 && t[i] == "D") Da = [];
            opts = $.merge(fDa.sort(SortByName), Da.sort(SortByName));
            if (t[i] == "D") d_drs = opts;

            reloadDatas(t[i]);

            if (t[i] == "D") d_drs = topts;

            sFndLst = "li[data-tcd" + ((mlti == 1) ? "*" : "") + "='" + vl + ((mlti == 1) ? "," : "") + "']";
            $('#ddl_' + t[i] + 'cus').find("li").hide();
            if (vl == "0") {
                $('#ddl_' + t[i] + 'cus').find("li").show();
                $('#ddl_' + t[i] + 'cus').find("li").removeClass('result-selected');
            }
            else {
                $('#ddl_' + t[i] + 'cus').find(sFndLst).addClass('Filted');
                $('#ddl_' + t[i] + 'cus').find(sFndLst).show();
                //$('#ddl_' + t[i] + 'cus').find(sFndLst).css('display', 'inline-block');
                $('#ddl_' + t[i] + 'cus').find("li").removeClass('result-selected');
                if ($dVal != "") {
                    $('#ddl_' + t[i] + 'cus').find("li[data-val='" + $dVal + "']").addClass('result-selected');
                }
            }
            $(".ProcessMsg").css('display', 'none');
        }
    }

    $(".ProcessMsg").css('display', 'none');
}
//srtCusTwbs = function (x) {
//    //sst = $('#ddl_WorkType').find('option:selected').data('etabs');
//    sst = 'D';
//    mlti = Setup.MultiDr;
//    t = (sst + ',').split(',');
//    for (i = 0; i < t.length - 1; i++) {
//        $dSrc = $('#ddl_' + t[i] + 'cus').data('src');
//        $dVal = $('#ddl_' + t[i] + 'cus').attr('data-value');
//        vl = $(x).val();
//        opts = eval($dSrc);

//        topts = opts;
//        fDa = $.grep(opts, function (a) { return ((',' + a.TCd + ',').indexOf(',' + vl + ',') > -1); });
//        Da = $.grep(opts, function (a) { return ((',' + a.TCd + ',').indexOf(',' + vl + ',') < 0); });
//        if (Setup.ShowPatchOnly == 1 && t[i] == "D") Da = [];
//        opts = $.merge(fDa.sort(SortByName), Da.sort(SortByName));
//        if (t[i] == "D") d_drs = opts;
//        if (t[i] == "C") d_chm = opts;
//        if (t[i] == "S") d_stk = opts;
//        if (t[i] == "U") d_udr = opts;
//        if (t[i] == "H") d_hos = opts;
//        reloadDatas(t[i]);

//        if (t[i] == "D") d_drs = topts;

//        sFndLst = "li[data-tcd" + ((mlti == 1) ? "*" : "") + "='" + vl + ((mlti == 1) ? "," : "") + "']";
//        $('#ddl_' + t[i] + 'cus').find(sFndLst).addClass('Filted');
//        $('#ddl_' + t[i] + 'cus').find("li").removeClass('result-selected');
//        if ($dVal != "") {
//            $('#ddl_' + t[i] + 'cus').find("li[data-val='" + $dVal + "']").addClass('result-selected');
//        }

//    }
//}
reloadDatas = function (ty) {
    if (ty == "J") {
        sst = $('#ddl_WorkType').find('option:selected').data('etabs');
        t = (sst + ',').split(',');
        for (i = 0; i < t.length - 1; i++) {
            ResetJW('#ddl_' + t[i] + 'jw');
        }
    }
    else if (ty == "MC") $('#ddl_NCat').reload();
    else if (ty == "MS") $('#ddl_NSpc').reload();
    else if (ty == "ML") $('#ddl_NCla').reload();
    else if (ty == "MQ") $('#ddl_NQua').reload();
    else {
        //if ($("#ddl_SDP").val() != "" && Setup.ShowPatchOnly == 1 && ty == "D") hideAlert();
        flag = 0;
        //if ($("#ddl_SDP").val() == "" && Setup.ShowPatchOnly == 1 && ty == "D") {
        //    showAlert('Select the ' + Setup.SDPCap + ' Name');
        //    flag = 1;
        //}
        $('#ddl_' + ty + 'cus').reload(flag);
    }
}
ResetJW = function (Jw) {

    $(Jw).reload();
    $(Jw).setItem(SFDet.SFCode, "SELF,");
    $(Jw).find("li[data-val='" + SFDet.SFCode + "']").find(".ckBx").prop('checked', true);
    $(Jw).find("li[data-val='" + SFDet.SFCode + "']").find(".ckBx").css('visibility', 'hidden');
    $(Jw).find("li[data-val='" + SFDet.SFCode + "']").addClass('disabled');
}
clearMasterData = function () {
    keyAry = ['d_drs_', 'd_chm_', 'd_stk_', 'd_udr_', 'd_hos_', 'd_JW_', 'd_twn_', 'd_Cla_', 'd_Qua_', 'd_Spec_', 'd_cat_'];
    for (j = 0; j < keyAry.length; j++) {
        for (i = localStorage.length - 1; i > -1; i--) {
            key = localStorage.key(i);
            if (key.indexOf(keyAry[j]) > -1) delData(key);
        }
    }
}
//setExistData = function (sData) {
//    Entry = getData("Entry_" + $("#hSF_Code").val() + '_' + $("#hDCRDt").val().replace(/\//g, '_'));
//    // && $('#hDCRDt').val() == DCRDatas.EDate
//    if (Entry.UModi == undefined) { Entry = {}; Entry.UModi = 0; }
//    if (Entry.UModi != 1) {
//        datEx = JSON.parse(sData);
//        $("#txa_RRmks").val(datEx.Head.Rem);
//        $("#ddl_WorkType").val(datEx.Head.Wtyp);
//        DCRDatas = datEx.Head;

//        sFWFl = $('#ddl_WorkType').find('option:selected').attr('data-fwflg');
//        if (sFWFl == 'L') {
//            $("#ddl_WorkType").val('');
//            DCRDatas.Wtyp = '';
//        }
//        setData('DCRMain', DCRDatas);
//        for (ty in tabs) {
//            if (datEx[tabs[ty]] != undefined) {
//                setData("dta_" + datEx.Head.SFCode + "_" + datEx.Head.EDate.replace(/\//g, '_') + "_" + ty, datEx[tabs[ty]]);
//            }
//        }
//        ChangeWT();
//    }
//}

changeMasCode = function (nCus) {
    if (nCus.sf != undefined) {
        dta = getData('d_' + tabs[nCus.typ].toLowerCase() + '_' + nCus.sf);
        fdta = $.grep(dta, function (a) { return (a.id == nCus.oCd); });
        fidx = dta.indexOf(fdta[0]);
        dta[fidx].id = nCus.nCd;
        setData('d_' + tabs[nCus.typ].toLowerCase() + '_' + nCus.sf, dta);
    }
}
$.fn.filtrNCus = function (typ, sf) {
    narr = [];
    for (il = 0; il < $(this).length; il++) {
        a = $(this)[il];
        cs = {};
        cs.id = a.id;
        cs.name = a.name;
        cs.TCd = a.twn.val;
        cs.TNm = a.twn.txt;
        cs.sf = a.sf;
        if (a.typ == typ && a.sf == sf) narr.push(cs);
    }
    return narr;
}
getMasterList = function () {

    $(".ProcessMsg").css('display', 'inline');
    $(".txtMsg").text('Loading.Please Wait...');
    sfty = $('#hSFTyp').val();
    sf = $('#ddl_HQ').val();
    Esf = $('#hSF_Code').val();
    oDiv = $('#ddl_HQ').find('option:selected').data('sfdiv');
    if (sfty == 1) { sf = Esf; oDiv = $('#hDiv').val(); }
    if (sf != '') {
        nlst = getData('nCus_' + Esf + '_' + DCR.EDate.replace(/\//g, '_'));
        d_drs = getData('d_drs_' + sf);
        if (d_drs.length < 1) {
            xlD = $("#ddl_Dcus").find(".disp-selopt").find("span");
            var xlDtx = xlD.text(); xlD.html('<b style="color:red">Loading...</b>');
            PageMethods.GetDoctorList(sf, function (data) {
                d_drs = JSON.parse(data) || [];
                d_drs = $.merge(d_drs, $(nlst).filtrNCus('D', sf));
                setData('d_drs_' + sf, d_drs);
                xlD.text(xlDtx);
                reloadDatas('D');
                srtCusTwbs($("#ddl_SDP"));
                //$(".ProcessMsg").css('display', 'none');
            }, function () {
                xlD.text(xlDtx);
            });
        }
        else reloadDatas('D');
        //d_chm = getData('d_chm_' + sf);
        //if (d_chm.length < 1) { xlC = $("#ddl_Ccus").find(".disp-selopt").find("span"); var xlCtx = xlC.text(); xlC.html('<b style="color:red">Loading...</b>'); PageMethods.GetChemistList(sf, function (data) { d_chm = JSON.parse(data) || []; d_chm = $.merge(d_chm, $(nlst).filtrNCus('C', sf)); setData('d_chm_' + sf, d_chm); xlC.text(xlCtx); reloadDatas('C'); }, function () { xlC.text(xlCtx); }); } else reloadDatas('C');
        //d_stk = getData('d_stk_' + sf);
        //if (d_stk.length < 1) { xlS = $("#ddl_Scus").find(".disp-selopt").find("span"); var xlStx = xlS.text(); xlS.html('<b style="color:red">Loading...</b>'); PageMethods.GetStockistList(sf, function (data) { d_stk = JSON.parse(data) || []; d_stk = $.merge(d_stk, $(nlst).filtrNCus('S', sf)); setData('d_stk_' + sf, d_stk); xlS.text(xlStx); reloadDatas('S'); }, function () { xlS.text(xlStx); }); } else reloadDatas('S');
        //d_udr = getData('d_udr_' + sf);
        //if (d_udr.length < 1) { xlU = $("#ddl_Ucus").find(".disp-selopt").find("span"); var xlUtx = xlU.text(); xlU.html('<b style="color:red">Loading...</b>'); PageMethods.GetUnlistedDrList(sf, function (data) { d_udr = JSON.parse(data) || []; d_udr = $.merge(d_udr, $(nlst).filtrNCus('U', sf)); setData('d_udr_' + sf, d_udr); xlU.text(xlUtx); reloadDatas('U'); }, function () { xlU.text(xlUtx); }); } else reloadDatas('U');
        //d_hos = getData('d_hos_' + sf);
        //if (d_hos.length < 1) { xlH = $("#ddl_Hcus").find(".disp-selopt").find("span"); var xlHtx = xlH.text(); xlH.html('<b style="color:red">Loading...</b>'); PageMethods.GetHospitalList(sf, function (data) { d_hos = JSON.parse(data) || []; d_hos = $.merge(d_hos, $(nlst).filtrNCus('H', sf)); setData('d_hos_' + sf, d_hos); xlH.text(xlHtx); reloadDatas('H'); }, function () { xlH.text(xlHtx); }); } else reloadDatas('H');
        //d_JW = getData('d_JW_' + sf);
        //if (d_JW.length < 1) { xlJW = $("[id$=jw]").find(".disp-selopt").find("span"); var xlJWtx = xlJW.text(); xlJW.html('<b style="color:red">Loading...</b>'); PageMethods.GetJntWrkList(sf, Esf, function (data) { d_JW = $.merge([{ "id": SFDet.SFCode, "name": "SELF" }], (JSON.parse(data) || [])); setData('d_JW_' + sf, d_JW); xlJW.text(xlJWtx); reloadDatas('J'); }, function () { xlJW.text(xlJWtx); }); } else reloadDatas('J');

        //d_Cat = getData('d_Cat_' + sf);
        //if (d_Cat.length < 1) PageMethods.GetCateList(oDiv, function (data) { d_Cat = JSON.parse(data) || []; setData('d_cat_' + sf, d_Cat); reloadDatas('MC'); }); else reloadDatas('MC');
        //d_Spec = getData('d_Spec_' + sf);
        //if (d_Spec.length < 1) PageMethods.GetSpecList(oDiv, function (data) { d_Spec = JSON.parse(data) || []; setData('d_Spec_' + sf, d_Spec); reloadDatas('MS'); }); else reloadDatas('MS');
        //d_Cla = getData('d_Cla_' + sf);
        //if (d_Cla.length < 1) PageMethods.GetClaList(oDiv, function (data) { d_Cla = JSON.parse(data) || []; setData('d_Cla_' + sf, d_Cla); reloadDatas('ML'); }); else reloadDatas('ML');
        //d_Qua = getData('d_Qua_' + sf);
        //if (d_Qua.length < 1) PageMethods.GetQualList(oDiv, function (data) { d_Qua = JSON.parse(data) || []; setData('d_Qua_' + sf, d_Qua); reloadDatas('MQ'); }); else reloadDatas('MQ');
    }
}

$(document).on('input', '.tBQ', function () {
    let qty = parseFloat($(this).val()) || 0;
    let rate = parseFloat($(this).closest('tr').find('.ddl-Box').attr('data-rate')) || 0;
    let value = qty * rate;

    //$(this).closest('tr').find('.tBV').val(value);
    $(this).closest('tr').find('.tBV').val(value.toFixed(2));
});


ClearAllData = function () {
    for (i = localStorage.length - 1; i > -1; i--) { //New21
        key = localStorage.key(i);

        if (key.indexOf('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_') > -1) delData(key);
    }
    //DCR.genPrev();
    //$("#txa_RRmks").val('');
}

function getBusiness_Details_TerritoryWise(Terri_code) {
    PageMethods.GetBusinessEntry_TerritoryWise(SFDet.SFCode, hndMonth, hndYear, Terri_code, function (data) {

        console.log('fetchDCRCalls-start-sucess');
        DataResult = JSON.parse(data);
        setJointData(data);
        //navTag($("[data-tag=R]"));
        //navTag($("[data-tag=D]"));
        //resolve();
    }, function (error) {
        showAlert(error.get_message());
        //reject(error);
    });
}

setJointData = function (sData) {
    Entry = getData("Entry_" + SFDet.SFCode + '_' + hndMonth + '_' + hndYear);
    // && $('#hDCRDt').val() == DCRDatas.EDate
    if (Entry.UModi == undefined) { Entry = {}; Entry.UModi = 0; }
    //if (Entry.UModi != 1) {
    //alert(sData);
    //alert(sData.length);

    if (sData.length > 2) {
        //if (sData != undefined) {
        datEx = JSON.parse(sData);



        //alert(JSON.stringify(datEx));





        DCRDatas = datEx.Head;
        //Route_SlNo = datEx.Head.Route_SLNO;

        //if (Route_SlNo == '0')
        //    $('#ddl_WorkType').removeAttr('disabled');



        setData('DCRMain', DCRDatas);


        for (ty in tabs) {
            if (datEx[tabs[ty]] != undefined) {
                setData("dta_" + datEx.Head.SFCode + '_' + datEx.Head.Month + '_' + datEx.Head.Year + "_" + ty, datEx[tabs[ty]]);





                var dta = datEx[tabs[ty]];//Feb28

            }




            DCR.clrEntry(ty);
            DCR.genGrid(ty);
        }



    }




    //else {
    //    showAlert("No Joint Calls Found.");
    //}
}

