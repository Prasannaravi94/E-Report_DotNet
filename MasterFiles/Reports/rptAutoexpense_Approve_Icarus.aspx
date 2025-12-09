<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptAutoexpense_Approve_Icarus.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement Approval View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
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
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>

<script type="text/javascript" language="javascript">
         $(document).ready(function () {
             $("#testImg").hide();
             $('#linkcheck').click(function () {
                 window.setTimeout(function () {
                     $("#testImg").show();
                 }, 500);
             })
         });
               
</script> 
 <script type="text/javascript">
     $(function () {
         var $txt = $('input[id$=txtNew]');
         var $ddl = $('select[id$=ddlSubdiv]');
         var $items = $('select[id$=ddlSubdiv] option');

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server"></div>
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSubdiv" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                     <td align="left" class="stylespc">
	<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
            ToolTip="Enter Text Here"></asp:TextBox>
            <asp:LinkButton ID="linkcheck" runat="server" 
                            onclick="linkcheck_Click">
                          <img src="../../Images/Selective_Mgr.png" />
					</asp:LinkButton>
                        <asp:DropDownList ID="ddlSubdiv" SkinID="ddlRequired" runat="server" Visible="false">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
	<td>
                    
                          <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height:20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold; ">Loading Please Wait...</span>
                           </div>
                    </td>
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc"><asp:dropdownlist ID="monthId" runat="server"></asp:dropdownlist></td>
                      <td align="left" class="stylespc">
                        <asp:Label ID="lblYr" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                    </td> 
                    <td align="left" class="stylespc"><asp:DropDownList ID="yearID" runat="server"></asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
					
                   <td><asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton" Visible="false"
                OnClick="btnSF_Click" /></td>
                </tr>
            </table>
        </center>
        <br />
        <table align="right" style="margin-right: 5%">
            <tr>
                <td align="right">
                    <asp:Panel ID="pnlprint" runat="server" Visible="false">
                        <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                            onclick="PrintGridData()" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
      
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                              <asp:Label ID="lblsfempid" runat="server" font-size='8' Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                         </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Fieldforce Name">
                                        <ItemStyle width="300px" Wrap="false" font-size='8' BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="sfNameHidden" runat="server" Value='<%#Eval("sf_name")%>' />
                                            <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%#Eval("SF_Code")%>' />
                                            <asp:HiddenField ID="Hiddenhold" runat="server" Value='<%#Eval("SF_VacantBlock")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label align='left' ID="lblsfName" runat="server" Width="200" font-size='8'  Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesign" runat="server" font-size='8' Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DOJ" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoJ" runat="server" font-size='8' WIDTH="90" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Width="100" font-size='8' Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Submission Date" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldt" runat="server" font-size='8' Width="100" Text='<%# Bind("Date") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="MGR Approved Date" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDTMGR" runat="server" Text='<%# Bind("Approval_Datea") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="Admin Approved Date" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDTadmin" runat="server" Text='<%# Bind("admin_approval_date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid"  Width="60px" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfAll" runat="server" font-size='8' Text='<%# Bind("allowance") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" Width="60px" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfFare" runat="server" font-size='8' Text='<%# Bind("fare") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                       <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label13" style="text-align:center;font-family:Calibri" font-size='8' Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label14" style="text-align:center;font-family:Calibri"  font-size='8' Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label15" style="text-align:center;font-family:Calibri"  font-size='8' Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            
                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label16"  style="text-align:center;font-family:Calibri" font-size='8' Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            
                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                     <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label17"  style="text-align:center;font-family:Calibri"  font-size='8' Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                         
                             <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                     <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label18"  style="text-align:center;font-family:Calibri"  font-size='8' Text='<%# Eval("Fixed_Column6")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Miscellaneous" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmisamt" runat="server" font-size='8' Text='<%# Bind("mis_Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Additional Expense" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblexpamt" runat="server" font-size='8' Text='<%# Bind("rw_amount") %>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText=" + " ItemStyle-HorizontalAlign="Left" Visible="true">
                                        <ItemStyle Width="40px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIncrement" runat="server" font-size='8' Text='<%# Bind("Increment") %>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText=" - " ItemStyle-HorizontalAlign="Left" Visible="true">
                                        <ItemStyle BorderStyle="Solid" Width="40px" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDecrement" runat="server" font-size='8' Text='<%# Bind("Decrement") %>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>

                                    

                                     <asp:TemplateField HeaderText="Claimed <br/>Amount(By MR)" ItemStyle-HorizontalAlign="Left">
                                                                              <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblClimedAmnt" runat="server" font-size='8' Text='<%# Bind("tot") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved <br/>Amount(By Admin)" ItemStyle-HorizontalAlign="Left">
                                                      
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppAmnt" runat="server" font-size='8' Text='<%# Bind("appAmnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                 
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
            
        </div>
    </div>
    </form>
</body>
</html>
