<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/DynamicDashboard/Layout.master" AutoEventWireup="true" CodeFile="ListedDoctors.aspx.cs" Inherits="MasterFiles_DynamicDashboard_DrillDown_ListedDoctors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageStyles" runat="Server">
    <style>
        .table-striped > tbody > tr:nth-of-type(odd) {
            --bs-table-accent-bg: rgb(170 192 245 / 5%) !important;
        }

        .table td {
            text-wrap: nowrap !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContents" runat="Server">
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
                            <th>Fieldforce Name</th>
                            <th>Doctor Name</th>
                            <th>Doctor Speciality</th>
                            <th>Doctor Category</th>
                            <th>Doctor Class</th>
                            <th>Doctor Territory</th>
                        </tr>
                        <tr>
                            <td colspan="11" style="text-align: center;">No records found.</td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="SL No" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="FieldForceName" HeaderText="Fieldforce Name" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="DoctorName" HeaderText="Doctor Name" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="DoctorSpeciality" HeaderText="Doctor Speciality" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="DoctorCategory" HeaderText="Doctor Category" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="DoctorClass" HeaderText="Doctor Class" HeaderStyle-CssClass="bg-dark text-light" />
                        <asp:BoundField DataField="DoctorTerritory" HeaderText="Doctor Territory" HeaderStyle-CssClass="bg-dark text-light" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageModals" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageScripts" runat="Server">
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

