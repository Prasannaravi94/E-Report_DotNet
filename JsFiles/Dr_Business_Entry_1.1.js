__w = window; lckMsg = ''; hndMonth = '', hndYear = ''; var count = 1;
DDL_SEL_ = ".ddl-Drop.active";
DDL_SEL_Srch_ = DDL_SEL_ + " > .ddl-search  input";
DDL_SEL_Pad_ = DDL_SEL_ + " > .ddl-results";
DDL_SEL_List_ = DDL_SEL_Pad_ + "> .active-result";
tabs = { 'D': 'Msl' };

_lcStore = __w.localStorage;
SFDet = {};
Setup = {};
Entry = {};
_st = {};
_st_eD = {};
_st_eP = {};
__Wndw = {
    D: { fields: {}, grd_Cols: {}, prv_Cols: {}, nw: 0 },
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
        $("body").removeClass('loading');
        
    });


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

        if (this.validSel('ddl_' + typ + 'cus') == false) return false;
        if ($(data).ObjIndexOf('cus.val', $o.attr('data-value')) > -1 && this.edtCus != $o.attr('data-value')) {
            alertBox.Show("This " + $o.attr('data-dft').replace(/-/g, '') + " Already Selected...", $o.find('.disp-selopt'));
            return false
        }

        pvl = $("#ddl_Dprd").attr("data-value");
        if (typ == "D" && pvl == '') {
            ddlWinOpen($("#ddl_Dprd"), $("#ddl_Dprd").find('.disp-selopt'), $("#wProd"));
            return false;
        }
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
        DCRDta_F = {};

        DCRDta_F.Month = hndMonth;
        DCRDta_F.Year = hndYear;


        for (tab in tabs) {
            if (tab == typ) {
                DCRDta_F[tabs[tab]] = [cust];
            }
            else {
                DCRDta_F[tabs[tab]] = [];
            }
        }


        SFData = JSON.stringify(SFDet);
        asyncState = false;
        PageMethods.Save_Business_Entry_Single_Data(JSON.stringify(SFDet), JSON.stringify(DCRDta_F), function (data) {

            result = JSON.parse(data);

            if (result.success == true) {
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

        data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);


        if (data.length > 0) {
            cust = data[indx];
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
                data = getData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ);

                if (data.length > 0) {

                    var indx = data.findIndex(img => img.cus.val === Dr_Code);
                    Dta = data[indx];
                    data.splice(indx, 1);
                    setData('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_' + typ, data);

                    data = getData('nCus_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear);


                    nDta = $.grep(data, function (a) { return (a.id == Dta.cus.val) });
                    if (nDta.length > 0) {
                        data.splice(data.indexOf(nDta[0]), 1);
   
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


    genEditRpt: function () {

        $(".ProcessMsg").css('display', 'inline');
        $(".txtMsg").text('Loading.Please Wait...');

        Reset_ddl();

        //sgrd = "<div style='padding:50px;'><div class='txhead'>Listed Dr-Business ValueWise +'-'+$('#ddl_HQ').find('option:selected').text()</div><br>";
        var sgrd = "<div style='padding:50px;'>" +
            "<div class='txhead'>Listed Dr - Business ValueWise - " + $('#ddl_HQ').find('option:selected').text() + "</div><br>" +
            "</div>";

        $("#planer").css('display', 'none');
        $("#idWD").css('display', 'none');

        asyncState = false;
         PageMethods.Get_AllReport(SFDet.SFCode, hndMonth, hndYear, function (data) {
            //alert(data);
            EditRpt = JSON.parse(data);

            sgrd += "<table id='tblRoute' class='fg-group' style='width:100%;'>";
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

                sgrd += "</tr>";

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

            sgrd += "</table>";

            sgrd += "</div>";
            $('#idWP').html(sgrd);

            $(".ProcessMsg").css('display', 'none');
         

        }, function (error) {
            showAlert(error.get_message());
            $(".ProcessMsg").css('display', 'none');

        });
    },


   
    ShowCusDt: function () {

        $(".ProcessMsg").css('display', 'inline');
        $(".txtMsg").text('Loading.Please Wait...');
      

        if ($('#hSFTyp').val() == '1') {
            showAlert('Select the Territory and Press Go.');
        }
        else {
            showAlert('Select the Headquarter,Territory and Press Go.');
        }
        count = 1;

        $("#planer").css('display', 'block');
        $("#idWD").css('display', 'inline');
        //$("#idWP").css('display', 'inline');
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
        Reset_ddl();
        ClearAllData();
        //$('#ddl_HQ').val('');
        $('#ddl_HQ').val('').trigger('change');
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
    //if (sf == '' && sfty != 1) {
    if (sf == '') {
        alertBox.Show("select the Headquarter", $("#ddl_HQ"));
        return false;
    }

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

        $('.disp-selopt.active').closest('.ddl-Box').attr('data-rate', $(item).data('rate'));
        $('.disp-selopt.active').closest('.ddl-Box').attr('data-pack', $(item).data('pack'));

        $(item).closest('tr').find('.Pack_Inp').text($(item).data('pack'));
        $(item).closest('tr').find('.Rate_Inp').text($(item).data('rate'));


        $(item).closest('tr').find('.tBV').val($(item).data('rate') * $(item).closest('tr').find('.tBQ').val());




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


        //$('#hTtl_Value').val(lvTotal_Val);
        $('#hTtl_Value').val(lvTotal_Val.toFixed(2));
    }
}
addRow = function (t) {
    t = $('#' + t);
    r = t.find('TR').last().clone(true);
    t.append(r);
    clearRow(r);


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



    var _d = DCR;
    _d.WrkWin.create(__Wndw);


}
ChangeHQ = function () {

  
    hSF = (sfty != 1) ? '' : $('#hSF_Code').val();
    if (sfty == 1) {
        $('#ddl_HQ').val(hSF);
        $('#ddl_HQ').attr('disabled', 'disabled');
    }

 
    sf = $('#ddl_HQ').val();
    $('#ddl_SDP').empty();
    $('#ddl_SDP').append('<option value="">Loading...</option>');
    loadClusters(sf);


    //sst = $('#ddl_WorkType').find('option:selected').data('etabs');
    sst = 'D,';
    if (sst != undefined) {
        t = (sst + ',').split(',');
        for (i = 0; i < t.length - 1; i++) {
            $('#ddl_' + t[i] + 'cus').find('li').remove();
        }
    }


    SFDet.SFCode = $('#ddl_HQ').val();
    ClearAllData();
    BuildMenu(1);
    
}

BuildMenu = function (flg) {
    hideAlert();
    if (flg == 0) {
        $("#mnuTab").html('');
        $('.wTab').addClass('active');
        $(".plnPlholder").css('display', 'inline-block');

    }
    sFWFl = $('#ddl_WorkType').find('option:selected').attr('data-fwflg');

    sfty = $('#hSFTyp').val();

    DCR.Menu.create(__Menu);

    getMasterList();
    $('.wTab').addClass('active');
       
}


loadClusters = function (sf) {
    d_twns = getData("d_twn_" + sf);
    if (d_twns.length < 1) {
        PageMethods.GetClusterList(sf, function (data) {
            d_twns = JSON.parse(data) || [];
            setData("d_twn_" + sf, d_twns);

            reloadTwns(sf)
        }, function (e) {
            $('#ddl_SDP').empty();
        });
    } else reloadTwns(sf);
}
reloadTwns = function (sf) {
    d_twns = getData("d_twn_" + sf);
    $('#ddl_SDP').empty();
    /* $('#ddl_SDP').append('<option value="">-- Select the ' + Setup.SDPCap + ' Name --</option>');*/
    $('#ddl_SDP').append('<option value="">-- Select the Territory Name --</option>');

    if (sf != '') { 


        for (var i = 0; i < d_twns.length; i++) {
            $('#ddl_SDP').append('<option value="' + d_twns[i].id + '">' + d_twns[i].name + '</option>');
        }
        d_twns = getData("d_twn_" + sf);
    }

}



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
            //$('#ddl_' + t[i] + 'cus').find("li").css('display', 'none');

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

        flag = 0;
 
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
    //if (sfty == 1) {
    //    sf = Esf;
    //    oDiv = $('#hDiv').val();
    //}
    if (sf != '' && sf != undefined) {
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
              
            }, function () {
                xlD.text(xlDtx);
            });
        }
        else {
            $(".ProcessMsg").css('display', 'none');
            reloadDatas('D');
        }
       
    }
    else {
        $(".ProcessMsg").css('display', 'none');
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
    for (i = localStorage.length - 1; i > -1; i--) {
        key = localStorage.key(i);

        if (key.indexOf('dta_' + SFDet.SFCode + '_' + hndMonth + '_' + hndYear + '_') > -1) delData(key);
    }
    
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


function Reset_ddl() {


    sst = 'D,';
    if (sst != undefined) {
        t = (sst + ',').split(',');
        for (i = 0; i < t.length - 1; i++) {
            $('#ddl_' + t[i] + 'cus').addClass("ddl-Box");
        }
    }
}



setJointData = function (sData) {
    Entry = getData("Entry_" + SFDet.SFCode + '_' + hndMonth + '_' + hndYear);

    if (Entry.UModi == undefined) { Entry = {}; Entry.UModi = 0; }


    if (sData.length > 2) {
        datEx = JSON.parse(sData);

        DCRDatas = datEx.Head;


        setData('DCRMain', DCRDatas);


        for (ty in tabs) {
            if (datEx[tabs[ty]] != undefined) {
                setData("dta_" + datEx.Head.SFCode + '_' + datEx.Head.Month + '_' + datEx.Head.Year + "_" + ty, datEx[tabs[ty]]);

                var dta = datEx[tabs[ty]];

            }




            DCR.clrEntry(ty);
            DCR.genGrid(ty);
        }



    }

}

