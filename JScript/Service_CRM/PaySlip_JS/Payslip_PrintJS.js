
        var NeNCtrl = false;
function cnvtExc($v) {
    if ($v == '_E') {
        lc = window.location.toString();
        var s = ('/' + window.location.pathname).split('/');
        var fn = (s[s.length - 1].substring(0, s[s.length - 1].indexOf('.')));
        with (document.forms[0]) {
            action = lc + ((lc.indexOf('?') > -1) ? "&" : "?") + "Md=" + $v + "&Fln=" + fn; method = 'post'; submit();
        }
    }
    else if ($v == '_P') {
        print();
    }
    else if ($v == '_C') {
        window.close();
    }
    else if ($v == '_B') {
        window.location.href = '';
    }
}
function creCtrl() {
    ReqctlStrs = "hSF$$$TNRM01^^^slM$$$10^^^slY$$$2015^^^hMod$$$_v^^^";
    if (document.forms.length <= 0) {
        var ctlfrmEx = document.createElement("Form");
        document.body.appendChild(ctlfrmEx);
    }
    else {
        var ctlfrmEx = document.forms[0];
    }
    if (ReqctlStrs != '') {
        ReqctlStrs = ReqctlStrs.split('^^^');
        for ($intCtls = 0; $intCtls < ReqctlStrs.length - 1; $intCtls++) {
            ReqctlStrsSP = ReqctlStrs[$intCtls].split('$$$');
            if (ReqctlStrsSP[0].indexOf('image') < 0) {
                if (eval('ctlfrmEx.' + ReqctlStrsSP[0]) == null) {
                    var ctlhidEx = document.createElement("INPUT");
                    with (ctlhidEx) {
                        type = 'hidden';
                        id = ReqctlStrsSP[0];
                        name = ReqctlStrsSP[0];
                        value = ReqctlStrsSP[1];
                        ctlfrmEx.appendChild(ctlhidEx);
                    }
                }
            }
        }
    }
    var ctlPrnEx = document.createElement("div");
    with (ctlPrnEx.style) {
        position = "absolute";
        top = "0px";
        right = "0px";
        cursor = "hand";
    }
    var $sH = "<table class='noPrnCtrl' style='border-collapse:collapse;'><tr>"; if (NeNCtrl == false) $sH += "<td style='border:solid 1px #000000;background-Color:#666633;color:#FFFFFF;cursor:pointer;font-size:13px' onclick=\"cnvtExc('_P')\">Print</td>";
    if (window.opener != null) {
        $sH += "<td style='width:2px'></td><td style='border:solid 1px #000000;background-Color:#666633;color:#FFFFFF;font-size:13px' onclick=\"cnvtExc('_C')\">Close</td>"
    }
    $sH = $sH + "</tr></table>";
    ctlPrnEx.innerHTML = $sH; document.body.appendChild(ctlPrnEx);
}
function creFltr() {
    $CMB = document.getElementsByAlphFilter();
    for ($iql = 0; $iql < $CMB.length; $iql++) {
        nCMB = document.createElement('SELECT');
        nSpn = document.createElement('span');
        nSpn.id = "spnSrc_" + $iql;
        nSpn.innerText = getSrcStr($CMB[$iql]);
        nSpn.style.display = 'none';
        $CMB[$iql].insertAdjacentElement('beforeBegin', nCMB);
        nCMB.insertAdjacentElement('beforeBegin', nSpn);
        if ($CMB[$iql].id == '') $CMB[$iql].id = "TSrch_" + $iql;
        nCMB.outerHTML = "<select class='combovalue' onchange='FilterCmb(this.value," + $CMB[$iql].id + ",spnSrc_" + $iql + ")'><option value=''>--ALL--</option>" + getFilterChar($CMB[$iql].innerHTML.replace(/&nbsp;/g, '')) + "</select>";
    }
}
function getSrcStr() {
    var $sStr = '';
    $opt = $CMB[$iql].getElementsByTagName("OPTION");
    for ($i = 0; $i < $opt.length; $i++) {
        if ($opt[$i].value != '') $sStr += $opt[$i].text.replace(/ /g, '').substring(0, 1).toLocaleUpperCase() + "#" + $opt[$i].outerHTML + '$';
    }
    return $sStr;
}
function FilterCmb($x, $y, $z) {
    var $rss = '';
    $sScr = $z.innerText;
    $spP = $sScr.indexOf($x + '#');
    while ($spP > -1) {
        $epP = $sScr.indexOf('$', $spP);
        $rss += $sScr.substring($spP, $epP);
        $spP = $sScr.indexOf($x + '#', $epP);
    } if ($rss == '') $rss = $sScr;
    if ($y.outerHTML.indexOf('</OPTION>', 0) > -1)
        $y.outerHTML = $y.outerHTML.substring(0, $y.outerHTML.indexOf('</OPTION>', 0)) + '</OPTION>' + $rss + '</SELECT>';
    else
        $y.outerHTML = $y.outerHTML.substring(0, $y.outerHTML.indexOf('</option>', 0)) + '</OPTION>' + $rss + '</SELECT>';
}
function getFilterChar(Str) {
    $rstr = '';
    for ($i = 65; $i < 91; $i++) {
        fnStr = '>' + String.fromCharCode($i);
        if (Str.indexOf(fnStr) > -1 || Str.indexOf(fnStr.toLowerCase()) > -1) $rstr += "<option value='" + String.fromCharCode($i) + "'>" + String.fromCharCode($i) + "</option>";
    }
    return $rstr;
}
document.getElementsByAlphFilter = function () {
    var retnode = [];
    $CMB = document.getElementsByTagName('SELECT');
    $sCnt = $CMB.length;
    for ($iql = 0; $iql < $sCnt; $iql++)
        if ($CMB[$iql].getAttribute('AlphFilter') != null && $CMB[$iql].getAttribute('AlphFilter').toLowerCase() == 'true') retnode.push($CMB[$iql]);
    return retnode;
}

objGetElementsByName = function ($TRElem, $Name) {
    var retnode = [];
    $CMB = $TRElem.getElementsByTagName('SELECT'); $sCnt = $CMB.length; for ($iql = 0; $iql < $sCnt; $iql++) if ($CMB[$iql].name != null && $CMB[$iql].name.toLowerCase() == $Name.toLowerCase()) retnode.push($CMB[$iql]);
    $CMB = $TRElem.getElementsByTagName('INPUT'); $sCnt = $CMB.length; for ($iql = 0; $iql < $sCnt; $iql++) if ($CMB[$iql].name != null && $CMB[$iql].name.toLowerCase() == $Name.toLowerCase()) retnode.push($CMB[$iql]);
    $CMB = $TRElem.getElementsByTagName('TEXTAREA'); $sCnt = $CMB.length; for ($iql = 0; $iql < $sCnt; $iql++) if ($CMB[$iql].name != null && $CMB[$iql].name.toLowerCase() == $Name.toLowerCase()) retnode.push($CMB[$iql]);

    return retnode;
}
var NNCtrl = false;
if (typeof (window.opener) == "undefined") { NNCtrl = true; }
window.onload = function () {
    if (NNCtrl == false) creCtrl();
    creFltr();
}

