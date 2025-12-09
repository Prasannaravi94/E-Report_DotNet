<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Scheme_Entry.aspx.cs" 
    Inherits="MasterFiles_ActivityReports_Product_Scheme_Entry" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Scheme</title>
    <style type="text/css">
        #Table1 {
            margin-left: 370px;
        }

        #tblLocationDtls {
            margin-left: 370px;
        }

        #Submit {
        }

        .modal {
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

        .loading {
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

        .style1 {
            width: 240px;
        }

        .style2 {
            height: 10px;
            width: 240px;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

     <script language="javascript" type="text/javascript">

        function isNumber(ev) {
            if (ev.type === "paste" || ev.type === "drop") {
                var textContent = (ev.type === "paste" ? ev.clipboardData : ev.dataTransfer).getData('text');
                return !isNaN(textContent) && textContent.indexOf(".") === -1;
            } else if (ev.type === "keydown") {
                if (ev.ctrlKey || ev.metaKey) {
                    return true
                };
                //                var keysToAllow = [8, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57];

                // return keysToAllow.indexOf(ev.keyCode) > -1;


                var charCode = (ev.which) ? ev.which : ev.keyCode
                //if (charCode = 8 || charCode == 190 || charCode > 46) {  //8-->backspace , 46--> Delete  , 190 --> Dot(.)
                //    return true
                //}

                if (charCode == 190 || charCode == 8 || charCode == 9 || charCode == 35 || charCode == 36 || charCode == 37 || charCode == 39 || charCode == 46) {
                    return true
                }
                else {

                    var key = Number(ev.key) //Numbers only Access
                    if (isNaN(key) || ev.key === null) {
                        console.log("is not numeric")
                        return false;
                    }
                    else {
                        console.log("is numeric")
                        return true;
                    }
                }
                var key = Number(ev.key)
                if (isNaN(key) || ev.key === null) {
                    console.log("is not numeric")
                    return false;
                }
                else {
                    console.log("is numeric")
                    return true;
                }
            } else {
                return true
            }
        }
    </script>
      <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>



    
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
</head>
<body>
 <script language="javascript" type="text/javascript">

        function validateTextBox() {
            // Get The base and Child controls
            var index = 0;

            var rowscount = document.getElementById("<%= Grd_Scheme.ClientID %>").rows.length;
            var TargetBaseControl = document.getElementById('<%=this.Grd_Scheme.ClientID%>');

            var TargetChildControl1 = "txtCampBusVal";
            var TargetChildControlCheck = "txtCampBusVal";
            // Get the all the control of the type Input in the basse contrl
            var inputList = TargetBaseControl.getElementsByTagName("input");
            // loop thorught the all textboxes            


            var cnt = 0;
            var index = '';
            var index1 = '';
            var Count = 0;
            var validation = 0;
            var validationRej = 0;

            for (i = 2; i <= rowscount; i++) {
                if (i.toString().length == 1) {
                    index = cnt.toString() + i.toString();
                }
                else {
                    index = i.toString();
                }
                var chkapp = document.getElementById('Grd_Scheme_ctl' + index + '_txtCampBusVal');
                var txtAmtSpnt = document.getElementById('Grd_Scheme_ctl' + index + '_txtAmtSpnt');

                if (txtAmtSpnt.value == "") {

                    alert('Enter Spent Amt');
                    $(txtAmtSpnt).focus();
                    return false;
                }
                if (chkapp.value != "") {
                    validation = validation + 1;
                }
            }
            if (validation == 0) {
                alert('Please Enter Value');
                var chkapp = document.getElementById('Grd_Scheme_ctl02_txtCampBusVal');
                $(chkapp).focus();
                return false;
            }
        }
    </script>


        <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                if ($("#chkView").is(':checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                }
                else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });
            $(document).on('change', '#chkView', function () {
                //debugger;
                if ($(this).prop('checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                } else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });

            $("#rbtnBased").change(function () {
                //var ddlVal = $("#rbtnBased").val();
                var ddlVal = $("input[name='rbtnBased']:checked").val();
                if ($("#chkView").is(':checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                }
                else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });

            $("#ddlState").change(function () {
                var ddlVal = $("#ddlState").val();

                if ($("#chkView").is(':checked')) {
                    $("#lblEffFrom").css("display", "none");
                    $("#txtEffFrom").css("display", "none");
                    $("#lblEffTo").css("display", "none");
                    $("#txtEffTo").css("display", "none");

                    $("#lblDate").css("display", "block");
                    $("#dvDate").css("display", "block");
                }
                else {
                    $("#lblEffFrom").css("display", "block");
                    $("#txtEffFrom").css("display", "block");
                    $("#lblEffTo").css("display", "block");
                    $("#txtEffTo").css("display", "block");

                    $("#lblDate").css("display", "none");
                    $("#dvDate").css("display", "none");
                }
            });
    </script>



    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var today = new Date();
        var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
        var year = today.getFullYear() - 1;


        var dd = today.getDate();
        var mm = today.getMonth() + 01; //January is 0!
        var yyyy = today.getFullYear();

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                minDate: dd + '/' + mm + '/' + yyyy,
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });

            j('.DOBfROMDate').datepicker
            ({
                //                                minDate: new Date(year, 11, 1),
                //                                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
        });
    </script>



    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-style: none;">Product Scheme Entry </h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:RadioButtonList ID="rbtnBased" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnBased_SelectedIndexChanged">
                                    <asp:ListItem Value="P" Selected="True" Text=" Primary&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                    <asp:ListItem Value="S" Text=" Secondary&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblstate" runat="server" CssClass="label" Text="State Name"></asp:Label>
                                <div id="dvState" class="row-fluid">
                                    <asp:DropDownList ID="ddlState" CssClass="nice-select" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffFrom" runat="server" CssClass="label">Effective From<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                <div id="dvEffc_Frm" class="row-fluid">
                                    <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" 
                                        onkeypress="Calendar_enter(event);" Width="100%"
                                        onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                             
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffTo" runat="server" CssClass="label">Effective To<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                <div id="dvEffc_To" class="row-fluid">
                                    <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" 
                                        onkeypress="Calendar_enter(event);" Width="100%"
                                        TabIndex="7" onblur="this.style.backgroundColor='White'" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblDate" runat="server" CssClass="label" Text="Select Date"></asp:Label>
                                <div id="dvDate" class="row-fluid">
                                    <asp:DropDownList ID="ddlDate" CssClass="nice-select" runat="server" SkinID="ddlRequired" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkView" Text="Edit" Checked="false" runat="server" />
                            </div>

                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="50px" Text="Go" OnClick="btnGo_Click" />
                    
                        </div>

                    </div>
                </div>
                <br />

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">

                                <div id="tblRate" runat="server" width="100%" align="center">

                                    <asp:GridView ID="Grd_Scheme" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                        AllowSorting="True" EmptyDataText="No Records Found">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="40px">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trans_Slno" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSl_No" runat="server" Text='<%# Bind("Sl_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Code" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProd_Code" runat="server" Text='<%# Bind("Product_Code_SlNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProd_Name" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pack" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPack" runat="server" Text='<%# Bind("Product_Sale_Unit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate " HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRate" runat="server" Width="120px" onkeypress="return onlyNumbersnw(event)"
                                                        MaxLength="7" onpaste="return isNumber(event)" onkeydown="return isNumber(event)"
                                                        ondrop="return isNumber(event)" rel="<%# Container.DataItemIndex  %>"
                                                        Text='<%# Bind("Product_Rate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount(%) " HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDiscount" runat="server" Width="120px" onkeypress="return onlyNumbersnw(event)"
                                                        MaxLength="7" onpaste="return isNumber(event)" onkeydown="return isNumber(event)"
                                                        ondrop="return isNumber(event)" rel="<%# Container.DataItemIndex  %>"
                                                        Text='<%# Bind("Discount_Percentage") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Scheme Fixation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSchem_Fixa" runat="server" Width="120px" onkeypress="return onlyNumbersnw(event)"
                                                        MaxLength="7" onpaste="return isNumber(event)" onkeydown="return isNumber(event)"
                                                        ondrop="return isNumber(event)" onkeyup="FetchData(this)" rel="<%# Container.DataItemIndex  %>"
                                                        Text='<%# Bind("Scheme_Qty_Fixation") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Scheme Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSchem_Qty" runat="server" Width="120px" onkeypress="return onlyNumbersnw(event)"
                                                        MaxLength="7" onpaste="return isNumber(event)" onkeydown="return isNumber(event)"
                                                        ondrop="return isNumber(event)" onkeyup="FetchData(this)" rel="<%# Container.DataItemIndex  %>"
                                                        Text='<%# Bind("Scheme_Qty") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <br />
            <br />
                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="BUTTON" Width="100px"
                    Height="25px" OnClick="btnSave_Click" Visible="false" OnClientClick="javascript:return validateTextBox();" />
          
            <div class="div_fixed">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" Width="100px"
                    Height="25px" OnClick="btnSave_Click" OnClientClick="javascript:return validateTextBox();"
                    Visible="false" />
            </div>
            
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

