<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_Weekly_Mapping.aspx.cs" Inherits="MasterFiles_MR_Territory_Territory_Weekly_Mapping" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territorywise-Week Mapping</title>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" /> 
            <br />
    <div>
            <table width="100%">
         <tr>
                <td style="width: 18%">
                </td>
                <td>
                    <asp:Panel ID="pnlAdmin" runat="server">
                        <asp:Label ID="lblSalesforce" runat="server" Text="Field Force Name"></asp:Label>
                             <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
            ToolTip="Enter Text Here"></asp:TextBox>  
                        <asp:DropDownList ID="ddlSFCode" runat="server" Width="300px" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                    </asp:Panel>
                       </td>
                <td align="right" width="30%">
                    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
         <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                          <asp:GridView ID="grdTerritory" runat="server" Width="65%" HorizontalAlign="Center"
                            AllowSorting="true"  EmptyDataText="No Records Found" OnPageIndexChanging="grdTerritory_PageIndexChanging"
                            AutoGenerateColumns="false" AllowPaging="True" OnRowDataBound="grdTerritory_RowDataBound"
                            PageSize="100" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="12px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdTerritory.PageIndex * grdTerritory.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="180px" HeaderText="Territory Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritory_Name" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Type" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="Territory_Type" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                          <asp:RequiredFieldValidator ControlToValidate="Territory_Type" ID="RequiredFieldValidator2"
                                            ErrorMessage="*Required" InitialValue="0" runat="server"  setfocusonerror="true" 
                                            Display="Dynamic"></asp:RequiredFieldValidator>   
                                    </EditItemTemplate>

                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Category" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left">
                                 <ItemTemplate>
                                        
                                       <asp:DropDownList ID="ddl_cat_type" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="I Week-Monday" Value="MO1"></asp:ListItem>
                                            <asp:ListItem Text="II Week-Monday" Value="MO2"></asp:ListItem>
                                            <asp:ListItem Text="III Week-Monday" Value="MO3"></asp:ListItem>
                                            <asp:ListItem Text="IV Week-Monday" Value="MO4"></asp:ListItem>
                                            <asp:ListItem Text="I Week-Tuesday" Value="TU1"></asp:ListItem>
                                            <asp:ListItem Text="II Week-Tuesday" Value="TU2"></asp:ListItem>
                                            <asp:ListItem Text="III Week-Tuesday" Value="TU3"></asp:ListItem>
                                            <asp:ListItem Text="IV Week-Tuesday" Value="TU4"></asp:ListItem>
                                            <asp:ListItem Text="I Week-Wednesday" Value="WE1"></asp:ListItem>
                                            <asp:ListItem Text="II Week-Wednesday" Value="WE2"></asp:ListItem>
                                            <asp:ListItem Text="III Week-Wednesday" Value="WE3"></asp:ListItem>
                                            <asp:ListItem Text="IV Week-Wednesday" Value="WE4"></asp:ListItem>
                                            <asp:ListItem Text="I Week-Thursday" Value="TH1"></asp:ListItem>
                                            <asp:ListItem Text="II Week-Thursday" Value="TH2"></asp:ListItem>
                                            <asp:ListItem Text="III Week-Thursday" Value="TH3"></asp:ListItem>
                                            <asp:ListItem Text="IV Week-Thursday" Value="TH4"></asp:ListItem>
                                            <asp:ListItem Text="I Week-Friday" Value="FR1"></asp:ListItem>
                                            <asp:ListItem Text="II Week-Friday" Value="FR2"></asp:ListItem>
                                            <asp:ListItem Text="III Week-Friday" Value="FR3"></asp:ListItem>
                                            <asp:ListItem Text="IV Week-Friday" Value="FR4"></asp:ListItem>
                                            <asp:ListItem Text="I Week-Saturday" Value="SA1"></asp:ListItem>
                                            <asp:ListItem Text="II Week-Saturday" Value="SA2"></asp:ListItem>
                                            <asp:ListItem Text="III Week-Saturday" Value="SA3"></asp:ListItem>
                                            <asp:ListItem Text="IV Week-Saturday" Value="SA4"></asp:ListItem>
                                        </asp:DropDownList>  
                                   </ItemTemplate>
                                    
                                   
                                    </asp:TemplateField>


                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                          </asp:GridView>
                        </td></tr></tbody></table>
        <center>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnsave" runat="server" onclick="btnsave_click" Text="Save"/> 
                </td>
                </tr>
            </table>
            </center>
    
    </div>
    </form>
</body>
</html>
