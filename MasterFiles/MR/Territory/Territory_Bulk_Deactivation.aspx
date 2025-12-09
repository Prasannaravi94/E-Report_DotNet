<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Bulk_Deactivation.aspx.cs" Inherits="MasterFiles_MR_Territory_Territory_Bulk_Deactivation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deactivation</title>

    <script type = "text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript" >
        function validateCheckBoxes() {
            
            var isValid = false;
            var gridView = document.getElementById('<%= grdTerr.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;

                            if (confirm('Do you want to Deactivate?')) {

                            }
                            else {
                                return false;
                            }
                            return true;

                            if (confirm('Do you want to Deactivate?')) {
                                if (confirm('Are you sure?')) {
                                    ShowProgress();

                                    return true;

                                }
                                else {
                                    return false;
                                }
                            }
                            else {
                                return false;
                            }
                        }
                    }
                }
            }
            alert("Please Select at least one record.");

            return false;
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <div id="Divid" runat="server">
        </div>
          <div class="container home-section-main-body position-relative clearfix">
         <div class="row justify-content-center ">
                    <div class="col-lg-5">
                        <h2 class="text-center">Bulk Deactivation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                  
                                      
    
         
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                </div>
                                        
                  <div class="single-des clearfix">
                                      
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="1000px"  CssClass="nice-select">
                            <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                        </asp:DropDownList>
                        </div>
                  
                    <div class="single-des clearfix">
                                      
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" Width="1000px" CssClass="nice-select" >
                        </asp:DropDownList>
                    </div>
                   
              <div class="col-lg-6" style="margin-left: -25px;">
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton" OnClick="btnGO_Click" />
               </div>
              
           </div>
                               
                             </div>
                           </div>
               </div>
            <br />
           
           <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          
        </asp:Panel>
        <table width="90%">
      
        <tr> 
          <td align="right" width="30%">
                 <%--   <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" SkinID="lblMand" Visible="true"></asp:Label>--%>
                </td>
                </tr>
                <tr>
                <td align="right">
                    &nbsp;</td>                    
     </tr>
     </table>
  
    <center>
        <asp:GridView ID="grdTerr" runat="server" Width="50%" HorizontalAlign="Center" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                            GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" OnRowCommand="grdTerr_RowCommand" Visible="false" >
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                            
                                
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
                                  <HeaderTemplate >
                                     <asp:CheckBox ID="chkAll" Text="Deactivate All" runat="server" onclick = "checkAll(this);" />
                                   </HeaderTemplate>
                                    <ItemTemplate>                            
                                        <asp:CheckBox ID="chkdeactivate" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Territory_Name" ShowHeader="true" ItemStyle-HorizontalAlign="Left" SortExpression="Territory_Name" 
                                    HeaderText="Name"  HeaderStyle-ForeColor="White" />
                                <asp:BoundField DataField="Territory_Cat" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Type" ItemStyle-Width="15%" />
<%--                                 <asp:TemplateField HeaderText ="Deactivate" HeaderStyle-ForeColor ="White" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle ForeColor ="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="true" />
                          <ItemStyle ForeColor ="DarkBlue" Font-Bold ="false" />
                          <ItemTemplate >
                              <asp:LinkButton ID="lnkbutDeactivate" runat ="server" CommandArgument='<%# Eval("Territory_Code") %>' 
                              CommandName ="Deactivate" OnClientClick ="return confirm('Do you want to Deactivate');">Deactivate 
                              </asp:LinkButton>
                          </ItemTemplate>
                        
                        </asp:TemplateField>--%>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" Width="80%"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />                        
                        </asp:GridView>
    </center>
    <center>
         <table>
            <tr>
                <td>
                   <asp:Button ID="btnSubmit" runat="server" Width="80px" Height="30px" Text="DeActivate" OnClientClick="return validateCheckBoxes()" CssClass="resetbutton" Visible="false"  
                     
                        onclick="btnSubmit_Click"/>                        
                </td>
            </tr>
        </table>
    </center>
                     
    </div>
             
   
        </div>

    </form>
</body>
</html>
