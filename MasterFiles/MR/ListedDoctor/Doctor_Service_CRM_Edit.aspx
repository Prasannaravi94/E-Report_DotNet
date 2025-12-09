<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Edit.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Edit" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="titlSS">Service-CRM-Edit</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../JScript/DateJs/dist/jquery-clockpicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/DateJs/assets/css/github.min.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">

        //        var j = jQuery.noConflict();
        //        j(document).ready(function () {
        //            j('#datepicker').datepicker({
        //                changeMonth: true,
        //                changeYear: true,
        //                yearRange: "2016:2020",
        //                dateFormat: 'mm/dd/yy'
        //            });

        //            j("#datepicker").datepicker("setDate", new Date());

        //        });
    
    </script>
    <script type="text/javascript">
        function preventMultipleSubmissions() {
            $('#btnProcess').prop('disabled', true);
        }
        window.onbeforeunload = preventMultipleSubmissions;
    </script>
    <link href="../../../JScript/Service_CRM/Crm_Dr_Css_Ob/ServiceCRM_EditCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/CRMService.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../JScript/Service_CRM/Crm_Dr_JS/ListedDr_CRM_EditJS.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#btnPrint").click(function () {

                var DocCode = $("#hdnDrCode").val();
                var Scode = $("#hdnSfCode").val();
                var SlNo = $("#hdnServiceDDL").val();

                showModalPopUp(DocCode, Scode, SlNo);

                // window.location.href = 'Doctor_Service_CRM_Print.aspx?ListedDrCode=' + $.trim(DocCode) + '&S_Code=' + $.trim(Scode) + '&SlNoDr=' + $.trim(SlNo) + '';
            });

            //rows += '<td><a href="Doctor_Service_CRM_Edit.aspx?ListedDrCode=' + data.d[i].Lst_Dr_Code + '&TypeDr=2&S_Code=' + data.d[i].Sf_Code + '&SerNo=' + data.d[i].Dr_Sl_No + '" style="color:black">Click here</a></td>';
        });
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(DocCode, Scode, SlNo) {
            popUpObj = window.open("Doctor_Service_CRM_Print.aspx?ListedDrCode=" + $.trim(DocCode) + "&S_Code=" + $.trim(Scode) + "&SlNoDr=" + $.trim(SlNo),
                                    "ModalPopUp",
                                    "toolbar=no," +
                                    "scrollbars=yes," +
                                    "location=no," +
                                    "statusbar=no," +
                                    "menubar=no," +
                                    "addressbar=no," +
                                    "resizable=yes," +
                                    "width=800," +
                                    "height=600," +
                                    "left = 0," +
                                    "top=0"
                                    );
            popUpObj.focus();

            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s27.postimg.org/b3g2np6oz/loading_12_ook.gif";

                //var ImgSrc = "https://s14.postimg.org/z7zlmgvn5/loading_28_ook.gif";

                var Text = "http://s9.postimg.org/hyt713i5b/Text_Purple.gif";

                $(popUpObj.document.body).append('<div><center><img src="' + Text + '"  alt="" /></center></div><div> <img src="' + ImgSrc + '"  style=" width:150px; height:150px;position: fixed;top: 15%;left:35%;"  alt="" /></div>');

            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <br />
        <div id="background">
            <p id="bg-text">
            </p>
        </div>
        <table id="Table2" runat="server" width="90%">
            <tr>
                <td align="right" colspan="2">
                    <input type="button" id="btnPrint" value="Print" class="BUTTON" style="width: 50px" />
                    <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" Width="40px"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <br />
        <center>
          
          <input type="hidden" id="hdnSerSlNo" />
          <input type="hidden" id="hdnDrCode" />
          <input type="hidden" id="hdn_SlNo" />
          <input type="hidden" id="hdn_Prd_Sl_No" />
          <input type="hidden" id="hdnDr_Name" />
          <input type="hidden" id="hdnDr_Ser_Req" />

          <input type="hidden" id="hdnApprove" />
          <input type="hidden" id="hdnServiceDDL" />
          <input type="hidden" id="hdnServiceType" />
          <input type="hidden" id="hdnCntLink" />

          <input type="hidden" id="hdnSfCode" />
          <input type="hidden" id="hdnSerTypeValue"/>
          <input type="hidden" id="hdnModeType" />
          <input type="hidden" id="hdnChemistCode" />

         <input type="hidden" id="hdnOtherII_SerReqNo" />

          <center>
          <div id="divDrDet" style="width:100%">
          <table id="tblDoc" class="tblDr" style="width:75%">
                 <tr>
                 <td>                     
                        <span id="lblDoctorName" style="font-size:12px"> Doctor Name </span>                       
                    </td>
                    <td>:</td>
                    <td >                  
                       <span id="txt_DoctorName"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                    <td >                       
                         <span id="lblAddress" style="font-size:12px">Address </span>
                    </td>
                     <td>:</td>
                    <td>  
                        <span id="txt_Doctor_Address" style="font-weight:bold;font-size:11px"></span>
                    </td>
                     <td >                   
                        <span id="lblCategory" style="font-size:12px">Category </span>
                    </td>
                     <td>:</td>
                    <td >    
                       <span id="txt_Category" style="font-weight:bold;font-size:11px"></span>
                    </td>
                 </tr>
                    <tr>
                    <td>                       
                       <span id="lblQualification" style="font-size:12px">Qualification </span>
                    </td>
                     <td>:</td>
                    <td>  
                       <span id="txt_Qualification"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                    <td >                       
                          <span id="lblspeciality" style="font-size:12px">Speciality </span>
                    </td>
                     <td>:</td>
                    <td>  
                         <span id="txt_Speciality"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                     <td>                     
                        <span id="lblClass" style="font-size:12px">Class </span>
                    </td>
                     <td>:</td>
                    <td >           
                        <span id="txt_Class"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                </tr>
                 <tr> 
                    <td>                       
                         <span id="lblMobile" style="font-size:12px">Mobile No </span>
                    </td>
                     <td>:</td>
                    <td>       
                       <span id="txt_Mobile" style="font-size:12px"></span>
                    </td>
                   <td>                   
                        <span id="lblEmail" style="font-size:12px">Email </span>
                    </td>
                     <td>:</td>
                    <td>      
                         <span id="txt_Email" style="font-size:12px"></span>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr> 
                 <tr>
                    <td>                     
                       <span id="lblDoctorServiceTime" style="font-size:12px">Service Amount Given to this Dr till Date</span>
                    </td>
                    <td>:</td>
                    <td>                     
                    <%-- <input  type="text" maxlength="100" id="txtService" 
                       onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                        class="textClass" />--%>

                        <span id="txtService" style="font-weight:bold;font-size:11px"></span>

                    </td>
                     <td></td>
                    <td></td>
                    <td></td>
                     <td></td>
                    <td></td>
                    <td></td>
                </tr>
                  <tr>
                 <td >                   
                    <span id="lblBusinessDate" style="font-size:12px">Business given till Date</span>
                 </td>
                   <td>:</td>
                 <td  >        
                       <span id="txtBusinessDate" style="font-weight:bold;font-size:12px"></span>
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
          <div id="divChemist" style="width:100%">
                 <table id="tblChemist" class="tblDr" style="width:75%">
                 <tr>
                 <td>                     
                        <span id="lblChemist" style="font-size:12px">Chemist Name </span>                       
                    </td>
                    <td>:</td>
                    <td >                  
                       <span id="txtChemist_Name"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                    <td >                       
                         <span id="lblAddressCh" style="font-size:12px">Address</span>
                    </td>
                     <td>:</td>
                    <td>  
                        <span id="txtAdddress_Chm" style="font-weight:bold;font-size:11px"></span>
                    </td>
                     <td >                   
                        <span id="Contact" style="font-size:12px">Contact </span>
                    </td>
                     <td>:</td>
                    <td >    
                       <span id="txtContact" style="font-weight:bold;font-size:11px"></span>
                    </td>
                 </tr>
                    <tr>
                    <td>                       
                       <span id="lblPhone_chm" style="font-size:12px">Mobile No </span>
                    </td>
                     <td>:</td>
                    <td>  
                       <span id="txtPhone_No"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                    <td>                       
                       <span id="lblTerritoryChm" style="font-size:12px">Territory</span>
                    </td>
                     <td>:</td>
                    <td>  
                       <span id="txtTerritoyChm"  style="font-weight:bold;font-size:11px"></span>
                    </td>
                     <td></td>
                    <td></td>
                    <td></td>
                </tr>
                
                 <tr>
                    <td>                     
                       <span id="lblSeviceAmt_Chemist" style="font-size:12px">Service Amount Given to this Dr till Date</span>
                    </td>
                    <td>:</td>
                    <td>                     
                    <%-- <input  type="text" maxlength="100" id="txtService" 
                       onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                        class="textClass" />--%>

                        <span id="txtService_AmtChm" style="font-weight:bold;font-size:11px"></span>

                    </td>
                     <td></td>
                    <td></td>
                    <td></td>
                     <td></td>
                    <td></td>
                    <td></td>
                </tr>
                  <tr>
                 <td >                   
                    <span id="lblBusgvTillDate" style="font-size:12px">Business given till Date</span>
                 </td>
                   <td>:</td>
                 <td  >        
                       <span id="txtBus_tillDate" style="font-weight:bold;font-size:12px"></span>
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

        <div id="divService">
            <table class="tblDr" style="width:75%;font-size:12px">
                <tr>
                    <td>
                        <span id="lblSelectService" style="font-size:12px" >Select the Service
                            <span style="color: Red">*</span> </span>

                              <select id="ddlService" class="input-sm">
                            <option>---Select---</option>
                        </select>

                         <input type="button" id="btnUpdate" value="Go" class="btn" /> 
                    </td>  
                    <td>
                    <div id="div_Prdlink">
                    <a id="btnPrd_Link" href="#" title="Doctor Service" class="Service" >Click here to Enter Business
                            against Service</a>
                    </div>
                        
                    </td>                    
                </tr>
                <tr>                    <td >
                <div id="div_TotBus">
                        <span id="lblTotalBus" class="lblClass" style="font-weight: bold; color: #ff229b;
                            font-size: 12px; width: 300px">Total Business Value Against this Service :</span>
                            <span id="txtTotalBus" class="lblClass" style="font-weight: bold; color: #ff5722;
                            font-size: 12px;"></span>
                            </div>
                    </td>                   
                </tr>
            </table>
        </div>
          </center>
       
        </center>
    </div>
    <br />
    <br />
    <center>
    <div id="divBusAga">
        <div id="output_Field_Prd">
        </div>
        <div id="overlay_Prd_Field" class="web_dialog_overlay">
        </div>
        <div id="dialog_Prd_Field" class="web_dialog_Prd">
            <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="f0">
                <tr>
                    <td>
                        <a href="#close" id="btn_Prd_Close" title="Close" class="close">X</a>
                    </td>
                </tr>
            </table>
            <div>
                <div style="float: right">
                    <input type="button" id="btnAdd" value="Add" class="btnAdd" />
                    <input type="button" id="btn_Close" value="Close" class="btnAdd" />
                </div>
                <br />
                <div>
                    <table>
                        <tr>
                            <td align="left" class="stylespc">
                                <span id="lblDrName" class="lblClass">Doctor Name :</span>
                            </td>
                            <td align="left" class="stylespc">
                                <span id="txtDrName" class="lblClass" style="font-weight: bold; color: #ff229b; font-size: 12px">
                                </span>
                            </td>
                            <td>
                                <span id="lblDrSpeciality" class="lblClass">Speciality :</span>
                            </td>
                            <td>
                                <span id="txtDrSpeclty" class="lblClass" style="font-weight: bold; color: #ff229b;
                                    font-size: 12px"></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="stylespc">
                                <span id="lblDrCategory" class="lblClass">Category :</span>
                            </td>
                            <td align="left" class="stylespc">
                                <span id="txtDrCategory" class="lblClass" style="font-weight: bold; color: #ff229b;
                                    font-size: 12px"></span>
                            </td>
                            <td>
                                <span id="lblDrQual" class="lblClass">Qualification :</span>
                            </td>
                            <td>
                                <span id="txtDrQual" class="lblClass" style="font-weight: bold; color: #ff229b; font-size: 12px">
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span id="lblMonth" class="lblClass">Month</span>
                            </td>
                            <td align="left">
                                <select id="ddlMonth" class="input-sm">
                                    <option value="1">Jan</option>
                                    <option value="2">Feb</option>
                                    <option value="3">Mar</option>
                                    <option value="4">Apr</option>
                                    <option value="5">May</option>
                                    <option value="6">Jun</option>
                                    <option value="7">Jul</option>
                                    <option value="8">Aug</option>
                                    <option value="9">Sep</option>
                                    <option value="10">Oct</option>
                                    <option value="11">Nov</option>
                                    <option value="12">Dec</option>
                                </select>
                            </td>
                            <td align="left">
                                <span id="lblYear" class="lblClass">Year</span>
                            </td>
                            <td align="left">
                                <select id="ddlYear" class="input-sm">
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div>
                    <div class="wrapper">
                        <div class="container">
                            <table id="tblProductQty">
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </center>
    <center>
     <div id="divVisit">
        <div>
            <p style="font-family:Sans-Serif;font-weight:bold;font-size:16px;color:Green" class="TextUnder">Visit - Last Three Months</p>
        </div>
       
        <br />
          
         <table width="80%" align="center">
                    <asp:Table ID="tbl" runat="server" class="table table-bordered table-striped" Width="87%">
                    </asp:Table>                 
         </table>
         </div>
     <br />
     <div id="divPrdDet">
     <table id="tblProduct" cellpadding="5" cellspacing="5" >
            <tr>
            <td>
                <div>
                    <table id="tblCurrentSupport"  class="table table-bordered table-striped" style="width:45%">
                    </table>
                </div>
            </td>
            <td style="margin-left:3%">
                <div style="margin-left:3%">
                    <table id="tblPotentialProduct" class="table table-bordered table-striped" 
                    style="width:50%" >
                    </table>
                </div>
            </td>
            </tr>
        </table>
     </div>
          
    </center>
    <br />
    <div style="margin-left: 10%" id="divBusReturn">
        <table id="tblTotal">
            <tr>
                <td>
                    <span id="lblTotalBussiness" style="font-size: 12px">Total Business Return Expected
                        the Doctor in Amt(Rs/-)<br />
                        (Target Amount)</span>
                </td>
                <td>
                    <input type="text" maxlength="7" id="txtTBusinessRAmt" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                        class="textClass" />
                </td>
            </tr>
            <tr>
                <td>
                    <span id="lblROI" style="font-size: 12px">ROI Duration Month</span>
                </td>
                <td>
                    <select id="ddlROI" class="input-sm" style="font-size: 12px">
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <span id="lblBillType" style="font-size: 12px">Bill Type</span> <span style="color: Red">
                        *</span> </span>
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
                    <span id="lblServiceRq" style="font-size: 12px">Service Required <span style="color: Red">
                        *</span> </span>
                </td>
                <td>
                    <input type="text" maxlength="100" id="txtServiceRq" onkeypress="AlphaNumeric_withPartofSpecialChar(event);"
                        class="textClass" style="width: 420px; height: 45px" />
                    <a id="btnField_Link" href="#" title="Reason Status" class="Service">Click here to Enter
                        Details</a>
                </td>
            </tr>
            <tr>
                <td>
                    <span id="lblServiceAmt" style="font-size: 12px">Service Amount <span style="color: Red">
                        *</span> </span>
                </td>
                <td>
                    <input type="text" maxlength="7" id="txtServiceAmt" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                        class="textClass" />
                </td>
            </tr>
            <tr>
                <td>
                    <span id="lblSpecAct" class="lblClass" style="width: auto">Specific Activities (Remarks)</span>
                </td>
                <td>
                    <input type="text" maxlength="100" id="txtSpecAct" onkeypress="AlphaNumeric_withPartofSpecialChar(event);"
                        class="textClass" style="width: 420px; height: 45px" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divOutlet">
                        <span id="lblPrescription" class="lblClass" style="width: auto">Prescription Outlets
                            (Chemist)<span style="color: Red">*</span></span>
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
                    <span id="lblStockist" class="lblClass" style="width: auto">Stockist <span style="color: Red">
                        *</span></span>
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
        <div id="divProcess">
            <input type="button" id="btnProcess" value="Update" class="btn"/>
           <%-- <input type="button" id="btnClose" value="Close" class="btn" />--%>

            <input type="button" id="btnReject" value="Reject" class="btn" />
           
         <%--   <input type="button" id="btnConfirm" value="Conform" class="btn"/>
            <input type="button" id="btnSanction" value="Sanction" class="btn" />--%>
        </div>
    </center>
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
                                        <span id="lblSer_DrName" class="lblClass">Doctor Name :</span>
                                    </td>
                                    <td align="left" class="stylespc">
                                        <span id="txtSer_Dr_Name" class="lblClass" style="font-weight: bold; color: #ff229b;
                                            font-size: 12px; width: 300px"></span>
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
                            <div class="wrapper_1">
                                <div class="container_1">
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
                                         </center>
                                        <div>
                                            <table id="tblTravel" class="table table-bordered table-striped" style="width: 100%">
                                            </table>
                                        </div>
                                        <span id="Detail" class="lblClass" style="width: 400px; font-size: 14px; font-weight: bold;
                                            color: Maroon">Details of Members Expected To Travel</span>
                                        <div>
                                            <table id="tblDetail" class="table table-bordered table-striped" style="width: 100%">
                                            </table>
                                        </div>
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
                                    <div id="divOthersII">
                                        <div>
                                            <center>
                                                    <table id="tblOtherII">
                                                    </table>
                                                       <br />
                                                <div style="background-color:#ffb99e">
                                                    <table id="tblBankDDl"></table>
                                                </div>
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
            </div>
        </div>
    </div>
    <center>
            <div id="divReject" >
                <div id="output_Field_Reject">
                </div>
                <div id="overlay_Reject_Field" class="web_dialog_overlay">
                </div>
                <div id="dialog_Reject_Field" class="web_dialog_Prd" style="width:30%;height:150px">
                    <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="f0">
                        <tr>
                            <td>
                                <a href="#close" id="btn_Close_Reject" title="Close" class="close">X</a>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div style="float: right">
                            <input type="button" id="btn_RejectOk" value="Ok" class="btnAdd" />
                            <input type="button" id="btnCloseRe" value="Close" class="btnAdd" />
                        </div>
                        <br />
                        <br />
                        <div>
                            <table>
                                <tr>
                                    <td align="left" class="stylespc">
                                        <span id="lblRejectReason" class="lblClass" style="font-weight:bold;font-size:11px;color:#ca1515">Reject Reason :</span>
                                    </td>
                                    <td align="left" class="stylespc">                                        
                                        <%--  <input type="text"  maxlength="100" id="txtRejectReason" onkeypress="AlphaNumeric_withPartofSpecialChar(event);"
                            class="textClass" style="width: 200px; height: 45px" />    --%>                                  
                                        <textarea id="txtRejectReason" class="textClass" style="width: 230px; height: 100px;display:block"></textarea>
                                    </td>                                   
                                </tr>                                                                
                            </table>
                        </div>                       
                    </div>
                </div>
            </div>
        </center>
    <div id="shader" class="shader">
        <div id="loading" class="bar">
            <p>
                loading</p>
        </div>
    </div>
    <%--  <div class="modal">
        <img src="https://s2.postimg.org/l99kqyrk9/loading_9_k.gif" style="width: 150px;
            height: 150px; position: fixed; top: 45%; left: 45%;" alt="" />
    </div>--%>
    </form>
</body>
</html>
