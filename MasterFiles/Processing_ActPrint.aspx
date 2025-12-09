<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Processing_ActPrint.aspx.cs" Inherits="MasterFiles_Processing_ActPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        h3, h5 {
            margin: 0px;
        }

        body {
            font-family: Arial !important;
        }

        .signPanel {
            border: 3px solid #000000;
            width: 200px;
            margin-left: 0px;
            text-align:center;
        }
        #GrdSubActiviy > tbody > tr:nth-child(1) {
            border: 1px solid #000000;
        }
        #pnlContents > div > center > table  {
            border-collapse: collapse;
           
        }
    </style>
      <script type="text/javascript">
        function PrintPanel() {
          var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><style type="text/css"> h3, h5 { margin: 0px; }body {font-family: Arial !important;}.signPanel {border: 3px solid #000000;width: 200px;margin-left: 0px;text-align:center;}#GrdSubActiviy > tbody > tr:nth-child(1) {border: 1px solid #000000;}#pnlContents > div > center > table  {border-collapse: collapse;}</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
               <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="80%"></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                   
                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                   
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server">
        <div style="width:90%;margin: auto;">
        <center>
        <div>
            <img src="../Images/tablet.png" />
           <%--<h2><u>TABLETS ( INDIA ) LIMITED</u></h2>
            <h5>JHAVER CENTER IV Floor R.A. Building</h5>
            <h5>72, Marshalls Road, Chennai- 600 008</h5>--%>
        </div>
            <div>
        <%--<h3><b><u>Payment Summary</u>  </b></h3>--%>
        <table width="100%">
            <tr><td align="right"><h3><b><u>Payment Summary</u>  </b></h3></td><td align="right"><b><asp:label ID="lblEmpID" runat="server"></asp:label></b></td></tr>
        </table>
<%--       <div style="text-align:right"> <b><asp:label ID="lblEmpID" runat="server"></asp:label></b></div></div>--%>
        <asp:Repeater ID="RptActiviy" runat="server" OnItemDataBound="RptActiviy_OnItemCommand">
            <ItemTemplate>
                <table runat="server" border="1" cell-collapse="0" width="100%" style="border-collapse: collapse;">
                    <tr>
                        <td width="40%"><b>Date Of Activity Approval</b></td>
                        <td><asp:label ID="lblDateOfApproval" runat="server" Text='<%# Eval("Date_Activity_Approval") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Name - Design - HQ</b></td>
                        <td><asp:label ID="lblNameDesignHQ" runat="server" Text='<%# Eval("sf_name") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Division Name</b></td>
                        <td><asp:label ID="lblDivisionName" runat="server" Text='<%# Eval("division_name") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Date of Activity</b></td>
                        <td><asp:label ID="lblDateOfActivity" runat="server" Text='<%# Eval("Date_Activity") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Month of Activity</b></td>
                        <td><asp:label ID="lblMonthOfActivity" runat="server" Text='<%# Eval("Month_Activity") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Year of Activity</b></td>
                        <td><asp:label ID="lblYearOfActivity" runat="server" Text='<%# Eval("Year_Activity") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Description</b></td>
                        <td><asp:label ID="lblDescription" runat="server" Text='<%# Eval("Activity_Description") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Advance (If Any)</b></td>
                        <td><asp:label ID="lblAdvanced" runat="server" Text='<%# Eval("Activity_Advance") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Approved Bill</b></td>
                        <td><asp:label ID="lblApprovedBill" runat="server" Text='<%# Eval("Activity_Approved_Bill_Amount") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Payment due to Company</b></td>
                        <td><asp:label ID="lblPaymentDueToCom" runat="server" Text='<%# Eval("paymentDueToCom") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td><b>Payment due From Company</b></td>
                        <td><asp:label ID="lblPaymentDueFromCom" runat="server" Text='<%# Eval("paymentDueFromCom") %>'></asp:label></td>
                    </tr>
                    <tr>
                        <td colspan="2"><center><b>Remarks</b></center></td>
                    </tr>
                    
                </table>
            </ItemTemplate>
        </asp:Repeater>
             <asp:gridview ID="GrdSubActiviy" runat="server" AutoGenerateColumns="false" width="100%" GridLines="Vertical" OnRowDataBound="GrdSubActiviy_RowDataBound">
                 <Columns>
                             <asp:TemplateField HeaderText="Addition/ Deletion Rs." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblActivity_Addition_Deletion" runat="server" Text='<%#Eval("Activity_Addition_Deletion")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date On Which Incurred" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                               <asp:Label ID="lblActivity_Dateon_Which_Incurred" runat="server" Text='<%#Eval("Activity_Dateon_Which_Incurred")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivity_Reason" runat="server" Text='<%#Eval("Activity_Reason")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                             
                                
                            </Columns>
                    </asp:gridview>
            <br />
                <br />
       
            </center>
            <footer>
        <div>    
            <div class="signPanel">
                <span>VERIFIED</span>
                <br />
                <br />
                <br />
                <br />
                <br />
                <span>INTERNAL AUDIT DEPT</span>
            </div>
        </div>
                </footer>
            </div>
            </asp:Panel>
    </form>
</body>
</html>
