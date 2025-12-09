<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expense_Status_View.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>


<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Status View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdWTAllowance.ClientID %>');
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
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
            <table>
                <tr>
                   
                     <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left" class="stylespc"><asp:dropdownlist ID="monthId" runat="server"></asp:dropdownlist></td>
                      <td align="left" class="stylespc">
                        <asp:Label ID="lblYr" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                    </td> 
                    <td align="left" class="stylespc"><asp:DropDownList ID="yearID" runat="server"></asp:DropDownList></td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
					
                   <td><asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton" OnClick="btnSF_Click"
                 /></td>
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
                            <asp:GridView ID="grdWTAllowance" Width="39%" runat="server" AutoGenerateColumns="false"
                            BorderStyle="Solid" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" GridLines="None" ShowFooter="true" ShowHeader="false"
                            OnRowCreated="grvMergeHeader_RowCreated" OnRowDataBound="grdExpense_RowDataBound" >
                                 <FooterStyle Width="60px"  BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="right"></FooterStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division Name" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label font-size="9pt" style="white-space:pre" font-name="Lucida Console"   ID="lblWorktype_Name" runat="server" Text='<%# Eval("division_name")%>'></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lblTotal" style="color:Red;font-weight:bold" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Base Level Applied" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblBA" Width="50px" Text='<%# Eval("BA")%>' runat="server"  ></asp:Label>
                                         <asp:HiddenField ID="code" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Base Level Approved" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA1" Width="50px" Text='<%# Eval("BA1")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code1" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA1" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>

                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Active" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA12" Width="50px" Text='<%# Eval("BA12")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code12" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA12" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>

                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="1st line Mgr Applied" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA2" Width="50px" Text='<%# Eval("BA2")%>' runat="server"  ></asp:Label>
                                         <asp:HiddenField ID="code2" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA2" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="1st line Mgr Approved" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA3" Width="50px" Text='<%# Eval("BA3")%>' runat="server"  ></asp:Label>
                                         <asp:HiddenField ID="code3" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA3" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA13" Width="50px" Text='<%# Eval("BA13")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code13" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA13" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>

                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="2nd Line Mgr Applied" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA4" Width="50px" Text='<%# Eval("BA4")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code4" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA4" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="2nd Line Mgr Approved" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA5" Width="50px" Text='<%# Eval("BA5")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code5" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA5" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA14" Width="50px" Text='<%# Eval("BA14")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code14" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA14" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="3rd Line Mgr Applied" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA6" Width="50px" Text='<%# Eval("BA6")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code6" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA6" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="3rd Line Mgr Approved" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA7" Width="50px" Text='<%# Eval("BA7")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code7" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA7" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA15" Width="50px" Text='<%# Eval("BA15")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code15" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA15" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="4th Line Mgr Applied" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA8" Width="50px" Text='<%# Eval("BA8")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code8" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA8" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="4th Line Mgr Approved" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate >
                                        <asp:Label ID="lblBA9" Width="50px" Text='<%# Eval("BA9")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code9" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA9" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA16" Width="50px" Text='<%# Eval("BA16")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code16" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA16" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="5th Line Mgr Applied" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate >
                                        <asp:Label ID="lblBAA1" Width="50px" Font-Bold="true" Text='<%# Eval("BA10")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="codeA1" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBAA1" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="5th Line Mgr Approved" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBAA2" Width="50px" Font-Bold="true" Text='<%# Eval("BA11")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="codeA2" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBAA2" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Active" Visible="false" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="right" >
                                    <ItemTemplate  >
                                        <asp:Label ID="lblBA17" Width="50px" Font-Bold="true" Text='<%# Eval("BA17")%>' runat="server" ></asp:Label>
                                         <asp:HiddenField ID="code17" runat="server" Value='<%# Bind("division_code") %>' />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="ftlblBA17" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>

                                </asp:TemplateField>


                            </Columns>

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
