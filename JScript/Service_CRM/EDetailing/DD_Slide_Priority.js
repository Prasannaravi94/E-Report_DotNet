var DD_SfCode = "",
    DD_SfType = "",
    DD_DivCode = "",
    DD_SubDiv = "",
    DD_SubDivTxt = "",
    DD_Brand = "",
    DD_BrandTxt = "",
    DD_Product = "",
    DD_Spec = "",
    DD_Therapy = "",
    DD_Mode = "",
    DD_ModeTxt = "",
    arrSlidePriority = [];

function Get_Slide_Priority_View(e) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../MR/webservice/EDetailingWebService.asmx/GetSlidePriority",
        data: "{objSlidePriority:" + JSON.stringify(e) + "}",
        dataType: "json",
        success: function (e) {
            if ($.trim(e.d)) {
                var g = 0;
                $("#grdSlide").empty(), $("#grdSlide").append("<thead><tr style='background-color:White;'><th scope='col'>S.No</th><th class='grdHeader' scope='col'>ImgS.No</th><th class='grdHeader' scope='col'>ImgSrc</th><th scope='col'>Image</th><th scope='col'>Priority</th><tbody>");
                for (var t = 0; t < e.d.length; t++) {
                    var d = t + 1;
                    $("#grdSlide").append("<tr><td><span id='lblSNo_" + t + "'>" + d + "</span></td> <td class='grdHeader'><span id='imgSI_NO_" + t + "'>" + e.d[t].SI_NO + "</span></td>  <td class='grdHeader'><span id='imgName_" + t + "'>" + e.d[t].Img_Name + "</span></td>  <td><img id='imgSrc_" + t + "'  src='" + e.d[t].Img_Src + "' title='" + e.d[t].Img_Name + "' style='height:120px;width:150px;'></td>  <td><select id='ddlSPriority' class='form-control ddlSPriority" + t + "'></select></td> </tr> ")
                    if (e.d[t].Priority != '')
                        g = g + 1;
                }
                $("#grdSlide").append("</tbody>");
                var i = $("#grdSlide tr").length;
                $("#grdSlide > tbody  > tr").each(function () {
                    var e = $(this).find("select:eq(0)");
                    var o = 0;
                    e.empty();
                    //e.append($("<option/>").val(0).text('Nothing selected'));
                    for (var t = 1; t < i; t++) {
                        e.append($("<option/>").val(t).text(t));
                        if (t <= g) {
                            e.selectpicker(), e.selectpicker("val", $(this).index() + 1)
                            o = 1;
                        }
                        else {
                            if (o == 0) {
                                    e.selectpicker(), //e.selectpicker("val", 0)
                                    e.selectpicker({ title: 'Nothing selected' }).selectpicker('render');
                            }
                        }
                    }
                }), $("#grdSlide").DataTable({
                    destroy: !0,
                    paging: !1,
                    ordering: !1,
                    info: !1,
                    searching: !1
                }), $("#grdSlide").show()
            } else $("#grdSlide").empty(), $("#grdSlide").hide(), $.alert("No Records Found!", "Alert!")
        },
        failure: function (e) { },
        error: function (e) { }
    })
}
$(document).ready(function () {
    $(document).ajaxStart(function () {
        $("#loader").css("display", "block")
    }).ajaxStop(function () {
        $("#loader").css("display", "none")
    }), DD_SfCode = $("#DD_SfCode").val(), DD_SfType = $("#DD_SfType").val(), DD_DivCode = $("#DD_DivCode").val(), DD_SubDiv = $("#DD_SubDiv").val(), DD_SubDivTxt = $("#DD_SubDivTxt").val(), DD_Brand = $("#DD_Brand").val(), DD_BrandTxt = $("#DD_BrandTxt").val(), DD_Product = $("#DD_Product").val(), DD_Spec = $("#DD_Spec").val(), DD_Therapy = $("#DD_Therapy").val(), DD_Mode = $("#DD_Mode").val(), DD_ModeTxt = $("#DD_ModeTxt").val();
    var e = "";
    "0" == DD_Mode ? ($("#lblSDivisionVal").empty(), $("#lblSMode").empty(), $("#lblSModeVal").empty(), $("#lblSBrandVal").empty(), $("#lblSBrandVal").text(DD_BrandTxt), $(".dvSubDiv").hide(), $(".dvMode").hide(), $(".dvBrand").show()) : "1" == DD_Mode ? (e = DD_Product, $("#lblSDivisionVal").empty(), $("#lblSDivisionVal").text(DD_SubDivTxt), $("#lblSMode").empty(), $("#lblSModeVal").empty(), $("#lblSMode").text("Product :"), $("#lblSModeVal").text(DD_ModeTxt), $("#lblSBrandVal").empty(), $("#lblSBrandVal").text(DD_BrandTxt), $(".dvSubDiv").show(), $(".dvMode").show(), $(".dvBrand").show()) : "2" == DD_Mode ? (e = DD_Spec, $("#lblSDivisionVal").empty(), $("#lblSMode").empty(), $("#lblSModeVal").empty(), $("#lblSMode").text("Speciality :"), $("#lblSModeVal").text(DD_ModeTxt), $("#lblSBrandVal").empty(), $("#lblSBrandVal").text(DD_BrandTxt), $(".dvSubDiv").hide(), $(".dvMode").show(), $(".dvBrand").show()) : "3" == DD_Mode && (e = DD_Therapy, $("#lblSDivisionVal").empty(), $("#lblSMode").empty(), $("#lblSModeVal").empty(), $("#lblSMode").text("Therapy :"), $("#lblSModeVal").text(DD_ModeTxt), $("#lblSBrandVal").empty(), $("#lblSBrandVal").text(DD_BrandTxt), $(".dvSubDiv").hide(), $(".dvMode").show(), $(".dvBrand").show()), (arrSlidePriority = []).push(DD_SubDiv), arrSlidePriority.push(DD_Brand), arrSlidePriority.push(e), arrSlidePriority.push(DD_Mode), Get_Slide_Priority_View(arrSlidePriority.join("^"))
}), $(document).on("change", "#ddlSPriority", function () {
    var e = $(this).parents("tr:first"),
        t = e.find("select:eq(0)"),
        d = [];
    for (i = 1; i <= $("#grdSlide > tbody > tr").length; i++) d.push(i);
    var r = [];
    r = $("#grdSlide > tbody > tr").map(function () {
        return $(this).find("select:eq(0)").val()
    }).get(), r = $.unique(r.sort());
    var o = {},
        l = [];
    for (let e of r) o[e] = !0;
    for (let e of d) o[e] || l.push(e);
    $("#grdSlide > tbody > tr").each(function () {
        var d = $(this),
            i = d.find("select:eq(0)");
        if (d.find("td:nth-child(5) > div > button").text().trim() != 'Nothing selected') {
            t.val() == i.val() && e.index() != d.index() && (i.selectpicker(), i.selectpicker("val", l[0]))
        }
    })
}), $(".Priority").click(function (e) {
    e.preventDefault(), $(document).ajaxStart(function () {
        $("#loader").css("display", "block")
    }).ajaxStop(function () {
        $("#loader").css("display", "none")
    });
    var t, d = "",
        i = "";
    "1" == DD_Mode ? (d = DD_SubDiv, i = DD_Product, modePriority = "1") : "2" == DD_Mode ? (d = DD_SubDiv, i = DD_Spec, modePriority = "2") : "3" == DD_Mode ? (d = DD_SubDiv, i = DD_Therapy, modePriority = "3") : "0" == DD_Mode && (d = DD_SubDiv, modePriority = "0"), t = DD_Brand;
    var r = [];
    var flagselect = 0;

    r = $("#grdSlide > tbody > tr").map(function () {
        var e = $(this);
        if (e.find("td:nth-child(5) > div > button").text().trim() == 'Nothing selected') {
            flagselect = flagselect + 1;
        }
        else {
            return e.find("span:eq(1)").text() + "^" + e.find("select:eq(0)").val() + "^" + modePriority + "^" + d + "^" + i + "^" + t
        }
    }).get()
    if (flagselect == 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../MR/webservice/EDetailingWebService.asmx/Update_Slide_Priority",
            data: "{objPriority:" + JSON.stringify(r.join(",")) + "}",
            dataType: "json",
            success: function (e) {
                $.confirm({
                    title: "Alert!",
                    content: "Update Success.",
                    buttons: {
                        ok: function () {
                            window.location.reload(!0)
                        }
                    }
                })
            },
            error: function (e) { }
        })
    }
    else {
        alert("Please select the priority order properly and click the update button.");
        return false;
    }
});