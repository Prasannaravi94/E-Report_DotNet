<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee_Application.aspx.cs" Inherits="MIS_Reports_Employee_Application" %>

<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu" TagPrefix="ucl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personal Entry Form</title>
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

    <script src="JsFiles/CommonValidation.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 365px;
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

        .auto-style2 {
            width: 801px;
        }

        .auto-style3 {
            height: 30px;
            width: 297px;
        }

        .auto-style4 {
            width: 297px;
        }

        .auto-style5 {
            height: 152px;
            width: 900px;
        }

        .auto-style7 {
            width: 69px;
        }

        .auto-style10 {
            height: 29px;
        }

        #add_family_det {
            height: 17px;
        }

        .auto-style11 {
            width: 28px;
        }

        .auto-style12 {
            width: 100px;
        }

        .auto-style13 {
            width: 90px;
        }

        .auto-style14 {
            width: 40px;
        }

        .auto-style15 {
            width: 150px;
        }

        .auto-style17 {
            width: 472px;
        }

        .auto-style18 {
            width: 472px;
            height: 30px;
        }

        table, table tr td {
            border-color: #DCE2E8;
        }

            table tr td input {
                border-radius: 8px;
                border: 1px solid #d1e2ea;
                background-color: #f4f8fa;
                color: #90a1ac;
                font-size: 12px;
                padding-left: 10px;
                margin-top: 5px;
            }

        .single-des .input {
            font-size: 12px;
        }

        .savebutton1 {
            width: 100px;
            height: 32px;
            border-radius: 8px;
            color: #0077ff;
            background-color: #e9f7fb !important;
            cursor: pointer;
            border: 0px;
            font-size: 14px;
            font-weight: 600;
            margin: 0 3px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
        }
    </style>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>

    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
                ({
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1930:' + new Date().getFullYear().toString(),
                    //                yearRange: "2010:2017",
                    dateFormat: 'dd/mm/yy'
                });

            $('#btnBack').click(function () {
                console.log('s');
            });
        });

    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <%--    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>

    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

</head>
<body>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btn_save').click(function () {
                //alert("test");
                if ($("#txt_full_name").val() == "") { alert("Enter Name in full."); $('#txt_full_name').focus(); return false; }
                if ($("#panno").val() == "") { alert("Enter PAN card no."); $('#panno').focus(); return false; }
                if ($("#adrname").val() == "") { alert("Enter Name as per Aadhar card."); $('#adrname').focus(); return false; }
                if ($("#bnknme").val() == "") { alert("Enter Current Bank Name."); $('#bnknme').focus(); return false; }
                if ($("#branchname").val() == "") { alert("Enter Branch Name."); $('#branchname').focus(); return false; }
                if ($("#adrno").val() == "") { alert("Enter Aadhar Number."); $('#adrno').focus(); return false; }
                if ($("#accno").val() == "") { alert("Enter Bank Account Number."); $('#accno').focus(); return false; }
                if ($("#ifsccode").val() == "") { alert("Enter IFSC code."); $('#ifsccode').focus(); return false; }
                if ($("#mob_no").val() == "") { alert("Enter Mobile Number."); $('#mob_no').focus(); return false; }
                if ($("#mail_id").val() == "") { alert("Enter Mail Id."); $('#mail_id').focus(); return false; }
                if ($("#txt_dte_of_birth").val() == "") { alert("Enter  Date Of Birth."); $('#txt_dte_of_birth').focus(); return false; }
                //if ($("#txt_date").val() == "") { alert("Enter Date Of Birth."); $('#txt_date').focus(); return false; }
                if ($("#txt_bld_gp").val() == "") { alert("Enter Blood Group."); $('#txt_bld_gp').focus(); return false; }
                if ($("#txtvillage").val() == "") { alert("Enter Village"); $('#txtvillage').focus(); return false; }
                if ($("#txt_taluk").val() == "") { alert("Enter Taluk."); $('#txt_taluk').focus(); return false; }
                if ($("#txt_district").val() == "") { alert("Enter District."); $('#txt_district').focus(); return false; }
                if ($("#txt_city").val() == "") { alert("Enter City."); $('#txt_city').focus(); return false; }
                if ($("#txt_state").val() == "") { alert("Enter State."); $('#txt_state').focus(); return false; }
                if ($("#txt_country").val() == "") { alert("Enter Country."); $('#txt_country').focus(); return false; }

                if ($("#txt_X_institude").val() == "") { alert("Enter Name Of Institution."); $('#txt_X_institude').focus(); return false; }
                if ($("#txt_board").val() == "") { alert("Enter Board/University"); $('#txt_board').focus(); return false; }
                if ($("#Yr_from").val() == "") { alert("Enter From Year for X"); $('#Yr_from').focus(); return false; }
                if ($("#Yr_to").val() == "") { alert("Enter To Year for X"); $('#Yr_to').focus(); return false; }
                if ($("#txt_Medium").val() == "") { alert("Enter Medium"); $('#txt_Medium').focus(); return false; }
                if ($("#txt_special").val() == "") { alert("Enter Area Of Specialization."); $('#txt_special').focus(); return false; }
                if ($("#txt_marks").val() == "") { alert("Enter Area Of Specialization."); $('#txt_marks').focus(); return false; }
                if ($("#txt_dte").val() == "") { alert("Enter Declaration Date."); $('#txt_dte').focus(); return false; }
                if ($("#off_join_dte").val() == "") { alert("Enter Joining Date."); $('#off_join_dte').focus(); return false; }
                if ($("#off_dte_application").val() == "") { alert("Enter Date Of Application."); $('#off_dte_application').focus(); return false; }
                if ($("#off_join_dte").val() == "") { alert("Enter Area Of Specialization."); $('#off_join_dte').focus(); return false; }




            });
        });
    </script>
    <script>
        function ValidateCheckBoxList() {

            var listItems = document.getElementById("chk_marrital").getElementsByTagName("input");
            var itemcount = listItems.length;
            var iCount = 0;
            var isItemSelected = false;
            for (iCount = 0; iCount < itemcount; iCount++) {
                if (listItems[iCount].checked) {
                    isItemSelected = true;
                    break;
                }
            }
            if (!isItemSelected) {
                alert("Check the Marital Status.");
            }
            else {
                return true;
            }
            return false;
        }



    </script>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <input id="SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <br />
        <br />
        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row" align="right">
                <div class="col-lg-12">
                    <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="savebutton1" OnClick="btnBack_Click" />
                </div>
            </div>
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
                    <div align="center">
                        <asp:Label ID="lbl_vw" Text="--Click Go button to Enter/View the Personal Data--" runat="server" ForeColor="red" Font-Bold="true" Font-Size="14px" Visible="false"></asp:Label>
                    </div>

                    <table id="tblnote" runat="server" align="center">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" Text="***Type all the contents into the relavant box." runat="server" ForeColor="red" Font-Bold="true" Visible="false" CssClass="label"></asp:Label><br />
                                <asp:Label ID="Label2" Text="Dont Copy & Paste.While Entering avoid the special characters like ' '' \...." runat="server" ForeColor="red" Font-Bold="true" Visible="false" CssClass="label"></asp:Label><br />
                                <asp:Label ID="Label3" Text="Upload photo->filename should not contain special characters." runat="server" ForeColor="red" Font-Bold="true" Visible="false" CssClass="label"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <center>
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">

                            <table id="tbl_personal" runat="server" visible="false" class="single-des" >

                                <tr>
                                    <td>
                                        <table style="background-color: #ffe6e6;" border="1">
                                            <tr>
                                                <td >
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td width="530px">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblh" runat="server" Text="" Font-Bold="true" Font-Size="20px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="add1" runat="server" Text="" Font-Bold="true" Width="200px"></asp:Label><br />
                                                                        <asp:Label ID="addd2" runat="server" Text="" Font-Bold="true" Width="200px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table align="right" style="width: 100%">
                                                        <tr>
                                                            <td width="450px">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:FileUpload ID="FilUpImage" runat="server" /><br />
                                                                            <br />
                                                                            <asp:Button ID="bt_upload" runat="server" EnableViewState="False" Width="80px" Height="25px" CssClass="resetbutton"
                                                                                Text="Upload" OnClick="bt_upload_OnClick" />&nbsp;&nbsp;
                                                                            <asp:Label ID="lbl_ph" runat="server" Text="Passport Size Photo(35*45mm)"></asp:Label><span style="color: Red;padding-left:2px">*</span>
                                                                        </td>

                                                                        <td>
                                                                            <asp:DataList ID="DataList1" runat="server" HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <table>
                                                                                        <tr>

                                                                                            <td>
                                                                                                <asp:Image ID="imgHome" ImageUrl='<%# Eval("Photogragh") %>' Width="35mm" Height="45mm" ImageAlign="Top"
                                                                                                    runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
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
                                <tr>
                                    <td>
                                        <table align="center" style="align-content: center; width: 100%;">
                                            <tr>
                                                <td >
                                                    <table border="1" width="100%"  >
                                                        <tr>

                                                            <td>01.Please tick in the &nbsp;<asp:TextBox ID="txt" runat="server" Width="20px" Height="25px" ></asp:TextBox>wherever applicable</td>
                                                        </tr>
                                                        <tr>

                                                            <td>*Have you been referred by any of our Company Employee?
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal"
                                                                    Font-Names="Calibri">
                                                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                                    <asp:ListItem Value="N" Text="No" Selected="True"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>If Yes, Please mention the Employee Name
                                <asp:TextBox ID="txt_emp_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px" ></asp:TextBox>&nbsp;and Employee Code<asp:TextBox ID="txt_emp_code" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="100px" Height="25px" ></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table align="right" style="width:100%">
                                            <tr>
                                                <td >
                                                    <table width="100%" border="1">
                                                        <tr>
                                                            <td style="background-color: #ccccff;">
                                                                <asp:Label ID="perdts" runat="server" Text="PERSONAL DETAILS" CssClass="vertical" Font-Bold="true"></asp:Label></td>
                                                            <td class="auto-style2">
                                                                <table  width="100%">
                                                                    <tr>
                                                                        <td >
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td align="left" >NAME IN FULL(in block letters)<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="txt_full_name" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" >PAN Card No:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="panno" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                    <td align="left">Aadhar card Number:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="adrno" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" >Name as per Aadhar Card:<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="adrname" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                    <td align="left">Current Bank  Account No :<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="accno" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" >Current Bank Name :<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="bnknme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                    <td align="left">IFSC Code :<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="ifsccode" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" >Name of the Branch :<span style="color: Red;padding-left:2px;">*</span></td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="branchname" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);"  Width="200px" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <table border="1" style="width: 100%">
                                                                                <tr>
                                                                                    <td >
                                                                                        <table style="width: 50%">
                                                                                            <tr>
                                                                                                <td align="center" class="auto-style17"><b>ADDRESS FOR COMMUNICATION</b></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="auto-style17">
                                                                                                    <textarea id="add_cmtn" runat="server" cols="55" rows="4" onkeypress="AlphaNumeric_NoSpecialChars(event);" class="input" style="height:100px;width:400px"></textarea></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="auto-style17" align="left">Mobile no:<span style="color: Red;padding-left:2px;">*</span> &nbsp;<asp:TextBox ID="mob_no" runat="server" Text="" Width="120px" onkeypress="CheckNumeric(event);" Height="27px" ></asp:TextBox>&nbsp;&nbsp;Tel no:<asp:TextBox ID="tel_no" runat="server" Text="" Width="120px" Height="25px" ></asp:TextBox></td>
                                                                                                <tr>
                                                                                                    <td class="auto-style18" align="left"> Personal Email Id : <span style="color: Red;padding-left:2px;">*</span> &nbsp;<asp:TextBox ID="mail_id" runat="server" Text="" Width="232px" Height="25px" ></asp:TextBox></td>
                                                                                                </tr>


                                                                                            </tr>

                                                                                        </table>
                                                                                    </td>
                                                                                    <td >
                                                                                        <table style="width:50%">
                                                                                            <tr>
                                                                                                <td align="center"><b>PERMANENT ADDRESS</b></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <textarea id="per_add" runat="server" cols="55" rows="4" class="input" style="height:100px;width:400px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left">Tel no  : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="per_telno" runat="server"  Text="" Width="150px" Height="25px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left">Mobile no: &nbsp;<asp:TextBox ID="per_mob_no" runat="server" Text=""  onkeypress="CheckNumeric(event);" Height="25px" Width="150px"></asp:TextBox></td>
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
                                                                                              <asp:TextBox ID="txt_dte_of_birth" runat="server"  Width="80px" Height="22px" MaxLength="10"
                                                                                                
                                                                                                   onkeypress="CheckNumeric(event);" TabIndex="8"></asp:TextBox>
                                                                                                     <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txt_dte_of_birth" CssClass="cal_Theme1" runat="server" />
                                                
                                                                                                </td>

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
                                                                                <asp:TextBox ID="txtvillage" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                                <td>Taluk:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_taluk" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                                <td>District:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_district" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="right">City:
                                                                                <asp:TextBox ID="txt_city" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                                <td>State:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_state" runat="server" Text="" Width="120px" Height="25px"></asp:TextBox></td>
                                                                                                <td>Country:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txt_country" runat="server" Text="" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
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
                                                                                    <td><b>GENDER:<span style="color: Red;padding-left:2px;">*</span></b></td>
                                                                                    <td align="center"><b>RELIGION:</b></td>
                                                                                    <td align="center"><b>MARITAL STATUS:<span style="color: Red;padding-left:2px;">*</span></b></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <asp:CheckBoxList ID="chkgender" runat="server" RepeatDirection="Horizontal" Font-Names="Calibri">
                                                                                            <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                                                                            <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                                                                                        </asp:CheckBoxList>

                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:TextBox ID="txt_religion" runat="server" Text="" Height="25px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txt_wed_dte" runat="server"  Width="80px" Height="22px" MaxLength="10"
                                                                        
                                                                        onkeypress="CheckNumeric(event);" TabIndex="8"></asp:TextBox>
                                                                         <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txt_wed_dte" CssClass="cal_Theme1" runat="server" />
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
                                                                        <td><b>BLOOD GROUP:<span style="color: Red;padding-left:2px;">*</span></b></td>
                                                                        <td align="center"><b>VISION:</b></td>
                                                                        <td><b>LAST MAJOR ILLNESS/SURGERY :</b></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_bld_gp" runat="server" Height="25px"></asp:TextBox></td>
                                                                        <td>Long Sight:<asp:TextBox ID="txt_lg_sight" runat="server" Height="25px"></asp:TextBox></td>
                                                                        <td>(Specify date):</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td>Short Sight:<asp:TextBox ID="txt_sht_sight" runat="server" Height="25px"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_illnes" runat="server" Text="" Width="250px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
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
                                                                        <td style="width: 100px"></td>
                                                                        <td style="width: 200px"><b>Name</b></td>
                                                                        <td style="width: 40px"><b>Age</b></td>
                                                                        <td style="width: 90px"><b>DOB</b></td>
                                                                        <td style="width: 120px"><b>Occupation</b></td>

                                                                        <td style="width: 200px"><b>Address:</b></td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Father<span style="color: Red;padding-left:2px;">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_father_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_father_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td rowspan="4">
                                                                            <textarea id="family_add_1" runat="server" cols="40" rows="5" class="input" style="width:380px;height:100px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Mother</b><span style="color: Red;padding-left:2px;">*</span></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_mother_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_mother_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_mother" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_mother" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Spouse</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_spouse_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_spouse_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_spouse" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_spouse" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Children: 1</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_child1_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_child1_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_child1" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_child1" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>2</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_child2_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_child2_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_child2" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_child2" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td rowspan="5">
                                                                            <textarea id="add2" runat="server" cols="40" rows="5" class="input" style="width:380px;height:100px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Brothers: 1</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_bro1_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_bro1_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_bro1" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_bro1" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>2</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_bro2_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_bro2_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_bro2" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_bro2" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Sisters: 1</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_sis1_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_sis1_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_sis1" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_sis1" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>2</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_sis2_nme" runat="server" Text="" Style="width: 199px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_sis2_age" runat="server" Text="" Style="width: 39px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dob_sis2" runat="server" Text="" Style="width: 89px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_occ_sis2" runat="server" Text="" Style="width: 119px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
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
                                                                            <table border="1" width="100%">
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
                                                                                    <td >To</td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td>X standard <span style="color: Red;padding-left:2px">*</span></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_X_institude" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_board" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="Yr_from" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="Yr_to" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_Medium" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_special" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_marks" runat="server" Text="" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Intermediate </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_inter_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_inter_board" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="from_yr_inter" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="To_yr_inter" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="medium_inter" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_spcl_inter" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_marks_inter" runat="server" Text="" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Graduation </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_grad_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_grad_univ" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="from_yr_grad" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="To_yr_grad" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="medium_grad" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_spcl_grad" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_marks_grad" runat="server" Text="" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Post-Graduation</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_PG_gradr_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_PG_grad_univ" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="from_yr_PG_grad" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="To_yr_PG_grad" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="medium_PG_grad" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_spcl_PG_grad" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_marks_PG_grad" runat="server" Text="" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Others </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_other_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_other_univ" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="from_yr_other" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="To_yr_other" runat="server" Text="" Width="60px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="medium_other" runat="server" Text="" Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_spcl_other" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_marks_other" runat="server" Text="" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>

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
                                                                    <asp:TextBox ID="txt_course_nme" runat="server" Text="" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>&nbsp;&nbsp;
                                                                        Name of the university &nbsp;&nbsp;<asp:TextBox ID="txt_univer_name" runat="server" Text="" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>&nbsp;&nbsp;
                                                                        Duration &nbsp;&nbsp;<asp:TextBox ID="course_duration" runat="server" Text="" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
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
                                                                <asp:TextBox ID="txt_compl_yr" Text="" runat="server" Height="25px"></asp:TextBox></td>
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
                                                                                        <textarea id="Aca_achieve" runat="server" cols="75" rows="3" class="input" style="width:800px;height:100px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea>
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
                                                                        <td><b>Extra-Curricular Activities:<span style="color: Red;padding-left:2px">*</span></b></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <textarea id="Extra_curricular" runat="server" cols="75" rows="3" class="input" style="width:800px;height:100px"  onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea></td>
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
                                                                            <table border="1" width="100%">
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
                                                                                    <td><span style="color: Red">*</span><asp:TextBox ID="txtorg1" Text="" runat="server" Style="width: 180px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                                    <td class="auto-style14"><span style="color: Red">*</span><asp:TextBox ID="txt_frm_my" Text="" runat="server" Width="50px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                                    <td class="auto-style14"><span style="color: Red">*</span><asp:TextBox ID="txt_to_my" Text="" runat="server" Width="50px" Height="25px" Style="margin-left: 0px;" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                                    <td class="auto-style14"><span style="color: Red">*</span><asp:TextBox ID="txt_duration" Text="" runat="server" Width="50px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                                    <td class="auto-style13"><span style="color: Red">*</span><asp:TextBox ID="txt_full_1" Text="" runat="server" Style="width: 85px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                                    <td><span style="color: Red">*</span><asp:TextBox ID="txt_designation" Text="" runat="server" Width="75px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                                    <td class="auto-style15"><span style="color: Red">*</span><asp:TextBox ID="txt_reason_1" Text="" runat="server" Style="width: 145px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                                    <td class="auto-style12"><span style="color: Red">*</span><asp:TextBox ID="txt_last_drawn_salary" Text="" runat="server" Style="width: 90px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_org_2" Text="" runat="server" Style="width: 180px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_frm_my_1" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_to_my_1" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_duration_1" Text="" runat="server" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style13">
                                                                                        <asp:TextBox ID="txt_full_2" Text="" runat="server" Style="width: 85px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_designation_1" Text="" runat="server" Width="75px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style15">
                                                                                        <asp:TextBox ID="txt_reason_2" Text="" runat="server" Style="width: 145px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style12">
                                                                                        <asp:TextBox ID="txt_last_drawn_salary_1" Text="" runat="server" Style="width: 90px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_org_3" Text="" runat="server" Style="width: 180px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_frm_my_2" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_to_my_2" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_duration_2" Text="" runat="server" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style13">
                                                                                        <asp:TextBox ID="txt_full_3" Text="" runat="server" Style="width: 85px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_designation_2" Text="" runat="server" Width="75px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style15">
                                                                                        <asp:TextBox ID="txt_reason_3" Text="" runat="server" Style="width: 145px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style12">
                                                                                        <asp:TextBox ID="txt_last_drawn_salary_2" Text="" runat="server" Style="width: 90px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_org_4" Text="" runat="server" Style="width: 180px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_frm_my_3" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_to_my_3" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style14">
                                                                                        <asp:TextBox ID="txt_duration_4" Text="" runat="server" Width="50px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style13">
                                                                                        <asp:TextBox ID="txt_full_4" Text="" runat="server" Style="width: 85px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_designation_3" Text="" runat="server" Width="75px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style15">
                                                                                        <asp:TextBox ID="txt_reason_4" Text="" runat="server" Style="width: 145px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                    <td class="auto-style12">
                                                                                        <asp:TextBox ID="txt_last_drawn_salary_4" Text="" runat="server" Style="width: 90px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox></td>
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
                                                                                    <td>Name:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_nomini_nme" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px" Width="200px"></asp:TextBox></td>
                                                                                    <td>Address:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                    <td>
                                                                                        <textarea id="txt_nomini_add" runat="server" cols="50" rows="4" class="input" style="width:400px;height:100px;" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Relationship:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_nomini_relation" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Contact number:<span style="color: Red;padding-left:2px">*</span></td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_contact_nomini" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox></td>
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
                                                                                    <td style="width: 220px">Name & Address
                                                                                    </td>
                                                                                    <td style="width: 150px">Occupation
                                                                                    </td>
                                                                                    <td style="width: 180px">Email
                                                                                    </td>
                                                                                    <td style="width: 180px">Contact Number
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <textarea id="txt_nme_add" runat="server" cols="40" rows="4" class="input" style="width:400px;height:100px;" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_occ1" Text="" runat="server" Width="150px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_mail1" Text="" runat="server" Width="180px" Height="25px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_num1" Text="" runat="server" Width="150px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <textarea id="txt_nme_add2" runat="server" cols="40" rows="4" class="input" style="width:400px;height:100px;" onkeypress="AlphaNumeric_NoSpecialChars(event);"></textarea>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_occ2" Text="" runat="server" Width="150px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_mail2" Text="" runat="server" Width="180px" Height="25px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_num2" Text="" runat="server" Width="150px" onkeypress="CheckNumeric(event);" Height="25px"></asp:TextBox>
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
                                                                                        <asp:TextBox ID="txt_vivo_emp_name" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_vivo_desig_name" Text="" runat="server" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_vivo_relation" Text="" runat="server" Width="150px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_vivo_contact" Text="" runat="server" Width="150px" Height="25px" onkeypress="CheckNumeric(event);"></asp:TextBox>
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
                                                                            <asp:TextBox ID="txt_mother_tongue" runat="server" Text="" Height="25px"></asp:TextBox></td>
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
                                                                        <td><span style="color: Red">*</span>
                                                                            <asp:TextBox ID="lang_1" Text="" runat="server" Width="300px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: Red">*</span>
                                                                            <asp:TextBox ID="under_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: Red">*</span>
                                                                            <asp:TextBox ID="speak_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: Red">*</span>
                                                                            <asp:TextBox ID="read_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: Red">*</span>
                                                                            <asp:TextBox ID="write_1" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="lang2" Text="" runat="server" Width="300px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="under_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="speak_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="read_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="write_2" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="lang_3" Text="" runat="server" Width="300px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="under_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="speak_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="read_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
                                                                        </td>
                                                                        <td><span style="color: white">*</span>
                                                                            <asp:TextBox ID="write_3" Text="" runat="server" Width="100px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_obligation" Text="" runat="server" Width="500px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txt_crime_detail" Text="" runat="server" Width="400px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
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
                                                                                    <td align="left">Date:&nbsp;&nbsp;<asp:TextBox ID="txt_dte" runat="server"  Width="80px" Height="22px" MaxLength="10"  onkeypress="CheckNumeric(event);" TabIndex="8"></asp:TextBox>
                                                                                         <asp:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txt_dte" CssClass="cal_Theme1" runat="server" />

                                                                                    </td>
                                                                                    <td style="width: 500px"></td>
                                                                                    <td align="right">Signature:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_sig" runat="server" Text="" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="25px"></asp:TextBox>
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
                                                                            <asp:TextBox ID="off_dte_application" runat="server"  Width="80px" Height="22px" MaxLength="10"
                                                                               
                                                                                onkeypress="CheckNumeric(event);" TabIndex="8"></asp:TextBox>
                                                                             <asp:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="off_dte_application" CssClass="cal_Theme1" runat="server" />
                                                                        </td>
                                                                        <td style="width: 300px"></td>
                                                                        <td>Joining Date : </td>
                                                                        <td>
                                                                            <asp:TextBox ID="off_join_dte" runat="server"  Width="80px" Height="22px" MaxLength="10"
                                                                               
                                                                                onkeypress="CheckNumeric(event);" TabIndex="8"></asp:TextBox>
                                                                             <asp:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="off_join_dte" CssClass="cal_Theme1" runat="server" />
                                                                        </td>
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
                                                                            <asp:TextBox ID="txt_empcode" Text="" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:TextBox></td>
                                                                        <td>Designation :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_desig_offuse" Text="" runat="server" onkeypress="AlphaNumeric_NoSpecialChars(event);" Width="200px" Height="25px"></asp:TextBox></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Date of offer and acceptance  :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_dte_accept" Text="" runat="server" Width="200px" Height="25px"></asp:TextBox></td>
                                                                        <td>Reporting&nbsp;Relation :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_report_relation" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Department / Division  :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_depart" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>Location of the branch / HQ  :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_loc" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Signature of the HR   :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_sig_hr" Text="" runat="server" Width="200px" Height="25px" onkeypress="AlphaNumeric_NoSpecialChars(event);"></asp:TextBox></td>
                                                                        <td>Corporate E-mail id  :  </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_corporat_email" Text="" runat="server" Width="200px" Height="25px"></asp:TextBox></td>
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
                                <tr>
                                    <td>
                                        <table align="center">
                                            <tr>
                                               
                                                <td>
                                                    <asp:Button ID="btn_save" Text="Save" runat="server" Visible="false" CssClass="savebutton"
                                                        OnClick="btn_save_click" OnClientClick="return ValidateCheckBoxList()" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                            </table>
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
