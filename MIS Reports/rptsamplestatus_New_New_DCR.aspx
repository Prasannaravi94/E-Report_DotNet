<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptsamplestatus_New_New_DCR.aspx.cs" Inherits="MIS_Reports_rptsamplestatus_New_New_DCR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../css/Report.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
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

     <style type="text/css">
       
        .blink_me {
    -webkit-animation-name: blinker;
    -webkit-animation-duration: 1s;
    -webkit-animation-timing-function: linear;
    -webkit-animation-iteration-count: infinite;
    
    -moz-animation-name: blinker;
    -moz-animation-duration: 1s;
    -moz-animation-timing-function: linear;
    -moz-animation-iteration-count: infinite;
    
    animation-name: blinker;
    animation-duration: 1s;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
}

@-moz-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@-webkit-keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}

@keyframes blinker {  
    0% { opacity: 1.0; }
    50% { opacity: 0.0; }
    100% { opacity: 1.0; }
}
 .blink {
  animation: blink-animation 1s steps(5, start) infinite;
  -webkit-animation: blink-animation 1s steps(5, start) infinite;
}
@keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
@-webkit-keyframes blink-animation {
  to {
    visibility: hidden;
  }
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
                <td width="20%">
                </td>
                <td>
                </td>
                <td width="80%" align="center">
                    <asp:Label ID="lblProd" runat="server" Text="Sample Despatch Status" ForeColor="#794044" Font-Names="Verdana" Font-Size="14px" Font-Underline="true" Font-Bold="true"></asp:Label>
                </td>
                <td align="right">
                   
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div>
        <br />
        <br />
        <center>
            <%--<asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand" ></asp:Label>--%>
            <br />
            <br />
        </center>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <table width="100%" align="center">
        
                    <tr>
                        <td  width="15%"></td>
                        <td align="left">
                            <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:"></asp:Label>
                            <asp:Label ID="lblname" runat="server" ForeColor="Red" Font-Bold="true" Font-Names="Verdana"></asp:Label>
                        </td>

                       
                        
                         
                        <td width="15%"></td>
                    </tr>

                    
                    </table>
                    <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdDespatch" runat="server" Width="50%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdDr_RowDataBoud"
                               AllowSorting="True" RowStyle-Wrap="false">
                                <HeaderStyle Font-Bold="False" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Width="30px" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product_Code_SlNo" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprd_code" runat="server" Width="10px" Text='<%#Eval("Product_Code_SlNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprod_name" runat="server" Width="220px" Text='<%#Eval("Product_Detail_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                   <%-- <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblissued" runat="server" Width="150px" Text='<%# Bind("issued") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                     <asp:TemplateField HeaderText="Closing Balance" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblclosing" runat="server" Width="70px" Text='<%# Bind("closing") %>'></asp:Label>
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
    </div>
    </form>
</body>
</html>

