<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Calendar_Approve.aspx.cs"
    Inherits="MasterFiles_MGR_TP_Calendar_Approve" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Tour Plan Approval</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../assets/css/nice-select.css">
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/style.css">
    <link rel="stylesheet" href="../assets/css/responsive.css">
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

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
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
        <ucl:Menu ID="menu1" runat="server" />
        <div>
            <center>
                <asp:Panel ID="pnlCalender" runat="server">
                    <div runat="server" class="container home-section-main-body position-relative clearfix" id="div_grdTP_Calander1">
                        <div class="row justify-content-center">
                            <div class="col-lg-11">
                                <h2 class="text-center" style="border-bottom: none">Tour Plan Calendar Approval</h2>
                                <div class="designation-reactivation-table-area clearfix">
                                    <div class="display-table clearfix">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdTP_Calander1" runat="server" HorizontalAlign="Center" EmptyDataText="TP No found for Approval"
                                                AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10"
                                                CssClass="table" OnRowDataBound="grdTP_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-Width="50" HeaderStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SF Code" ItemStyle-Width="100" Visible="false" HeaderStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Field Force Name" HeaderStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_HQ" Width="80px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tour Month" HeaderStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Tour_Month") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tour Year" HeaderStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="TourPlan_Calen.aspx?refer={0}"
                                                        DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                    </asp:HyperLinkField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdTP" runat="server" HorizontalAlign="Center" EmptyDataText="No found for Approval"
                                                AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10"
                                                CssClass="table" OnRowDataBound="grdTP_RowDataBound">
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
                                                    <asp:HyperLinkField HeaderText="Approve" DataTextField="Month"
                                                        DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}"
                                                        DataNavigateUrlFields="key_field" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                    </asp:HyperLinkField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdTP_Calander" runat="server" HorizontalAlign="Center" EmptyDataText="No found for Approval"
                                                AutoGenerateColumns="false" GridLines="None" AllowPaging="True" PageSize="10" OnPageIndexChanging="grdTP_Calander_PageIndexChanging"
                                                CssClass="table" OnRowDataBound="grdTP_RowDataBound">
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
                                                    <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-Width="185">
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
                                                            <asp:Label ID="lblSF_HQ" Width="70px" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
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
                                                    <asp:HyperLinkField HeaderText="Approve" DataTextField="Month" DataNavigateUrlFormatString="../MR/TourPlan.aspx?refer={0}&Index=AM"
                                                        DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" ItemStyle-Width="175">
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataTextField="Month" ShowHeader="false"
                                                        DataNavigateUrlFormatString="TourPlan_Calen.aspx?refer={0}&Index=AM"
                                                        DataNavigateUrlFields="key_field" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0">
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                                    </asp:HyperLinkField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="loading" align="center">
                                        Loading. Please wait.<br />
                                        <br />
                                        <img src="../../Images/loader.gif" alt="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </center>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
