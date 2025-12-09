<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptBrdwise_Star_Rating.aspx.cs" Inherits="MasterFiles_Reports_rptExp_Consolidated_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

     <script type="text/javascript" language="javascript">
         $(function () {
             $('#btnExcel').click(function () {
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 location.href = url
                 return false
             })
         })
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btnDrSn").mouseover(function () {
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrSn").mouseout(function () {
                $(this).css("color", "blue");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "13px");
            });
        });
    </script>  
    <script type = "text/javascript">
        var popUpObj;
        
        function showModalPopUp(sfcode, divcode, FMonth, FYear, mode,selValues, Sf_Name,rating, SfMGR) {
            debugger
            popUpObj = window.open("rptBrdwise_Star_Rating_Zoom.aspx?sfcode=" + sfcode + "&divcode=" + divcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&mode=" + mode + "&selValues=" + selValues + "&Sf_Name=" + Sf_Name + "&SfMGR=" + SfMGR + "&rating=" + rating,
            "_blank",
        "ModalPopUp_Level1," +
         "0," +
        "toolbar=no," +
        "scrollbars=1," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "status=no," +
        "addressbar=no," +
        "resizable=yes," +
        "width=650," +
        "height=450," +
        "left = 0," +
        "top=0"
        );
            popUpObj.focus();
            //LoadModalDiv();
        }
    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
        function popzoom(sfcode) {
            var mnth = document.getElementById('<%= hidmnth.ClientID%>').value;
            var yr = document.getElementById('<%= hidyr.ClientID%>').value;
            strOpen = "Miscellanious_Zoom.aspx?sf_code=" + sfcode + "&month=" + mnth + "&year=" + yr + "",
             window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=300,height=200,left = 0,top = 0');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
   <asp:Panel ID="pnlContents" runat="server">
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="lblHead" runat="server" Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <asp:Label ID="lblFieldForceName" SkinID="lblMand" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblHQ" Font-Bold="true" SkinID="lblMand" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDesig" Font-Bold="true" SkinID="lblMand" runat="server"></asp:Label>
                        </td>
                         <td>
                         <asp:HiddenField ID="hidmnth" runat="server" />
                        <asp:HiddenField ID="hidyr" runat="server" />
                       <asp:HiddenField ID="hiddiv" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:GridView ID="grdExpense" runat="server" AlternatingRowStyle-CssClass="alt" 
                                AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found" 
                                GridLines="Both" HorizontalAlign="Center"  BorderWidth="1"
                                OnRowCreated="grdExpense_RowCreated" onrowdatabound="grdExpense_RowDataBound" 
                                ShowHeader="False" Width="98%" Font-Names="calibri" Font-Size="Small">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt" />
                                <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                                <Columns>
                                </Columns>
                                <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black" 
                                    Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                            <%--<asp:GridView ID="grdExpense" runat="server" Width="100%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="true" OnRowDataBound="grdExpense_RowDataBound" >
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <FooterStyle Width="60px"  BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></FooterStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdExpense.PageIndex * grdExpense.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Employee_Code" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployee_Code" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="SF_Code">
                                        <ItemStyle Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code"  runat="server" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                        </ItemTemplate>   
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fieldforce Name">
                                        <ItemStyle Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                      
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation_Name" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bank Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccName" runat="server" Text='<%# Bind("SF_ContactAdd_One") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank A/C No" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblaccno" runat="server" Text='<%# Bind("SF_ContactAdd_Two") %>'></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                        <asp:Label ID="lblTotal" style="color:Red;font-weight:bold" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="HQ Visit" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                       <asp:Label ID="ftlblHQ" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX-HQ Visit" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEX" runat="server" Text='<%# Bind("EX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlblEX" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="OS Visit" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOS" runat="server" Text='<%# Bind("OS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlblOS" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Leave" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeave" runat="server" Text='<%# Bind("Leave") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlblLeave" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Holiday" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHoliday" runat="server" Text='<%# Bind("Holiday") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlblHoli" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transit" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransit" runat="server" Text='<%# Bind("transit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlbltransit" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="60px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></ItemStyle>
                                            
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfAll" runat="server" Text='<%# Bind("allowance") %>'></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                       <asp:Label ID="ftlblAll" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="60px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfFare"  runat="server" Text='<%# Bind("fare") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="ftlblFare" style="color:Red;font-weight:bold"  runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column1")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlbl3" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column2")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlbl4" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label15" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column3")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlbl5" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label16" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column4")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlbl6" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label17" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column5")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                       <asp:Label ID="ftlbl7" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Miscellaneous" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmisamt"  runat="server" Text='<%# Bind("mis_Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="ftlblmisamt" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="Additional Expenses" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdamt"  runat="server" Text='<%# Bind("rw_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="ftlbladdamt" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applied <br/>Amount(By MR)" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BackColor="LightBlue" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppliedAmnt" runat="server" Text='<%# Bind("tot") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="ftlblAppliedAmt" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Addition & Detection" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BackColor="Yellow" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblAddDetAmnt" runat="server" Text='<%# Bind("appAmnt") %>'></asp:Label>--%>
                                            <%--<asp:LinkButton ID="lblAddDetAmnt" runat="server" CausesValidation="False" Text='<%# Bind("appAmnt") %>' Font-Bold ="true" 
                                            OnClientClick='<%# "return popzoom(\"" + Eval("SF_Code") + "\");" %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="ftlblAddDetAmnt" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed <br/>Amount(By Admin)" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BackColor="LightGreen" BorderWidth="1px" BorderColor="Black"
                                            HorizontalAlign="center"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfirmAmnt" runat="server" Text='<%# Bind("confirmAmnt") %>'></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                          <asp:Label ID="ftlblConfirmAmnt" style="color:Red;font-weight:bold" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
	<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
</body>
</html>
