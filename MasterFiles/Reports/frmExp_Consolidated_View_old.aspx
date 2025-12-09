<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmExp_Consolidated_View.aspx.cs" Inherits="MasterFiles_frmExp_Consolidated_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title>Expense Consolidate View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <script type="text/javascript">
         var popUpObj;
         function showModalPopUp(sfcode, fmon, fyr, StrVac, sf_name) {             
             popUpObj = window.open("rptExp_Consolidated_View.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&StrVac=" + StrVac + "&sf_name=" + sf_name,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
             popUpObj.focus();
             // LoadModalDiv();
         }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
                
                var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

                var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value

                var sf_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
				
				var chkVacant = document.getElementById("chkVacant");

                var strValue = 1;
                if (chkVacant.checked) {
                    var strValue = 0;
                }

                showModalPopUp(sf_code, ddlMonth, ddlYear, strValue, sf_name);
            });
        }); 
    </script> 
    
   
</head>
<body>
    <form id="form1" runat="server">
   
    <div>
         <div id="Divid" runat="server">
        </div>
        
        <center>
            <br />
            <table id="tblSFRpt" cellpadding="0" cellspacing="8" width="50%">
              
                
                <tr>
                    <td align="left">
                        <asp:Label ID="lblState" runat="server" Text="Field Force Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">                       
                         <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                           ToolTip="Enter Text Here" AutoPostBack="false"></asp:TextBox>                          
                          
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left"  style="height: 33px">
                        <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                       &nbsp&nbsp <asp:CheckBox ID="chkProcessed" Text=" At a Glance" runat="server"/>
                    </td>
                </tr>
                 <tr>

                    <td align="left" class="stylespc">
                        <asp:CheckBox ID="chkVacant" Checked="true" runat="server" />
                        <asp:Label ID="lblVacant" Text="Including Vacant" runat="server" SkinID="lblMand"></asp:Label>
                    </td>
                </tr>  
                
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View" 
                CssClass="savebutton" 
                         />

                
              
        </center>
        <br />
        <br />
        <center>
            
            
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    
    </form>
</body>
</html>
