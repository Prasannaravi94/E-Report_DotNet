<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recommendation_For_Confirmation.aspx.cs" Inherits="MasterFiles_MGR_Recommendation_For_Confirmation" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RECOMMENDATION FOR CONFIRMATION</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>

    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.12.4.js"></script>
    <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

         <script type="text/javascript" language="javascript">
             var today = new Date();
             var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
             var year = today.getFullYear() - 1;


             var dd = today.getDate();
             var mm = today.getMonth() + 01; //January is 0!
             var yyyy = today.getFullYear();

             var j = jQuery.noConflict();
             j(document).ready(function () {

                 j('.DOBfROMDate').datepicker
                 ({

                     maxDate: 0,
                     dateFormat: 'dd/mm/yy'

                 });

             });
    </script>

    <script type="text/javascript">
        function PrintGridData() {
            // alert('test');
            var prtGrid = document.getElementById('<%=pnlContents.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
            printWindow.document.getElementById("btnPrint").style.visibility = "hidden";
            printWindow.document.getElementById("btnClose").style.visibility = "hidden";
        }

    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            var date = new Date();

            var currentDate = date.getDate();
            var currentMonth = date.getMonth();
            var currentYear = date.getFullYear();
            $('#txtEffFrom').datepicker({
                maxDate: 0,
                dateFormat: 'dd/mm/yy'

            });
        });
    </script>
    <style type="text/css">
        #firsttable tr td, #secondtable tr td, #secondtable tr {
            border: 1px solid #DCE2E8;
        }

        #firsttable .single-des, #secondtable .single-des {
            margin-bottom: 0px;
        }

        #firsttable [type="checkbox"]:not(:checked) + label, #firsttable [type="checkbox"]:checked + label {
           color:white;
        }
         #secondtable .single-des .input{
             min-width:70px;
         }
    </style>
</head>

<body style="background: white">
    <form id="form1" runat="server">

        <div id="Divid" runat="server">
        </div>

        <asp:Panel ID="pnlbutton" runat="server" Visible="false">
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label5" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>

                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label6" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>


        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div>
                            <div align="center">
                                <asp:Label ID="lblhead" runat="server" Text="CONFIRMATION FORMAT" Visible="false" CssClass="reportheader"></asp:Label>
                            </div>
                            <br />


                            <div class="row justify-content-center">
                                <div class="col-lg-10">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:Label ID="Lblname" CssClass="label" runat="server" Font-Bold="true" ForeColor="#292a34">Name: </asp:Label>
                                            <asp:DropDownList ID="ddlfieldforce" CssClass="nice-select" runat="server" Width="250px" OnSelectedIndexChanged="ddlfield_selectindexchanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblFieldname" runat="server" CssClass="label" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:Label ID="lbldte" CssClass="label" runat="server" Font-Bold="true" ForeColor="#292a34">Date Of Joining: </asp:Label>
                                            <asp:Label ID="lbljoindte" CssClass="label" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblhq" CssClass="label" runat="server" Font-Bold="true" ForeColor="#292a34">HQ: </asp:Label>
                                            <asp:Label ID="lblehq" CssClass="label" runat="server"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblsalary" CssClass="label" runat="server" Font-Bold="true" ForeColor="#292a34">Current Salary: </asp:Label>
                                            <div class="single-des">
                                                <asp:TextBox ID="txtsalary" CssClass="input" runat="server" MaxLength="10" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblsal" runat="server" CssClass="label" Text=""></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblabm" CssClass="label" runat="server" Font-Bold="true" ForeColor="#292a34">Reporting Manager:</asp:Label>
                                            <asp:Label ID="labeladm" CssClass="label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <center>    
                        <br/>
                                 <div class="table-responsive" style="scrollbar-width: thin">
                        <table cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
                <tr>
                    <td >
                        
            <table cellpadding="0" cellspacing="0" border="1" id="firsttable" >
                <tr>
                    <td style="width:300px" align="left">
                       <asp:Label ID="lblcri" CssClass="label" ForeColor="#292a34" runat="server" Font-Bold="true">CRITERIA EVALUATION</asp:Label>
                    </td>
                     <td style="width:100px">
                      <asp:Label ID="lblgud" CssClass="label" ForeColor="#292a34"  runat="server" Font-Bold="true">GOOD</asp:Label>
                    </td>
                    <td style="width:100px">
                      <asp:Label ID="lblavg" CssClass="label" ForeColor="#292a34"  runat="server" Font-Bold="true">AVERAGE</asp:Label>
                    </td>
                     <td style="width:100px">
                      <asp:Label ID="lblpoor" CssClass="label" ForeColor="#292a34"  runat="server" Font-Bold="true">POOR</asp:Label>
                    </td>
                    <td style="width:300px">
                      <asp:Label ID="lblcmts" CssClass="label" ForeColor="#292a34"  runat="server" Font-Bold="true">COMMENTS/FEEDBACK</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
            <asp:Label ID="lbldetail" CssClass="label" runat="server" >1.DETAILING</asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkgudD" runat="server" Text="."/>
                        <asp:Label ID="lblDgud" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkavgD" runat="server" Text="."/>
                          <asp:Label ID="lblDa" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkpoorD" runat="server" Text="."/>
                         <asp:Label ID="lblDp" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>

                      <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtcommentD" CssClass="input"  runat="server" MaxLength="200" Width="250px"  onkeypress="AlphaNumeric_NoSpecialChars(event)"></asp:TextBox> 
                              </div>
                           <asp:Label ID="lblDcomm" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td align="left">
            <asp:Label ID="lblinchamber" CssClass="label"  runat="server" >2.INCHAMBER ACTIVITY</asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkgudC" runat="server" Text="."/>
                         <asp:Label ID="lblCgud" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkavgC" runat="server" Text="."/>
                          <asp:Label ID="lblCavg" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkpoorC" runat="server" Text="."/>
                          <asp:Label ID="lblCpoor" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>

                      <td style="width:230px">
                           <div class="single-des">
                       <asp:TextBox ID="txtcommentC" CssClass="input"  runat="server" MaxLength="200" Width="250px"  onkeypress="AlphaNumeric_NoSpecialChars(event)"></asp:TextBox> 
                               </div>
                          <asp:Label ID="lblCcomment" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td align="left">
            <asp:Label ID="lblwrk" CssClass="label"  runat="server" >3.WORK PUNCTUALITY</asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkgudW" runat="server" Text="."/>
                          <asp:Label ID="lblWgud" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkavgW" runat="server" Text="."/>
                          <asp:Label ID="lblWAvg" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkpoorW" runat="server" Text="."/>
                          <asp:Label ID="lblWpoor" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>

                      <td style="width:230px">
                          <div class="single-des">
                       <asp:TextBox ID="txtcommentW" CssClass="input"  runat="server" MaxLength="200" Width="250px"  onkeypress="AlphaNumeric_NoSpecialChars(event)"></asp:TextBox> 
                              </div>
                           <asp:Label ID="lblWcomment" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td align="left">
            <asp:Label ID="lblreport" CssClass="label"  runat="server" >4.REPORTS PUNCTUALITY</asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkgudR" runat="server" Text="."/>
                           <asp:Label ID="lblRgud" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkavgR" runat="server" Text="."/>
                           <asp:Label ID="lblRAvg" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                     <td>
                        <asp:CheckBox ID="chkpoorR" runat="server" Text="."/>
                           <asp:Label ID="lblRPoor" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>

                      <td style="width:230px">
                            <div class="single-des">
                       <asp:TextBox ID="txtcommentR" CssClass="input" runat="server" MaxLength="200" Width="250px"  onkeypress="AlphaNumeric_NoSpecialChars(event)"></asp:TextBox>
                                </div>
                           <asp:Label ID="lblRcomment" runat="server" Text="" CssClass="label"></asp:Label> 
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" border="1" id="secondtable">
                <tr>
                    <td align="left" style="width:300px">
                       <asp:Label ID="lblperform" CssClass="label"  runat="server" >5.PERFORMANCE DETAILS</asp:Label>
                     </td>
                     <td style="width:85px">
                       <asp:Label ID="lblexis" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">EXISTING</asp:Label>
                     </td>
                     <td style="width:85px">
                       <asp:Label ID="lblfmnth" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">1st Month</asp:Label>
                     </td>
                    <td style="width:85px">
                       <asp:Label ID="lblsmnth" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">2nd Month</asp:Label>
                     </td>
                     <td style="width:85px">
                       <asp:Label ID="lbltmnth" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">3rd Month</asp:Label>
                     </td>
                     <td style="width:85px">
                       <asp:Label ID="lblfrmnth" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">4th Month</asp:Label>
                     </td>
                    <td style="width:85px">
                       <asp:Label ID="lblfvmnth" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">5th Month</asp:Label>
                     </td>
                     <td style="width:85px">
                       <asp:Label ID="lblsxmnth" CssClass="label"  ForeColor="#292a34"  runat="server" Font-Bold="true">6th Month</asp:Label>
                     </td>
                 </tr>
                <tr>
                    <td align="left">
                         <asp:Label ID="lbltar" CssClass="label"  runat="server" >a).TARGET</asp:Label>
                        </td>
                    <td align="left">
                         <div class="single-des">
                       <asp:TextBox ID="txtexttar" CssClass="input" runat="server"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                             </div>
                        <asp:Label ID="lblexttar" runat="server" CssClass="label"></asp:Label>
                        </td>
                     <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtextfmnth" runat="server"  CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                          <asp:Label ID="lblextfmnth" runat="server" CssClass="label"></asp:Label>

                     </td>
                    <td align="left">
                         <div class="single-des">
                     <asp:TextBox ID="txtextsmnth" runat="server"  CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                             </div>
                           <asp:Label ID="lblextsmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                         <div class="single-des">
                       <asp:TextBox ID="txtextthidmnth" runat="server"  CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                             </div>
                         <asp:Label ID="lblextthdmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                         <div class="single-des">
                       <asp:TextBox ID="txtextfrthmnth" runat="server"  CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                             </div>
                         <asp:Label ID="lblextfrthmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                         <div class="single-des">
                       <asp:TextBox ID="txtextfivthmnth" runat="server"   CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                             </div>
                         <asp:Label ID="lblextfivmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                     <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtextsixmnth" runat="server"  CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                          <asp:Label ID="lblextsixmnth" runat="server"  CssClass="label"></asp:Label>
                         </td>
                     </tr>
                <tr>
                    <td align="left">
                         <asp:Label ID="lblachieve"  CssClass="label"  runat="server" >b).ACHIEVEMENT(Primary)</asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                      <asp:TextBox ID="txtacvetar" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachvetar" runat="server"  CssClass="label"></asp:Label>
                        </td>
                     <td align="left">
                           <div class="single-des">
                       <asp:TextBox ID="txtacveext" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                               </div>
                         <asp:Label ID="lblachveext" runat="server"  CssClass="label"></asp:Label>
                         </td>
                    <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtacvefmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachvefmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                      <asp:TextBox ID="txtacvesmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachvesmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                      <asp:TextBox ID="txtacvethrdmnht" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachvethdmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                      <asp:TextBox ID="txtacvefthmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachvefrthmnth" runat="server"  CssClass="label"></asp:Label>
                        </td>
                     <td align="left">
                           <div class="single-des">
                      <asp:TextBox ID="txtacvefvthmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                               </div>
                         <asp:Label ID="lblachvefvthmnth" runat="server"  CssClass="label"></asp:Label>
                         </td>
                    </tr>
                <tr>
                    <td align="left">
                         <asp:Label ID="lblsec" CssClass="label"  runat="server" >c).ACHIEVEMENT(Secondary)</asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtachevsecext" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                         <asp:Label ID="lblachsecext" runat="server" CssClass="label"></asp:Label>
                        </td>
                     <td align="left">
                           <div class="single-des">
                       <asp:TextBox ID="txtachevsecfmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                               </div>
                         <asp:Label ID="lblachsecone" runat="server" CssClass="label"></asp:Label>
                         </td>
                    <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtachevsecsmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachsectwo" runat="server" CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                        <asp:TextBox ID="txtachevsecthdmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachsecthree" runat="server" CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtachevsecfrthmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachsecfour" runat="server" CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                          <div class="single-des">
                       <asp:TextBox ID="txtachevsecfivthmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                              </div>
                        <asp:Label ID="lblachsecfive" runat="server" CssClass="label"></asp:Label>
                        </td>

                     <td align="left">
                           <div class="single-des">
                        <asp:TextBox ID="txtachevsecsixmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                               </div>
                        
                         <asp:Label ID="lblachsecsix" runat="server" CssClass="label"></asp:Label>
                          </td>
                    </tr>
                <tr>
                    <td align="left">
                         <asp:Label ID="lblperformance" CssClass="label"  runat="server" >d).PERFORMANCE</asp:Label>
                        </td>
                    <td align="left">
                       
                        </td>
                     <td align="left">
                         <div class="single-des">
                      <asp:TextBox ID="txtperfomfmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                             </div>
                          <asp:Label ID="lblperfmnth" runat="server" CssClass="label"></asp:Label>
                         </td>
                    <td align="left">
                        <div class="single-des">
                      <asp:TextBox ID="txtperfomsmnth" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                            </div>
                         <asp:Label ID="lblpersec" runat="server" CssClass="label"></asp:Label>
                            </td>
                    <td align="left">
                        <div class="single-des">
                      <asp:TextBox ID="txtperfomthdmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div> <asp:Label ID="lblperthree" runat="server" CssClass="label"></asp:Label>
                            </td>
                    <td align="left">
                        <div class="single-des">
                      <asp:TextBox ID="txtperfomforthmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                       </div>  <asp:Label ID="lblperfour" runat="server" CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                        <div class="single-des">
                      <asp:TextBox ID="txtperfomfivthmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div> <asp:Label ID="lblperfive" runat="server" CssClass="label"></asp:Label>
                        </td>
                     <td align="left">
                         <div class="single-des">
                      <asp:TextBox ID="txtperfomsixthmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div>  <asp:Label ID="lblpersix" runat="server" CssClass="label"></asp:Label>
                         </td>
                    </tr>
                <tr>
                    <td align="left">
                         <asp:Label ID="lblcallavg" CssClass="label"  runat="server" >e).LAST THREE MONTHS CALL AVERAGE</asp:Label>
                        </td>
                     <td align="left">
                       
                        </td>
                     <td align="left">
                         <div class="single-des">
                        <asp:TextBox ID="txtcallfmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div>  <asp:Label ID="lblcallfirst" runat="server" CssClass="label"></asp:Label>
                         </td>
                    <td align="left">
                        <div class="single-des">
                        <asp:TextBox ID="txtcallSmnth" runat="server"  CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div> <asp:Label ID="lblcallsecond" runat="server" CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                        <div class="single-des">
                        <asp:TextBox ID="txtcallthrdmnth" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div> <asp:Label ID="lblcallthird" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                <tr>
                    <td align="left">
                         <asp:Label ID="lblmarketavg" CssClass="label"  runat="server" >e).LAST THREE MONTHS MARKET COVERAGE</asp:Label>
                        </td>
                         <td align="left">
                       
                        </td>
                     <td align="left">
                         <div class="single-des">
                        <asp:TextBox ID="txtmarcov" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                         </div> <asp:Label ID="lblmarfirst" runat="server" CssClass="label"></asp:Label>
                         </td>
                    <td align="left">
                        <div class="single-des">
                        <asp:TextBox ID="txtmarcovsec" runat="server" CssClass="input"  onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div> <asp:Label ID="lblmarsecond" runat="server" CssClass="label"></asp:Label>
                        </td>
                    <td align="left">
                        <div class="single-des">
                         <asp:TextBox ID="txtmarcovthrd" runat="server" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="40"></asp:TextBox>
                        </div> <asp:Label ID="lblthird" runat="server" CssClass="label"></asp:Label>
                        </td>
                    
                    
                    </tr>
                </table>
                            
                        </td>
                    </tr>
                </table>
                                     </div>
                        <table>
                <tr>
                    <td align="left" >
                   <asp:Label ID="lblrecomm" CssClass="label"  ForeColor="#292a34"  runat="server" >Recommendation:</asp:Label>
                        </td>
                    <td width="500px">
                        </td>
                    </tr>
                <tr>
                     <td align="right">
                   <asp:Label ID="lbllrecomm" CssClass="label" runat="server" >Based on the above evaluation I recommend Confirmation with effect from:</asp:Label>
                        </td>
                    <td  align="left">
                        <div class="single-des">
                        <asp:TextBox ID="txtEffFrom" runat="server"  Width="90px" 
                        CssClass="DOBfROMDate input"  onkeypress="Calendar_enter(event)" 
                        TabIndex="10"></asp:TextBox>
                            </div>
                      <u>  <asp:Label ID="lblcondte" runat="server" Text="" CssClass="label"></asp:Label></u>
                        </td>
                </tr>
                </table>
                        <table>
                <tr>
                    <td>
                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnsave_click" />
                        </td>
                    </tr>
                </table>
                           </center>
                            <br />
                            <center>
                        <table id="tbl" runat="server" width="80%" visible="false">
                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" CssClass="label" ForeColor="#292a34" runat="server" Font-Bold="true" >FORM SUBMITTED DATE:</asp:Label>&nbsp;
                        <U><asp:Label ID="lblsubdte" CssClass="label" ForeColor="#292a34"  runat="server" Font-Bold="true" Text=""></asp:Label></U>

                        </td>
                    <td align="left">
                        <asp:Label ID="Label4" CssClass="label" ForeColor="#292a34"  runat="server" Font-Bold="true" >SEND BY:</asp:Label>
                        </td>
                      <td class="space" align="left">
                        <U>  <asp:Label ID="lblnameofmgr" runat="server"  CssClass="label" ForeColor="#292a34"  Font-Bold="true" Text=""></asp:Label></U>
                          </td>
                    </tr>
                </table>
                      </center>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
</body>
</html>
