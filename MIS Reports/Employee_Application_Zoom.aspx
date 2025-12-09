<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee_Application_Zoom.aspx.cs" Inherits="MIS_Reports_Employee_Application_Zoom" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personal Detail View</title>
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" />

    <script src="../assets/js/jQuery.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/jquery.nice-select.min.js"></script>
    <script src="../assets/js/main.js"></script>

    <style type="text/css">
        .auto-style1 {
            width: 933px;
        }

        .vertical {
            writing-mode: bt-lr;
            -webkit-transform: rotate(270deg);
            -moz-transform: rotate(270deg);
            -o-transform: rotate(270deg);
            -ms-transform: rotate(270deg);
            transform: rotate(270deg);
            white-space: nowrap;
            display: block;
            bottom: 0;
            width: 45px;
        }

        table, table tr td {
            border-color: #DCE2E8;
        }
    </style>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSFCode]');
            var $items = $('select[id$=ddlSFCode] option');

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

    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <br />
        <br />

        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row" align="right">
                <div class="col-lg-12">
                    <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>
            <br />
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-12">

                    <h2 class="text-center">
                        <asp:Label ID="lbl_name" runat="server">Personal Entry Form</asp:Label></h2>

                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lbl_fieldforce" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtNew" runat="server" CssClass="input"
                                        ToolTip="Enter Text Here"></asp:TextBox>
                                    <asp:DropDownList ID="ddlSFCode" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                    </asp:DropDownList>

                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btn_go" runat="server" Text="GO" CssClass="savebutton" OnClick="btn_go_click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <table id="tablenorec" runat="server" visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="lblnorec" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="20px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <center>
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">
                             <asp:Panel ID="pnlhidden" runat="server" Visible="false" class="single-des">

                    <asp:panel ID="pnl_hide2" runat="server"  Visible="false">
                           <table width="940px" style="background-color:#ffe6e6">
                                  <tr>
                                    <td>
                  
        
                                 <table align="right" border="1" style="width:100%">
                                      <tr>
                     
                                      <td>
                        
                                     <table>
                                         <tr>
                                         <td>
                                          <asp:Label ID="lblh" runat="server" Text="" Font-Bold="true" Font-Size="20px"></asp:Label>
                                          </td>
                      
                                           </tr>
                                           <tr>
                     <td align="center">
                      <asp:Label ID="add1" runat="server" Text="" Font-Bold="true" Width="200px"></asp:Label><br/>
                      <asp:Label ID="addd2" runat="server" Text="" Font-Bold="true" Width="200px"></asp:Label>
                     </td>
                     </tr>
                         </table>
                    </td>
                     <td>
                         <table align="right">
                             <tr><td>
                                 <table>
                                     <tr>
                                         <td>
                                         <asp:DataList ID="DataList1" runat="server" HorizontalAlign="center">
                            <ItemTemplate>
                                <table><tr>
                                   
                                    <td><asp:Image ID="imgHome" ImageUrl='<%# Eval("Photogragh") %>' width="35mm" Height="45mm" ImageAlign="Top"
                                        runat="server" />
                                </td></tr></table>
                            </ItemTemplate>
                        </asp:DataList>
                                         </td>

                                     </tr>
                                     </table>
           
                                 </td>
                                 </tr>
                             </table>
                         </td>
                 </tr>
                 </table>
        
                      
                      </td>
               </tr>
            </table>

        </asp:Panel>

              <asp:Panel ID="pnlhide" runat="server">
            <table width="940px">
                <tr>

                    <td >
                        <table border="1" width="100%">
                            <tr>

                                <td>01.Please tick in the &nbsp;<asp:Label ID="txt" runat="server" Width="20px"></asp:Label>wherever applicable</td>
                            </tr>
                            <tr>

                                <td>*Have you been referred by any of the  VIVO Employee?
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal"
                                        Font-Names="Calibri">
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>If Yes, Please mention the Employee Name
                                <asp:Label ID="txt_emp_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label>&nbsp;and Employee Code<asp:Label ID="txt_emp_code" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="100px" Height="25px"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>

                                    <table align="center">
                                        <tr>
                                            <td class="auto-style5">
                                                <table width="100%" border="1">
                                                    <tr>
                                                        <td style="background-color: #ccccff">
                                                            <asp:Label ID="perdts" runat="server" Text="PERSONAL DETAILS" CssClass="vertical" Font-Bold="true"></asp:Label></td>
                                                        <td class="auto-style2">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td >
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left" class="auto-style3">NAME IN FULL(in block letters)<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="txt_full_name" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="auto-style4">PAN Card No:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="panno" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                                <td align="left">Aadhar card Number:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="adrno" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="auto-style4">Name as per Aadhar Card:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="adrname" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                                <td align="left">Current Bank  Account No :<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="accno" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="auto-style4">Current Bank Name :<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="bnknme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                                <td align="left">IFSC Code :<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="ifsccode" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="auto-style4">Name of the Branch :<span style="color: Red;padding-left:2px">*</span></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="branchname" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <table border="1" style="width: 100%">
                                                                            <tr>
                                                                                <td >
                                                                                    <table style="width: 430px">
                                                                                        <tr>
                                                                                            <td align="center" class="auto-style17"><b>ADDRESS FOR COMMUNICATION</b></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="auto-style17">
                                                                                                <label id="add_cmtn" runat="server" cols="55" rows="4" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="auto-style17" align="left">Mobile no:<span style="color: Red;padding-left:2px">*</span> &nbsp;<asp:Label ID="mob_no" runat="server" Text="" Width="120px" onkeypress="CheckNumeric(event);" Height="27px"></asp:Label>&nbsp;&nbsp;Tel no:<asp:Label ID="tel_no" runat="server" Text="" Width="120px" Height="25px"></asp:Label></td>
                                                                                            <tr>
                                                                                                <td class="auto-style18" align="left"> Personal Email Id :<span style="color: Red;padding-left:2px">*</span> &nbsp;<asp:Label ID="mail_id" runat="server" Text="" Width="200px" Height="25px"></asp:Label></td>
                                                                                            </tr>


                                                                                        </tr>

                                                                                    </table>
                                                                                </td>
                                                                                <td >
                                                                                    <table style="width: 430px">
                                                                                        <tr>
                                                                                            <td align="center"><b>PERMANENT ADDRESS</b></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <label id="per_add" runat="server" cols="55" rows="4" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">Tel no  : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="per_telno" runat="server" Text="" Width="150px" Height="25px"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">Mobile no: &nbsp;<asp:Label ID="per_mob_no" runat="server" Text="" onkeypress="CheckNumeric(event);" Height="25px" Width="125px"></asp:Label></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" >
                                                                        <table border="1" width="100%">

                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 250px">
                                                                                        <tr>
                                                                                            <td><asp:Label ID="lbldob" runat="server" Text="Date Of Birth:"></asp:Label><span style="color: Red;padding-left:2px">*</span>&nbsp;
                                            <asp:Label ID="txt_dte_of_birth" runat="server" SkinID="TxtBxNumOnly" Width="60px" Height="22px" MaxLength="10"
                                                CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                                onkeypress="CheckNumeric(event);" TabIndex="8"></asp:Label></td>

                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>(dd/mm/yyyy)</td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="pob" runat="server" Text="Place Of Birth:" Font-Bold="true"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Village:
                                                                                <asp:Label ID="txtvillage" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" ></asp:Label></td>
                                                                                            <td>Taluk:</td>
                                                                                            <td>
                                                                                                <asp:Label ID="txt_taluk" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" ></asp:Label></td>
                                                                                            <td>District:</td>
                                                                                            <td>
                                                                                                <asp:Label ID="txt_district" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" ></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td >City:
                                                                                <asp:Label ID="txt_city" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" ></asp:Label></td>
                                                                                            <td>State:</td>
                                                                                            <td>
                                                                                                <asp:Label ID="txt_state" runat="server" Text="" Width="120px" ></asp:Label></td>
                                                                                            <td>Country:</td>
                                                                                            <td>
                                                                                                <asp:Label ID="txt_country" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" ></asp:Label></td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td >
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td><b>GENDER:<span style="color: Red;padding-left:2px">*</span></b></td>
                                                                                <td align="center"><b>RELIGION:</b></td>
                                                                                <td align="center"><b>MARITAL STATUS:<span style="color: Red;padding-left:2px">*</span></b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:CheckBoxList ID="chkgender" runat="server" RepeatDirection="Horizontal" Font-Names="Calibri">
                                                                                        <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                                                                        <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                                                                                    </asp:CheckBoxList>

                                                                                </td>
                                                                                <td align="center">
                                                                                    <asp:Label ID="txt_religion" runat="server" Text="" Height="25px"></asp:Label></td>
                                                                                <td align="center">
                                                                                    <asp:CheckBoxList ID="chk_marrital" runat="server" RepeatDirection="Horizontal"
                                                                                        Font-Names="Calibri">
                                                                                        <asp:ListItem Value="MRD" Text="Married"></asp:ListItem>
                                                                                        <asp:ListItem Value="UNMRD" Text="UnMarried"></asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td></td>
                                                                                <td align="center">Date of Wedding :
                                         <asp:Label ID="txt_wed_dte" runat="server" Width="80px" Height="25px" MaxLength="10"
                                             TabIndex="8"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="background-color: #cceeff">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblhealth" runat="server" Text="HEALTH" CssClass="vertical" Font-Bold="true"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td >
                                                            <table style="width: 100%">

                                                                <tr>
                                                                    <td><b>BLOOD GROUP:<span style="color: Red;padding-left:2px">*</span></b></td>
                                                                    <td align="center"><b>VISION:</b></td>
                                                                    <td><b>LAST MAJOR ILLNESS/SURGERY :</b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="txt_bld_gp" runat="server" Height="25px"></asp:Label></td>
                                                                    <td>Long Sight:<asp:Label ID="txt_lg_sight" runat="server" Height="25px"></asp:Label></td>
                                                                    <td>(Specify date):</td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>Short Sight:<asp:Label ID="txt_sht_sight" runat="server" Height="25px"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_illnes" runat="server" Text="" Width="250px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>

                                                            </table>

                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkbp" Text="Are you Suffering from any of the following diseases? If Yes, Please tick in the checkbox" Font-Size="Medium" runat="server" /></td>
                                                                </tr>
                                                            </table>

                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBoxList ID="chk_diesease" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="BP" Text="BP"></asp:ListItem>
                                                                            <asp:ListItem Value="Diabetes" Text="Diabetes"></asp:ListItem>
                                                                            <asp:ListItem Value="Asthma" Text="Asthma"></asp:ListItem>
                                                                            <asp:ListItem Value="Chronic" Text="Chronic Bronchitis"></asp:ListItem>
                                                                            <asp:ListItem Value="Skin" Text="Skin Diseases"></asp:ListItem>
                                                                            <asp:ListItem Value="Venereal" Text="Venereal Disease"></asp:ListItem>
                                                                            <asp:ListItem Value="AIDS" Text="AIDS etc."></asp:ListItem>
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #e6ccff">
                                                            <table>
                                                                <tr>
                                                                    <td class="auto-style10">
                                                                        <asp:Label ID="Fam_details" runat="server" Text="FAMILY DETAILS" CssClass="vertical" Font-Bold="true"></asp:Label>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="auto-style10" >
                                                            <table border="1" width="100%">
                                                                <tr>
                                                                    <td style="width: 90px"></td>
                                                                    <td style="width: 200px"><b>Name</b></td>
                                                                    <td style="width: 40px"><b>Age</b></td>
                                                                    <td style="width: 90px"><b>DOB</b></td>
                                                                    <td style="width: 120px"><b>Occupation</b></td>

                                                                    <td style="width: 200px"><b>Address:</b></td>

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Father</b><span style="color: Red;padding-left:2px">*</span></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_father_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_father_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td rowspan="4">
                                                                        <label id="family_add_1" runat="server" cols="40" rows="5" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Mother</b><span style="color: Red;padding-left:2px">*</span></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_mother_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_mother_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_mother" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_mother" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Spouse</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_spouse_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_spouse_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_spouse" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_spouse" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Children: 1</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_child1_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_child1_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_child1" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_child1" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>2</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_child2_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_child2_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_child2" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_child2" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td rowspan="5">
                                                                        <label id="add2" runat="server" cols="40" rows="5" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Brothers: 1</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_bro1_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_bro1_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_bro1" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_bro1" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>2</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_bro2_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_bro2_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_bro2" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_bro2" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Sisters:&nbsp;&nbsp;&nbsp; 1</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_sis1_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_sis1_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_sis1" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_sis1" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2</b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_sis2_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_sis2_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dob_sis2" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_occ_sis2" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #ccd9ff">

                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_edu_details" runat="server" Text="EDUCATIONAL DETAILS" CssClass="vertical" Font-Bold="true"></asp:Label>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td >
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table border="1">
                                                                            <tr>
                                                                                <td style="width: 100px" rowspan="2"><b>Level</b></td>
                                                                                <td style="width: 120px" rowspan="2"><b>Name of the institution</b></td>
                                                                                <td style="width: 100px" rowspan="2"><b>Board/ University</b></td>
                                                                                <td colspan="2"><b>Year Attended</b></td>
                                                                                <td style="width: 100px" rowspan="2"><b>Medium</b></td>
                                                                                <td style="width: 100px" rowspan="2"><b>Subjects/ Area of Specialization</b></td>
                                                                                <td rowspan="2"><b>Marks(% / CGPA)</b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>From</td>
                                                                                <td class="auto-style11">To</td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>X standard<span style="color: Red;padding-left:2px;">*</span> </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_X_institude" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_board" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="Yr_from" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="Yr_to" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_Medium" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_special" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_marks" runat="server" Text="" Width="50px" Height="25px"></asp:Label></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>Intermediate </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_inter_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_inter_board" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="from_yr_inter" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="To_yr_inter" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="medium_inter" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_spcl_inter" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_marks_inter" runat="server" Text="" Width="50px" Height="25px"></asp:Label></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>Graduation </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_grad_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_grad_univ" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="from_yr_grad" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="To_yr_grad" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="medium_grad" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_spcl_grad" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_marks_grad" runat="server" Text="" Width="50px" Height="25px"></asp:Label></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>Post-Graduation</td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_PG_gradr_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_PG_grad_univ" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="from_yr_PG_grad" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="To_yr_PG_grad" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="medium_PG_grad" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_spcl_PG_grad" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_marks_PG_grad" runat="server" Text="" Width="50px" Height="25px"></asp:Label></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>Others </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_other_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_other_univ" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="from_yr_other" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="To_yr_other" runat="server" Text="" Width="30px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="medium_other" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_spcl_other" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_marks_other" runat="server" Text="" Width="50px" Height="25px"></asp:Label></td>

                                                                            </tr>

                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td><b>Any Educational courses that you are currently pursuing?</b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Course  Name &nbsp;
                                                                    <asp:Label ID="txt_course_nme" runat="server" Text="" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>&nbsp;&nbsp;
                                                                        Name of the university &nbsp;&nbsp;<asp:Label ID="txt_univer_name" runat="server" Text="" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>&nbsp;&nbsp;
                                                                        Duration &nbsp;&nbsp;<asp:Label ID="course_duration" runat="server" Text="" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">Mode of the course:&nbsp;&nbsp;
                                   <asp:CheckBoxList ID="chk_mode" runat="server" RepeatDirection="Horizontal">
                                       <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                                       <asp:ListItem Value="Part-Time" Text="Part-Time"></asp:ListItem>
                                       <asp:ListItem Value="Distance" Text="Distance"></asp:ListItem>
                                   </asp:CheckBoxList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Expected  year of completion of the course:
                                                                &nbsp;
                                                                <asp:Label ID="txt_compl_yr" Text="" runat="server" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td><b>Academic Achievements</b> (Ranks, Merit Scholarships, Prizes, etc.)
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label id="Aca_achieve" runat="server" cols="75" rows="3" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #ccd9ff"></td>
                                                        <td >
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td><b>Extra-Curricular Activities:<span style="color: Red;padding-left:2px;">*</span></b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label id="Extra_curricular" runat="server" cols="75" rows="3" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #d9d9f2">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_wrk_experience" runat="server" Text="WORK  EXPERIENCE" CssClass="vertical" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </td>
                                                        <td >
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table align="left">
                                                                            <tr>
                                                                                <td><b>PLEASE WRITE ‘NA’ IF NOT APPLICABLE:</b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Specify clearly in case of part time/contract work experience.</td>
                                                                            </tr>
                                                                        </table>

                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 35px">
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="1">
                                                                            <tr>
                                                                                <td rowspan="2" style="width: 50px"><b>Organization</b></td>
                                                                                <td colspan="3" align="center" style="width: 50px"><b>Period</b></td>
                                                                                <td rowspan="2" class="auto-style13"><b>Full time/  part time/ contract</b></td>
                                                                                <td rowspan="2" style="width: 50px"><b>Designation</b></td>
                                                                                <td rowspan="2" class="auto-style15"><b>Reason for leaving</b></td>
                                                                                <td rowspan="2" class="auto-style12"><b>Last drawn salary(per month)</b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="auto-style14">From (mm/yy)</td>
                                                                                <td class="auto-style14">To (mm/yy)</td>
                                                                                <td class="auto-style14">Duration (in months)</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txtorg1" Text="" runat="server" Style="width: 180px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                                <td class="auto-style14"><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_frm_my" Text="" runat="server" Width="50px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                                <td class="auto-style14"><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_to_my" Text="" runat="server" Width="50px" Height="25px" Style="margin-left: 0px;" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                                <td class="auto-style14"><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_duration" Text="" runat="server" Width="50px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                                <td class="auto-style13"><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_full_1" Text="" runat="server" Style="width: 85px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                                <td><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_designation" Text="" runat="server" Width="75px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                                <td class="auto-style15"><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_reason_1" Text="" runat="server" Style="width: 145px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                                <td class="auto-style12"><span style="color: Red;padding-left:2px;">*</span><asp:Label ID="txt_last_drawn_salary" Text="" runat="server" Style="width: 90px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="txt_org_2" Text="" runat="server" Style="width: 180px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_frm_my_1" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_to_my_1" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_duration_1" Text="" runat="server" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style13">
                                                                                    <asp:Label ID="txt_full_2" Text="" runat="server" Style="width: 85px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_designation_1" Text="" runat="server" Width="75px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style15">
                                                                                    <asp:Label ID="txt_reason_2" Text="" runat="server" Style="width: 145px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style12">
                                                                                    <asp:Label ID="txt_last_drawn_salary_1" Text="" runat="server" Style="width: 90px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="txt_org_3" Text="" runat="server" Style="width: 180px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_frm_my_2" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_to_my_2" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_duration_2" Text="" runat="server" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style13">
                                                                                    <asp:Label ID="txt_full_3" Text="" runat="server" Style="width: 85px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_designation_2" Text="" runat="server" Width="75px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style15">
                                                                                    <asp:Label ID="txt_reason_3" Text="" runat="server" Style="width: 145px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style12">
                                                                                    <asp:Label ID="txt_last_drawn_salary_2" Text="" runat="server" Style="width: 90px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="txt_org_4" Text="" runat="server" Style="width: 180px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_frm_my_3" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_to_my_3" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style14">
                                                                                    <asp:Label ID="txt_duration_4" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style13">
                                                                                    <asp:Label ID="txt_full_4" Text="" runat="server" Style="width: 85px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_designation_3" Text="" runat="server" Width="75px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style15">
                                                                                    <asp:Label ID="txt_reason_4" Text="" runat="server" Style="width: 145px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                                <td class="auto-style12">
                                                                                    <asp:Label ID="txt_last_drawn_salary_4" Text="" runat="server" Style="width: 90px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 35px">
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #f5e6ff">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="nommini" runat="server" Text="NOMINATION" CssClass="vertical" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td >
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td>Nominate the person to be contacted in case of emergency:</td>
                                                                            </tr>
                                                                            <tr style="height: 20px">
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>Name:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_nomini_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px" Width="200px"></asp:Label></td>
                                                                                <td>Address:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                <td>
                                                                                    <label id="txt_nomini_add" runat="server" cols="50" rows="4" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Relationship:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_nomini_relation" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Contact number:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_contact_nomini" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 20px">
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #cce6ff">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_refer" runat="server" Text="REFERENCES" CssClass="vertical" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td >
                                                            <table>
                                                                <tr>
                                                                    <td><b>LIST PROFESSIONAL REFERENCES</b> (not related)</td>
                                                                </tr>

                                                                <tr>
                                                                    <td style="width: 100%">
                                                                        <table border="1">
                                                                            <tr>
                                                                                <td style="width: 200px">Name & Address
                                                                                </td>
                                                                                <td style="width: 150px">Occupation
                                                                                </td>
                                                                                <td style="width: 180px">Email
                                                                                </td>
                                                                                <td style="width: 150px">Contact Number
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label id="txt_nme_add" runat="server" cols="40" rows="4" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_occ1" Text="" runat="server" Width="150px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_mail1" Text="" runat="server" Width="180px" Height="25px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_num1" Text="" runat="server" Width="150px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label id="txt_nme_add2" runat="server" cols="40" rows="4" onkeypress="AlphaNumeric_NoSpecialChars(event);"></label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_occ2" Text="" runat="server" Width="150px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_mail2" Text="" runat="server" Width="180px" Height="25px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_num2" Text="" runat="server" Width="150px" onkeypress="CheckNumeric(event);" Height="25px"></asp:Label>
                                                                                </td>
                                                                            </tr>

                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>Do, you know any of the employee  from Vivo Organization 
                                                If yes, please fill the details 
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBoxList ID="chk_vivo_known_emp" runat="server" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="1" style="width: 100%">
                                                                            <tr>
                                                                                <td width="200px"><b>Name</b></td>
                                                                                <td width="150px"><b>Designation</b></td>
                                                                                <td width="150px"><b>Relationship</b></td>
                                                                                <td width="150px"><b>Contact number</b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="txt_vivo_emp_name" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_vivo_desig_name" Text="" runat="server" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_vivo_relation" Text="" runat="server" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_vivo_contact" Text="" runat="server" Width="150px" Height="25px" onkeypress="CheckNumeric(event);"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #ebf0fa">
                                                            <asp:Label ID="lbl_languages" runat="server" Text="LANGUAGES" CssClass="vertical" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table style="width: 100%">
                                                                <tr style="height: 15px">
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>MOTHER TONGUE: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="txt_mother_tongue" runat="server" Text="" Height="25px"></asp:Label></td>
                                                                </tr>
                                                                <tr style="height: 15px">
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Languages known(other than mother tongue) : </b></td>
                                                                    <td><b>Can Understand</b></td>
                                                                    <td><b>Can Speak</b></td>
                                                                    <td><b>Can Read</b></td>
                                                                    <td><b>Can Write</b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="color: Red;padding-left:2px;">*</span>
                                                                        <asp:Label ID="lang_1" Text="" runat="server" Width="300px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: Red;padding-left:2px;">*</span>
                                                                        <asp:Label ID="under_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: Red;padding-left:2px;">*</span>
                                                                        <asp:Label ID="speak_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: Red;padding-left:2px;">*</span>
                                                                        <asp:Label ID="read_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: Red;padding-left:2px;">*</span>
                                                                        <asp:Label ID="write_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="lang2" Text="" runat="server" Width="300px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="under_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="speak_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="read_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="write_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="lang_3" Text="" runat="server" Width="300px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="under_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="speak_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="read_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                    <td><span style="color: white">*</span>
                                                                        <asp:Label ID="write_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 15px">
                                                                    <td></td>
                                                                </tr>

                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #c2c2f0">
                                                            <asp:Label ID="misc" runat="server" Text="MISCELLANEOUS" CssClass="vertical" Font-Bold="true"></asp:Label></td>
                                                        <td >
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr style="border-bottom: 1px solid #DCE2E8">
                                                                                <td>Do you have any legal obligations to your previous employer /employee?</td>
                                                                                <td>
                                                                                    <asp:CheckBoxList ID="chk_legal_oblig" runat="server" RepeatDirection="Horizontal"
                                                                                        Font-Names="Calibri">
                                                                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                                                    </asp:CheckBoxList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>If yes, please mention :
                                                                    <asp:Label ID="txt_obligation" Text="" runat="server" Width="500px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td width="650px">Have you at any time been convicted by a court of India for any criminal
                                                 offence and sentenced to imprisonment, or any criminal proceedings
                                                 are pending against you before a court in India.  
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBoxList ID="chk_crime" runat="server" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                                                    </asp:CheckBoxList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>If yes, please specify the details :
                                                                    <asp:Label ID="txt_crime_detail" Text="" runat="server" Width="400px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td align="center"><b><u>DECLARATION</u></b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>I certify that the above – stated information is TRUE to the best of my knowledge & 
                                                                         belief. All the academic marks / percentages / CGPA / years are true. I agree that
                                                                          in case the company finds at any time that the information given by me in this form 
                                                                         is not correct, the company will have the right to withdraw my letter of appointment
                                                                          or to terminate my appointment at any time without notice or compensation.
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td align="left">Date:&nbsp;&nbsp;<asp:Label ID="txt_dte" runat="server" SkinID="TxtBxNumOnly" Width="57px" Height="22px" MaxLength="10"
                                                                                    CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                                                                    onkeypress="CheckNumeric(event);" TabIndex="8"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 500px"></td>
                                                                                <td align="right">Signature:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="txt_sig" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #e6ccff">
                                                <asp:Label ID="lbl_offuse" runat="server" Text="OFFICE USE ONLY" CssClass="vertical" Font-Bold="true"></asp:Label></td>
                                            <td >
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>Date of application : </td>
                                                                    <td>
                                                                        <asp:Label ID="off_dte_application" runat="server" SkinID="TxtBxNumOnly" Width="57px" Height="22px" MaxLength="10"
                                                                            CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                                                            onkeypress="CheckNumeric(event);" TabIndex="8"></asp:Label></td>
                                                                    <td style="width: 300px"></td>
                                                                    <td>Joining Date : </td>
                                                                    <td>
                                                                        <asp:Label ID="off_join_dte" runat="server" SkinID="TxtBxNumOnly" Width="57px" Height="22px" MaxLength="10"
                                                                            CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                                                                            onkeypress="CheckNumeric(event);" TabIndex="8"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="height: 200px">
                                                                <tr>
                                                                    <td>Emp Code : </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_empcode" Text="" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                    <td>Designation :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_desig_offuse" Text="" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Date of offer and acceptance  :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_dte_accept" Text="" runat="server" Width="200px" Height="25px"></asp:Label></td>
                                                                    <td>Reporting&nbsp;Relation :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_report_relation" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Department / Division  :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_depart" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>Location of the branch / HQ  :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_loc" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Signature of the HR   :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_sig_hr" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:Label></td>
                                                                    <td>Corporate E-mail id  :  </td>
                                                                    <td>
                                                                        <asp:Label ID="txt_corporat_email" Text="" runat="server" Width="200px" Height="25px"></asp:Label></td>
                                                                </tr>

                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        </asp:Panel>
                         
                        </div>
                    </div>
                    </center>
                </div>

                <%-- <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />--%>
            </div>
        </div>
        <br />
        <br />

        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

    </form>
</body>
</html>
