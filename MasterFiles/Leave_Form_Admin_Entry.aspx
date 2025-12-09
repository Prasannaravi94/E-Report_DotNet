<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_Form_Admin_Entry.aspx.cs"
    Inherits="MasterFiles_Leave_Form_Admin_Entry" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application Form</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        table, th
        {
            border: 1px solid black;
            border-collapse: collapse;
        }
        
        .padding
        {
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
            padding-bottom: 3px;
        }
        .chkboxLocation label
        {
            padding-right: 5px;
        }
        .Cal_Theme .ajax__calendar_container
        {
            background-color: #EDCF81;
            border: solid 1px #cccccc;
        }
        
        .Cal_Theme .ajax__calendar_header
        {
            background-color: #FFFFEA;
            margin-bottom: 4px;
        }
        
        .Cal_Theme .ajax__calendar_title, .Cal_Theme .ajax__calendar_next, .Cal_Theme .ajax__calendar_prev
        {
            color: #004080;
            padding-top: 3px;
        }
        
        .Cal_Theme .ajax__calendar_body
        {
            background-color: #FFFFEA;
            border: solid 1px #cccccc;
        }
        
        .Cal_Theme .ajax__calendar_dayname
        {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
        }
        
        .Cal_Theme .ajax__calendar_day
        {
            text-align: center;
        }
        
        .Cal_Theme .ajax__calendar_hover .ajax__calendar_day, .Cal_Theme .ajax__calendar_hover .ajax__calendar_month, .Cal_Theme .ajax__calendar_hover .ajax__calendar_year, .Cal_Theme .ajax__calendar_active
        {
            color: #FFFFFF;
            font-weight: bold;
            background-color: #4A89B9;
        }
        
        .Cal_Theme .ajax__calendar_today
        {
            font-weight: bold;
        }
        
        .Cal_Theme .ajax__calendar_other, .Cal_Theme .ajax__calendar_hover .ajax__calendar_today, .Cal_Theme .ajax__calendar_hover .ajax__calendar_title
        {
            color: #000000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //  $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
                            $('#btnApprove').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnApprove').click(function () {
                var divi = $('#<%=ddltype.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Type of Leave."); $('#ddltype').focus(); return false; }
                if ($("#txtLeave").val() == "") { alert("Enter Leave From Date."); $('#txtLeave').focus(); return false; }
                if ($("#txtLeaveto").val() == "") { alert("Enter Leave To Date."); $('#txtLeaveto').focus(); return false; }
                if ($("#txtreason").val() == "") { alert("Enter Reason for Leave."); $('#txtreason').focus(); return false; }
                if (confirm('Do you want to Submit?')) {
                    return true;
                }
                else {
                    return false;
                }

            });
        });
    </script>
    

 <script type="text/javascript">
     $(function () {
         var $txt = $('input[id$=txtNew]');
         var $ddl = $('select[id$=ddlFieldForce]');
         var $items = $('select[id$=ddlFieldForce] option');

         $txt.on('keyup', function () {
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
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" SkinID="lblMand"></asp:Label>
                    </td>
                      <td align="left" class="stylespc">
                       <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" 
            ToolTip="Enter Text Here"></asp:TextBox>
              <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" 
                              AutoPostBack="true" Width="400px" 
                              onselectedindexchanged="ddlFieldForce_SelectedIndexChanged"  ></asp:DropDownList>
                      </td>
                     <td>
            <asp:Button ID="btnGo"  runat="server" Width="35px" Height="25px" Text="Go" 
                BackColor="LightBlue" onclick="btnGo_Click"  />
                </td>
                </tr>
            </table>
        </center><br />
        <asp:Panel ID="pnlLeave" runat="server" Visible="false">
        <center>
        
            <asp:Table ID="tblLeave" BorderStyle="Solid" BackColor="White" BorderWidth="1" Width="90%"
                CellSpacing="5" CellPadding="5" runat="server">
                <asp:TableHeaderRow HorizontalAlign="Center" Font-Size="14px" Font-Names="Verdana">
                    <asp:TableHeaderCell BackColor="LightBlue" VerticalAlign="Middle" ColumnSpan="2">
                        Leave Form
                        <br />
                        <table style="border: none" width="95%">
                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="lblEle" runat="server" Text="Eligiblity:-" Font-Bold="true" ForeColor="Green"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCL" runat="server" Text="CL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblCLL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblPL" runat="server" Text="PL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblPLL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblSL" runat="server" Text="SL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblSLL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblLL" runat="server" Text="LOP:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblLLL" runat="server" SkinID="lblMand"></asp:Label>
                                      &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblTL" runat="server" Text="TL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblTLL" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label11" runat="server" Text="Balance:-" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label12" runat="server" SkinID="lblMand" Text="CL:"></asp:Label>
                                    <asp:Label ID="lblBCL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblB" runat="server" Text="PL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblBPL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label16" runat="server" Text="SL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblBSL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label15" runat="server" Text="LOP:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblBLL" runat="server" SkinID="lblMand"></asp:Label>
                                      &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label20" runat="server" Text="TL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblBTL" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label13" runat="server" Text="Approval Pending:-" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label14" runat="server" SkinID="lblMand" Text="CL:"></asp:Label>
                                    <asp:Label ID="lblACL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label17" runat="server" Text="PL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblAPL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label19" runat="server" Text="SL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblASL" runat="server" SkinID="lblMand"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label18" runat="server" Text="LOP:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblALL" runat="server" SkinID="lblMand"></asp:Label>
                                      &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label21" runat="server" Text="TL:" SkinID="lblMand"></asp:Label>
                                    <asp:Label ID="lblATL" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow CssClass="padding" HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="padding" VerticalAlign="Middle">
                        <asp:Label ID="lblName" runat="server" Width="140px" Text=" Name Of the Employee "
                            SkinID="lblMand"></asp:Label>
                        <asp:Label ID="lblcol" runat="server">:</asp:Label>
                        <asp:Label ID="lblemp" runat="server" SkinID="lblMand"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" CssClass="padding" VerticalAlign="Middle">
                        <asp:Label ID="lblHq" runat="server" Width="120px" Text="HQ " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="Label1" runat="server">: </asp:Label>
                        <asp:Label ID="lblSfhq" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                        <asp:Label ID="lblCode" runat="server" Width="140px" Text="Emp Code " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="Label2" runat="server">: </asp:Label>
                        <asp:Label ID="lblempcode" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                        <asp:Label ID="lblDesign" runat="server" Width="120px" Text="Designation " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="Label3" runat="server">: </asp:Label>
                        <asp:Label ID="lbldesig" runat="server" SkinID="lblMand"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableCell BorderWidth="1" CssClass="padding">
                        <asp:Label ID="lblDivision" SkinID="lblMand" Width="140px" runat="server" Text="Division Name "></asp:Label>
                        <asp:Label ID="Label4" runat="server">: </asp:Label>
                        <asp:Label ID="lbldivi" runat="server" SkinID="lblMand"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lbltype" runat="server" Width="140px" Text="Type of Leave " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="Label5" runat="server">: </asp:Label>
                        <asp:DropDownList ID="ddltype" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddltype_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="lblLeave" runat="server" Width="140px" SkinID="lblMand" Text="Leave From Date "></asp:Label>
                        <asp:Label ID="Label6" runat="server">: </asp:Label>
                        <asp:TextBox ID="txtLeave" runat="server" SkinID="MandTxtBox" onkeypress="Calendar_enter(event);"
                            Width="100px" AutoPostBack="true" OnTextChanged="txtLeave_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom"
                            runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtLeave"
                            PopupButtonID="imgPopup" runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="lblLeaveto" runat="server" Width="140px" SkinID="lblMand" Text="Leave To Date "></asp:Label>
                        <asp:Label ID="Label7" runat="server">: </asp:Label>
                        <asp:TextBox ID="txtLeaveto" SkinID="MandTxtBox" runat="server" onkeypress="Calendar_enter(event);"
                            AutoPostBack="true" Width="100px" OnTextChanged="txtLeaveto_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="imgPop" ImageUrl="~/Images/calendar.png" ImageAlign="Bottom"
                            runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtLeaveto"
                            PopupButtonID="imgPop" runat="server" />
                        <br />
                        <br />
                        <asp:Label ID="lblDays" runat="server" Width="140px" SkinID="lblMand" Text="Number Of Days "></asp:Label>
                        <asp:Label ID="Label8" runat="server">: </asp:Label>
                        <asp:Label ID="lblDaysCount" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell CssClass="padding">
                        <asp:Label ID="lblreason" Width="120px" runat="server" Text="Reason For Leave " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="Label9" runat="server">: </asp:Label>
                        <br />
                        <asp:TextBox ID="txtreason" runat="server" onkeypress="AlphaNumeric_NoSpecialChars_New(event);"
                            TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" Height="70px" Width="350px"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblAddr" runat="server" Width="120px" Text="Address On Leave " SkinID="lblMand"></asp:Label>
                        <asp:Label ID="Label10" runat="server">: </asp:Label>
                        <br />
                        <asp:TextBox ID="txtAddr" runat="server" onkeypress="AlphaNumeric_NoSpecialChars_New(event);"
                            TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" Height="70px" Width="350px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="padding" BorderWidth="1" HorizontalAlign="Left" ColumnSpan="2">
                        <asp:Label ID="lblInform" runat="server" SkinID="lblMand" Text="Informed Manager : "></asp:Label>
                        <asp:CheckBoxList ID="chkmanager" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                            CssClass="chkboxLocation" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text=" Phone"></asp:ListItem>
                            <asp:ListItem Value="2" Text=" E-Mail"></asp:ListItem>
                            <asp:ListItem Value="3" Text=" SMS"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:Label ID="lblho" runat="server" SkinID="lblMand" Text="Informed HO : "></asp:Label>
                        <asp:CheckBoxList ID="chkho" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                            CssClass="chkboxLocation" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text=" Phone"></asp:ListItem>
                            <asp:ListItem Value="2" Text=" E-Mail"></asp:ListItem>
                            <asp:ListItem Value="3" Text=" SMS"></asp:ListItem>
                        </asp:CheckBoxList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="padding" HorizontalAlign="Left" ColumnSpan="2">
                        <span style="vertical-align: top">
                            <asp:Label ID="lblValid" runat="server" SkinID="lblMand" Text="If no Phone / E-Mail /SMS ,Valid Reason : "></asp:Label></span>
                        <asp:TextBox ID="lblValidreason" Width="450px" BorderStyle="Solid" BorderColor="Gray"
                            Height="40px" runat="server" TextMode="MultiLine" SkinID="lblMand"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>
        <br />
        <center>
            <asp:Panel ID="pnlmr" runat="server">
                <asp:Button ID="btnApprove" runat="server" Text="Submit" CssClass="savebutton"
                    Width="90px" Height="25px" OnClick="btnApprove_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="savebutton" Width="90px"
                    Height="25px" OnClick="btnClear_Click" />
            </asp:Panel>
        </center>
        </asp:Panel>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
