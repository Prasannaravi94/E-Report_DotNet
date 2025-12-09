/// <reference path="../jquery-1.10.2.js" />
$(document).ready(function () {
    //GetHQ_DDL();

    $(".modal").hide();

    var ddlSearch = $("#ddlSearch").val();
    BindState();

    if (ddlSearch = "2")
    {
        $("#txtStockist").show();
        $("#txtHQwise").hide();      
        $("#ddlState").hide();
        $("#txtStockist").val("All Stockist");
        BindHQwise_Cont();
    }
    else {
        $("#txtStockist").hide();
    }
   
    $("#ddlSearch").change(function ()
    {
        ddlSearch = $("#ddlSearch").val();

        if (ddlSearch == "1")
        {
            $("#txtHQwise").show();
            $("#txtStockist").hide();
            $("#ddlState").show();
            BindState();
        }
        else
        {
            $("#txtHQwise").hide();
            $("#txtStockist").show();
            $("#ddlState").hide();
            $("#txtStockist").val("All Stockist");
        }
    });
     
    $("#tblHQCont").hide();

    $("#txtHQwise").keyup(function () {
        searchSel();
    });

    $("#btnGo").click(function () {
        BindHQwise_Cont();
    });
     
});

function myFunction()
{
    var input, filter, table, tr, td, i;
    input = document.getElementById("txtStockist");
    filter = input.value.toUpperCase();
    table = document.getElementById("tblHQCont");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else
            {
                tr[i].style.display = "none";
            }
        }       
    }
}

function searchSel()
{
    var input = document.getElementById('txtHQwise').value.toLowerCase();

    len = input.length;
    output = document.getElementById('ddlState').options;
    for (var i = 0; i < output.length; i++)
        if (output[i].text.toLowerCase().indexOf(input) != -1) {
            output[i].selected = true;
            break;
        }
    if (input == '')
        output[0].selected = true;
}


function GetHQ_DDL()
{
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Stockist_View_HQwise.aspx/BindHQDetail",
        data: '{}',
        dataType: "json",
        success: function (result)
        {
            $("#ddlHQwise").empty();
            $("#ddlHQwise").append("<option value='0'>--Select--</option>");
            $.each(result.d, function (key, value)
            {
                $("#ddlHQwise").append($("<option></option>").val(value.HQ_Code).html(value.HQ_Name));
            });
        },
        error: function ajaxError(result)
        {
            alert("Error");
        }
    });
}

function BindState() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Stockist_View_HQwise.aspx/BindState",
        data: '{}',
        dataType: "json",
        success: function (result) {
            $("#ddlState").empty();
            $("#ddlState").append("<option value='0'>--Select--</option>");
            $.each(result.d, function (key, value) {
                $("#ddlState").append($("<option></option>").val(value.StateCode).html(value.StateName));
            });
        },
        error: function ajaxError(result) {
            alert("Error");
        }
    });
}

function BindHQwise_Cont()
{    
    var txtSearch = "";
    var ddlSearch = $("#ddlSearch").val();

    if (ddlSearch == "1") {
        txtSearch = $("#ddlState option:selected").text();
    }
    else {
        txtSearch = $("#txtStockist").val();
    }

    $("#tblHQCont").hide();

    $(".modal").show();

    $.ajax({

        type: "POST",
        url: "Stockist_View_HQwise.aspx/GetStockist_Detail",
        data: '{SearchText:' + JSON.stringify(txtSearch) + ',SearchOpt:' + JSON.stringify(ddlSearch) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
                
            if (data.d.length > 0)
            {
                $("#tblHQCont").show();
                    
                var rows = '<tr>';
                rows += '<th>S.No</th>';
                rows += '<th>Stockist Name</th>';
                rows += '<th>ERP Code</th>';
                rows += '<th>HQ Name</th>';
                rows += '<th>HQ Code</th>';
                rows += '<th>Contribution (in %)</th>';
                rows += '</tr>';
                var Cnt = 0;
                for (var i = 0; i < data.d.length; i++)
                {                        
                    Cnt += 1;
                    rows += '<tr>';
                    rows += '<td>' + Cnt + '</td>';
                    rows += '<td>' + data.d[i].Stockist_Name + '</td>';
                    rows += '<td>' + data.d[i].ERP_Code + '</td>';
                    rows += '<td>' + data.d[i].HQ_Name + '</td>';
                    rows += '<td>' + data.d[i].SF_HQ_Code + '</td>';
                    rows += '<td>' + data.d[i].SF_HQ_Cont +'%'+'</td>';
                    rows += '</tr>';
                }
                $("#tblHQCont").html(rows);

                $(".modal").hide();
            }
        },
        error: function (res) {
            $(".modal").hide();
        }
    });
}