<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Tablets_PaySlipView.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Frm_Tablets_PaySlipView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />

    <script src="../../JScript/Service_CRM/PaySlip_JS/Payslip_PrintJS.js" type="text/javascript"></script>
    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/PaySlipView.css" rel="stylesheet" />
    <style media="print" type="text/css">
        .noPrnCtrl {
            display: none;
        }

        .none {
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <div style="width: 100%">
            <center>
                <div id="MainDiv" runat="server" style="width: 95%; border: 1px solid black">
                    <div style="width: 100%; height: 80px;">
                        <div style="width: 20%; float: left">
                            <div style="height: 60px">
                                <img src="../../Images/tablet.png" width="200px"/>
                            </div>
                        </div>
                        <div style="width: 80%; float: left">
                            <div>
                                <table width="75%">
                                    <tr>
                                        <th align="left">
                                            <%--<b style="font-family: Verdana; font-size: 9px; text-decoration: underline">SALARY SLIP FOR:</b>--%>
                                            <asp:Label ID="lblfor" runat="server" style="font-family: Verdana; font-size: 9px;font-weight:bold;text-decoration: underline"></asp:Label> 
                                        </th>

                                    </tr>
                                    <tr>
                                        <td>
                                            <table>

                                                <tr>
                                                    <td>
                                                        <b style="font-family: Verdana; font-size: 9px">Name:</b>
                                                      <asp:Label ID="lblsf" runat="server" style="font-family: Verdana; font-size: 9px;font-weight:bold"></asp:Label> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">Department:</span>
                                                        <asp:Label ID="lbldep" style="font-family: Verdana; font-size: 9px" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">Designation: </span>
                                                        <asp:Label ID="lbldes" runat="server" style="font-family: Verdana; font-size: 9px"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>

                                                        <span style="font-family: Verdana; font-size: 9px">Employee No: </span>
                                                          <asp:Label ID="lblemp" runat="server" style="font-family: Verdana; font-size: 9px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">PF No: </span>
                                                         <asp:Label ID="lblpf" runat="server" style="font-family: Verdana; font-size: 9px" ></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">DOJ:</span>
                                                         <asp:Label ID="lbldoj" runat="server" style="font-family: Verdana; font-size: 9px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td>
                                            <table>
                                                <tr>
                                                    <td>

                                                        <span style="font-family: Verdana; font-size: 9px">Bank Name: </span>
                                                         <asp:Label ID="lblbank" runat="server" style="font-family: Verdana; font-size: 9px" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">Bank Acc No:  </span>
                                                          <asp:Label ID="lblacc" runat="server" style="font-family: Verdana; font-size: 9px"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">Bank IFS Code:</span>
                                                           <asp:Label ID="lblifs" runat="server" style="font-family: Verdana; font-size: 9px" ></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>

                                                        <span style="font-family: Verdana; font-size: 9px">UAN No:  </span>
                                                        <asp:Label ID="lbluan" runat="server" style="font-family: Verdana; font-size: 9px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style="font-family: Verdana; font-size: 9px">ESIC No:  </span>
                                                         <asp:Label ID="lblesi" runat="server" style="font-family: Verdana; font-size: 9px" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        </td>
                                                    </tr>

                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                
                            </div>
                        </div>
                    </div>
                    <table  class="table table-bordered">
                        <tr>
                            <th colspan="2" style="border-right:2px solid">ATTENDANCE
                            </th>
                            <th colspan="2" style="border-right:2px solid">EARNING
                            </th>
                            <th colspan="2" style="border-right:2px solid">DEDUCTION
                            </th>
                            <th rowspan="2">GROSS PAY
                            </th>
                        </tr>
                        <tr>
                            <th>PARTICULARS
                            </th>
                            <th style="border-right:2px solid">DAYS</th>
                            <th>PARTICULARS
                            </th>
                            <th style="border-right:2px solid">AMOUNT</th>
                            <th>PARTICULARS
                            </th>
                            <th style="border-right:2px solid">AMOUNT</th>


                        </tr>
                        <%--  <tr>
                             <td colspan="6" >
                               <br />

                          </td>
                        </tr>--%>
                        <tr >

                            <td style="border-bottom:none">
                               
                                Month Days 
                            </td>
                            <td align="right" style="border-right:2px solid">
                             
                              <asp:Label ID="lblmnth" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>
                            <td>
                               
                                Basic
                            </td>
                            <td align="right" style="border-right:2px solid">
                             
                               <asp:Label ID="lblbasic" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>
                            <td>
                              
                                PF
                            </td>
                            <td align="right" style="border-right:2px solid">
                              
                              <asp:Label ID="lblpfd" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>
                            <td rowspan="2" align="center" >
                              
                                 <asp:Label ID="lblgross" runat="server" style="font-weight:bold;font-size:12px; text-align:right"></asp:Label>
                            </td>

                        </tr>
                        <tr class="none">

                            <td class="none">Present Days 
                            </td>
                            <td align="right" style="border-right:2px solid"> 
                                  <asp:Label ID="lblpresent" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>
                            <td>HRA
                            </td>
                            <td align="right" style="border-right:2px solid">  <asp:Label ID="lblhra" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>
                            <td>ESI
                            </td>
                            <td align="right" style="border-right:2px solid">  <asp:Label ID="lbles" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                        </tr>
                        <tr>

                            <td>Loss Of Pay
                            </td>
                            <td align="right" style="border-right:2px solid">
                                <asp:Label ID="lblloss" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>


                            <td>City Comp Allowance 
                            </td>
                            <td align="right" style="border-right:2px solid">  <asp:Label ID="lblcity" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                            <td>Prof Tax
                            </td>
                            <td align="right" style="border-right:2px solid">  <asp:Label ID="lblprof" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>
                            <td align="center"><b>TOTAL DEDUCTION</b>
                            </td>
                        </tr>
                        <tr>

                            <td ></td>
                            <td style="border-right:2px solid"></td>


                            <td>Metro Comp Allowance 
                            </td>
                            <td align="right" style="border-right:2px solid">  <asp:Label ID="lblmetro" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                            <td>Tour Advance
                            </td>
                            <td align="right" style="border-right:2px solid">  <asp:Label ID="lbltour" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                             
                            </td>
                            <td rowspan="3" align="center"> <br /> <asp:Label ID="lbltotd" runat="server" style="font-weight:bold;font-size:12px;text-align:right"></asp:Label></td>
                        </tr>
                        <tr>

                            <td></td>
                            <td style="border-right:2px solid"></td>


                            <td>Education Allowance 
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lbledu" runat="server" style="text-align:right;font-weight:bold"></asp:Label> 
                            </td>

                            <td>Income Tax
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lblincome" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                             
                            </td>
                         
                        </tr>
                        <tr>

                            <td></td>
                            <td style="border-right:2px solid"></td>


                            <td >Special Allowance
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lblspecial" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                            <td>Other Deductions 
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lbloth" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                             
                            </td>

                        </tr>
                        <tr>

                            <td></td>
                            <td style="border-right:2px solid"></td>


                            <td>Travel Allowance
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lbltrav" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                            <td>Loans If Any
                            </td>
                            <td align="right" style="border-right:2px solid"> <asp:Label ID="lblloan" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                             
                            </td>
                            <td  align="center"><b>NET SALARY</b>
                            </td>
                        </tr>
                        <tr>

                            <tr>

                            <td></td>
                            <td style="border-right:2px solid"></td>


                            <td>Adv.against statutory Bonus
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lbladv" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                            <td>Loans If Any
                            </td>
                            <td align="right" style="border-right:2px solid"> <asp:Label ID="lbladvloan" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                             
                            </td>
                            <td  align="center"><b>NET SALARY</b>
                            </td>
                        </tr>



                            <td></td>
                            <td style="border-right:2px solid"></td>


                            <td>LTA
                              
                            </td>
                            <td align="right" style="border-right:2px solid"><asp:Label ID="lbllta" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                               
                            </td>

                            <td></td>
                            <td style="border-right:2px solid"></td>
                              <td align="center">
                                 
                              <asp:Label ID="lblnets" runat="server" style="font-weight:bold;font-size:12px;text-align:right"></asp:Label>
                              
                              </td>
                        </tr>
                          <tr>

                            <td></td>
                            <td style="border-right:2px solid"></td>


                            <td>PF Basic
                            </td>
                            <td align="right" style="border-right:2px solid"> <asp:Label ID="lblpfbasic" runat="server" style="text-align:right;font-weight:bold"></asp:Label>
                            </td>

                            <td >
                            </td>
                            <td style="border-right:2px solid">
                             
                            </td>
                               <td>Bank:

                                   <br />
                             Code No./A/C No:
                            </td>
                        </tr>
                    </table>

                    
                  
                </div>
            </center>
            <center>
                <asp:Panel id="tblRecord" Visible="false" runat="server" style="width: 50%; height: 33px; border: 2px solid black; margin-top: 5%; text-align: center; color: Red;">
                    <div>
                        <b>No Records Found</b>
                    </div>
                </asp:Panel>
            </center>
        </div>
    </form>
</body>
</html>
