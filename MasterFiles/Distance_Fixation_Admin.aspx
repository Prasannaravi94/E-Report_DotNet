<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distance_Fixation_Admin.aspx.cs"
    Inherits="MasterFiles_Distance_Fixation" EnableEventValidation="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SFC Updation</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function GridViewRepeatColumns(grdHQ, repeatColumns) {
            //Created By: Brij Mohan(http://techbrij.com)
            GridViewRepeatColumns("<%=grdHQ.ClientID %>", 2);
            if (repeatColumns < 2) {
                alert('Invalid repeatColumns value');
                return;
            }
            var $gridview = $('#' + grdHQ);
            var $newTable = $('<table></table>');

            //Append first row in table
            var $firstRow = $gridview.find('tr:eq(0)'),
            firstRowHTML = $firstRow.html(),
            colLength = $firstRow.children().length;

            $newTable.append($firstRow);

            //Append first row cells n times
            for (var i = 0; i < repeatColumns - 1; i++) {
                $newTable.find('tr:eq(0)').append(firstRowHTML);
            }

            while ($gridview.find('tr').length > 0) {
                var $gridRow = $gridview.find('tr:eq(0)');
                $newTable.append($gridRow);
                for (var i = 0; i < repeatColumns - 1; i++) {
                    if ($gridview.find('tr').length > 0) {
                        $gridRow.append($gridview.find('tr:eq(0)').html());
                        $gridview.find('tr:eq(0)').remove();
                    }
                    else {
                        for (var j = 0; j < colLength; j++) {
                            $gridRow.append('<td></td>');
                        }
                    }
                }
            }
            //update existing GridView
            $gridview.html($newTable.html());
        }

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
    </div>  

    <center>
        <table>
            <tr>
 <td align="left" class="stylespc">
                            <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                
			<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
            ToolTip="Enter Text Here"></asp:TextBox>
            <asp:LinkButton ID="linkcheck" runat="server" 
                            onclick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
					</asp:LinkButton>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" Visible="false" SkinID="ddlRequired">
<asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton" Visible="false"
                      OnClick="btnSubmit_Click" />
                      <asp:Button ID="btnclear" runat="server" CssClass="savebutton" Width="40px" Height="25px" Text="Clear" OnClick="btnClear_Click" />
                </td>
 				<td>
                    
                          <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height:20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold; ">Loading Please Wait...</span>
                           </div>
                    </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        
    </center>
    <table width="100%">
        <tr>
            <td style="width: 5%">
            </td>
            <td align="left">
                <asp:Label ID="lblFieldName" runat="server" Font-Size="12px" Font-Names="Verdana"
                    Visible="true"></asp:Label>
            </td>
            <td align="right">
                <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana"
                    Visible="true"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblSelect" Text="Please Select the Field Force Name" runat="server"
                        ForeColor="Red" Font-Size="Large"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <asp:Panel ID="pnlDist" runat="server" Visible="false">
            <table width="70%">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblHQSta" runat="server" Text="Head Quarter" Font-Bold="true" Font-Size="Small"
                            ForeColor="Black"></asp:Label>
                        <span style="color: Blue; font-weight: bold">(No Fare Only Allowance)</span>
                    </td>
                    <%-- <td><asp:Label ID="lblFare" runat="server" Text="No Fare Only Allowance" ForeColor="Red"></asp:Label></td>--%>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdHQ" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                         OnRowDataBound="grdHQ_RowDataBound"
                            GridLines="None" BorderStyle="Solid" CssClass="mGridImg" EmptyDataText="No Head Quarter Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQDoctor" runat="server" Text='<%# Eval("Territory_Name")%>'></asp:Label>
                                      <asp:Label ID="Label4" runat="server" Text='<%# Eval("ListedDR_Count")%>'></asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label2" runat="server" Text="EX Station" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdEX" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" BorderStyle="Solid" CssClass="mGridImg" EmptyDataText="No EX Station Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromEX" runat="server" Text='<%#Eval("Sf_HQ")%>'>
                                        </asp:Label>
                                        <asp:HiddenField ID="hdnFrmTerrCode" runat="server" Value='<%#Eval("sf_code")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToEX" runat="server" Text='<%#Eval("Territory_Name")%>'>
                                        </asp:Label>
                                        <asp:HiddenField ID="hdnToTerrCode" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                        <asp:HiddenField ID="hidcat" runat="server" Value='<%#Eval("Territory_Cat")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance(One Way in Kms)">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtKms" runat="server" Text='<%#Eval("Distance") %>' SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        
                        <asp:LinkButton ID="linkos" Text="Out Station" runat="server" ForeColor="Black" Font-Underline="false" Font-Bold="true"
                                 ></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdOS" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" BorderStyle="Solid" CssClass="mGridImg" EmptyDataText="No Out Station Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromOs" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnOSFrmTerrCode" runat="server" Value='<%#Eval("sf_code")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToOs" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnOSToTerrCode" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                        <asp:HiddenField ID="oSHidCat" runat="server" Value='<%#Eval("Territory_Cat")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance(One Way in Kms)" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOsKms" runat="server" SkinID="MandTxtBox" Text='<%#Eval("Distance") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label3" runat="server" Text="OS-EX" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdOSEX" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            GridLines="None" BorderStyle="Solid" CssClass="mGridImg" EmptyDataText="No OS-EX Station Found"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromOSEX" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnOSEXFrmTerrCode" runat="server" Value='<%#Eval("FCode")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToOSEX" runat="server" Text='<%#Eval("TName")%>'></asp:Label>
                                         <asp:HiddenField ID="hdnOSEXToTerrCode" runat="server" Value='<%#Eval("TCode")%>' />
                                        <asp:HiddenField ID="oSEXHidCat" runat="server" Value='<%#Eval("TCat")%>' />
                                   </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance(One Way in Kms)" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOsExKms" runat="server" Text='<%#Eval("Distance")%>' SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </center>
    <div>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                PopupControlID="pnlpopup" TargetControlID="linkos" BackgroundCssClass="modalBackgroundNew">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="#e0f3ff" Height="480px" Width="470px"
                class="ontop" Style="left: 450px; top: 200px; position: absolute; display: none">
                <table width="50%" style="border: Solid 3px #4682B4; width: 100%; height: 100%" cellpadding="0"
                    cellspacing="0">
                    <tr style="background-color: #4682B4;">
                        <td style="height: 10%; color: White; font-weight: bold; font-size: larger;" align="center">
                            <asp:Label ID="lblHead" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 50%; color: black; font-weight: bold; font-size: 20pt">
                            <asp:Label ID="lblos" Font-Bold="true" Font-Size="20px" runat="server" Text="OS Permutations"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="width: 450px; height: 400px; padding: 2px; overflow: auto; border: 1px solid Black;">
                                                    <asp:GridView ID="grdosPer" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center"
                                                        GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <HeaderStyle Font-Bold="False" />
                                                        <PagerStyle CssClass="gridview1"></PagerStyle>
                                                        <SelectedRowStyle BackColor="BurlyWood" />
                                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                        <Columns>
                                                       
                                                           <asp:TemplateField HeaderText="From" HeaderStyle-Width="300px" HeaderStyle-ForeColor="white">
                                                                <ControlStyle Width="90%"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfrom" runat="server" Text='<%# Bind("territory_name_From") %>'></asp:Label>
                                                                    <asp:HiddenField ID="Terrfrm" runat="server" Value='<%# Eval("territory_Code_From") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="To">
                                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTo" runat="server" Text='<%# Bind("territory_name_To") %>'></asp:Label>
                                                                    <asp:HiddenField ID="TerrTo" runat="server" Value='<%# Eval("territory_Code_To") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Distance(One Way in Kms)" ControlStyle-Width="40" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOsPermKms" runat="server" Text='<%#Eval("Distance")%>' SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Return to HQ" ControlStyle-Width="40" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtrtnKms" runat="server" Text='<%#Eval("RtnHQ") %>' SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Width="90px" Height="25px"
                                CssClass="savebutton" Text="Save" OnClick="btnosPerm_Click"  />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" Height="25px"
                                CssClass="savebutton"  />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    <center>
        <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Width="60px" Height="25px"
            Text="Save SFC" Visible="false" OnClick="btnSave_Click" />
    </center>
    </form>
</body>
</html>
