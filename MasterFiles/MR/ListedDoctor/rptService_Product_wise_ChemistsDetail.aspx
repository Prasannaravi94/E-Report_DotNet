<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptService_Product_wise_ChemistsDetail.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_rptService_Product_wise_ChemistsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <script type="text/javascript">
            function PrintPanel() 
            {
                var panel = document.getElementById("<%=pnlContents.ClientID %>");
                var printWindow = window.open('', '', 'height=400,width=800');
                printWindow.document.write('<html><head>');
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
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#GrdFixation tr:last-child').each(function () {
                //tr:last-child { color:red; }
                $(this).children('td').css('color', 'red');
                $(this).children('td').css('border-color', 'black');
                $(this).children('td').css('font-size', '16px');
            });
        });
    </script>
    <style type="text/css">
        .cl
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <center>
            <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="return PrintPanel();" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                         />
                                </td>
                              
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
            <center>
                <asp:Panel ID="pnlContents" runat="server">
                    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Doctor List" Font-Underline="True"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                        <br />
                      <br />
                        <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                             <br />
                           
                        <asp:Label ID="lblstock" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="9pt"></asp:Label>
                    </div>
                    <br />                   
                    <center>
                        <asp:Panel ID="Panel1" runat="server">
                            <table width="95%">
                                <caption>
                                   <asp:GridView ID="gvProduct" runat="server" Width="100%" HorizontalAlign="Center"
                                BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                OnRowDataBound="gvMyDayPlan_OnDataBound"
                                CssClass="mGrid">
                               <HeaderStyle Font-Names="calibri" Font-Size="9" />
                               <RowStyle Font-Names="calibri" Font-Size="9" />    
                                <Columns>
                                <asp:BoundField DataField="Sl_No" HeaderText="#" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="50" />
                                <asp:BoundField DataField="Sf_Code" HeaderText="FieldForce Name" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="70" />
                                <asp:BoundField DataField="Chemists_Name" HeaderText="Chemists Name" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="70" />
                                <asp:BoundField DataField="Chemists_Contact" HeaderText="Contact" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="70" />
                                <asp:BoundField DataField="Chemists_Address1" HeaderText="Address" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="150" />                              
                                <asp:BoundField DataField="Chemists_Phone" HeaderText="Phone" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="150" />
                                <asp:BoundField DataField="territory_Name" HeaderText="HQ" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Product_Detail_Name" HeaderText="Product Name" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Cur_Total" HeaderText="Amount Spent" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" ItemStyle-Width="150" />
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>

                                </caption>
                            </table>
                        </asp:Panel>
                    </center>
                </asp:Panel>
            </center>
        </center>
        </div>
    </div>
    </form>
</body>
</html>
