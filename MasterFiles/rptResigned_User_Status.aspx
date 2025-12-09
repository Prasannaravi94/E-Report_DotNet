<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptResigned_User_Status.aspx.cs" Inherits="MasterFiles_rptResigned_User_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resigned User Status</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link type="text/css" rel="Stylesheet" href="../css/Report.css"/>--%>
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

    <script language="javascript" type="text/javascript">
        function popUp(SF_Code, DCR_Start_Date, DCR_End_Date, SF_Name, TP_DCR,Employee_Code) {
            strOpen = "rptResigned_User_Status1.aspx?sfcode=" + SF_Code + "&DCR_Start_Date=" + DCR_Start_Date + "&DCR_End_Date=" + DCR_End_Date + "&SF_Name=" + SF_Name + "&Employee_Code=" + Employee_Code + "&TP_DCR=TP",
            window.open(strOpen, 'popWindow', '');
        }
    </script>
    <script language="javascript" type="text/javascript">
        function popdcr(SF_Code, DCR_Start_Date, DCR_End_Date, SF_Name,TP_DCR,Employee_Code) {
            strOpen = "rptResigned_User_Status1.aspx?sfcode=" + SF_Code + "&DCR_Start_Date=" + DCR_Start_Date + "&DCR_End_Date=" + DCR_End_Date + "&SF_Name=" + SF_Name + "&Employee_Code=" + Employee_Code + "&TP_DCR=DCR",
             window.open(strOpen, 'popWindow', '');
        }
    </script>

    <script language="javascript" type="text/javascript">
        function popleave(SF_Code, DCR_Start_Date, DCR_End_Date, SF_Name,TP_DCR,Employee_Code) {
            strOpen = "rptResigned_User_Status1.aspx?sfcode=" + SF_Code + "&DCR_Start_Date=" + DCR_Start_Date + "&DCR_End_Date=" + DCR_End_Date + "&SF_Name=" + SF_Name + "&Employee_Code=" + Employee_Code + "&TP_DCR=Leave",
           window.open(strOpen, 'popWindow', '');
        }
    </script>
       <script language="javascript" type="text/javascript">
           function popInputStatus(SF_Code, DCR_Start_Date, DCR_End_Date, SF_Name,TP_DCR,Employee_Code) {
            strOpen = "rptResigned_User_Status1.aspx?sfcode=" + SF_Code + "&DCR_Start_Date=" + DCR_Start_Date + "&DCR_End_Date=" + DCR_End_Date + "&SF_Name=" + SF_Name + "&Employee_Code=" + Employee_Code + "&TP_DCR=InputStatus",
           window.open(strOpen, 'popWindow', '');
        }
    </script>
     <script language="javascript" type="text/javascript">
         function popExpenseStatus(SF_Code, DCR_Start_Date, DCR_End_Date, SF_Name, TP_DCR, Employee_Code) {
            strOpen = "rptResigned_User_Status1.aspx?sfcode=" + SF_Code + "&DCR_Start_Date=" + DCR_Start_Date + "&DCR_End_Date=" + DCR_End_Date + "&SF_Name=" + SF_Name + "&Employee_Code=" + Employee_Code +"&TP_DCR=ExpenseStatus",
           window.open(strOpen, 'popWindow', '');
        }
    </script>
    <script language="javascript" type="text/javascript">
        function popPaylsipStatus(SF_Code, DCR_Start_Date, DCR_End_Date, SF_Name, TP_DCR,Employee_Code) {
            strOpen = "rptResigned_User_Status1.aspx?sfcode=" + SF_Code + "&DCR_Start_Date=" + DCR_Start_Date + "&DCR_End_Date=" + DCR_End_Date + "&SF_Name=" + SF_Name + "&Employee_Code="+Employee_Code +"&TP_DCR=Paylsip",
           window.open(strOpen, 'popWindow', '');
        }
    </script>

    <style type="text/css">
        .display-reportMaintable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #363670 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #363670 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        - See more at: http://www.dotnetfox.com/articles/gridview-custom-css-style-example-in-Asp-Net-1088.aspx#sthash.vNmATWeI.dpuf .mGrid {
            margin-bottom: 0px;
        }
    </style>
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Panel ID="pnlbutton" runat="server">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                            </div>
                            <div class="col-lg-3">
                                <table width="100%">
                                    <tr>
                                        <td></td>
                                        <%--  <td width="80%" align="center">
                            <asp:Label ID="lblHead" Text="Resigned User Status" Font-Underline="True" Font-Size="14px" Font-Bold="True" ForeColor="#794044"
                                runat="server"></asp:Label>
                        </td>--%>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 40px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="25px" Style="border-width: 0px;" />
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
            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <div>
                                <div align="center">
                                    <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
                                    <asp:Label ID="lblHead" Text="Resigned User Status" CssClass="reportheader"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="label" Font-Size="16px" Visible="false"></asp:Label>
                                        <asp:Label ID="lblRegionName" runat="server" CssClass="label" Font-Size="16px" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblIDMonth" Text="Month :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblIDYear" Text="Year :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblprd_name" Text="Product Name :" runat="server" CssClass="label" Font-Size="16px" Visible="false"></asp:Label>
                                        <asp:Label ID="lblname" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                        <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" GridLines="Both" Style="background-color: white" BorderColor="WhiteSmoke" BorderWidth="1"
                                            AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found" AllowSorting="True" OnSorting="grdSalesForce_Sorting"
                                            CssClass="table" AlternatingRowStyle-CssClass="alt" HeaderStyle-CssClass="stickyFirstRow">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ControlStyle Width="90%"></ControlStyle>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sf_Code" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Code" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemp_code" runat="server" Text='<%# Bind("Employee_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField SortExpression="SF_Name" HeaderText="FieldForce Name" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("SF_Name") %>'></asp:Label></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField SortExpression="sf_Designation_Short_Name" HeaderText="Design" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DCR Start Date" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Dcr_Start" runat="server" Text='<%# Bind("DCR_Start_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DCR End Date" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Dcr_End" runat="server" Text='<%# Bind("DCR_End_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField HeaderText="TP View">
                                  <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                                  <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                                   ForeColor="darkblue" />
                                       <ItemTemplate>
                                 <a target="_blank" href="rptResigned_User_Status1.aspx?sfcode=<%# Eval("SF_Code") %>&DCR_Start_Date=<%#Eval("DCR_Start_Date") %>&DCR_End_Date=<%#Eval("DCR_End_Date") %>&div_code=<%#Eval("Division_Code") %>&SF_Name=<%#Eval("SF_Name") %>&TP_DCR=TP" class="tr_det_head"
                                onclick="ShowProgress();">Click Here </a>
                              
                                </ItemTemplate>
                             </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="TP View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text="Click Here" Font-Bold="true"
                                                            OnClientClick='<%# "return popUp(\"" + Eval("SF_Code") + "\",\"" + Eval("DCR_Start_Date")  + "\",\"" + Eval("DCR_End_Date")  + "\",\"" + Eval("SF_Name")  + "\",\"" + Eval("TP_DCR")  + "\",\"" + Eval("Employee_Code")+"\");" %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DCR View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkcount2" runat="server" CausesValidation="False" Text="Click Here" Font-Bold="true"
                                                            OnClientClick='<%# "return popdcr(\"" + Eval("SF_Code") + "\",\"" + Eval("DCR_Start_Date")  + "\",\"" + Eval("DCR_End_Date")  + "\",\"" + Eval("SF_Name")  + "\",\"" + Eval("TP_DCR")  + "\",\"" + Eval("Employee_Code")+"\");" %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkcount3" runat="server" CausesValidation="False" Text="Click Here" Font-Bold="true"
                                                            OnClientClick='<%# "return popleave(\"" + Eval("SF_Code") + "\",\"" + Eval("DCR_Start_Date")  + "\",\"" + Eval("DCR_End_Date")  + "\",\"" + Eval("SF_Name")  + "\",\"" + Eval("TP_DCR")  + "\",\"" + Eval("Employee_Code")+"\");" %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Input Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkcount4" runat="server" CausesValidation="False" Text="Click Here" Font-Bold="true"
                                                            OnClientClick='<%# "return popInputStatus(\"" + Eval("SF_Code") + "\",\"" + Eval("DCR_Start_Date")  + "\",\"" + Eval("DCR_End_Date")  + "\",\"" + Eval("SF_Name")  + "\",\"" + Eval("TP_DCR")  + "\",\"" + Eval("Employee_Code")+"\");" %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Expense Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkcount5" runat="server" CausesValidation="False" Text="Click Here" Font-Bold="true"
                                                            OnClientClick='<%# "return popExpenseStatus(\"" + Eval("SF_Code") + "\",\"" + Eval("DCR_Start_Date")  + "\",\"" + Eval("DCR_End_Date")  + "\",\"" + Eval("SF_Name")  + "\",\"" + Eval("TP_DCR")  + "\",\"" + Eval("Employee_Code")+"\");" %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                              </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Paysllip" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkcount6" runat="server" CausesValidation="False" Text="Click Here" Font-Bold="true"
                                                            OnClientClick='<%# "return popPaylsipStatus(\"" + Eval("SF_Code") + "\",\"" + Eval("DCR_Start_Date")  + "\",\"" + Eval("DCR_End_Date")  + "\",\"" + Eval("SF_Name")  + "\",\"" + Eval("TP_DCR")  + "\",\"" + Eval("Employee_Code")+"\");" %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                              </asp:TemplateField>
                                                <%--  <asp:HyperLinkField HeaderText="TP View" Text="Click here" DataNavigateUrlFormatString="~/MasterFiles/rptResigned_User_Status1.aspx?sfcode={0}&amp;DCR_Start_Date={1}&amp;DCR_End_Date={2}&amp;div_code={3}&amp;SF_Name={4}&amp;tp_DCR=TP"
                                        DataNavigateUrlFields="SF_Code,DCR_Start_Date,DCR_End_Date,Division_Code,SF_Name,TP_DCR">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" Font-Size="9pt" 
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                    </asp:HyperLinkField>--%>
                                                <%-- <asp:HyperLinkField HeaderText="DCR View" Text="Click here" DataNavigateUrlFormatString="~/MasterFiles/rptResigned_User_Status1.aspx?sfcode={0}&amp;DCR_Start_Date={1}&amp;DCR_End_Date={2}&amp;div_code={3}&amp;SF_Name={4}&amp;tp_DCR=DCR"
                                        DataNavigateUrlFields="SF_Code,DCR_Start_Date,DCR_End_Date,Division_Code,SF_Name,TP_DCR">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" Font-Size="9pt" 
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                    </asp:HyperLinkField>--%>

                                                <%--  <asp:HyperLinkField HeaderText="Leave Status" Text="Click here" DataNavigateUrlFormatString="~/MasterFiles/rptResigned_User_Status1.aspx?sfcode={0}&amp;DCR_Start_Date={1}&amp;DCR_End_Date={2}&amp;div_code={3}&amp;SF_Name={4}&amp;tp_DCR=Leave"
                                        DataNavigateUrlFields="SF_Code,DCR_Start_Date,DCR_End_Date,Division_Code,SF_Name,TP_DCR">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px" Font-Size="9pt" 
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" ></HeaderStyle>
                                    </asp:HyperLinkField>--%>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <br />
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
