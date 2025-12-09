<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Competitor_View.aspx.cs" Inherits="MasterFiles_Competitor_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../assets/js/jQuery.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/jquery.nice-select.min.js"></script>
    <script src="../assets/js/main.js"></script>
      <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />



   <%--<link type="text/css" rel="stylesheet" href="../css/Report.css" />--%>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
   <script type="text/javascript">
       $(function () {
           $('#lnkExcel').click(function () {
               var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
               location.href = url
               return false
           })
       })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <br /><br />
    <div class="container home-section-main-body position-relative clearfix">
        <div class="row justify-content-center ">
            <div class="col-lg-11">
                <h2 class="text-center">Competitor - Product tagged View</h2>
                <asp:Panel ID="pnlbutton" runat="server" CssClass="panelmarright">
                    <%--<asp:Label ID="lblProd" runat="server" Text="Competitor - Product tagged View" Font-Size="16px" ForeColor="Maroon" Font-Bold="true"></asp:Label>--%>
                    <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/assets/images/Printer.png" ToolTip="Print" Width="30px" Style="border-width: 0px;" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkExcel" ToolTip="Excel" runat="server">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/assets/images/Close.png" ToolTip="Close" Width="30px" Style="border-width: 0px;" />
                    </asp:LinkButton>
                </asp:Panel>
                <br />
                <div class="display-table clearfix">
                   <div class="table-responsive" scrollbar-width: thin;">
                        <center>
                            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                                <table width="100%" align="center">
                                    <tbody>
                                    <%--    <tr>
                                    <td align="center" >
                                    <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:" ></asp:Label>
                                    <asp:Label ID="lblname" runat="server" SkinID="lblMand"></asp:Label>
                                    </td>
              
                                       </tr>--%>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="grdComp" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                                    AllowSorting="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sl_No" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSl_No" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Competitor Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComp_Name" runat="server" Text='<%#Eval("Comp_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Competitor Product - Tagged" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltagged" runat="server" Text='<%#Eval("Comp_Prd_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                       <%-- <table width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                                font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="70%" >
                                        </asp:Table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>  --%>
                   </center>
                   </div>
                 </div>
            </div>
        </div>
    </div>
        <br /><br />
    </form>
</body>
</html>

