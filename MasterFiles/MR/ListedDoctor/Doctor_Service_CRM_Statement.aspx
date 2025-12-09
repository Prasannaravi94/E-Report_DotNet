<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Statement.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Statement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="65%" border="0" cellpadding="3" cellspacing="3" align="center">            
                <tr>
                    <td align="left" class="stylespc">                     
                        <span id="lblDoctorName" class="lblClass"> Doctor Name :</span>                       
                    </td>
                    <td align="left" class="stylespc">                  
                       <span id="txt_DoctorName" class="lblClass" style="font-weight:bold"></span>
                    </td>
                    <td align="left" class="stylespc">                       
                         <span id="lblAddress" class="lblClass">Address :</span>
                    </td>
                    <td align="left" class="stylespc">  
                        <span id="txt_Doctor_Address" class="lblClass" style="width:300px;font-weight:bold"></span>
                    </td>
                     <td align="left" class="stylespc">                   
                        <span id="lblCategory" class="lblClass">Category :</span>
                    </td>
                    <td align="left" class="stylespc">    
                       <span id="txt_Category" class="lblClass" style="font-weight:bold"></span>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">                       
                       <span id="lblQualification" class="lblClass">Qualification :</span>
                    </td>
                    <td align="left" class="stylespc">  
                       <span id="txt_Qualification" class="lblClass" style="font-weight:bold"></span>
                    </td>
                    <td align="left" class="stylespc">                       
                          <span id="lblspeciality" class="lblClass">Speciality :</span>
                    </td>
                    <td align="left" class="stylespc">  
                         <span id="txt_Speciality" class="lblClass" style="font-weight:bold"></span>
                    </td>
                     <td align="left" class="stylespc">                     
                        <span id="lblClass" class="lblClass">Class :</span>
                    </td>
                    <td align="left" class="stylespc">           
                        <span id="txt_Class" class="lblClass" style="font-weight:bold"></span>
                    </td>
                </tr>
               <tr> 
                    <td align="left" class="stylespc">                       
                         <span id="lblMobile" class="lblClass">Mobile No :</span>
                    </td>
                    <td align="left" class="stylespc">       
                       <span id="txt_Mobile" class="lblClass"></span>
                    </td>
                   <td align="left" class="stylespc">                   
                        <span id="lblEmail" class="lblClass">Email :</span>
                    </td>
                    <td align="left" class="stylespc">      
                         <span id="txt_Email" class="lblClass"></span>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">                     
                       <span id="lblDoctorServiceTime" class="lblClass" style="width:300px">Service Amount Given to this Dr till Date</span>
                    </td>
                    <td align="left" class="stylespc">                     
                    <%-- <input  type="text" maxlength="100" id="txtService" 
                       onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                        class="textClass" />--%>

                        <span id="txtService" class="lblClass" style="font-weight:bold"></span>

                    </td>
                </tr>
                <tr>
                 <td align="left" class="stylespc">                   
                    <span id="lblBusinessDate" class="lblClass" style="width:auto">Business given till Date</span>
                 </td>
                 <td align="left" class="stylespc">                   
                    <%-- <input  type="text" maxlength="100" id="txtBusinessDate" 
                       onkeypress="AlphaNumeric_NoSpecialChars(event);" 
                       class="textClass" />--%>

                       <span id="txtBusinessDate" class="lblClass" style="font-weight:bold"></span>
                 </td>
                </tr>
            </table>


            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <span id="lblTotalBussiness" class="lblClass" style="width: auto">Total Business Return
                            Expected the Doctor in Amt(Rs/-)<br />
                            (Target Amount)</span>
                    </td>
                    <td align="left" class="stylespc">
                        <input type="text" maxlength="100" id="txtTBusinessRAmt" onkeypress="CheckNumeric(event);"
                            class="textClass" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <span id="lblROI" class="lblClass" style="width: auto">ROI Duration Month</span>
                    </td>
                    <td align="left" class="stylespc">
                        <select id="ddlROI" class="input-sm" style="width: 100px">
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
                    <td align="left" class="stylespc">
                        <span id="lblServiceRq" class="lblClass" style="width: auto">Service Required <span
                            style="color: Red">*</span> </span>
                    </td>
                    <td align="left" class="stylespc">
                        <input type="text" maxlength="200" id="txtServiceRq" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            class="textClass" style="width: 420px; height: 45px" />
                        <a id="btnField_Link" href="#" title="Reason Status" class="blink" style="color: Red;
                            font-family: Verdana; font-weight: bold; font-size: 12px">Click here to Enter Details</a>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <span id="lblServiceAmt" class="lblClass" style="width: auto">Service Amount <span
                            style="color: Red">*</span> </span>
                    </td>
                    <td align="left" class="stylespc">
                        <input type="text" maxlength="100" id="txtServiceAmt" onkeypress="CheckNumeric(event);"
                            class="textClass" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <span id="lblSpecAct" class="lblClass" style="width: auto">Specific Activities (Remarks)</span>
                    </td>
                    <td align="left" class="stylespc">
                        <input type="text" maxlength="200" id="txtSpecAct" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            class="textClass" style="width: 420px; height: 45px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <span id="lblPrescription" class="lblClass" style="width: auto">Prescription Outlets
                            (Chemist) <span style="color: Red">*</span></span>
                    </td>
                    <td align="left" class="stylespc">
                        <select id="ddlChemist_1" class="input-sm" style="width: 160px">
                        </select>
                        <select id="ddlChemist_2" class="input-sm" style="width: 160px">
                        </select>
                        <select id="ddlChemist_3" class="input-sm" style="width: 160px">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <span id="lblStockist" class="lblClass" style="width: auto">Stockist <span style="color: Red">
                            *</span> </span>
                    </td>
                    <td align="left" class="stylespc">
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
    </form>
</body>
</html>
