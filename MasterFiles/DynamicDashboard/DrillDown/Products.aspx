<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/DynamicDashboard/Layout.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="MasterFiles_DynamicDashboard_DrillDown_Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageStyles" Runat="Server">
    <style>
        .table-striped > tbody > tr:nth-of-type(odd) {
            --bs-table-accent-bg: rgb(170 192 245 / 5%) !important;
        }

        .table td {
            text-wrap: nowrap !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContents" Runat="Server">
    <div class="container py-4">
        <div class="text-end">
            <a class="btn btn-success" id="exportExcel" href="javascript:void(0)" style="background-color: #1D6F42"><i class="fa fa-file-excel-o mr-2" aria-hidden="true"></i>Export Excel</a>
        </div>
        <div id="drilldownWrapper">
            <table class="mb-3">
                <tr>
                    <asp:Repeater ID="TableCaptions" runat="server">
                        <ItemTemplate>
                            <td class=" pe-4"><%# Eval("Label") %> <span class="fw-bold "><%# Eval("Value") %></span></td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <tr></tr>
                <tr></tr>
            </table>
            <div class="table-responsive bg-white">

                <asp:GridView ID="DrillDownRecords" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                    <EmptyDataTemplate>
                        <tr class="">
                            <th>No</th>
                            <th>Name</th>
                            <th>Pack</th>
                            <th>Sales ERP Code</th>
                            <th>SampleERPCode</th>
                            <th>Brand</th>
                            <th>Category</th>
                            <th>GroupName</th>
                        </tr>
                        <tr>
                            <td colspan="8" style="text-align: center;">No records found.</td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="SL No" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="Pack" HeaderText="Pack" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="SalesERPCode" HeaderText="Sales ERP Code" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="SampleERPCode" HeaderText="Sample ERP Code" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="Brand" HeaderText="Brand" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="GroupName" HeaderText="Group" HeaderStyle-CssClass="bg-dark text-light" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageModals" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageScripts" Runat="Server">
    <script type="text/javascript">
        $(function () {
            function exportToExcel() {
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#drilldownWrapper').html())
                 location.href = url
                 return false
            }
            $('#exportExcel').click(function () {
                exportToExcel();
            })

        });
    </script>
</asp:Content>

