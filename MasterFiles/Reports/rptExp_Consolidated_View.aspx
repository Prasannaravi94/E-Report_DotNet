<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExp_Consolidated_View.aspx.cs" Inherits="MasterFiles_Reports_rptExp_Consolidated_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(function () {
            $('#btnExcel').click(function () {
                //var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                //location.href = url
                //return false
                var myBlob = new Blob([pnlContents.innerHTML], { type: 'application/vnd.ms-excel' });
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 var a = document.createElement("a");
                 document.body.appendChild(a);
                 a.href = url;
                 a.download = "export.xls";
                 a.click();
                 setTimeout(function () { window.URL.revokeObjectURL(url); }, 0);
                 return false;
            })
        })
    </script>

    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
     <style type="text/css">
        #grdExpense > tbody > tr:nth-child(1) {
            position: sticky;
            left: 0px;
            top:0px;
            z-index: 1;
            background-color: #F1F5F8;
        }
        #grdExpense > tbody > tr:nth-child(n) > th:nth-child(1) {
            position: sticky;
            left: 0px;
            top:0px;
            z-index: 2;
 
        }
        #grdExpense > tbody > tr:nth-child(n) > th:nth-child(2) {
            position: sticky;
            left: 36px;
            top:0px;
            z-index: 3;
            min-width:170px;
        }
        #grdExpense > tbody > tr:nth-child(n) > th:nth-child(3) {
            position: sticky;
            left: 215px;
            top:0px;
            z-index: 3;
        }
        #grdExpense > tbody > tr:nth-child(n+1) > td:nth-child(1)
         {
            position: sticky;
            left: 0px;
            top:0px;
            
        }
        #grdExpense > tbody > tr:nth-child(n+1) > td:nth-child(2)
         {
            position: sticky;
            left: 36px;
            top:0px;
            background-color:white;
        }
        #grdExpense > tbody > tr:nth-child(n+1) > td:nth-child(3)
         {
            position: sticky;
            left: 215px;
            top:0px;
            background-color:white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="80%"></td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="padding-right: 30px">
                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" Visible="false" OnClientClick="return PrintPanel();">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                    <td style="padding-right: 50px">
                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContents" runat="server">
                <div class=" home-section-main-body position-relative clearfix">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <table width="100%" align="center">
                                <tbody>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Label ID="lblHead" runat="server" CssClass="reportheader"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="40%">
                                            <asp:Label ID="lblFieldForceName" Font-Size="14px" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblHQ" ForeColor="#696d6e" Font-Size="14px" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblDesig" ForeColor="#696d6e" Font-Size="14px" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive" style="height: 700px;">
                                        <asp:GridView ID="grdExpense" runat="server" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                            GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                            Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Font-Size="Smaller" runat="server" Text='<%# (grdExpense.PageIndex * grdExpense.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="SF_Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Code" Font-Size="Smaller" runat="server" Text='<%# Bind("sf_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fieldforce Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Name" Font-Size="Smaller" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotal" Style="color: Red; font-weight: bold" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation_Name" Font-Size="Smaller" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfName" Font-Size="Smaller" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee_Code" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployee_Code" Font-Size="Smaller" runat="server" Text='<%# Bind("usrdfd_Username") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Mgr.App.Dt" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle width="80px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproval" runat="server" Text='<%# Bind("Approval_date2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="HQ Days" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHQdys" runat="server" Font-Size='8' Text='<%# Bind("HQ_Days") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EX Days" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEXdys" runat="server" Font-Size='8' Text='<%# Bind("EX_Days") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OS Days" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOSdys" runat="server" Font-Size='8' Text='<%# Bind("OS_Days") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KMS" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKMs" runat="server" Font-Size='8' Text='<%# Bind("Expense_distance") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfAll" Font-Size="Smaller" runat="server" Text='<%# Bind("allowance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlblAll" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfFare" Font-Size="Smaller" runat="server" Text='<%# Bind("fare") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlblFare" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" Font-Size="Smaller" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column1")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" Font-Size="Smaller" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column2")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" Font-Size="Smaller" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column3")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" Font-Size="Smaller" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column4")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label17" Font-Size="Smaller" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column5")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label25" Font-Size="Smaller" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column6")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Miscellaneous" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmisamt" Font-Size="Smaller" runat="server" Text='<%# Bind("mis_Amt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlblmisamt" Font-Size="Smaller" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Additional Expenses" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladdamt" Font-Size="Smaller" runat="server" Text='<%# Bind("rw_amt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlbladdamt" Font-Size="Smaller" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Applied <br/>Amount(By MR)" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="90%"></ControlStyle>
                                                    <ItemStyle BackColor="LightBlue" HorizontalAlign="center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAppliedAmnt" Font-Size="Smaller" runat="server" Text='<%# Bind("tot") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlblAppliedAmt" Font-Size="Smaller" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Addition & Detection" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle BackColor="#eec9fb" HorizontalAlign="center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddDetAmnt" Font-Size="Smaller" runat="server" Text='<%# Bind("appAmnt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlblAddDetAmnt" Font-Size="Smaller" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Confirmed <br/>Amount(By Admin)" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle BackColor="LightGreen" HorizontalAlign="center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConfirmAmnt" Font-Size="Smaller" runat="server" Text='<%# Bind("confirmAmnt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlblConfirmAmnt" Font-Size="Smaller" Style="color: Red; font-weight: bold" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
