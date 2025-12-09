<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Map_SubDivisionwise.aspx.cs"
    Inherits="MasterFiles_Frm_Product_SubDiv_Test" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SubDivision Wise - Product Map</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[src*=plus]").live("click", function () {
                if ($(this).closest("tr").next().css('display') == "table-row") {
                    $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                    $(this).next().remove();
                } else
                    $(this).closest("tr").next().css('display', 'table-row');
                $(this).attr("src", "../../../Images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../../../Images/plus.png");
                $(this).closest("tr").next().css('display', 'none');
            });

        });
    </script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        .textalign
        {
            text-align: center;
            font-weight: bold;
        }
        
        .mycheckbox input[type="checkbox"]
        {
            margin-right: 7px;
        }
        
        
       label, input[type="checkbox"]
        {
            line-height: 2.1ex;
        }
        
        input[type=checkbox]
        {
            font-size: 16px;
            position: relative;
            border: 1px solid #262626;
            display: inline-block;
            margin: 0;
            padding: 0;
            width: 1em;
            height: 1em;
            vertical-align: text-top;
            -webkit-appearance: none;
            -webkit-box-shadow: inset 0 2px 5px rgba(0,0,0,0.25);
            -webkit-border-radius: 3px;
            margin-right: 0.5em;
        }
        
        input[type=checkbox]:checked::after
        {
            content: '';
            position: absolute;
            width: 1.2ex;
            height: 0.4ex;
            background: rgba(0, 0, 0, 0);
            top: 0.5px;
            left: 0.4ex;
            border: 3px solid #228B22;
            border-top: none;
            border-right: none;
            -webkit-transform: rotate(-45deg);
            -moz-transform: rotate(-45deg);
            -o-transform: rotate(-45deg);
            -ms-transform: rotate(-45deg);
            transform: rotate(-45deg);
        }
        
        
        
    </style>
    <%--  <link href="../JScript/CheckBoxCSS.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">

        function confirm_Save() {

            if (confirm('Do you want to Tag the Product?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();

                    return true;

                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {

                var SubDiv = $('#<%=ddlSubDivision1.ClientID%> :selected').text();


                if (SubDiv == "---Select---") {

                    var txt = "Please Select SubDivision";
                    createCustomAlert(txt);

                    $('#ddlSubDivision1').focus();
                    return false;
                }

            });


            $('#btnclr').click(function () {

                $('#ddlSubDivision1').val("0");


                $("#lblSelect").css("visibility", "hidden");
                $("#btnSubmit").css("visibility", "hidden");
                $('#lblSelect').css("visibility", "hidden");


            });

            $('input.selectAll').click(function () {

                var all = this;
                var chk = $(this).prop("id");
                var arrChk = [];
                arrChk = chk.split('-');

                var $this = $('#' + chk);

                var TableID = $(this).parents().children("table").eq(arrChk[1]).prop("id");

                if ($this.is(':checked')) {

                    // alert($(this).parents().children("table").eq(arrChk[1]).prop("id"));

                    $('#' + TableID + ' input[type=checkbox]').attr('checked', 'checked');

                }
                else {

                    $('#' + TableID + ' input[type=checkbox]:checked').removeAttr('checked');
                }

            });


            $("#btnSubmit").click(function () {

                $('#pnl table').each(function () {

                    var ID = this.id;

                    var tableName = ID;

                    //alert(tableName);

                    var subDivName;
                    var hdnProductId;
                    var ProductName;
                    var SubProductData = {};

                    var list = $('#' + tableName)
                    var SubDivID = $("#ddlSubDivision1").val();

                    var chklist = $("[id*=" + tableName + "] input:checked");

                    selected = $("[id*=" + tableName + "] tr td input[type='checkbox']:checked");

                    var tableControl = $("[id*=" + tableName + "] tr td");
                    var arrayOfValues = [];

                    $('input:checkbox:checked', tableControl).each(function (i) {

                        ProductName = $(selected[i]).closest("td").find("label").html();
                        subDivName = $('#' + tableName).find('#divHeader').text();
                        subCode = $('#' + tableName).find('#hdnSubDiv').val();
                        // alert("subCode :" + subCode);
                        hdnProductId = $(chklist[i]).closest("td").find("#myDiv").html();

                        var SubProductData = ProductName + "," + subDivName + "," + hdnProductId + "," + SubDivID + "," + subCode;

                        //alert(SubProductData);

                        arrayOfValues.push(SubProductData);

                    }).get();


                    if (arrayOfValues.length != 0) {

                        $.ajax({
                            type: "POST",
                            url: "Product_Map_SubDivisionwise.aspx/AddProductData_2",
                            data: '{objProductData:' + JSON.stringify(arrayOfValues) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                console.log("success" + data);
                                var txt = "Product Mapped Successfully";
                                createCustomAlert(txt);
                            },
                            error: function (result) {
                                console.log("Error" + result);
                            }

                        });
                    }



                    //                    for (var i = 0; i < chklist.length; i++) {
                    //                        if (chklist[i].type == "checkbox") {

                    //                            var isChecked = $(chklist).prop("checked");

                    //                            ProductName = $(chklist[i]).closest("td").find("label").html();

                    //                            subDivName = $('#' + tableName).find('#divHeader').text();

                    //                            hdnProductId = $(chklist[i]).closest("td").find("#myDiv").html();

                    //                            SubProductData.ProductCodeSlNo = hdnProductId;
                    //                            SubProductData.SubDivName = subDivName;
                    //                            SubProductData.ProductName = ProductName;
                    //                            SubProductData.SubDivID = SubDivID;
                    //                            SubProductData.StateID = StateID;

                    //                            $.ajax({
                    //                                type: "POST",
                    //                                url: "Product_Map_SubDivisionwise.aspx/AddProductDetail",
                    //                                data: '{ProductData: ' + JSON.stringify(SubProductData) + '}',
                    //                                contentType: "application/json; charset=utf-8",
                    //                                dataType: "json",
                    //                                success: function (data) 
                    //                                {

                    //                                    $("#ddlSubDivision1 option:first").attr('selected','selected');                            
                    //                                   
                    //                                   // $('#ddlSubDivision1').val("0");
                    //                                    $('#ddlStateProduct').val("0");
                    //                                    $("#lblSelect").css("visibility", "hidden");
                    //                                    $("#btnSubmit").css("visibility", "hidden");
                    //                                    $('#lblSelect').css("visibility", "hidden");

                    //                                    var txt = "Product Mapped Successfully";
                    //                                    createCustomAlert(txt);

                    //                                }

                    //                            });

                    //                        }

                    //                    }

                });

            });


            function callme(code) {

                var tableName = code;

                var list = $('#' + tableName)
                var chklist = $("[id*=" + tableName + "] input:checked");
                for (var i = 0; i < chklist.length; i++) {
                    if (chklist[i].type == "checkbox") {

                        var isChecked = $(chklist).prop("checked");

                        var text = $(chklist[i]).closest("td").find("label").html();

                        var header = $("#divHeader").text();

                        var divlabel = $('#' + tableName).find('#divHeader').text();

                    }
                }

            }

        });


        function CheckBoxSelectionValidation() {

            var count = 0;
            var objgridview = document.getElementById('<%= pnl.ClientID %>');

            for (var i = 0; i < objgridview.getElementsByTagName("input").length; i++) {

                var chknode = objgridview.getElementsByTagName("input")[i];

                if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
                    count = count + 1;
                }
            }

            if (count == 0) {
                var txt = "Please select atleast one checkbox.";
                createCustomAlert(txt);

                return false;
            }
            else {
                return true;
            }

        }

    </script>
    <link href="../JScript/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../JScript/css/buttons/buttons.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 100%">
        <%--  <div id="Divid" runat="server">
        </div>--%>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table align="center">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSubDivision" runat="server" Text="Select SubDivision" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlSubDivision1" runat="server" SkinID="ddlRequired" Height="24px"
                            Width="170px">
                        </asp:DropDownList>
                    </td>
                </tr>
            
            </table>
            <br />
            <div style="margin-left: 7%">
                <asp:Button ID="btnGo" Text="GO" CssClass="savebutton" Width="60px" Height="28px" runat="server"
                    OnClick="btnGo_Click" />
                <asp:Button ID="btnclr" runat="server" Text="Clear" CssClass="savebutton" Width="60px"
                    Height="28px"></asp:Button>
            </div>
            <br />
            <div>
            </div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblSelect" Text="Select the Product" Font-Bold="true" runat="server"
                            ForeColor="#A52A2A" Font-Size="Medium" Font-Underline="true"></asp:Label>
                          
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <div>
                <asp:Panel ID="pnl" runat="server">
                </asp:Panel>
            </div>
            <br />
        </center>
        <center>
            <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="savebutton" Width="60px"
                Height="28px" />
        </center>
    </div>
    </form>
</body>
</html>
