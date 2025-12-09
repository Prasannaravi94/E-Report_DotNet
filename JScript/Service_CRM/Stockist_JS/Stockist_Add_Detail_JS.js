/// <reference path="../../JsFiles/jquery-1.10.1.js" />

$(document).ready(function () {
    var ddlVal = $("#ddlFields").val();

    if (ddlVal == "State") {
        $("#ddlSrc").css("display", "block"); 
        $("#txtsearch").css("display", "none");
        $("#btnSearch").css("display", "block");
        document.getElementById("DDdisplay").style.display = "block";
        var TypeDel = {};
        TypeDel.Type = ddlVal;

        GetDropDownVal(TypeDel, ddlVal);

    }
    if (ddlVal == "Territory") {
        $("#ddlSrc").css("display", "block");
        $("#txtsearch").css("display", "none");
        $("#btnSearch").css("display", "block");
        document.getElementById("DDdisplay").style.display = "block";
      
        
        var TypeDel = {};
        TypeDel.Type = ddlVal;

        GetDropDownVal(TypeDel, ddlVal);

        //alert($("#hdnProduct").val());

    }
    if (ddlVal == "Stockist_Name") {
        $("#ddlSrc").css("display", "none");     
        $("#txtsearch").css("display", "block");
        $("#btnSearch").css("display", "block");
        document.getElementById("DDdisplay").style.display = "none";
      
   
        var TypeDel = {};
        TypeDel.Type = ddlVal;

        GetDropDownVal(TypeDel, ddlVal);

    }
    if (ddlVal == "Stockist_Designation") {
        $("#ddlSrc").css("display", "none");
        $("#txtsearch").css("display", "block");
        $("#btnSearch").css("display", "block");
        document.getElementById("DDdisplay").style.display = "none";


        var TypeDel = {};
        TypeDel.Type = ddlVal;

        GetDropDownVal(TypeDel, ddlVal);

    }
    if (ddlVal == "")
    {
        document.getElementById("DDdisplay").style.display = "none";
        $("#txtsearch").css("display", "none");
        $("#btnSearch").css("display", "none");
    }

    $("#ddlFields").change(function () {

        var ddlVal = $("#ddlFields").val();

        if (ddlVal == "Stockist_Name") {
            $("#txtsearch").css("display", "block");
            $("#ddlSrc").css("display", "none");         
            $("#lblValue").css("display", "block");
            $("#btnSearch").css("display", "block");
            document.getElementById("DDdisplay").style.display = "none";
          

            var TypeDel = {};
            TypeDel.Type = ddlVal;

            GetDropDownVal(TypeDel, ddlVal);

        }
        else if (ddlVal == "State") {
            $("#ddlSrc").css("display", "block");
            $("#txtsearch").css("display", "none");
            $("#lblValue").css("display", "block");
            $("#btnSearch").css("display", "block");
            document.getElementById("DDdisplay").style.display = "block";
         

            var TypeDel = {};
            TypeDel.Type = ddlVal;
            $("#txtsearch").val("");
            GetDropDownVal(TypeDel, ddlVal);
        }
        else if (ddlVal == "Territory") {
            $("#ddlSrc").css("display", "block");
            $("#txtsearch").css("display", "none");
            $("#lblValue").css("display", "block");
            $("#btnSearch").css("display", "block");
            document.getElementById("DDdisplay").style.display = "block";

            var TypeDel = {};
            TypeDel.Type = ddlVal;
            $("#txtsearch").val("");
            GetDropDownVal(TypeDel, ddlVal);


        }
        else if (ddlVal == "Stockist_Designation") {
            $("#txtsearch").css("display", "block");
            $("#ddlSrc").css("display", "none");
            $("#lblValue").css("display", "block");
            $("#btnSearch").css("display", "block");
            document.getElementById("DDdisplay").style.display = "none";


            var TypeDel = {};
            TypeDel.Type = ddlVal;

            GetDropDownVal(TypeDel, ddlVal);

        }
        else if (ddlVal == "")
        {
            document.getElementById("DDdisplay").style.display = "none";
            $("#txtsearch").css("display", "none");
            $("#btnSearch").css("display", "none");
            
        }

    });



    $('#btnSearch').click(function () {


        var ddlVal = $("#ddlFields").val();
        var divi1 = $('#<%=ddlSrc.ClientID%> :selected').text();
        // if (divi1 == "---Select---") { createCustomAlert("Please Select " + divi + "."); $('#ddlStockist').focus(); return false; }


        if (ddlVal == "State") {
            if (divi1 == "---Select---") {
                alert("Please Select State.");
                $('#ddlStockist').focus();
                return false;
            }
        }
        if (ddlVal == "Territory") {
            if (divi1 == "--Select--") {
                alert("Please Select HQ Name.");
                $('#ddlStockist').focus();
                return false;
            }
        }
        if (ddlVal == "Stockist_Name") {
            if ($("#txtsearch").val() == "") {

                alert("Please Enter Stockist Name."); $('#txtsearch').focus(); return false;
            }
        }
        if (ddlVal == "Stockist_Designation") {
            if ($("#txtsearch").val() == "") {

                alert("Please Enter ERP Code."); $('#txtsearch').focus(); return false;
            }
        }

        else {
            return true;
        }


    });


    $("#ddlState").change(function () {

        var State = $("#ddlState option:selected").text();

        $.ajax({

            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "webservice/Add_Stockist_WebService.asmx/Bind_Statewise_HQ",
            data: "{objState:'" + State + "'}",
            dataType: "json",
            success: function (data) {
                $("#ddlPoolName").empty();
                $.each(data.d, function (key, value) {
                    $("#ddlPoolName").append($("<option></option>").val(value.PoolId).html(value.Name));

                });

            },
            error: function ajaxError(result) {

            }

        });

    });

});


function GetDropDownVal(TypeDel, ddlVal) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "webservice/Add_Stockist_WebService.asmx/GetDropDown",
        data: '{objDDL:' + JSON.stringify(TypeDel) + '}',
        dataType: "json",
        success: function (result) {
            $('#ddlSrc').empty();
            //  $('#ddlSrc').append("<option value='0'>--Select--</option>");

            if (ddlVal == "State") {

                $.each(result.d, function (key, value) {
                    $('#ddlSrc').append($("<option></option>").val(value.State_Code).html(value.StateName));

                });

                var StateName = $("#hdnProduct").val();

                $("#ddlSrc option:contains('" + StateName + "')").attr('selected', 'selected');

            }
            else if (ddlVal == "Territory") {

                $.each(result.d, function (key, value) 
                {

                    $('#ddlSrc').append($("<option></option>").val(value.Hq_ID).html(value.HQ_Name));

                });

                var HQName = $("#hdnProduct").val();

                $("#ddlSrc option:contains('" + HQName + "')").attr('selected', 'selected');

            }

        },
        error: function ajaxError(result) {
            alert("Error");
        }
    });

}

function ProcessData() {
    $("#hdnProduct").val($("#ddlSrc option:selected").text());

    $("#ddlSrc option:selected").val($("#hdnProduct").val());
    return true;
}

function StateDDL() {
    $.ajax({

        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "webservice/Add_Stockist_WebService.asmx/BindState",
        data: "{}",
        dataType: "json",
        success: function (result) {

            for (var k = 0; k < State_id.length; k++) {

                var Data = [];
                Data = State_id.split('^');

                var StateID = Data[0];
                var StateName = Data[1];

                $("#ddlState").empty();
                // $("#ddlState").append("<option value='--Select--'>--Select--</option>");

                $.each(result.d, function (key, value) {
                    $("#ddlState").append($("<option></option>").val(value.StateCode).html(value.StateName));
                    // $("#" + StateID + " option[value='" + StateName + "']").attr('selected', 'selected');

                });

            }

        },
        error: function ajaxError(result) {

        }

    });
}