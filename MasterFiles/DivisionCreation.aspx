<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivisionCreation.aspx.cs"
    Inherits="MasterFiles_DivisionCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Division Creation</title>

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/SS_Report_Table_CSS.css" rel="stylesheet"  type="text/css" />

    <style type="text/css">
        .test tr input {
            margin-right: 10px;
            padding-right: 10px;
        }

        .panelposition {
            top: 404px;
        }

        #tblDivisionDtls {
            margin-left: 300px;
        }

        #tblLocationDtls {
            margin-left: 300px;
        }

        .style2 {
            width: 92px;
            height: 25px;
        }

        .style3 {
            height: 25px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .padding {
            padding: 3px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .boxshadow {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }

        .roundbox {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }

        .grd {
            border: 1;
            border-color: Black;
        }

        .roundbox-top {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }

        .roundbox-bottom {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }

        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright {
            padding: 6px 6px 6px 6px;
            background: #003399 url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: khaki;
        }

        .gridheaderleft {
            text-align: left;
        }

        .gridheaderright {
            text-align: right;
        }

        .gridheaderbig {
            font-size: 135%;
        }
    </style>

   



    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
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

         function hideRadioSymbol() {
             var rads = new Array();
             rads = document.getElementsByName('div');
             for (var i = 0; i < rads.length; i++)
                 document.getElementById(rads.item(i).id).style.display = 'none'; //hide
         }

    </script>


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
        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
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
            $('#btnSubmit').click(function () {
                if ($("#txtDivision_Sname").val() == "") { alert("Enter Short Name."); $('#txtDivision_Sname').focus(); return false; }
                if ($("#txtDivision_Name").val() == "") { alert("Enter Division Name."); $('#txtDivision_Name').focus(); return false; }
                if ($("#txtDivision_Add1").val() == "") { alert("Enter Address1."); $('#txtDivision_Add1').focus(); return false; }
                if ($("#txtCity").val() == "") { alert("Enter City."); $('#txtCity').focus(); return false; }
                if ($("#txtPincode").val() == "") { alert("Enter Pincode."); $('#txtPincode').focus(); return false; }
                if ($("#txtAlias").val() == "") { alert("Enter Alias Name."); $('#txtAlias').focus(); return false; }
                if ($("#txtYear").val() == "") { alert("Enter Year."); $('#txtYear').focus(); return false; }
                if ($("#txtWeekOff").val() == "") { alert("Select Weak Off."); $('#txtWeekOff').focus(); return false; }
                if ($('#div input:checked').length > 0) { return true; } else { alert('Select State'); return false; }
            });
        });
    </script>
    <%--<script type="text/javascript">
        function ValidateCheckBoxList() {

            var listItems = document.getElementById("chkboxLocation").getElementsByTagName("input");
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
                alert("Please select State");
            }
            else {
                return true;
            }
            return false;
        }
    </script>--%>
   

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />


            <div class="container home-section-main-body position-relative clearfix">
                <br/>
                <h2 class="text-center" style="padding-left: 50px;">Division Creation</h2>
                <div class="row " >
                    <div class="col-lg-12">
                <div class="row" style="padding-left: 100px;">

                    <div class="col-lg-6 ">

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblShortName" runat="server" CssClass="label">Short Name <span style="color: Red;padding-left:1px;">*</span>
                                    <asp:Label ID="lblCap" runat="server" Font-Size="XX-Small" Font-Names="Arial" Text="(For ID Creation)">
                                    </asp:Label></asp:Label>
                                <br />                              
                                <asp:TextBox ID="txtDivision_Sname" runat="server" CssClass="input" TabIndex="1" MaxLength="3" onkeypress="CharactersOnly(event);" ></asp:TextBox>
                              
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblAddress1" runat="server" CssClass="label">Address 1<span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDivision_Add1" runat="server" CssClass="input" TabIndex="3" MaxLength="150" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                            </div>



                            <div class="single-des clearfix">
                                <asp:Label ID="lblCity" runat="server" CssClass="label">City <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCity" runat="server" TabIndex="5" CssClass="input" MaxLength="20" onkeypress="CharactersOnly(event);"> </asp:TextBox>
                            </div>

                            <div class="single-des clearfix">

                                <asp:Label ID="lblAlias" runat="server" CssClass="label">Alias Name <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAlias" runat="server" CssClass="input" TabIndex="7" MaxLength="8" onkeypress="CharactersOnly(event);">     
                                </asp:TextBox>
                            </div>


                            <div class="single-des clearfix">
                                <asp:Label ID="lblWeekOff" runat="server" CssClass="label">Week Off <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:UpdatePanel ID="updatepanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtWeekOff" ReadOnly="true" CssClass="input" runat="server"></asp:TextBox>
                                     
                                        
                                        <asp:PopupControlExtender ID="txtWeek_PopupControlExtender" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtWeekOff" PopupControlID="Panel1" Position="Bottom" 
                                            OffsetY="2" >  <%-- OffsetY="22"--%>
                                        </asp:PopupControlExtender>
                                         
                                        <asp:Panel ID="Panel1" runat="server" Height="185px" Width="365px" 
                                            BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none;border-radius:8px"  >
                                          
                                            <asp:CheckBoxList ID="Chkweek" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Chkweek_SelectedIndexChanged" CssClass="txt">
                                                <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </asp:Panel>


                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                        </div>

                    </div>

                    <div class="col-lg-6">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivisionName" runat="server" CssClass="label">Division Name <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDivision_Name" runat="server" CssClass="input" TabIndex="2" MaxLength="100" onkeypress="AlphaNumeric(event);"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblAddress2" runat="server" Text="Address 2" CssClass="label"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtDivision_Add2" runat="server" TabIndex="4" MaxLength="150" CssClass="input" onkeypress="AlphaNumeric_NoSpecialChars(event);">        
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblPincode" runat="server" CssClass="label">Pin Code <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPincode" runat="server" TabIndex="6" CssClass="input" MaxLength="6" onkeypress="CheckNumeric(event);"> </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblYear" runat="server" CssClass="label">Year <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtYear" runat="server" MaxLength="4" CssClass="input" TabIndex="8" onkeypress="CheckNumeric(event);">
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblImpleDate" runat="server" CssClass="label">Implementation Date <span style="color: Red;padding-left:1px;">*</span></asp:Label>
                                <br />
                                <asp:TextBox ID="txtImpleDate" runat="server" CssClass="input"  TabIndex="9"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtImpleDate" CssClass= "cal_Theme1" />
                              <%--  <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtImpleDate"  CssClass="DOBDate"/>--%>
                            </div>
                        </div>
                    </div>

                </div>

                <br />
                 <div class="row justify-content-center" style="overflow-x: auto;" >
                      <div class="col-lg-9 "> 
                <table  >
                    <tr>
                        <td rowspan=""  align="center">
                            <asp:Label ID="lblTitle_LocationDtls" runat="server" Text="Select the State/Location" Visible="false"
                                TabIndex="6" CssClass="h2 ">
                               
                            </asp:Label>
                            <span runat="server" id="spState" style="color: #292a34; font-size: 24px; font-weight: 700; margin-bottom: 50px;">Select the<asp:LinkButton ID="lnk" runat="server" Text=" " OnClick="lnk_Click"></asp:LinkButton>State/Location</span>
                            <%--style="font-weight: bold; text-decoration: underline; border-style: none; font-family: Verdana; border-color: #E0E0E0; color: #292a34"--%>

                        </td>
                    </tr>
                    <tr style="height: 5px">
                        <td style="width: 92px; height: 5px"></td>
                    </tr>
                    <tr class="divchk" >
                        <td style="width: 500px; height: 10px;padding:20px;color:#696d6e;" align="left" class="stylespc">
                            <div class="checkboxes">

                        <asp:CheckBoxList ID="div" runat="server" DataTextField="State_Name" DataValueField="State_Code"
                             RepeatColumns="4" RepeatDirection="vertical" class="checkBoxClass" Font-Bold="false" 
                            Width="810px" TabIndex="7">
                        </asp:CheckBoxList>
                          
                             <%--  <asp:RadioButtonList ID="div"  RepeatLayout="Flow"  runat="server" DataTextField="State_Name" DataValueField="State_Code" RepeatColumns="3" ></asp:RadioButtonList>--%>
                            <asp:HiddenField ID="HidDivCode" runat="server" />
                               </div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center">

                            <asp:Button ID="btnSubmit" runat="server" class="savebutton"
                                Text="Save" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>

                <br />
                <br />

                     <asp:Panel ID="pnlNew" runat="server" Visible="false" >
                    <table cellpadding="4" cellspacing="4" align="center" style="border: 1px solid #d1e2ea; border-collapse:collapse">
                        <tr>
                            <td style="color: white; font-weight: bold; font-family: Arial; background-image: linear-gradient(to top, rgb(0, 119, 255) 0%, rgb(40, 181, 224) 100%)"
                                align="center">Assign the New Division
                            </td>
                        </tr>
                        <tr>
                            <td align="center"  >
                                <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" 
                                    Width="600px" CssClass="test"
                                   >
                                    <asp:ListItem Value="0" Text="Product Category" ></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Product Group"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Product Detail"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Doctor Category"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Doctor Specialty"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Doctor Qualification"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Doctor Class"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Setup"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Flash News"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Designation"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Holiday"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnProcess" runat="server" Text="Process" class="resetbutton"
                                    OnClick="btnProcess_Click" />
                                <br />

                            </td>
                        </tr>
                    </table>

                    <br />
                    <center>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="lbllocation_based" Text="Location Based" CssClass="label" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                <td >

                                    <asp:DropDownList ID="ddllocation" runat="server"  >
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbleffectivedate" Text="Effective Date" CssClass="label" runat="server" ></asp:Label>
                                </td>
                                <td></td>
                                <td class="single-des clearfix"">

                                    <asp:TextBox ID="txteffective" onkeypress="Calendar_enter(event);" runat="server" Width="200px"
                                        CssClass=" input" SkinID="input"></asp:TextBox>
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txteffective" CssClass= " cal_Theme1" />
                                </td>

                                <td>
                                    <asp:Button ID="btnlocation" runat="server" CssClass="savebutton"
                                        Text="Save Location" OnClick="btnlocation_Click" />
                                </td>
                            </tr>
                        </table>
                    </center>



                </asp:Panel>
               
              <%--  </div>--%>
                   
               <br />
                 <center>

                <div class="roundbox boxshadow" style="width: 600px; border: solid 2px steelblue;display:inline-block ">
                    <div class="gridheaderleft" style="color: white; font-weight: bold; font-family: Arial; background-image: linear-gradient(to top, rgb(0, 119, 255) 0%, rgb(40, 181, 224) 100%)" >
                        <center>Upload Logo</center>
                    </div>
                    <div class="boxcontenttext" >
                        <div id="pnlPreviewSurveyData">
                            <br />
                            <%--    <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" BackColor="LightBlue" OnClick="Upload" />
                            --%>
                            <%--   <asp:TemplateField HeaderText="Upload" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>--%>
                            <asp:FileUpload ID="FilUpImage" runat="server" Font-Size="14px" />
                            <asp:Button ID="bt_upload" runat="server" EnableViewState="False" 
                                CssClass="resetbutton" Text="Upload" OnClick="bt_upload_OnClick" />
                            <br />
                             <br />
                            <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center">
                                <ItemTemplate>

                                    <div>
                                        <asp:Image ID="imgHome" ImageUrl='<%# Eval("div_logo") %>' Width="200px" ImageAlign="Top" runat="server" />
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </center>

        

               
            <br />
           
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />

                  
              </div> </div></div>
 <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click"  />



                    </div>  </div>
            <br />
            <br />
             
        </div>

         <script type="text/javascript"> hideRadioSymbol()</script> 
         
    </form>
    
</body>
</html>
