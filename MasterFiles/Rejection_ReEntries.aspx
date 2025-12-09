<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rejection_ReEntries.aspx.cs" Inherits="MasterFiles_Rejection_ReEntries" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rejection/ReEntries</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />

    <%--    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />--%>
    <style type="text/css">
        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .roundCorner {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 100px;
            height: 30px;
        }

        .leaveformtable {
            padding: 0px;
            width: 90%;
        }

            .leaveformtable th {
                border-radius: 0px;
                color: #636d73;
            }

            .leaveformtable td {
                border: 0px solid #dee2e6;
                border-top: 1px solid #dee2e6;
            }

            .leaveformtable .leaveformth tr td {
                color: White;
                background-color: #179BED;
                padding: 10px;
                border-radius: 10px;
                border-bottom-right-radius: 10px;
                border-bottom-left-radius: 10px;
                border: 0px solid black;
                text-align: center;
                border-bottom-left-radius: 0px;
                border-bottom-right-radius: 0px;
            }

        .display-reportMaintable .table tr.no-result-area td:first-child, .no-result-area {
            font-size: 14px;
        }

        #lblcrm1 {
            width: 100%;
            display: block;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <center>
                            <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="LblUser" runat="server" Text="User" CssClass="reportheader"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnHome" runat="server" Width="150px" CssClass="savebutton" Height="30px" Text="Goto Home Page" OnClick="btnHome_Click" />
                                        &nbsp;&nbsp;
                                     <asp:Button ID="btnLogout" runat="server" Width="90px" CssClass="resetbutton" Height="30px" Text="Logout" OnClick="btnLogout_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <br />
                        <center>
                            <div class="leaveformtable">
                                <table cellpadding="0" cellspacing="0" id="Table2" align="center" class="leaveformth" width="100%">
                                    <tr>
                                        <td align="center" width="90%">
                                            <asp:Label ID="lblApproval" runat="server" Text="Rejection/ReEntries" Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div align="center">
                                            <asp:Label ID="lbldcr" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">DCR(Rejection)</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdDCR" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    EmptyDataText="No Data Found" GridLines="None" CssClass="table"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SF Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DCR_Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDCRDate" runat="server" Text='<%#Eval("DCR_Activity_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Work_Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected_By">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReasonforRejection" runat="server" Text='<%#Eval("ReasonforRejection")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div align="center">
                                            <asp:Label ID="lbltp" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Tour Plan</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdTP" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    EmptyDataText="No Data Found" GridLines="None" CssClass="table"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="50">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tour Month">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tour Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected by">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRejectBy" runat="server" Text='<%#Eval("TP_Approval_MGR")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Rejection_Reason")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Click here" DataTextField="Month" DataNavigateUrlFormatString="MR/TourPlan.aspx?reject={0}"
                                                            DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                                <br />
                                                <asp:GridView ID="grdTP_Calander" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data Found" GridLines="None"
                                                    CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="50">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tour Month">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tour Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejected by">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRejectBy" runat="server" Text='<%#Eval("TP_Approval_MGR")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Rejection_Reason")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Click here" DataTextField="Month" DataNavigateUrlFormatString="MGR/TourPlan_Calen.aspx?refer={0}"
                                                            DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div align="center">
                                            <asp:Label ID="lblcrm" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Expense</asp:Label>
                                        </div>
                                       
                                        <%--  <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">--%>
                                        <asp:Label ID="lblcrm1" runat="server" CssClass="no-result-area">No Data Found</asp:Label>
                                        <%-- </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <br />
                                <asp:Panel ID="pnldoc" runat="server">
                                    <div class="row justify-content-center">
                                        <div class="col-lg-11">
                                            <div align="center">
                                                <asp:Label ID="lblAdd" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Listed Doctor Addition/Deactivation (Rejection)</asp:Label>
                                            </div>
                                            <div class="display-reportMaintable clearfix">
                                                <div class="table-responsive" style="scrollbar-width: thin;">
                                                    <asp:GridView ID="grdListedDR" runat="server" Width="100%" HorizontalAlign="Center"
                                                        AutoGenerateColumns="false" EmptyDataText="No Data Found" GridLines="None"
                                                        CssClass="table" PagerStyle-CssClass="gridview1" RowStyle-Font-Size="Smaller" AlternatingRowStyle-CssClass="alt">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SF Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <%--     <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No Of Listed Doctor's" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDrCount" runat="server" Text='<%#Eval("LSTCount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rejected by">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReporting" runat="server" Text='<%#Eval("Listeddr_App_Mgr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMode" runat="server" Text='<%#Eval("mode")%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div align="center">
                                            <asp:Label ID="lblleave" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Leave</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdLeave" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data Found"
                                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Leave From" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("From_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leave To" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("To_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Leave Days" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbldays" runat="server" Text='<%#Eval("No_of_Days")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Rejected by" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRejectedBy" runat="server" Text='<%#Eval("Leave_App_Mgr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Rejected_Reason") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <!-- Included Secondary Sales Entry -->
                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div align="center">
                                            <asp:Label ID="lblSecSales" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Secondary Sales</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdSecSales" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data found for Rejection's"
                                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SF Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FieldForce Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDes" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Month">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSSMonth" runat="server" Text='<%#Eval("Cur_Month")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSSYear" runat="server" Text='<%#Eval("Cur_Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Click here" DataTextField="Month"
                                                            DataNavigateUrlFormatString="~/MasterFiles/MR/SecSales/SecSalesEntry.aspx"
                                                            DataNavigateUrlFields="key_field" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>

                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End  -->
                                <br />

                            </div>
                        </center>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
