<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoutePlan_Catgwise.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_RoutePlanView" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route Plan</title>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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
        $('form').live("go", function () {
            ShowProgress();
        });
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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

            $('#btnGo').click(function () {

                var cat = $('#<%=ddlTerritory.ClientID%> :selected').text();
                if (cat == "---Select---") { alert("Please Select Route Plan."); $('#ddlTerritory').focus(); return false; }

            });
        });
    </script>
    <style type="text/css">
        .divlabel {
            background-color: #FFFF66;
            font-size: 14px;
            width: 50%;
            height: 30px;
            padding: 5px;
        }

        .divlabel1 {
            width: 90%;
            height: 30px;
            padding: 5px;
            display: inline-block;
            border-radius: 5px;
        }

        .tbltd {
            font-size: 14px;
            width: 50px;
            height: 30px;
            padding: 5px;
        }

        .container {
            max-width: 1325px !important;
        }

        .display-table .table th {
            font-size: 12px !important;
        }

            .display-table .table th:last-child {
                border-radius: 0px 0px 0px 0px !important;
            }

            .display-table .table th:first-child {
                /*background-color: #F1F5F8 !important;
                color: #636d73 !important;*/
                border-radius: 0px 0 0 0px !important;
            }

        .display-table .table tr td:first-child {
            border-top: 1px solid #dee2e6 !important;
            text-align: center !important;
        }

        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: static !important;
            left: -9999px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-4">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Territory Classic(Categorywise)</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix" style="text-align: center;">
                            <asp:Label ID="lblAllocate" runat="server" Text="Route Plan " BackColor="#FFFF66" CssClass="label divlabel" Font-Size="14px"></asp:Label>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblSDP" runat="server" CssClass="label" Text="Route Plan"></asp:Label>
                            <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="nice-select" Width="100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Table ID="tbl" runat="server" GridLines="Both" Width="100%">
                        </asp:Table>
                    </div>
                </div>
                <div class="col-lg-12">
                    <br />
                    <table width="100%">
                        <tr>
                            <td align="center" width="50%">
                                <asp:Label ID="lblNote1" runat="server" Text="Note : Highlighted in green represents Missed Doctors" BackColor="LightGreen" CssClass="label divlabel1" Font-Size="14px" Visible="false"></asp:Label>
                            </td>
                            <td align="center" width="50%">
                                <asp:Label ID="lblNote2" runat="server" Text="Note : Highlighted in Light Orange represents Doctors mapped in other plans" BackColor="PapayaWhip" CssClass="label divlabel1" Font-Size="14px" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%">
                        <tr style="width: 100%;">
                            <td align="center" width="30%">
                                <table width="80%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCatg1" Font-Bold="true" runat="server" CssClass="label" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="designation-reactivation-table-area clearfix">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                        <asp:GridView ID="grdDoctor1" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor1_RowDataBound" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocSpecShortName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCatShortName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%"></td>
                            <td align="center" width="30%">
                                <table width="80%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCatg2" Font-Bold="true" runat="server" CssClass="label" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div class="designation-reactivation-table-area clearfix">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                        <asp:GridView ID="grdDoctor2" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor2_RowDataBound" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocSpecShortName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCatShortName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%"></td>
                            <td align="center" width="30%">
                                <table width="80%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCatg3" Font-Bold="true" runat="server" CssClass="label" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="designation-reactivation-table-area clearfix">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                        <asp:GridView ID="grdDoctor3" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor3_RowDataBound" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocSpecShortName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCatShortName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="width: 100%;">
                            <td align="center" width="30%">
                                <table width="80%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCatg4" Font-Bold="true" runat="server" CssClass="label" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="designation-reactivation-table-area clearfix">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                        <asp:GridView ID="grdDoctor4" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor4_RowDataBound" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocSpecShortName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCatShortName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%"></td>
                            <td align="center" width="30%">
                                <table width="80%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCatg5" Font-Bold="true" runat="server" CssClass="label" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div class="designation-reactivation-table-area clearfix">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                        <asp:GridView ID="grdDoctor5" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor5_RowDataBound" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocSpecShortName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCatShortName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%"></td>
                            <td align="center" width="30%">
                                <table width="80%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCatg6" Font-Bold="true" runat="server" CssClass="label" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div class="designation-reactivation-table-area clearfix">
                                                <div class="display-table clearfix">
                                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 500px;">
                                                        <asp:GridView ID="grdDoctor6" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDoctor6_RowDataBound" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Terr_Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerrCode" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocSpecShortName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocCatShortName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Update" Visible="false" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
