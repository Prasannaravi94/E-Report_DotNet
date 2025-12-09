<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Index.aspx.cs" Inherits="MasterFiles_MGR_MGR_Index" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <style type="text/css">
        .roundCorner {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 150px;
            height: 30px;
        }
    </style>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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

        .display-reportMaintable .table tr.no-result-area td:first-child {
            font-size: 14px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
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
                                        <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="savebutton"
                                            Text="Goto Home Page" OnClick="btnHome_Click"
                                            Visible="false" />
                                        &nbsp;&nbsp;
                        <asp:Button ID="btnLogout" runat="server" Width="90px" CssClass="savebutton"
                            Text="Logout" OnClick="btnLogout_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Label ID="lblhomepage" runat="server" Visible="false"
                                CssClass="reportheader">For Getting the
              <%--  <asp:Button ID="Button1" runat="server" BackColor="Green" ForeColor="White" Text="Goto Home Page"
                    Enabled="false" />--%>
                                <asp:Label ID="Label3" runat="server" Text="Goto Home Page" ForeColor="#27d624" CssClass="reportheader"></asp:Label>
                                Link --> (
                <asp:Label ID="lbltext" runat="server" CssClass="reportheader" ForeColor="Red"></asp:Label>
                                ) Approval is Mandatory</asp:Label>
                        </center>
                        <br />
                        <br />
                        <center>
                            <div class="leaveformtable">
                                <table cellpadding="0" cellspacing="0" id="Table2" align="center" class="leaveformth" width="100%">
                                    <tr>
                                        <td align="center" width="90%">
                                            <%-- <a href="../../MGR_Home.aspx" style="text-decoration: none; color: white">--%>
                                            <asp:Label ID="lblApproval" runat="server" Text="Approvals" Font-Size="Medium"></asp:Label>
                                            <%-- </a>--%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <center>
                                    <table cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired"
                                                    AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                    </table>
                                </center>
                                <%--<table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lbldcr" runat="server" Font-Size="Medium" Font-Bold="true" Font-Names="Verdana"
                            ForeColor="Chocolate">DCR</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdDCR" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false"
                            EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="mGridImg"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
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
                                        <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HQ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Mon")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                            <asp:HyperLinkField HeaderText="Month - Year" 
                                       DataNavigateUrlFormatString="~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                       DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-HorizontalAlign="Center">
                            </asp:HyperLinkField>                           
                               
                                <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                    DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>--%>
                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div align="center">
                                            <asp:Label ID="lbldcr" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader" Visible="false">DCR</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdDCR" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="table"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt" Visible="false">

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
                                                                <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Month" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Mon")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <%--                            <asp:HyperLinkField HeaderText="Month - Year" 
                                       DataNavigateUrlFormatString="~/MasterFiles/MGR/DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                       DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-HorizontalAlign="Center">
                            </asp:HyperLinkField>                           
                                                        --%>
                                                     <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="DCR_Bulk_Approval.aspx?sfcode={0}&amp;Mon={1}&amp;Year={2}"
                                                            DataNavigateUrlFields="SF_Code,Mon,Year" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                                    EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="table"
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
                                                        <asp:TemplateField HeaderText="Field Force Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSF_Name" Width="160px" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDG" Width="100px" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSF_HQ" Width="120px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tour Month">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMonth" Width="80px" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tour Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" Width="80px" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}"
                                                            DataNavigateUrlFields="key_field" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>

                                                <asp:GridView ID="grdTP_Calander" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" OnRowDataBound="grdTP_RowDataBound" EmptyDataText="TP No found for Approval"
                                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

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
                                                        <asp:TemplateField HeaderText="FieldForce Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSF_HQ" Width="80px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
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
                                                        <asp:HyperLinkField HeaderText="Approve" ItemStyle-Width="200" DataTextField="Month"
                                                            DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}&Index=A" DataNavigateUrlFields="key_field"
                                                            ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>

                                                        <asp:HyperLinkField ShowHeader="false" DataTextField="Month" DataNavigateUrlFormatString="../MR/TP_ENTRY_STP.aspx?refer={0}&amp;sf_name={1}&amp;Index=A"
                                                            DataNavigateUrlFields="key_field,sf_name" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black"></asp:HyperLinkField>

                                                        <%--  <asp:HyperLinkField ShowHeader="false" ItemStyle-Width="200" DataTextField="Month"
                                    DataNavigateUrlFormatString="../MR/TP_ENTRY_STP.aspx?refer={0}&Index=A" DataNavigateUrlFields="key_field"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField>--%>

                                                        <%--                                  <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="../MR/TP_Entry_daywise.aspx?refer={0}&amp;sf_name={1}&amp;Index=A"
                                        DataNavigateUrlFields="key_field,sf_name" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                    </asp:HyperLinkField>--%>
                                                        <asp:HyperLinkField DataTextField="Month" ShowHeader="false" DataNavigateUrlFormatString="TourPlan_Calen.aspx?refer={0}&Index=A"
                                                            DataNavigateUrlFields="key_field" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                            <asp:Label ID="Label2" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Expense</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="GridExpense" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's"
                                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#((GridViewRow)Container).RowIndex + 1%>'></asp:Label>
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
                                                                <asp:Label ID="lblDes" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Month" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Mon")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Approve" DataTextField="mondt"
                                                            DataNavigateUrlFormatString="RptAutoExpense_View_Mgr_Index.aspx?sf_code={0}&amp;monthId={1}&amp;yearID={2}&amp;divCode={3}"
                                                            DataNavigateUrlFields="SF_Code,Mon,Year,div_code" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                            <asp:Label ID="lblAdd" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Listed Doctor Addition</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdListedDR" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                                                    CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExist" runat="server" Text='<%#Eval("mode")%>'></asp:Label>
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
                                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Approve">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutApprove" runat="server" CommandArgument='<%# Eval("Sf_Code") %>'
                                        CommandName="Approve" OnClientClick="return confirm('Do you want to Approve');">Approve
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                                        <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="ListedDR_Add_Approval.aspx?sfcode={0}"
                                                            DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                        <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="../Common_Doctors/UniqueDR_Add_Approval.aspx?sfcode={0}&amp;mode={1}"
                                                            DataNavigateUrlFields="SF_Code,mode" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                            <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Listed Doctor Deactivation</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdListedDR1" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                    AlternatingRowStyle-CssClass="alt" EmptyDataText="No Data found for Approval's">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExist" runat="server" Text='Existing'></asp:Label>
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
                                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--             <asp:TemplateField HeaderText="Click Here">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbutApprove" runat="server" CommandArgument='<%# Eval("Sf_Code") %>'
                                        CommandName="Approve" OnClientClick="ListedDR_DeActivate_Approval.aspx">Approve
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                                        <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="ListedDR_DeActivate_Approval.aspx?sfcode={0}"
                                                            DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                                        <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="../Common_Doctors/UniqueDR_DeActivate_Approval.aspx?sfcode={0}"
                                                            DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                            <asp:Label ID="Lbladddeact" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader">Add Against Deactivated Doctor Approval</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdadddeactivate" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" GridLines="None"
                                                    CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

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
                                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="AddAgainstDeact_App.aspx?sfcode={0}"
                                                            DataNavigateUrlFields="SF_Code" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                            <asp:Label ID="lblleave" runat="server" Font-Size="Medium" Font-Bold="true" CssClass="reportheader" Visible="false">Leave</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <%--  <asp:Label ID="lbll" runat="server" Text="Leave Approvals Available only in Mobile App" Font-Size="Large" Font-Bold="true" ForeColor="Red"></asp:Label>--%>
                                                <asp:GridView ID="grdLeave" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    EmptyDataText="No Data found for Approval's" GridLines="None" CssClass="table"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt" Visible="false">

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
                                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designaion" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Emp.Code" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemp" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("From_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Date" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltodate" runat="server" Text='<%#Eval("To_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leave Days" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldays" runat="server" Text='<%#Eval("No_of_Days")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField HeaderText="Click Here" Text="Click Here to Approve" DataNavigateUrlFormatString="~/MasterFiles/MR/LeaveForm.aspx?sfcode={0}&amp;Leave_Id={1}"
                                                            DataNavigateUrlFields="SF_Code,Leave_Id" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
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
                                            <asp:Label ID="lblSecSales" runat="server" Font-Size="Medium" Font-Bold="true"
                                                CssClass="reportheader">Secondary Sales</asp:Label>
                                        </div>
                                        <div class="display-reportMaintable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:GridView ID="grdSecSales" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Data found for Approval's" OnRowDataBound="grdSecSales_RowDataBound"
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
                                                        <%-- <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" 
                                DataNavigateUrlFormatString="~/MasterFiles/MGR/SecSales/SecSale_Entry.aspx?refer={0}"
                                DataNavigateUrlFields="key_field" ItemStyle-Width="240px" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>--%>
                                                        <asp:TemplateField HeaderText="Approve">

                                                            <ItemTemplate>
                                                                <a href="../MR/SecSales/SecSalesEntry_New.aspx?refer=<%#Eval("key_field")%>" onclick="javascript:return ShowProgress();"
                                                                    style="color: DarkBlue; font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center">
                                                                    <%#Eval("Month")%>
                                                                </a>
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
                            </div>
                        </center>
                        <br />
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
