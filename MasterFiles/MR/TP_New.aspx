<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_New.aspx.cs" Inherits="MasterFiles_MR_TP_New" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
    <link href="../../css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>

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
    <style type="text/css">
        .Color
        {
            background: yellow;
        }
    </style>
    <%--<script type="text/javascript">
        $(function () {
            $(".ddlclass").change(function () {
                if ($(this).find('option:selected').text() == "Field Work") {
                    alert('1');
                   
                } else {
                    alert('2');
                }
            }); 
        });
</script>--%>
    <%--  <script type="text/javascript">

 function disable(id) {
     $(id)
        .parent()// parent of ddl is td
        .next()// gives the next td 
        .find('select')// finds a select element in that td
        .attr('disabled', (id.value != "96") ? 'disabled' : true); //enable-disable
   
} 
</script>--%>
</head>
<body>

  <%-- <script language="javascript" type="text/javascript">
       function ValidateEmptyValue() {

           var gv = document.getElementById("grdTP");
           var items = gv.getElementsByTagName('select');
           for (i = 0; i < items.length; i++) {
               if (items[i].type == "select-one") {
                   var index = items[i].selectedIndex;
                   if (items[i].options[index].value == "0") {
                       var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlwrktype');
                       alert(index);
                       items[i].focus();
                       return false;
                   }
               }
           }
       }

 </script>--%>
   <script type="text/javascript">
       function ValidateEmptyValue() {
           var gridView = $("table[id*=grdTP]");
           var dropdownList = $("table[id*=grdTP] select");

           var GvLength = $("table[id*=grdTP] tr:not(:first)").length;

           selected = $("table[id*=grdTP]  tr td select");

           Len = $("table[id*=grdTP]  tr td select").length;
           //alert(Len);

           Textbox = $("table[id*=grdTP] tr td input:text");

           TxtLen = $("table[id*=grdTP] tr td input:text").length;

           if (GvLength > 0) {

               for (var i = 0; i < Len; i++) {

                   var ddlwrktype = $(selected[i]).closest("td").find("select[id$='ddlwrktype'] option:selected").val();

                   var ddlTerr = $(selected[i]).closest("td").find("select[id$='ddlTerr'] option:selected").val();



                   if (ddlwrktype == "0") {
                       createCustomAlert("Select Work Type");
                       $(this).focus();
                       return false;

                   }
                   //                   if (ddlwrktype == "96") {
                   //                       if (ddlTerr == "0") {
                   //                           createCustomAlert("Select Patch");
                   //                           $(this).focus();
                   //                           return false;
                   //                       }
                   //                   }


               }





               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Do You want to Submit?")) {
                   confirm_value.value = "Yes";
               }
               else {
                   confirm_value.value = "No";
                   return false;
               }
               document.forms[0].appendChild(confirm_value);
           }
       }

       
</script>

 <script type="text/javascript">
     function DraftValidateEmptyValue() {
         var grid = document.getElementById('<%= grdTP.ClientID %>');
         if (grid != null) {

             var isEmpty = false;
             var Inputs = grid.getElementsByTagName("input");
             var Incre = Inputs.length;
             var cnt = 0;
             var index = '';


             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to Save as a Draft ?")) {
                 confirm_value.value = "Yes";
             }
             else {
                 confirm_value.value = "No";
                 return false;

             }
             document.forms[0].appendChild(confirm_value);

         }
     }
</script>
    <form id="form1" runat="server">
    <div>

        <ucl:Menu1 ID="menu" runat="server" />

    

      
</div>
        <div align="center">
            <table id="tblMargin" runat="server" align="center">
                <tr>
                    <td>
                        <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of " Font-Size="Medium" ForeColor="Green"
                            Font-Names="Verdana"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLink" runat="server" Font-Size="Small" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        <asp:LinkButton ID="hylEdit" runat="server" OnClick="hylEdit_Onclick"
                            Font-Size="Small" Font-Names="Verdana" ForeColor="Blue"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>

        <div>
            <center>
                <table width="65%" align="center" runat="server" id="tabsf">
                    <tr>
                        <%-- <td width="2.5%"></td>--%>
                        <td align="left">
                            <asp:Label ID="lblname" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server" Font-Names="Verdana" ForeColor="#A0522D"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                           
                        </td>
                        <td align="left">
                         
                        </td>
                        <td align="left">
                         
                        </td>
                    </tr>
                    <tr>
                    <td align="left">
                      <asp:Label ID="lblemp" Text="Employee Id :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblempid" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td align="left">
                       <asp:Label ID="lbldoj" Text="DOJ :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lbldoj2" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                        <td align="left">
                         <asp:Label ID="lblreport" Text="Reporting To :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblreportname" runat="server" Font-Names="Verdana" ForeColor="#A0522D"
                                Font-Bold="true"></asp:Label>
                    </td>
                    </tr>
                </table>
            </center>

                 <section style="display: block">
   <ol id="ExampleList">
   
    <li><a href="#NCP_Lightbox" onclick="getInternetExplorerVersion()"><img src="../../Images/help_animated.gif" alt="" /></a></li>
  
    </ol>
<div class="ncp-popup ncp-popup-overlay" id="NCP_Lightbox">
	<div class="ncp-popup-spacer">
		<a href="#" id="LnkClose" class="ncp-popup-close">X</a>
		<div class="ncp-popup-container">
			
			<div class="ncp-popup-content">
				 <h2 style="color: Red; font-weight: bold;font-family: Arial, Helvetica, sans-serif;">
                    TP - Entry / Edit</h2>
                <p class="p">
                    1. Fill Your "TP" for all days and Press "Send to Manager Approval" Button for Manager
                    Approval.</p>
                <p>
                    2. After Approval From Your Manager, then next Month "TP" will open.</p>
                <p>
                    3. After Selecting the "Field Work" , the Area will appear for Selection for the
                    Particular Day. Whichever having the Doctors, those area only will appear.
                </p>
                <p>
                    4. "Without Doctor Areas" - will not reflect in your TP- Entry.
                </p>
                <p>
                    5. For Other Worktypes, not Possible to Select the Areas. The "Selection box" will
                    be in "Disable" Mode.</p>
                <p>
                    6. Before Approval from your Manager, You can Edit your TP for the Particular Month.</p>
                <p>
                    7. After Approval from your Manager, the Fieldforce cannot Edit their TP. Get the
                    Permission from "Admin", then the Fiedlforce can Edit their "TP" for the required
                    month.</p>
			</div>
		</div>
	</div>
        </div>


        <center>
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdTP" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" CssClass="mGridImg" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                      
                                        <asp:DropDownList ID="ddlwrktype" runat="server" SkinID="ddlRequired" Width="150px"
                                            DataSource="<%# FillWorkType() %>" CssClass="ddlclass" DataTextField="WorkType_Name_B"
                                            DataValueField="WorkType_Code_B" onchange="disable(this)">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Patch Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                           <asp:DropDownList ID="ddlTerr" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                            DataTextField="Territory_Name" DataValueField="Territory_Code">
                                        </asp:DropDownList>
                                    
                                 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                      <asp:HiddenField ID="hdnholiday" runat="server" Value='<%#  Eval("Holiday") %>' />
                                        <asp:HiddenField ID="hdnweekoff" runat="server" Value='<%#  Eval("weekoff") %>' />
                                        <asp:HiddenField ID="hdnField_Work" runat="server" Value='<%#  Eval("Field_Work") %>' />
                                            <asp:HiddenField ID="hdnterr_code" runat="server" Value='<%#  Eval("territory_code") %>' />
                                            <asp:HiddenField ID="hdnwrktype_code" runat="server" Value='<%#  Eval("WorkType_Code_B") %>' />
                                        <asp:TextBox ID="txtObjective" runat="server" SkinID="MandTxtBox" Text='<%#  Eval("Objective") %>'
                                            Width="250">                                           
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Width="85px" Height="26px" Text="Draft Save" 
                        OnClick="btnSave_Click" OnClientClick="return DraftValidateEmptyValue()" />     &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" CssClass="savebutton" Width="175px" Height="26px" runat="server"
                Text="Send to Manager Approval" OnClick="btnSubmit_Click" OnClientClick="return ValidateEmptyValue()" />
        </center>
    </div>
    </form>
</body>
</html>
