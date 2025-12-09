<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorList_Reportaspx.aspx.cs"
    Inherits="Reports_DoctorList_Reportaspx" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Doctor Summary Count - View</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>

    <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" type="text/css" />
    <link href="../../E-Report_DotNet/assets/css/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {

                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <%--<script type="text/javascript">
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
    </script>--%>
    <style type="text/css">
        #tblDocRpt {
        }

        #tbl {
            border-collapse: collapse;
        }
        /*table, td, th
        {
            border: 1px solid black;
        }*/
        .GrdWidth {
            width: 500px;
        }

        .display-Approvaltable #GrdDoctor tr:nth-child(3) td:first-child {
            background-color: #F1F5F8;
        }

        .table td {
            border-bottom: 1px solid #dee2e6;
        }
         /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .stickySecondRow {
            position: sticky;
            position: -webkit-sticky;
            top: 40px;
            z-index: 0;
            background: inherit;
        }
         .display-Approvaltable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 38px;
            background: inherit;
            z-index: 2;
            min-width: 158px;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 38px;
        }
        /*Fixed Heading & Fixed Column-End*/
         .display-Approvaltable #GrdDoctor tr td:first-child {
            padding: 8px 10px;
        }

        #GrdDoctor td {
            padding: .5rem;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>

            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Doctor Details - View</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblView" runat="server" Text="View By" CssClass="label"></asp:Label>
                                    <asp:RadioButtonList ID="rdoMGRState" runat="server"
                                        RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoMGRState_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="FieldForce-wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                            Selected="True"></asp:ListItem>
                                        <%--<asp:ListItem Value="1" Text="State-wise"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="false"
                                                CssClass="nice-select">
                                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                                <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <br />
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblType" runat="server" Text="Type" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="rdoType" runat="server" CssClass="nice-select">
                                        <%--<asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>--%>
                                        <asp:ListItem Value="0" Text="Category"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Speciality"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                                        <%-- <asp:ListItem Value="3" Text="Qualification"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>


                        <div class="single-des clearfix">
                            <%-- <asp:Label ID="lblWOVacant" Text="With Vacants" runat="server" SkinID="lblMand"></asp:Label>--%>
                            <asp:CheckBox ID="chkWOVacant" runat="server" Text="With Vacants" />
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server" Text="View"
                                CssClass="savebutton" OnClick="btnSubmit_Click" />

                        </div>

                    </div>

                </div>

                <div class="row ">
                    <div class="col-lg-11">

                        <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright" Visible="false">
                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                            <%-- <asp:Button ID="btnExcel" BorderColor="Black" Visible="false" BorderWidth="1PX" BackColor="Yellow"
                                Text="Excel" runat="server" Style="text-align: center; width: 60px; height: 22px; text-decoration: none; color: Black"></asp:Button>--%>
                        </asp:Panel>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="display-Approvaltable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <div id="pnlContents" runat="server">
                                    <asp:Table ID="tbl" runat="server" CssClass="table">
                                    </asp:Table>
                                    <asp:GridView ID="GrdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="true" PageSize="10" EmptyDataText="No Records Found"
                                        GridLines="none" CssClass="table" OnRowDataBound="GrdDoctor_RowDataBound" OnRowCreated="GVMissedCall_RowCreated"
                                        AlternatingRowStyle-CssClass="alt" ShowHeader="false">
                                        <PagerStyle CssClass="gridview1"></PagerStyle>
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="20px" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> --%>
                                            <%-- <asp:TemplateField HeaderText="#" HeaderStyle-Width="20px" Visible="false" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSf_Code" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                                            <%-- <asp:TemplateField>
                            <ItemTemplate>
                                  <asp:LinkButton ID="lnkBtn" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                            </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                                <%-- <asp:Panel ID="pnlDoctorView" runat="server" Height="400px">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="838px">
                        </rsweb:ReportViewer>
                    </asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<asp:Label ID="lblCount" runat="server"></asp:Label><br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
            <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        </div>
    </form>
</body>
</html>
