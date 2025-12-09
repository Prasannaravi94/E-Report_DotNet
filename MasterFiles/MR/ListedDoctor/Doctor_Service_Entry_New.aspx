<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_Entry_New.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_Entry_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Doctor - Service Entry</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
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
    <script type="text/javascript" language="javascript">
        function disp_confirm() {
            if (confirm("Do you want to Create the ID ?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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


        });
    </script>
    <link href="../../../JScript/BootStrap/dist/css/CRMService.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/Service_CRM/Crm_Dr_Css_Ob/ServiceCRM_AddCSS.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../JScript/Service_CRM/Crm_Dr_JS/Listed_Dr_ServiceCRM_Add_JS.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var keyDown = false, ctrl = 17, vKey = 86, Vkey = 118;

            $(document).keydown(function (e) {
                if (e.keyCode == ctrl) keyDown = true;
            }).keyup(function (e) {
                if (e.keyCode == ctrl) keyDown = false;
            });

            $('.txtSerCss').on('keypress', function (e) {
                if (!e) var e = window.event;
                if (e.keyCode > 0 && e.which == 0) return true;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                var character = String.fromCharCode(code);
                if (character == '\b' || character == ' ' || character == '\t') return true;
                if (keyDown && (code == vKey || code == Vkey)) return (character);
                else return (/[0-9]$/.test(character));
            }).on('focusout', function (e) {
                var $this = $(this);
                $this.val($this.val().replace(/[^0-9]/g, ''));
            }).on('paste', function (e) {
                var $this = $(this);
                setTimeout(function () {
                    $this.val($this.val().replace(/[^0-9]/g, ''));
                }, 5);
            });
        });

    </script>
    <script type="text/javascript">
        function preventMultipleSubmissions() {
            $('#btnProcess').prop('disabled', true);
        }
        window.onbeforeunload = preventMultipleSubmissions;
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
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
            j('.ui-datepicker').addClass('notranslate');
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <br />
            <table id="Table1" runat="server" width="90%">
                <tr>
                    <td align="right" width="30%">
                        <%--  <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" Width="40px"
                            OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
           
            <center>
            <table>

                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFrmDate" runat="server" Text="Effective Date" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtEffFrom" runat="server" SkinID="MandTxtBox" CssClass="DOBfROMDate"
                            onkeypress="Calendar_enter(event);" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>                                             
                    </td>
                </tr>
            </table>
                </center>
            <br />
            <center>
                <div>
                    <p id="divTitle" style="font-family: Arial; font-weight: bold; font-size: 16px; color: #ff00b1;" class="TextUnder">Listed Doctor Detail</p>
                </div>
                <br />
                <div>

                    <input type="hidden" id="hdnDrCode" />
                    <input type="hidden" id="hdnFnlYear" />
                    <input type="hidden" id="hdnServiceType" />
                    <input type="hidden" id="hdnSerTypeValue" />
                    <input type="hidden" id="hdnModeType" />
                    <input type="hidden" id="hdnChemistCode" />

                    <div id="divDoctor" style="width: 100%">
                        <table id="tblDoc" class="tblDr" style="width: 75%">
                            <tr>
                                <td>
                                    <span id="lblDoctorName" style="font-size: 12px">Doctor Name </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_DoctorName" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="lblAddress" style="font-size: 12px">Hospital/Clinic </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Doctor_Address" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="lblCategory" style="font-size: 12px">Category </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Category" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="lblQualification" style="font-size: 12px">Qualification </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Qualification" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="lblspeciality" style="font-size: 12px">Speciality </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Speciality" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="lblClass" style="font-size: 12px">Class </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Class" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="lblMobile" style="font-size: 12px">Mobile No </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Mobile" style="font-size: 12px"></span>
                                </td>
                                <td>
                                    <span id="lblEmail" style="font-size: 12px">Email </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txt_Email" style="font-size: 12px"></span>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="lblDoctorServiceTime" style="font-size: 12px">Service Amount Given to this Dr till Date</span>
                                </td>
                                <td>:</td>
                                <td>
                                    <%-- <input  type="text" maxlength="100" id="txtService" 
                       onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                        class="textClass" />--%>

                                    <span id="txtService" style="font-weight: bold; font-size: 11px"></span>

                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="lblBusinessDate" style="font-size: 12px">Business given till Date</span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtBusinessDate" style="font-weight: bold; font-size: 12px"></span>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>

                    <div id="divChemist" style="width: 100%">
                        <table id="tblChemist" class="tblDr" style="width: 75%">
                            <tr>
                                <td>
                                    <span id="lblChemist" style="font-size: 12px">Chemist Name </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtChemist_Name" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="lblAddressCh" style="font-size: 12px">Address</span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtAdddress_Chm" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="Contact" style="font-size: 12px">Contact </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtContact" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="lblPhone_chm" style="font-size: 12px">Mobile No </span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtPhone_No" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td>
                                    <span id="lblTerritoryChm" style="font-size: 12px">Territory</span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtTerritoyChm" style="font-weight: bold; font-size: 11px"></span>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td>
                                    <span id="lblSeviceAmt_Chemist" style="font-size: 12px">Service Amount Given to this Dr till Date</span>
                                </td>
                                <td>:</td>
                                <td>
                                    <%-- <input  type="text" maxlength="100" id="txtService" 
                       onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                        class="textClass" />--%>

                                    <span id="txtService_AmtChm" style="font-weight: bold; font-size: 11px"></span>

                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="lblBusgvTillDate" style="font-size: 12px">Business given till Date</span>
                                </td>
                                <td>:</td>
                                <td>
                                    <span id="txtBus_tillDate" style="font-weight: bold; font-size: 12px"></span>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>

                </div>

                <br />
                <div id="divVisit">
                    <p style="font-family: Arial; font-weight: bold; font-size: 15px; color: Green" class="TextUnder">Visit - Last Three Months</p>

                    <table width="80%" align="center">
                        <asp:Table ID="tbl" runat="server" class="table table-bordered table-striped" Width="87%">
                        </asp:Table>
                    </table>
                    <br />
                </div>


                <%-- <div>
        <table id="tblProduct" cellpadding="5" cellspacing="5" class="table">
        
        </table>
        </div>--%>


                <table id="tblProduct" cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
                            <div>
                                <table id="tblCurrentSupport" class="table table-bordered table-striped" style="width: 45%">
                                </table>
                            </div>
                        </td>
                        <td>
                            <div>
                                <table id="tblPotentialProduct" class="table table-bordered table-striped"
                                    style="width: 50%">
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>


            </center>
            <br />
            <div style="margin-left: 10%">
                <table class="tblDr">
                    <tr>
                        <td>
                            <span id="lblTotalBussiness" style="font-size: 12px">Total Business Return Expected
                            the Doctor in Amt(Rs/-)<br />
                                (Target Amount)</span>
                        </td>
                        <td>
                            <input type="text" maxlength="7" id="txtTBusinessRAmt" class="textClass txtSerCss" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="lblROI" style="font-size: 12px">ROI Duration Month</span>
                        </td>
                        <td>
                            <select id="ddlROI" class="input-sm" style="font-size: 12px">
                                <option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                                <option>6</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="lblBillType" style="font-size: 12px">Bill Type</span> <span style="color: Red">*</span> </span>
                        </td>
                        <td>
                            <input type="checkbox" id="ChkAdvance" value="Advance" name="BillType" />
                            <label for="ChkAdvance">
                                ADVANCE SALES</label>
                            <input type="checkbox" id="ChkPost" value="Post" name="BillType" />
                            <label for="ChkPost">
                                POST SALES</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="lblServiceRq" style="font-size: 12px">Service Required <span style="color: Red">*</span> </span>
                        </td>
                        <td>
                            <input type="text" maxlength="200" id="txtServiceRq" onkeypress="AlphaNumeric_withPartofSpecialChar(event);"
                                class="textClass" style="width: 420px; height: 45px" />
                            <a id="btnField_Link" href="#" title="Reason Status" class="Service">Click here to Enter
                            Details</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="lblServiceAmt" style="font-size: 12px">Service Amount <span style="color: Red">*</span> </span>
                        </td>
                        <td>
                            <input type="text" maxlength="7" id="txtServiceAmt" class="textClass txtSerCss" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="lblSpecAct" style="font-size: 12px">Specific Activities (Remarks)</span>
                        </td>
                        <td>
                            <input type="text" maxlength="200" id="txtSpecAct" onkeypress="AlphaNumeric_withPartofSpecialChar(event);"
                                class="textClass" style="width: 420px; height: 45px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divOutlet">
                                <span id="lblPrescription" style="font-size: 12px">Prescription Outlets (Chemist) <span
                                    style="color: Red">*</span></span>
                            </div>
                        </td>
                        <td>
                            <div id="divChemistDDL">
                                <select id="ddlChemist_1" class="input-sm" style="width: 160px">
                                </select>
                                <select id="ddlChemist_2" class="input-sm" style="width: 160px">
                                </select>
                                <select id="ddlChemist_3" class="input-sm" style="width: 160px">
                                </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="lblStockist" style="font-size: 12px">Stockist <span style="color: Red">*</span>
                            </span>
                        </td>
                        <td>
                            <select id="ddlStockist_1" class="input-sm" style="width: 160px">
                            </select>
                            <select id="ddlStockist_2" class="input-sm" style="width: 160px">
                            </select>
                            <select id="ddlStockist_3" class="input-sm" style="width: 160px">
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <center>
                <div>
                    <input type="button" id="btnProcess" value="Process" class="btn" />
                    <%--    &nbsp;
            <input type="button" id="btnSendtoMgr" value="Send To Manager" class="btn"/>--%>
                </div>
            </center>
        </div>
        <div>
            <div>
                <%--<div id="openModal" class="modalDialog">
                <div>
                    <a href="#close" title="Close" class="close">X</a>
                    <div>
                        <div>
                            <table>
                                <tr>
                                    <td align="left" class="stylespc">
                                        <span id="lblDrName" class="lblClass">Doctor Name :</span>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <span id="txtDrName" class="lblClass" style="font-weight: bold; color: #ff229b; font-size: 12px;
                                            width: 300px"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="stylespc">
                                        <span id="lblForWhat" class="lblClass">For What </span>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <select id="ddlForWhat" class="input-sm">
                                            <option value="1">Stay</option>
                                            <option value="2">Travel/Itinerary</option>
                                            <option value="3">Books</option>
                                            <option value="4">Conference/Seminar</option>
                                            <option value="5">Others</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div>
                            <div class="wrapper">
                                <div class="container">
                                    <div id="divStay">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td align="left" class="stylespc">
                                                        <span id="lblLocation" class="lblClass">Location/Venue :</span>
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <input type="text" maxlength="100" id="txtLocation" class="textClass" />
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <span id="lblHotel" class="lblClass">Type of Hotel :</span>
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <input type="text" maxlength="100" id="txtHotel" class="textClass" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <table id="tblStaty" class="table table-bordered table-striped" style="width: 50%">
                                            </table>
                                        </div>
                                    </div>
                                    <div id="divTravel">
                                        <center>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td align="left" class="stylespc">
                                                        <span id="lblTypeofTravel" class="lblClass">Type of Travel :</span>
                                                    </td>
                                                    <td align="left" class="stylespc">
                                                        <input type="checkbox" id="chkAir" value="Air" />
                                                        <label for="chkAir">Air</label>
                                                        <input type="checkbox" id="chkRail" value="Rail" />
                                                        <label for="chkRail">
                                                            Rail</label>
                                                        <input type="checkbox" id="chkRoad" value="Road" />
                                                        <label for="chkRoad">
                                                            Road</label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                       
                                        <div>
                                            <table id="tblTravel" class="table table-bordered table-striped" style="width:50%">
                                            </table>
                                        </div>
                                        <span id="Detail" class="lblClass" style="width:400px;font-size:14px;font-weight:bold;color:Maroon">Details of Members Expected To Travel</span>
                                        
                                        <div>
                                            <table id="tblDetail" class="table table-bordered table-striped" style="width:50%">
                                            </table>
                                        </div>
                                        </center>
                                    </div>
                                    <div id="divBook">
                                        <div>
                                            <center>
                                            <table id="tblBook" >
                                            </table>
                                            </center>
                                        </div>
                                    </div>
                                    <div id="divConference">
                                        <div>
                                            <table id="tblConference" class="table table-bordered table-striped">
                                            </table>
                                        </div>
                                        <div>
                                            <table id="tblConf_Check">
                                            </table>
                                        </div>
                                    </div>
                                    <div id="divOthers">
                                        <div>
                                            <center>
                                            <table id="tblOthers">
                                            </table>
                                        </center>
                                        </div>
                                    </div>
                                    <center>
                                    <div>
                                     <input type="button" id="btnSave" value="Save" class="btn"/>
                                    </div>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
        <div>
            <%--   <a href="#" id="btnField_Link">Click</a>--%>
            <div>
                <div id="output_Field">
                </div>
                <div id="overlay_Field" class="web_dialog_overlay">
                </div>
                <div id="dialog_Field" class="web_dialog">
                    <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="f0">
                        <tr>
                            <%--  <td class="web_dialog_title">
                            Field Parameters
                        </td>--%>
                            <td>
                                <a href="#close" id="btnClose_Field" title="Close" class="close">X</a>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div>
                            <div>
                                <table>
                                    <tr>
                                        <td align="left" class="stylespc">
                                            <span id="lblDrName" class="lblClass">Doctor Name :</span>
                                        </td>
                                        <td align="left" class="stylespc">
                                            <span id="txtDrName" class="lblClass" style="font-weight: bold; color: #ff229b; font-size: 12px; width: 300px"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="stylespc">
                                            <span id="lblForWhat" class="lblClass">For What </span>
                                        </td>
                                        <td align="left" class="stylespc">
                                            <select id="ddlForWhat" class="input-sm">
                                                <option value="1">Stay</option>
                                                <option value="2">Travel/Itinerary</option>
                                                <option value="3">Books</option>
                                                <option value="4">Conference/Seminar</option>
                                                <option value="5">Others - I</option>
                                                <option value="6">Other - II</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div>
                                <div class="wrapper">
                                    <div class="container">
                                        <div id="divStay">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td align="left" class="stylespc">
                                                            <span id="lblLocation" class="lblClass">Location/Venue :</span>
                                                        </td>
                                                        <td align="left" class="stylespc">
                                                            <input type="text" maxlength="100" id="txtLocation" class="textClass" />
                                                        </td>
                                                        <td align="left" class="stylespc">
                                                            <span id="lblHotel" class="lblClass">Type of Hotel :</span>
                                                        </td>
                                                        <td align="left" class="stylespc">
                                                            <input type="text" maxlength="100" id="txtHotel" class="textClass" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table id="tblStaty" class="table table-bordered table-striped" style="width: 50%">
                                                </table>
                                            </div>
                                        </div>
                                        <div id="divTravel">
                                            <center>
                                                <div>
                                                    <table id="tblTravelType">
                                                        <tr>
                                                            <td align="left" class="stylespc">
                                                                <span id="lblTypeofTravel" class="lblClass">Type of Travel :</span>
                                                            </td>
                                                            <td align="left" class="stylespc">
                                                                <input type="checkbox" id="chkAir" value="Air" name="TravelType" />
                                                                <label for="chkAir">Air</label>
                                                                <input type="checkbox" id="chkRail" value="Rail" name="TravelType" />
                                                                <label for="chkRail">
                                                                    Rail</label>
                                                                <input type="checkbox" id="chkRoad" value="Road" name="TravelType" />
                                                                <label for="chkRoad">
                                                                    Road</label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <div>
                                                    <table id="tblTravel" class="table table-bordered table-striped" style="width: 50%">
                                                    </table>
                                                </div>
                                                <span id="Detail" class="lblClass" style="width: 400px; font-size: 14px; font-weight: bold; color: Maroon">Details of Members Expected To Travel</span>

                                                <div>
                                                    <table id="tblDetail" class="table table-bordered table-striped" style="width: 50%">
                                                    </table>
                                                </div>
                                            </center>
                                        </div>
                                        <div id="divBook">
                                            <div>
                                                <center>
                                                    <table id="tblBook">
                                                    </table>
                                                </center>
                                            </div>
                                        </div>
                                        <div id="divConference">
                                            <div>
                                                <table id="tblConference" class="table table-bordered table-striped">
                                                </table>
                                            </div>
                                            <div>
                                                <table id="tblConf_Check">
                                                </table>
                                            </div>
                                        </div>
                                        <div id="divOthers">
                                            <div>
                                                <center>
                                                    <table id="tblOthers">
                                                    </table>
                                                </center>
                                            </div>
                                        </div>
                                        <div id="divOthersII">
                                            <div>
                                                <center>
                                                    <table id="tblOtherII">
                                                    </table>
                                                    <br />
                                                    <div style="background-color: #ffb99e">
                                                        <table id="tblBankDDl"></table>
                                                    </div>
                                                </center>
                                            </div>
                                        </div>
                                        <center>
                                            <div>
                                                <input type="button" id="btnSave" value="Save" class="btn" />
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
