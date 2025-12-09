<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_ServiceClose.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_ServiceClose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Close The Service</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../../../JScript/CRM_Css/ServiceCRM_CloseCSS.css" rel="stylesheet" type="text/css" />   
    <script src="../../../JScript/CRM/Listed_Dr_ServiceCRM_Close_JS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>
        <br />
        <div>
            <input type="hidden" id="hdnDrCode" />
              <input type="hidden" id="hdnSlNo" />
        </div>
        <center>
        <div>
            <table id="tblServiceClose" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th rowspan="3">
                            Select
                        </th>
                        <th align="left" class="stylespc">
                            <span id="lblDoctorName" class="lblClass">Doctor Name :</span> <span id="txt_DoctorName"
                                class="lblClass" style="font-weight: bold"></span>
                        </th>
                        <th align="left" class="stylespc">
                            <span id="lblspeciality" class="lblClass">Speciality :</span> <span id="txt_Speciality"
                                class="lblClass" style="font-weight: bold"></span>
                        </th>
                        <th></th>
                    </tr>
                    <tr>
                        <th align="left" class="stylespc">
                            <span id="lblCategory" class="lblClass">Category :</span> <span id="txt_Category"
                                class="lblClass" style="font-weight: bold"></span>
                        </th>
                        <th align="left" class="stylespc">
                            <span id="lblQualification" class="lblClass">Qualification :</span> <span id="txt_Qualification"
                                class="lblClass" style="font-weight: bold"></span>
                        </th>
                        <th></th>
                    </tr>
                    <tr>                                          
                        <th style="background-color:#968405">
                            Service Detail
                        </th>
                        <th style="background-color:#968405">
                            Business Given(in Rs/-)
                        </th>
                        <th style="background-color:#968405">
                        Target Amount
                        </th>
                    </tr>                   
                </thead>
           
            </table>
            ​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​​
        </div>
         </center>
        <center>
        <div>
             <input type="button" id="btnCloseService" value="Close Service" class="btn CloseCSS" style="visibility:hidden"/>
             <input type="button" id="btnConfirm" value="Confirm" class="btn CloseCSS" />
             <input type="button" id="btnReject" value="Reject" class="btn CloseCSS" />
        </div>
        </center>
    </div>
    </form>
</body>
</html>
