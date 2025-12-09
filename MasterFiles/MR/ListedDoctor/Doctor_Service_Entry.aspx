<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_Entry.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor - Service Entry</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .modal
        {
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
        .loading
        {
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
        .height
        {
            height: 20px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <script type="text/javascript" language="javascript">
        function disp_confirm() {
            if (confirm("Do you want to Create the ID ?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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

                if ($("#txtStockist_Name").val() == "") {
                    createCustomAlert("Please Enter Stockist Name.");
                    $('#txtStockist_Name').focus();
                    return false;
                }
                if ($("#ddlPoolName").val() == 0) {

                    createCustomAlert("Please Select HQ Name");
                    $('#ddlPoolName').focus();
                    return false;

                }
                if (!CheckBoxSelectionValidation()) {
                    return false;
                }

            });


            $('#btnSave').click(function () {

                if ($("#txtStockist_Name").val() == "") {
                    createCustomAlert("Please Enter Stockist Name.");
                    $('#txtStockist_Name').focus();
                    return false;
                }
                if ($("#ddlState").val() == 0) {

                    createCustomAlert("Please Select State");
                    $('#ddlState').focus();
                    return false;
                }
                if ($("#ddlPoolName").val() == 0) {

                    createCustomAlert("Please Select HQ Name");
                    $('#ddlPoolName').focus();
                    return false;
                }

                if (!CheckBoxSelectionValidation()) {
                    return false;
                }

            });

        }); 
    </script>
    <style type="text/css">
        .div_fixed
        {
            position: fixed;
            top: 400px;
            right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
       <div><p style="font-family:Arial;font-weight:bold;font-size:14px;color:Blue">Listed Doctor Detail</p></div> 
     </center>
        <br />
        <center>
            <table width="65%" border="0" cellpadding="3" cellspacing="3" align="center">
            
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDoctorName" runat="server" Height="19px" Width="100px" SkinID="lblMand">
                        Doctor Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_DoctorName" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblAddress" runat="server" Text="Address" Height="19px" Width="100px"
                            SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Doctor_Address" runat="server" SkinID="TxtBxAllowSymb" Width="240px"
                            TabIndex="2" onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White' "
                            MaxLength="150" onkeypress="AlphaNumeric(event);"> 
                        </asp:TextBox>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblCategory" runat="server" Text="Category" Height="19px" Width="100px"
                            SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Category" runat="server" SkinID="TxtBxAllowSymb" Width="100px"
                            TabIndex="2" onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White' "
                            MaxLength="150" onkeypress="AlphaNumeric(event);" Height="22px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblQualification" runat="server" Text="Qualification" Height="19px"
                            Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Qualification" runat="server" Width="100px" TabIndex="3"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);" SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblspeciality" runat="server" Text="Speciality" Height="19px"
                            Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Speciality" runat="server" SkinID="TxtBxAllowSymb"
                            Width="100px" TabIndex="4" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White' " MaxLength="15" onkeypress="AlphaNumeric_NoSpecialCharshq(event);">
                        </asp:TextBox>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblClass" runat="server" Text="Class" Height="19px"
                            Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Class" runat="server" SkinID="TxtBxAllowSymb"
                            Width="100px" TabIndex="4" onfocus="this.style.backgroundColor='LavenderBlush'"
                            onblur="this.style.backgroundColor='White' " MaxLength="15" onkeypress="AlphaNumeric_NoSpecialCharshq(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMobile" runat="server" Text="Mobile No" Height="19px"
                            Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Mobile" runat="server" Width="100px" TabIndex="5" SkinID="TxtBxNumOnly"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="CheckNumeric(event);">
                        </asp:TextBox>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:Label ID="lblEmail" runat="server" Text="Email ID" Height="19px"
                            Width="100px" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txt_Email" runat="server" Width="100px" TabIndex="5" SkinID="TxtBxNumOnly"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="CheckNumeric(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblDoctorServiceTime" runat="server" Text="Service Amount Given to this Dr till Date" Height="19px"
                            SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtService" runat="server" Width="100px" TabIndex="5" SkinID="TxtBxNumOnly"
                            onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                            MaxLength="100" onkeypress="CheckNumeric(event);">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                 <td align="left" class="stylespc">
                        <asp:Label ID="lblBusinessDate" runat="server" Text="Business given till Date" Height="19px"
                            SkinID="lblMand"></asp:Label>
                 </td>
                 <td align="left" class="stylespc">
                    <asp:TextBox ID="txtBusinessDate" runat="server" Width="100px" TabIndex="5" SkinID="TxtBxNumOnly"
                        onfocus="this.style.backgroundColor='LavenderBlush'" onblur="this.style.backgroundColor='White'"
                        MaxLength="100" onkeypress="CheckNumeric(event);">
                    </asp:TextBox>
                 </td>
                </tr>
            </table>           
        </center>
        <center>
        <p style="font-family:Sans-Serif;font-weight:bold;font-size:14px;color:Green">Visit - Last Three Months</p>
        </center>
        <center>
        <asp:GridView ID="grdVisit" runat="server" Width="85%" HorizontalAlign="Center"
        EmptyDataText="No Records Found" AutoGenerateColumns="false"      
        GridLines="None" CssClass="mGrid"  AlternatingRowStyle-CssClass="alt">
        <HeaderStyle Font-Bold="False" />
        <PagerStyle CssClass="gridview1"></PagerStyle>
        <SelectedRowStyle BackColor="BurlyWood" />
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                  <asp:Label ID="lblSNo" runat="server" Text='<%#((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>          
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SF Code" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblSfCode" runat="server" Text=''></asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
          <asp:TemplateField  HeaderText="MR"
            ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:Label ID="lblMR" runat="server" Text=''></asp:Label>              
            </ItemTemplate>
          </asp:TemplateField>
           <asp:TemplateField  HeaderText="ASM"
            ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:Label ID="lblASM" runat="server" Text=''></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField  HeaderText="RSM"
            ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:Label ID="lblRSM" runat="server" Text=''></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
           <asp:TemplateField  HeaderText="ZSM"
            ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:Label ID="lblZSM" runat="server" Text=''></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        </asp:GridView>
        </center>
        <br />
        <center>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:TemplateField>
        
        </asp:TemplateField>
        </Columns>
        </asp:GridView>
        </center>
        <br />
        <center>
        
        <table width="80%" border="0" cellpadding="3" cellspacing="3" align="center">
            
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTotalBusiness" runat="server" Height="19px"  SkinID="lblMand">
                        Total Business return Expected the Doctor in Amt (Rs/-)</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtTotalBusiness" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                 
                </tr>
                  <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblROI" runat="server" Height="19px"  SkinID="lblMand">
                        ROI Duration Mark</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtROI" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                 
                </tr>
               <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Height="19px"  SkinID="lblMand">
                        Service Requried</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtServiceReq" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                 
                </tr>
                 <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label2" runat="server" Height="19px"  SkinID="lblMand">
                        Service Amount</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtServiceAmt" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                 
                </tr>
                 <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSpecificActive" runat="server" Height="19px"  SkinID="lblMand">
                        Specific Activities (Remarks)</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtSpecificAct" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                 
                </tr>
                 <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblPrescription" runat="server" Height="19px"  SkinID="lblMand">
                        Prescription Outlets (Chemist)</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtPrescription_1" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>&nbsp;
                             <asp:TextBox ID="txtPrescription_2" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>&nbsp;
                             <asp:TextBox ID="txtPrescription_3" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                 
                </tr>
                <tr>
                 <td align="left" class="stylespc">
                        <asp:Label ID="lblStockist" runat="server" Height="19px"  SkinID="lblMand">
                            Stockist
                        </asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                            <asp:TextBox ID="txtStockist_1" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>&nbsp;
                             <asp:TextBox ID="txtStockist_2" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>&nbsp;
                             <asp:TextBox ID="txtStockist_3" runat="server" Width="120px" TabIndex="1" onfocus="this.style.backgroundColor='#E0EE9D'"
                            onblur="this.style.backgroundColor='White'" MaxLength="100" onkeypress="AlphaNumeric_NoSpecialChars(event);"
                            SkinID="TxtBxAllowSymb"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </center>
        <center>
        <div>
          <asp:Button ID="btnProcess" runat="server" Text="Process" CssClass="savebutton" 
                         Width="100px" Height="25px" />
        </div>
        </center>
    </div>
    </form>
</body>
</html>
