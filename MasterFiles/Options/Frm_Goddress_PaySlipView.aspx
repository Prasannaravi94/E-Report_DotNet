<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Goddress_PaySlipView.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Frm_Goddress_PaySlipView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>   
    <link href="../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />  
    <script src="../../JScript/Service_CRM/PaySlip_JS/Goddress_Pay_JS.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/PaySlip_JS/Payslip_PrintJS.js" type="text/javascript"></script>
    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/PaySlipView.css" rel="stylesheet" />
     <style media="print" type="text/css">
        .noPrnCtrl
        {
            display: none;
        }
    </style>        
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%">
    <center>
        <div id="MainDiv" style="width: 70%; border: 1px solid black">
            <div style="width: 100%; height: 80px;">
                <div style="width: 15%; float: left">
                    <div style="height: 75px">
                      <img src="../../Images/GoddressImg/Goddress.jpg" />
                    </div>
                </div>
                <div style="width: 75%; float: left">
                    <div>
                        <center><b style="font-family:Calisto MT;font-size:11px">GODDRES PHARMACEUTICALS PVT. LTD.</b><br />
                        <b style="font-family:Verdana;font-size:10px">Unit No.304, 3rd Floor , A Wing , Western Edge II, Near Western Express Highway,
                            Borivali (East),</b>
                        <br />
                        <b style="font-family:Verdana;font-size:10px">Mumbai – 400066, Maharashtra </b>
                        <br />
                        <b style="font-family:Verdana;font-size:10px"><span id="PayM"></span></b></center>
                    </div>
                </div>
            </div>
            <div>
                <table id="tablePay" class="table table-bordered table-striped" style="width: 100%;">
                    <tr>
                        <td>
                            <div style="width: 100%">
                                <table id="tblEmpDet" class="table">
                                </table>
                            </div>
                        </td>
                        <td>
                            <div style="width: 100%">
                                <table id="tblBank" class="table">
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tblEarning" class="table">
                            </table>
                        </td>
                        <td>
                            <table id="tblDeduct" class="table">
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tblEarnTotal" class="table">
                            </table>
                        </td>
                        <td>
                            <table id="tblDeductTot" class="table">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: left">
                <b style="font-family: Verdana; font-size: 10px">Net Salary:<span id="NetSalary"></span></b><hr
                    style="border: 1px thin black" />
                <b style="font-family: Verdana; font-size: 10px"><span id="NetRs"></span></b><hr />
            </div>
            <div>
                <center><b style="font-family: Verdana; font-size: 10px">Note: This is a computer generated statement, hence does not require any signature</b></center>
            </div>
            <br />
            <br />
            <br />
        </div>
    </center>
      <center>
            <div id="tblRecord" style="width: 50%; height: 33px; border: 2px solid black; margin-top: 5%;
                text-align: center; color: Red">
                <div>
                    <b>No Records Found</b>
                </div>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
