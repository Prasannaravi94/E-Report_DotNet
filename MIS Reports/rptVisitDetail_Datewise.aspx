<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptVisitDetail_Datewise.aspx.cs" Inherits="MIS_Reports_rptVisitDetail_Datewise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Detail - DateWise</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" />

    <%-- <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
  <link type="text/css" rel="stylesheet" href="../css/Report.css" />--%>
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
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .divider {
            height: 2px;
            width: 90%;
            margin: 5px auto 5px auto;
            vertical-align: middle;
            background-color: #E9E7E3;
        }

        .remove {
            text-decoration: none;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reportMaintable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }

        .dataalignment {
            height: 40px;
            text-align: center;
            align-content: center;
            display: grid;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-reportMaintable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            /*background: inherit;*/
            z-index: 2;
            min-width: 158px;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 33px;
        }

        .display-reportMaintable .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            /*background: inherit;*/
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 190px;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 650px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 40px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <br />
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-2">
                        <div runat="server" id="divSettings" style="margin-left: 35px; cursor: pointer;">
                            <asp:ImageButton ID="settings" runat="server" ImageUrl="../Images/cog.png" ToolTip="Show/Hide Grid Columns" Style="width: 30px; height: 30px; position: absolute;" />
                            <asp:Label ID="Label5" runat="server" Text="Show/Hide Columns" CssClass="label" Font-Size="14px" Style="margin-left: 32px; margin-top: 4px; height: 30px; display: inline-block; vertical-align: middle; font-weight: 401;"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-7">
                        <div align="center">
                            <%--<asp:Label ID="lblHead" Text="Product Exposure Analysis" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>--%>
                            <asp:Label ID="lblHead" Text="Visit Detail - DateWise" CssClass="reportheader"
                                runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <asp:Panel ID="pnlbutton" runat="server">
                            <table width="100%">
                                <tr>
                                    <td></td>
                                    <%--  <td width="80%" align="center">
                        <asp:Label ID="lblHead" Text="Visit Detail - DateWise" SkinID="lblMand" Font-Bold="true" Font-Underline="true"
                            runat="server"></asp:Label>
                    </td>--%>
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
                    </div>
                </div>
                <br />

                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                                <div>

                                    <br />
                                    <center>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                                <asp:Label ID="lblRegionName" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblIDMonth" Text="Month :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                                <asp:Label ID="lblMonth" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblIDYear" Text="Year :" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                                <asp:Label ID="lblYear" runat="server" CssClass="label" Font-Size="16px"></asp:Label>
                                            </div>
                                        </div>
                                    </center><br />
                                    <div class="display-reportMaintable clearfix">
                                        <br />

                                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancelnew"
                                            PopupControlID="Panel2" TargetControlID="divSettings" BackgroundCssClass="modalBackgroundNew">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Width="250px" align="center" Style="display: none; height: auto;">
                                            <div class="header">
                                                Show/Hide Column
                                            </div>
                                            <div class="body">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div style="height: auto; background-color: #f4f8fa; border: 1px solid Silver; overflow: auto; color: #90a1ac; font-size: 14px; border-radius: 10px; border: 1px solid #d1e2ea; background-color: #f4f8fa; margin-top: 0px; text-align: left;">
                                                            <asp:CheckBoxList ID="cblGridColumnList" Font-Size="8pt" runat="server"
                                                                CssClass="chkChoice">
                                                            </asp:CheckBoxList>
                                                            <br />
                                                            <br />
                                                            <div class="w-100 designation-submit-button text-center clearfix">
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                                                                <asp:Button ID="btnCancelnew" runat="server" Text="Cancel" CssClass="savebutton" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>

                                        <div class="table-responsive" style="scrollbar-width: thin; overflow:inherit">
                                            <table width="100%" align="center">
                                                <tbody>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Table ID="tbl" runat="server" CssClass="table"
                                                                GridLines="None" Width="100%">
                                                            </asp:Table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table width="100%" align="center">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <div align="center">
                                                                <asp:DataList ID="DLoneVisitDoc"
                                                                    runat="server" RepeatDirection="Vertical" OnItemDataBound="ItemDB" align="center"
                                                                    RepeatColumns="1">
                                                                    <HeaderStyle BorderColor="#DCE2E8" />
                                                                    <ItemStyle BackColor="#E6F5F7" />
                                                                    <%-- <ItemTemplate> 
                                       <asp:Label ID="lblName" Style="text-align: left; font-family: Verdana; font-size: 8pt"
                                       runat="server" Text='<%# Eval("sf_name") %>'></asp:Label>
                               </ItemTemplate>--%>
                                                                    <ItemStyle BackColor="White" BorderColor="#DCE2E8" BorderWidth="1px" />
                                                                    <%--<AlternatingItemStyle BackColor="#B2E0E6" />--%>
                                                                    <ItemStyle />
                                                                    <%-- <ItemTemplate>                                   
                                   <asp:Label ID="lblName" Style="text-align: left; font-family: Verdana; font-size: 8pt"
                                       runat="server" Text='<%# Eval("Territory_Planned") %>'></asp:Label>
                               </ItemTemplate>--%>
                                                                    <ItemTemplate>

                                                                        <table>

                                                                            <tr>
                                                                                <td colspan="6">

                                                                                    <h3 width="200px" align="center" style="color: #0077FF; font-size: 11pt; font-weight: 500;">
                                                                                        <%# Eval("Date_Name")%>
                                        
                                                                                    </h3>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <%--         <td>
                                      <asp:Label id="lblSLNO" runat="server" Width="50px" Text='<%# Container.ItemIndex+1 %>' ></asp:Label>
                                    </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblDr_Name" runat="server" Width="200px" Font-Size="9pt" Text='<%# Eval("ListedDr_Name")%>'></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lblQual" runat="server" Width="100px" Font-Size="9pt" Text='<%# Eval("Doc_Qua_Name")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblspec" runat="server" Width="100px" Font-Size="9pt" Text='<%# Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblcat" runat="server" Width="100px" Font-Size="9pt" Text='<%# Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblclass" runat="server" Width="100px" Font-Size="9pt" Text='<%# Eval("Doc_Class_ShortName")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblprod" runat="server" Width="500px" Font-Size="9pt" Text='<%# Eval("Product_Detail")%>'></asp:Label>
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                        <%--<br />
                                    <b>
                                        <asp:Label runat="server" Text='<%# Eval("sf_name")%>' ID="lblTitle"> </asp:Label>
                                        <br />
                                        Tour Date :
                                        <%#Eval("Date")%>
                                        <br />
                                        Territory_Planned :
                                        <%# Eval("Territory_Planned")%>
                                        <br />--%>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
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



