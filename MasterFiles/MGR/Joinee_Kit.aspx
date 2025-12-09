<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Joinee_Kit.aspx.cs" Inherits="MasterFiles_MGR_Joinee_Kit" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NEW JOINEE KIT REQUSITION FORMAT</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" />

    <%--    <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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

                 maxDate:0,
                 dateFormat: 'dd/mm/yy'

             });

         });
    </script>

    <script type="text/javascript">
        function PrintGridData() {
            // alert('test');
            var prtGrid = document.getElementById('<%=pnlContents.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
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
    <script type="text/javascript">
        $(function () {
            var date = new Date();

            var currentDate = date.getDate();
            var currentMonth = date.getMonth();
            var currentYear = date.getFullYear();
            $('#txtbox').datepicker({
                maxDate: 0,
                dateFormat: 'dd/mm/yy'

            });
        });
    </script>
    <style type="text/css">
        .space {
            padding: 5px 5px;
        }

        .auto-style1 {
            padding: 5px 5px;
            height: 34px;
        }

        #disablelabel [type="checkbox"]:not(:checked) + label, #disablelabel [type="checkbox"]:checked + label {
            color: white;
        }
    </style>
</head>
<body style="background: white">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>

        <asp:Panel ID="pnlbutton" runat="server" Visible="false">
            <br />
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
            <br />
        </asp:Panel>
        <div class="container home-section-main-body position-relative clearfix" >
            <div class="row justify-content-center" style="overflow-x:auto">
                <div class="col-lg-12">

                    <asp:Panel ID="pnlContents" runat="server" Width="100%">


                        <div align="center">
                            <asp:Label ID="lblhead" runat="server" Text="NEW JOINEE KIT REQUISITION FORMAT" Visible="false" CssClass="reportheader"></asp:Label>
                        </div>
                        <br />
                        <div>
                            <center>
                            <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
                                <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
                            </asp:Panel>
                         </center>



                            <table width="100%">


                                <tr>


                                    <td>
                                        <center>
             <table width="80%" border="0" cellpadding="5" cellspacing="5">
                  
                
                <tr>
                    <td class="space" align="left">
                        <asp:Label ID="Lblname" CssClass="label"  runat="server" Width="180px">Name Of The Candidate :<span style="Color:Red;padding-left:2px">*</span> </asp:Label>
                     </td>
                    
                    <td class="space" align="left">
                         <asp:Label ID="lblnameNC" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                         <div class="single-des">
                        <asp:TextBox ID="txtName" runat="server" Width="300px" 
                            onkeypress="AlphaNumeric_NoSpecialChars(event)" CssClass="input">
                        </asp:TextBox>
                             </div>
                       
                    </td>
                    </tr>

                  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lbldesig" CssClass="label"  runat="server" >Designation : <span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                     </td>
                    <td class="space" align="left">
                         <asp:Label ID="lbldesiNC" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                         <div class="single-des">
                        <asp:TextBox ID="txtdesig" runat="server" Width="300px" CssClass="input"
                            onkeypress="AlphaNumeric_NoSpecialChars(event)" >
                        </asp:TextBox>
                             </div>
                        
                    </td>
                    </tr>
                  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblhq" CssClass="label"  runat="server" >Name Of the HQ For Which Selected :<span style="Color:Red;padding-left:2px;">*</span> </asp:Label>
                     </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblhqNC" runat="server" CssClass="label" Text="" visible="false"></asp:Label>
                          <div class="single-des">
                        <asp:TextBox ID="txthq" runat="server" Width="300px" CssClass="input"
                            onkeypress="AlphaNumeric_NoSpecialChars(event)" >
                        </asp:TextBox>
                              </div>
                        
                    </td>
                    </tr>

                 <tr >
                    <td class="space" align="left">
                        <asp:Label ID="lbl" CssClass="label"  runat="server" >Joining Date(dd/mm/yyyy):<span style="Color:Red;padding-left:2px;">*</span></asp:Label>
                     </td>
                    <td class="space" align="left" >
                          <asp:Label ID="lbldate" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                          <div class="single-des">
                      <asp:TextBox ID="txtEffFrom" runat="server" CssClass="DOBfROMDate input" Width="130px" onkeypress="Calendar_enter(event)"                     
                        TabIndex="10"></asp:TextBox>
                  <%--    <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                          CssClass="cal_Theme1" runat="server" />--%>
                              </div>
                      
                    </td>
                    </tr>
               
                 <tr id="disablelabel">
                      <td class="space" align="left" colspan="2">
                          <div style="float:left;width:4%"><asp:Label ID="lblticket" CssClass="label"  runat="server" >Please</asp:Label></div>
                           <div style="float:left;width:3%"><asp:CheckBox ID="chktick" runat="server" Checked="true"  Text="."  /></div>
                           <div style="float:left;width:20%"><asp:Label ID="lblb" CssClass="label"  runat="server" >Below The Items Required:</asp:Label></div>
                     
                         
                     
                      </td>
                     
                 </tr>

                  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblwrk" CssClass="label"  runat="server" >1.WORK BAG :</asp:Label>
                     </td>
                    <td class="space" align="left">
                         <asp:Label ID="lblwrkbag" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdowrk"  runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes "></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    </tr>

                 <tr>
                    <td class="auto-style1" align="left">
                        <asp:Label ID="lblsample" CssClass="label"  runat="server" >2.SAMPLES :</asp:Label>
                     </td>
                    <td class="auto-style1" align="left">
                         <asp:Label ID="lblsamples" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdosample" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes " ></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    </tr>

                  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblstat" CssClass="label"  runat="server" >3.STATIONARY :</asp:Label>
                     </td>
                    <td class="space" align="left">
                         <asp:Label ID="lblstationary" runat="server" CssClass="label" Text="" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdostationary" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes " ></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    </tr>

                 <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblvisual" CssClass="label"  runat="server" >4.VISUAL AID:</asp:Label>
                     </td>
                    <td class="space" align="left">
                        <asp:Label ID="lblvisuall" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdovisualaid"  runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes " ></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                         
                    </td>
                    </tr>

                  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblglossaryy" CssClass="label"  runat="server" >5.PRODUCT GLOSSARY CARDS:</asp:Label>
                     </td>
                    <td class="space" align="left">
                         <asp:Label ID="lblglossary" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdoglossary" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes " ></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    </tr>

                 <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lblre" CssClass="label"  runat="server" >6.READY RECKONER:</asp:Label>
                     </td>
                    <td class="space" align="left">
                         <asp:Label ID="lblreckoner" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdoreck"  runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes " ></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    </tr>


                  <tr>
                    <td class="space" align="left">
                        <asp:Label ID="lbladd" CssClass="label"  runat="server" >7.ADDRESS FOR SAMPLE REQUEST:</asp:Label>
                     </td>
                    <td class="space" align="left">
                        <asp:Label ID="lbladdress" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                          <div class="single-des">
                       <asp:TextBox ID="txtadd" runat="server" 
                    Width="400px" Height="200px" CssClass="input"  
                      MaxLength="250" TextMode="MultiLine" ></asp:TextBox>
                              </div>
                          
                    </td>
                    </tr>
                  </table>
            <br/>
            <table>
                 <tr>
                     <td>
 <asp:Label ID="lblvacancy" CssClass="label"  runat="server" >If Vacancy Is Being Filled Up For Existing HQ Indicate The Following:</asp:Label>
                         </td>
                     </tr>

                </table>
            <br/>
            <table width="80%" border="0" cellpadding="5" cellspacing="5">
                <tr><td class="space" align="left" Width="400px">
                        <asp:Label ID="Label1" CssClass="label"  runat="server" >1.Name of TM/ABM/RBM Being Replaced:</asp:Label>
                     </td>
                     <td class="space" align="left">
                           <asp:Label ID="lblreplace" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                           <div class="single-des">
                        <asp:TextBox ID="txtreplaced" runat="server" Width="300px" 
                            onkeypress="AlphaNumeric_NoSpecialChars(event)" CssClass="input">
                        </asp:TextBox>
                               </div>
                         
                    </td>
                    </tr>
                <tr><td class="space" align="left">
                        <asp:Label ID="Label2" CssClass="label"  runat="server" >2.Whether Company Property Collected(Do Not Collect Materials if they are more than 30 days old):</asp:Label>
                     </td>
                     <td class="space" align="left">
                         <asp:Label ID="lblcompany" runat="server" Text="" CssClass="label" visible="false"></asp:Label>
                       <asp:RadioButtonList ID="rdocompany"  runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Text=" Yes " ></asp:ListItem>
                            <asp:ListItem Value="N" Text=" No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                         
                    </td>
                    </tr>
               
                
               
                </table>
            <br/>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnsave_click"/>
                     </td>
                    </tr>
                </table>
            </center>
                                    </td>
                                </tr>
                            </table>
                            </center>
                            <br />
                            <center>
            <table id="tbl" runat="server" width="80%" visible="false">
                <tr>
                    <td style="width:30px">
                        </td>
                    <td align="left" >
                        <asp:Label ID="Label3" CssClass="label"  runat="server" Font-Bold="true" ForeColor="#292a34" >FORM SUBMITTED DATE:</asp:Label>&nbsp;
                      <U> <asp:Label ID="lblsubmi" runat="server" CssClass="label" ForeColor="#292a34" Text=""></asp:Label></U>
                         </td>
                    <td>
                       
                         <%--<asp:TextBox ID="txtbox" runat="server" SkinID="MandTxtBox" Width="90px" Height="22px"
                        CssClass="TEXTAREA" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                        TabIndex="10"></asp:TextBox>--%>
                        
                        </td>
                    <td align="left">
                        <asp:Label ID="Label4" CssClass="label"  runat="server" Font-Bold="true" ForeColor="#292a34" >SEND BY:</asp:Label>
                        </td>
                    <td class="space" align="left">
                        <U>  <asp:Label ID="lblnameofmgr" runat="server"  CssClass="label" ForeColor="#292a34" Font-Bold="true" Text=""></asp:Label></U>
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
