/// <reference path="../../JsFiles/jquery-1.10.2.js" />


$(document).ready(function ()
{
    BindField_Force_DDL();
    BindYear_DDL();

    var d = new Date();
    n = d.getMonth() + 1;

    // alert("Month :"+ n);

    $('#ddlMonth option:eq(' + n + ')').prop('selected', true);
    $('#ddlTMonth option:eq(' + n + ')').prop('selected', true);

});

function BindField_Force_DDL() {
    
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../webservice/SecSale_Stockist_EntryStatus_WS.asmx/FieldForce_DDL",
        data: "{}",
        dataType: "json",
        success: function (result) 
        {
            $("#ddlFieldForce").empty();
            //  $("#ddlFieldForce").append("<option value='0'>--Select--</option>");
            $.each(result.d, function (key, value) 
            {
                $("#ddlFieldForce").append($("<option></option>").val(value.Field_Sf_Code).html(value.Field_Sf_Name));
            });

            //setTimeout(function () { });

            $("#ddlFieldForce").trigger('change');
        },
        error: function ajaxError(result) 
        {
            createCustomAlert("Error");
        }

    });
}

function BindYear_DDL() 
{
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../webservice/SecSale_Stockist_EntryStatus_WS.asmx/Year_DDL",
        data: "{}",
        dataType: "json",
        success: function (data) 
        {
            $("#ddlYear").empty();

            var Year = data.d[0].Year;
            var Cur_Year = new Date().getFullYear();

            for (var i = parseInt(Year); i <= Cur_Year; i++) 
            {
                $("#ddlYear").append($("<option></option>").val(i).html(i));
                $("#ddlTYear").append($("<option></option>").val(i).html(i));
            }

            $("#ddlYear option:contains('" + Cur_Year + "')").attr('selected', 'selected');
            $("#ddlTYear option:contains('" + Cur_Year + "')").attr('selected', 'selected');
        },
        error: function ajaxError(result) 
        {
            createCustomAlert("Error");
        }

    });
}