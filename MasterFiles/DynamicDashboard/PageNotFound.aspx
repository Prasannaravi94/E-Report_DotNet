<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/DynamicDashboard/Layout.master" AutoEventWireup="true" CodeFile="PageNotFound.aspx.cs" Inherits="MasterFiles_DynamicDashboard_PageNotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageStyles" runat="Server">
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/css/bootstrap-datepicker.min.css" integrity="sha512-34s5cpvaNG3BknEWSuOncX28vz97bRI59UnVtEEpFX536A7BtZSJHsDyFoCl8S7Dt2TPzcrCEoHBGeM4SUBDBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/js/bootstrap-datepicker.min.js" integrity="sha512-LsnSViqQyaXpD4mBBdRYeP6sRwJiJveh2ZIbW41EBrNmKxgr/LFZIiWT6yr+nycvhvauz8c2nYMhrP80YhG7Cw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContents" runat="Server">
    <div class="container py-4 d-flex justify-content-center" style="min-height: calc( 100vh - 60px )">
        <div class="text-center col-md-5" style="margin-top: 150px">
            <h3 class="mb-3">Oops!</h3>
            <p class="mb-1 text-muted">Page Not Found</p>
            <p class="mb-3 text-muted">The page your were looking for doesn't exist.</p>
            <a class="btn btn-primary" href="../../../../../Default.aspx"><i class="fa-solid fa-house"></i>&nbsp;Home</a>
        </div>
    </div>
</asp:Content>

