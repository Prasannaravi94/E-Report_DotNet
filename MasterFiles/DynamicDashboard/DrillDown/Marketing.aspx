<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/DynamicDashboard/Layout.master" AutoEventWireup="true" CodeFile="Marketing.aspx.cs" Inherits="MasterFiles_DynamicDashboard_DrillDown_Marketing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageStyles" runat="Server">
    <style>
        .virtual-scroller-table {
            
            max-height: 82vh;
        }
        virtual-scroller-table-body-wrapper{
            z-index:2;
        }
        .virtual-scroller-table-header-wrapper{
            z-index:1;
            position:sticky;
            top:0px;
        }
        .virtual-scroller-table-body-wrapper{
            z-index:2;
        }
        .virtual-scroller-table-header-wrapper th:nth-child(2),.virtual-scroller-table-body td:nth-child(2){
            position:sticky;
            left:0px;
            background-color: inherit;
        }
        .virtual-scroller-table-header-wrapper th:nth-child(2){
            background-color: #212529;
        }
        
        .virtual-scroller-table-body tr:nth-of-type(odd) td:nth-child(2){
            background-color:#fbfcfe !important;
        }
        .virtual-scroller-table-body tr:nth-of-type(even) td:nth-child(2){
            background-color:#fff !important;
        }
        .virtual-scroller-table-info{
            display:flex;
            align-items:center;
        }


        .table-striped > tbody > tr:nth-of-type(odd) {
            --bs-table-accent-bg: rgb(170 192 245 / 5%) !important;
        }

        .table td,.table th {
            text-wrap: nowrap !important;
        }

        .captions-table td:first-child {
          width: 1%;
        }
        #sortable-list li ,#sortable-list li .form-check-label{
            cursor:move;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContents" runat="Server">
    <div class="container py-4">
        <%= Drilldown.GetCaptionsTable() %>
        
        <table class="table table-bordered mb-0" id="Mytable">
            <%= Drilldown.GetTableHeaders() %>
        </table>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageModals" runat="Server">
    <div id="offCanvasWrapper">

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageScripts" runat="Server">
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/virtual-scrolling-table.js") %>"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script>
    ScrollableTable("Mytable", {
        remoteUrl: "./DrillDownWebService.asmx/Marketing",
        totalRecords:<%= Drilldown.GetTotalRecords() %>,
        filters: <%= Drilldown.getFiltersJs() %>,
        columns: <%= Drilldown.GetColumnsJs() %>,
        visibleColumns: <%= Drilldown.GetVisibleColumnsJs() %>,
        preferenceName: '<%= Drilldown.PreferenceName %>',
    });
</script>
</asp:Content>

