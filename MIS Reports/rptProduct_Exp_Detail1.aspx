<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProduct_Exp_Detail1.aspx.cs" Inherits="MIS_Reports_rptProduct_Exp_Detail1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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

    <%--<link type="text/css" rel="stylesheet" href="../css/Report.css" />--%>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', '');
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

    <style type="text/css">
        .display-reportMaintable .table th {
            padding: 15px 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:Panel ID="pnlbutton" runat="server">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-lg-9">
                            <center>
                                <asp:Label ID="lblProd" runat="server" Text="Product Exposure" CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblfieldname" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                <asp:Label ID="lblname" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                <br />
                            </center>
                        </div>
                        <div class="col-lg-3">
                            <table width="100%">
                                <tr>
                                    <td></td>
                                    <td>
                                        <%--<asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand"></asp:Label>--%>
                                    </td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 40px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                </div>
            </div>
        </asp:Panel>
        <br />

        <div class="container clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div>

                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                <asp:Panel ID="pnlContents" runat="server" Width="100%">

                                    <asp:GridView ID="grdDr" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdDr_RowDataBound"
                                        AllowSorting="True" style="background-color:white">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ListedDrCode" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrCode" runat="server" Width="120px" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblforce" runat="server" Width="120px" Text='<%#Eval("Fieldforce_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprod_name" runat="server" Width="120px" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrName" runat="server" Width="150px" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblquali" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Brand" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_Brd_Name" runat="server" Text='<%# Bind("Product_Brd_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date of Visit" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="60%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitDate" runat="server" Text='<%# Bind("activity_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Count of Visit" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="60%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvisit" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--   <asp:TemplateField HeaderText="Visit Count" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ControlStyle Width="40%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblVisitCount" runat="server"></asp:Label>
                                </ItemTemplate>
                               </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Date of Visit" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="60%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblVisitDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Worked With" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="80%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblworked_with" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> --%>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <br />
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
