<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDR_Business_Valuewise_Detail.aspx.cs" Inherits="MIS_Reports_rptDR_Business_Valuewise_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- <link type="text/css" rel="stylesheet" href="../css/Report.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
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
                    <td width="20%"></td>
                    <td></td>
                    <td width="80%" align="center">
                        <asp:Label ID="lblProd" runat="server" Text="Input Despatch Status" CssClass="reportheader" Font-Size="14" Font-Underline="True" Font-Bold="True" ForeColor="#414d55"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" Visible="false" OnClick="btnPrint_Click">
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
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" >
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
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-center">
                    <div class="col-lg-12">
                        <table width="100%" align="center">
                            <tr>
                                <td align="center">
                                    <%--       <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:"></asp:Label>
                                    <asp:Label ID="lblname" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true" Font-Names="Verdana"></asp:Label>--%>
                                </td>

                            </tr>
                        </table>
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:GridView ID="grdDespatch" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found" OnRowDataBound="grdDespatch_RowDataBound"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%">
                                        <HeaderStyle Font-Bold="False" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Gift_Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGift_Code" runat="server" Text='<%#Eval("Gift_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Listed Dr Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLstDr_Name" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpeciality_Name" runat="server" Text='<%# Bind("Speciality_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory_Name" runat="server" Width="150px" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub Area" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory_Name" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_Name" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Product_Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValue" runat="server" Text='<%# Bind("TotalValue") %>'></asp:Label>
                                                </ItemTemplate>
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
