<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDocList_Consolidate.aspx.cs"
    Inherits="MasterFiles_Reports_rptDocList_Consolidate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor List Consolidate</title>
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

    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                           <%-- <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPrint_Click" />
                            </td>--%>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <%--<td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPDF_Click" />
                            </td>--%>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlContents" runat="server">
        <asp:GridView ID="grdSalesForce" runat="server" Width="95%" HorizontalAlign="Center"
            EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowDataBound="grdSalesForce_RowDataBound"
            BorderStyle="Solid" BorderWidth="1" GridLines="Both" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
            <HeaderStyle Font-Bold="False" />
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                <asp:TemplateField HeaderText="User Name" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Drs Count" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server" 
                    NavigateUrl='<%# String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?sf_code={0}&sf_name={1}&type={2}&status={3}", Eval("Sf_code"), Eval("Sf_Name"),2,0) %>' 
                    Text='<%# Bind("Lst_drCount") %>' />
                        <%--<asp:Label ID="lblDrsCnt" runat="server" Font-Size="12px" Font-Bold="true" Width="10%"
                            Font-Names="sans-serif" ForeColor="Red" Text='<%# Bind("Lst_drCount") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="160px" />
                    <ItemTemplate>
                        <asp:Label ID="lblPlace" runat="server" Font-Size="10px" Font-Names="Verdana" ForeColor="#000000"
                            Text='<%# Bind("sf_hq") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State Name" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="180px" />
                    <ItemTemplate>
                        <asp:Label ID="lblState" runat="server" Font-Size="10px" Font-Names="Verdana" ForeColor="#000000"
                            Text='<%# Bind("StateName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="lblFieldForce" runat="server" Font-Size="10px" Font-Names="Verdana"
                            ForeColor="#000000" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="120px" />
                    <ItemTemplate>
                        <asp:Label ID="lblDesignation" runat="server" Font-Size="10px" Font-Names="Verdana"
                            ForeColor="#000000" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reporting1" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="lblTPReporting" runat="server" Font-Size="10px" Font-Names="Verdana"
                            ForeColor="#000000" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reporting2" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="lblsf_Reporting2" runat="server" Font-Size="10px" Font-Names="Verdana"
                            ForeColor="#000000" Text='<%# Bind("sf_Reporting2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Division_Code" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="12px"
                    ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="250px" />
                    <ItemTemplate>
                        <asp:Label ID="lblDivCode" runat="server" Font-Size="10px" Font-Names="Verdana"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                VerticalAlign="Middle" />
        </asp:GridView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
