<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Device_Lock.aspx.cs" Inherits="MasterFiles_Options_Device_Lock" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Device Lock</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <script type="text/javascript">
             function SelectAllCheckboxes(chk, selector) {

                 $('#<%=gvDetails.ClientID%>').find(selector + " input:checkbox").each(function () {
                   $(this).prop("checked", $(chk).prop("checked"));
               });
             }

             
    </script>
      <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <style type="text/css" >
       [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
left: inherit !important;
}
        [type="checkbox"]:not(:checked) + label:before
         {
            display:none !important;
        
        }
        [type="checkbox"]:checked + label:before
        {
            display:none !important;
        
        }
        [type="checkbox"]:checked + label:after
        {
            display:none !important;
        
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <ucl:Menu ID="menu1" runat="server" />
                <br />
                <center>
            <div id="Divid" runat="server">
            </div>
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name"  CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>
                                </div>
                            <div class="single-des clearfix">
                     <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" CssClass="custom-select2 nice-select">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>                                 
                     <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" CssClass="custom-select2 nice-select">
                    </asp:DropDownList>
                    
</div>
                            </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                      <br />
            <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" 
                onclick="btnGo_Click" />
          
                             </div>
                        </div>
                    </div>
                      </div>
                  
                <br />
            <table width="90%">
                <tr>
                    <td>             
                        <asp:GridView ID="gvDetails"  runat="server"  HorizontalAlign="Center" 
                            AutoGenerateColumns="false"  
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" EmptyDataText="No Records Found" 
                            AlternatingRowStyle-CssClass="alt" OnRowDataBound="gvDetails_RowDataBound"  >
                            <HeaderStyle Font-Bold="False" />
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (gvDetails.PageIndex * gvDetails.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left" ></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblsf_code" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"   HeaderText="FieldForce Name">
                                    <ControlStyle Width="50%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>  
                                        <asp:Label ID="lblsf_name"  runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>   

                                  <asp:TemplateField ItemStyle-HorizontalAlign="Left"   HeaderText="HQ">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="10px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>  
                                        <asp:Label ID="lblhq"  runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Left"   HeaderText="Designation">
                                    <ControlStyle Width="5%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="10px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>  
                                        <asp:Label ID="lbldesg"  runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"   HeaderText="Emp Code">
                                    <ControlStyle Width="5%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="10px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>  
                                        <asp:Label ID="lblsf_emp_id"  runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left"   HeaderText="State Name">
                                    <ControlStyle Width="5%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Width="10px" BorderColor="Black"></ItemStyle>                                
                                    <ItemTemplate>  
                                        <asp:Label ID="lblStateName"  runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="Andorid App">
                                     <HeaderTemplate>
                                         <asp:CheckBox ID="chkAndoridAppAll" runat="server" Text="Andorid App " onclick="SelectAllCheckboxes(this, '.AndoridApp')" />
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:hiddenfield ID="HdnAndoridApp"  runat="server" Value='<%# Bind("Android_App") %>'></asp:hiddenfield>
                                         <asp:CheckBox ID="chkAndoridApp" runat="server" Font-Bold="true" CssClass="AndoridApp" />
                                     </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="IOS App">
								    <HeaderTemplate>
									    <asp:CheckBox ID="chkIOSAppAll" runat="server" Text="IOS App" onclick="SelectAllCheckboxes(this, '.IOSApp')" />
								    </HeaderTemplate>
								    <ItemTemplate>
                                        <asp:hiddenfield ID="HdnIOSApp"  runat="server" Value='<%# Bind("ios_app") %>'></asp:hiddenfield>
									    <asp:CheckBox ID="chkIOSApp" runat="server" Font-Bold="true" CssClass="IOSApp" />
								    </ItemTemplate>
							    </asp:TemplateField>
                             
                                    <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="Andorid Detailing">
                                      <HeaderTemplate>
                                          <asp:CheckBox ID="chkAndoridDetailingAll" runat="server" Text="Andorid Detailing" onclick="SelectAllCheckboxes(this, '.AndoridDetailing')" />
                                      </HeaderTemplate>
                                      <ItemTemplate>
                                      <asp:hiddenfield ID="HdnAndoridDetailing"  runat="server" Value='<%# Bind("Android_Detailing") %>'></asp:hiddenfield>
                                      <asp:CheckBox ID="chkAndoridDetailing" runat="server" Font-Bold="true" CssClass="AndoridDetailing" />
                                     </ItemTemplate>
                                 </asp:TemplateField> 
                                 <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" HeaderText="IOS Detailing">
                                      <HeaderTemplate>
                                          <asp:CheckBox ID="chkIOSDetailingAll" runat="server" Text="IOS Detailing" onclick="SelectAllCheckboxes(this, '.IOSDetailing')" />
                                      </HeaderTemplate>
                                      <ItemTemplate>
                                      <asp:hiddenfield ID="HdnIOSDetailing"  runat="server" Value='<%# Bind("ios_Detailing") %>'></asp:hiddenfield>
                                      <asp:CheckBox ID="chkIOSDetailing" runat="server" Font-Bold="true" CssClass="IOSDetailing" />
                                     </ItemTemplate>
                                 </asp:TemplateField> 
                              </Columns>
                              <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                                   
                    </td>
                </tr>
            </table>
                    
        </center>
          <br />
                 <center>
            <asp:Button ID="btnSumbit" runat="server" Width="70px" Height="25px" Text="Save" CssClass="BUTTON" Visible="false" onclick="btnSumbit_Click" 
                />

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
