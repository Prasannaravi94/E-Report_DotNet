<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFiles/DynamicDashboard/Layout.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="MasterFiles_DynamicDashboard_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageStyles" runat="Server">
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/css/bootstrap-datepicker.min.css" integrity="sha512-34s5cpvaNG3BknEWSuOncX28vz97bRI59UnVtEEpFX536A7BtZSJHsDyFoCl8S7Dt2TPzcrCEoHBGeM4SUBDBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/js/bootstrap-datepicker.min.js" integrity="sha512-LsnSViqQyaXpD4mBBdRYeP6sRwJiJveh2ZIbW41EBrNmKxgr/LFZIiWT6yr+nycvhvauz8c2nYMhrP80YhG7Cw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContents" runat="Server">
    <div class="container py-4">
        <div class="mb-4">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a class="text-dark text-decoration-none" href="../../../../../Default.aspx">Home</a></li>
                    <li class="breadcrumb-item">
                        <asp:DropDownList ID="div" runat="server" CssClass="selectpicker division-selector" data-live-search="true">
                        </asp:DropDownList>
                    </li>
                    <% if (CurrentDashboard != null) {  %>
                        <% if (CurrentDashboard.Module == "master_kpi") { %>
                            <li class="breadcrumb-item"><a class="text-muted text-decoration-none" >Master KPI</a></li>
                        <%} else if (CurrentDashboard.Module == "marketing_kpi") { %>
                        <li class="breadcrumb-item"><a class="text-muted text-decoration-none" >Marketing KPI</a></li>
                        <%} else if (CurrentDashboard.Module == "sales_kpi") { %>
                        <li class="breadcrumb-item"><a class="text-muted text-decoration-none">Sales KPI</a></li>
                         <% } %>
                        <li class="breadcrumb-item active dropdown" aria-current="page">
                            <a href="javascript:void(0)" class="dropdown-toggle text-dark text-decoration-none" data-bs-toggle="dropdown"><%= CurrentDashboard.Name %></a>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#editDashboardFormModal">Edit</a></li>
                                <li><a class="dropdown-item text-danger" data-bs-toggle="modal" data-bs-target="#deleteDashboardModal" href="javascript:void(0)" >Delete</a></li>
                            </ul>
                        </li>
                     <% } %>
                </ol>
            </nav>
        </div>
    <% if (CurrentDashboard != null)
        { %>
    
        <div class="dashboard-bar d-md-flex mb-3 justify-content-between align-items-center">
            <div class="">
                <div class="row g-3 mb-2">
                    <div class="col-auto">
                        <button type="button" class="btn btn-primary" id="generatePdf">
                            <i class="fa-solid fa-download me-2"></i>Download
                        </button>
                    </div>
                    <div class="col-auto">
                        <button type="button" id="openAddWidgetModal" class="btn btn-primary d-none d-lg-block">
                            Add Widgets (<span id="widgetsCound">0/6</span>)
                        </button>
                    </div>
                </div>
            </div>
            <div class="d-flex">
                <div class="row g-3 align-items-center" id="DashboardFilter">
                    <div class="col-auto">
                        <label for="SalesForceList" class="col-form-label">Field Force</label>
                    </div>
                    <div class="col-auto">
                        <asp:DropDownList ID="SalesForceList" runat="server" CssClass="selectpicker" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                    <% if (CurrentDashboard != null && CurrentDashboard.Module =="marketing_kpi")
                        { %>
                    <div class="col-auto">
                        <label for="SalesForceList" class="col-form-label">Months</label>
                    </div>
                    <div class="col-auto">
                        <input type="text" name="monthStart" readonly class="form-control dashboard-filter" id="monthsFilterFrom" style="max-width: 105px;">
                    </div>
                    <div class="col-auto">
                        to
                    </div>
                    <div class="col-auto">
                        <input type="text" name="monthEnd" readonly class="form-control dashboard-filter" id="monthsFilterTo" style="max-width: 105px;">
                    </div>
                    <% } %>
                    <div class="col-auto">
                        <button class="btn btn-primary ms-3" id="applyFilter">Apply</button>
                        <a class="btn btn-secondary ms-1" href="#" onclick="reloadWindow()">Clear</a>
                    </div>
                </div>
            </div>
        </div>

        <div id="dashboardWrapper">
            <div class="grid-stack"></div>
        </div>
        <div id="nowidgets-container" style="display:none">
            <div class="container d-flex justify-content-center" style="min-height: calc( 100vh - 250px )">
                <div class="text-center col-md-5" style="margin-top: 150px">
                    <p class="mb-1" style="font-size: 38px;"><i class="fa-solid fa-magnifying-glass-chart"></i></p>
                    <h5 class="mb-3">You must add widgets to be shown</h5>
                </div>
            </div>
        </div>
        <% }
        else
        { %>
        <div class="py-4 d-flex justify-content-center" style="min-height: calc( 100vh - 60px )">
            <div class="text-center col-md-5" style="margin-top: 150px">
                <h5 class="mb-3">No Dashboards Available</h5>
                <p class="mb-5">You currently don't have any dashboards created.Create your first dashboard by clicking the button below.</p>
                <a class="btn btn-primary" href="../../../../../Default.aspx"><i class="fa-solid fa-house"></i>&nbsp;Home</a>
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-id="0" data-bs-target="#dashboardFormModal">
                    <i class="fa-solid fa-plus"></i>&nbsp;Create New Dashboard
                </button>
            </div>
        </div>
        

        <% } %>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageModals" runat="Server">
    <!-- modals -->
    <% if (CurrentDashboard != null)
        { %>
    <div class="modal fade" id="editDashboardFormModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <form id="editDashboardForm">
                    <input type="hidden" name="dashboard_id" value="<%= CurrentDashboard.Id %>" />
                    <input type="hidden" name="dashboard_module" value="<%= CurrentDashboard.Module %>" />
                    
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Edit Dashboard</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="dashboard_name" class="form-label">Name</label>
                            <input type="text" class="form-control alphanumeric-input" id="dashboard_name"
                                name="dashboard_name" value="<%= CurrentDashboard.Name %>">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="submit" class="btn btn-primary">Update</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <% } %>
    <form id="widgetForm">
    <div class="modal fade" id="widgetModal" tabindex="-1" aria-labelledby="widgetModalLabel" aria-hidden="true">
        
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="widgetModalLabel">Add widget</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-4 border-end">
                                <h5>Select Parameters</h5>
                                <div id="widgetModalsSidebar">
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="mb-3">
                                            <div class="mb-3">
                                                <label for="widget_name" class="form-label">Widget name</label>
                                                <input type="text" id="widget_name" name="widget_name"
                                                    class="form-control alphanumeric-input required">
                                            </div>
                                            <div class="mb-3 split-by-container">
                                                <label for="split_by" class="form-label">Split By</label>
                                                <select class="form-select" name="split_by">
                                                    <option value="">None</option>
                                                </select>
                                            </div>
                                            <div id="widgetFormFiltersContainer" class="widget-custom-filter-wrapper">

                                            </div>
                                            <input type="hidden" id="widget_id" name="widget_id" value=""
                                                class="form-control">
                                        </div>
                                    </div>
                                </div>
                                
                                <div id="widgetPreviewerWrapper" class="p-3 mt-3">
                                    
                                    <div>
                                        <label for="widget_name" class="form-label">Choose Chart Type</label><br />

                                        <a class="btn btn-outline-secondary active chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Pie Chart"
                                            data-charttype="pie"><i class="fa-solid fa-chart-pie"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Donut Chart"
                                            data-charttype="donut"><i class="fa-solid fa-circle-dot"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Column Chart"
                                            data-charttype="column"><i class="fa-solid fa-chart-simple"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Line Chart"
                                            data-charttype="line"><i class="fa-solid fa-chart-line"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Area Chart"
                                            data-charttype="area"><i class="fa-solid fa-chart-area"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Funnel Chart"
                                            data-charttype="funnel"><i class="fa-solid fa-play fa-rotate-90"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Line Column Chart"
                                            data-charttype="linecolumn"><i class="fa-solid fa-chart-simple"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Table Format"
                                            data-charttype="table"><i class="fa-solid fa-table-cells-large"></i></a>
                                        <a class="btn btn-outline-secondary chartpreviewer-type"
                                            data-bs-toggle="tooltip" data-bs-placement="top" title="Score Card"
                                            data-charttype="score"><i class="fa-solid fa-gauge-high"></i></a>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8 mx-auto">
                                            <div class="card mt-3 border-0">
                                                <div class="card-body">
                                                    <div id="widgetPreviewer"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="submit" class="btn btn-primary" id="addWidget">Add</button>
                    </div>               
            </div>
        </div>
        
    </div>
        </form>

    <div class="modal fade" id="removeWidgetModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirm Remove</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Are you sure you want to remove this widget?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <button type="button" class="btn btn-danger" id="removeWidget" data-widget-id="">Remove</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="deleteDashboardModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirm Delete</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this dashboard?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <button type="button" class="btn btn-danger" id="deleteDashboard" data-widget-id="">Delete</button>
                </div>
            </div>
        </div>
    </div>

    <!-- end modals -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageScripts" runat="Server">
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/jspdf.min.js") %>"></script>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/html2canvas.js") %>"></script>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/gridstack/gridstack.min.js") %>"></script>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/apexchart/apexcharts.min.js") %>"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/js/bootstrap-select.min.js"></script>

    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/widgets-filter-options.js?v=1.5") %>"></script>
    <% if (CurrentDashboard != null) {  %>
    <% if (CurrentDashboard.Module == "master_kpi") {  %>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/kpis/masterkpi.js?v=1.5") %>"></script>
    <script>
        const widgets = {
            master_kpi: master_kpi_widgets,
        }
    </script>
    <% }  %>
    <% if (CurrentDashboard.Module == "marketing_kpi") {  %>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/kpis/marketingkpi.js?v=1.5") %>"></script>
    <script>
        const widgets = {
            marketing_kpi: marketing_kpi_widgets,
        }
    </script>
    <% }  %>
    <% if (CurrentDashboard.Module == "sales_kpi") {  %>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/kpis/saleskpi.js?v=1.5") %>"></script>
    <script>
        const widgets = {
            sales_kpi: sales_kpi_widgets,
        }
    </script>
    <% }  %>
    <% }  %>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/widgets.js?v=1.5") %>"></script>
    <script src="<%= Page.ResolveClientUrl("~/assets/dynamic_dashboard/chart.js?v=1.5") %>"></script>
    <script type="text/javascript">
        function reloadWindow() {
            location.reload();
        }
        <% if (CurrentDashboard != null)
        { %>
        var dashboardModule = '<%= CurrentDashboard.Module %>'
        var dashboardId = '<%= CurrentDashboard.Id %>'
        var currentSfcode = $('#' + '<%= SalesForceList.ClientID %>').val();
        var sfcodeDropdownSelector = '#' + '<%= SalesForceList.ClientID %>';
        <% }%>
        var grid;
        let windowWidth = document.body.clientWidth;

        function CreatePDFfromHTML() {
            showLoader()
            var HTML_Width = $("#dashboardWrapper").width();
            var HTML_Height = $("#dashboardWrapper").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

            html2canvas($("#dashboardWrapper")[0]).then(function (canvas) {
                
                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);
                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }
                pdf.save("Dashboard.pdf");
                hideLoader()
            });
        }

        function setWidgetCount() {
            count = $('.grid-stack-item .grid-stack-item-content').length;
            $('#widgetsCound').html(count + "/6");
            if (count == 6) {
                $('#widgetsCound').parent().addClass('disabled');
            } else {
                $('#widgetsCound').parent().removeClass('disabled');
            }
            if (count > 0) {
                $("#nowidgets-container").hide();
            } else {
                $("#nowidgets-container").show();
            }
        }
        function saveDashboard() {
            if (parseInt(windowWidth) > 1100) {
                serializedFull = grid.save(true, true);
                dashboardwidgets = serializedFull.children;

           
                $.each(dashboardwidgets, function (key, widget) {
                    dashboardwidgets[key].widgetfilters = {};
                    
                    
                    var htmlString = widget.content;
                    var $parsedHtml = $(htmlString);

                    $(`.widget-filter-wrapper[data-id="widget-container-id-${key}"] .widget-form-filter:not(div)`).each(function (index, element) {
                        dashboardwidgets[key].widgetfilters[$(this).attr('name')] = $(this).data('value');
                    });

                    dashboardwidgets[key].module = $parsedHtml.data('module');
                    dashboardwidgets[key].measureby = $parsedHtml.data('measureby');
                    dashboardwidgets[key].viewby = $parsedHtml.data('viewby');
                    dashboardwidgets[key].chart = $parsedHtml.data('chart');
                    dashboardwidgets[key].title = $parsedHtml.data('title');
                    dashboardwidgets[key].splitby = $parsedHtml.data('splitby');
                    dashboardwidgets[key].content = '';
                });
                setWidgetCount();
                var WidgetsData = {
                    Id: dashboardId,
                    Widgets: JSON.stringify(dashboardwidgets),
                }
                $.ajax({
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: "./DynamicDashboardWebService.asmx/SaveWidgets",
                    dataType: 'json',
                    data: JSON.stringify(WidgetsData),
                    success: function (data) {

                    }
                });
                //localStorage.setItem('dashboard', JSON.stringify(dashboardwidgets));
            }
        }

        function resetWidgetSelection() {
            var firstMeasureby = $($(`#widgetModalsSidebar .accordion-button`)[0]);
            if (firstMeasureby.hasClass("collapsed")) {
                firstMeasureby.trigger('click');
            }
            var viewby = $(`.widget-viewby`);
            $(viewby).removeClass('active');
            $(viewby[0]).addClass('active').trigger('click');
            //$(`.chartpreviewer-type`).removeClass('active');
            //$(`.chartpreviewer-type[data-charttype="pie"]`).trigger('click');
        }

        function convertMmYyyyToDate(mmYyyyString) {
            var parts = mmYyyyString.split('-');
            if (parts.length === 2 && !isNaN(parts[0]) && !isNaN(parts[1])) {
                var month = parseInt(parts[0]) - 1;
                var year = parseInt(parts[1]);

                if (month >= 0 && month <= 11) {
                    return new Date(year, month);
                }
            }
            return null;
        }

        $(function () {
         
            $('.selectpicker').selectpicker();

            $("ul.kpi-nav-dropdown:not(:has(li))").remove();
            <% if (CurrentDashboard != null && CurrentDashboard.Module == "marketing_kpi")
        { %>
            $('#monthsFilterFrom').on('changeDate', function () {
                var selectedStartDate = convertMmYyyyToDate($('#monthsFilterFrom').val());
                var endDate = new Date(selectedStartDate);
                endDate.setMonth(selectedStartDate.getMonth() + 2);
                $('#monthsFilterTo').datepicker('setStartDate', selectedStartDate);
                $('#monthsFilterTo').datepicker('setEndDate', endDate);
                $("#monthsFilterTo").datepicker("setDate", selectedStartDate);

            });


            $("#monthsFilterFrom").datepicker({
                format: "mm-yyyy",
                viewMode: "months",
                minViewMode: "months"
            });
            $("#monthsFilterTo").datepicker({
                format: "mm-yyyy",
                viewMode: "months",
                minViewMode: "months"
            });
            $("#monthsFilterFrom").datepicker("setDate", new Date());
            $("#monthsFilterTo").datepicker("setDate", new Date());
            <% } %>

            <% if (CurrentDashboard != null){ %>

            $('.dashbaord-link[data-id="' + dashboardId + '"]').addClass('active');
            $('.dashbaord-module-link[data-name="' + dashboardModule + '"]').addClass('active');
            initWidegets(dashboardModule);
            resetWidgetSelection();


            var items = '<%= CurrentDashboard.Widgets %>';
            if (!items) {
                items = [];
            } else {
                items = JSON.parse(items);
            }
            var column = 12;
            var cellHeight = '';
            if (windowWidth < 700) {
                columns = 1;
                cellHeight = '100px';
            } else if (windowWidth < 850) {
                cellHeight = '100px';
                columns = 1;
            } else if (windowWidth < 950) {
                cellHeight = '100px';
                columns = 6;
            } else if (windowWidth < 1100) {
                cellHeight = '100px';
                columns = 6;
            } else {
                cellHeight = '110px';
                columns = 12;
            }


            $.each(items, function (key, item) {
                if (typeof item.splitby == 'undefined') {
                    item.splitby = "";
                }
                items[key].content = widgetUi(key,item);
                if (columns == 1) {
                    delete items[key].x;
                    delete items[key].y;
                    items[key].w = 12;
                } else if (columns == 6) {
                    delete items[key].x;
                    delete items[key].y;
                    if (items[key].w == 1) {
                        items[key].w = 3;
                    } else if (items[key].w == 1) {
                        items[key].w = 3;
                    } else if (items[key].w == 2) {
                        items[key].w = 3;
                    } else if (items[key].w == 3) {
                        items[key].w = 3;
                    } else if (items[key].w == 4) {
                        items[key].w = 3;
                    } else if (items[key].w == 5) {
                        items[key].w = 3;
                    } else if (items[key].w == 6) {
                        items[key].w = 3;
                    } else if (items[key].w == 7) {
                        items[key].w = 4;
                    } else if (items[key].w == 8) {
                        items[key].w = 4;
                    } else if (items[key].w == 9) {
                        items[key].w = 5;
                    } else if (items[key].w == 10) {
                        items[key].w = 5;
                    } else if (items[key].w == 11) {
                        items[key].w = 6;
                    } else if (items[key].w == 12) {
                        items[key].w = 6;
                    }
                }
            });
            grid = GridStack.init({ column: columns, cellHeight: cellHeight });
            oldItems = JSON.parse(JSON.stringify(items));
            grid.load(items);
            setWidgetCount();
            
           
            //$.each(items, function (key, item) {


            //    if (typeof item.widgetfilters != 'undefined') {
            //        $.each(item.widgetfilters, function (name, value) {
            //            var forminput = $(`.widget-filter-wrapper[data-id="widget-container-id-${key}"] .widget-form-filter[name="${name}"]`);
            //            forminput.data('value', value);
            //            if (forminput.is('[multiple]')) {
            //                value = value.split(',').map(function (item) {
            //                    return item.trim();
            //                });
            //            }
            //            forminput.val(value);

            //        });

            //    }
            //});
            
            $('.widget').each(function (index, element) {
                getFilterInputsUi($(this).data('key'), oldItems[$(this).data('key')]);

            });
            refreshWidgets();

            setChart('widgetPreviewer', 'pie');
        <% } %>
            $('.chartpreviewer-type').on('click', function () {
                $('.chartpreviewer-type').removeClass('active');
                $(this).addClass('active');
                var newChartType = $(this).data('charttype');
                setChart('widgetPreviewer', newChartType);
            });


            $('.widget-viewby').on('click', function () {
                $('#widgetForm [name="split_by"]').html('');
                $("#widgetForm .split-by-container").hide();
                var splitby = getSplitBy(dashboardModule, $(this).data('measureby'), $(this).data('viewby'));
                if (splitby !== null) {
                    $('#widgetForm [name="split_by"]').append('<option value="">None</option>');
                    $.each(splitby, function (key, split) {
                        $('#widgetForm [name="split_by"]').append(`<option value="${key}">${split.label}</option>`);
                    })
                    $("#widgetForm .split-by-container").show();
                    
                }
                var allowedCharts = getAllowedCharts(dashboardModule, $(this).data('measureby'), $(this).data('viewby'));
                var defaultChart = getDefaultChart(dashboardModule, $(this).data('measureby'), $(this).data('viewby'));

                var filteroptions = {
                    module: dashboardModule,
                    measureby: $(this).data('measureby'),
                    viewby: $(this).data('viewby'),
                    widgetfilters: {}
                }
                var widgetIndex = $('#widget_id').val()
                if (widgetIndex != '') {
                    $(`.widget-filter-wrapper[data-id="widget-container-id-${widgetIndex}"] .widget-form-filter:not(div)`).each(function (index, element) {

                        var value = $(this).data('value').toString();
                        if ($(this).is('[multiple]')) {
                            value = value.split(',').map(function (item) {
                                return item.trim();
                            });
                        }
                        filteroptions.widgetfilters[$(this).attr('name')] = value;
                    });
                }

                getFilterInputsUi(null, filteroptions);
                $('.chartpreviewer-type').removeClass('active');
                $('.chartpreviewer-type[data-charttype="' + defaultChart +'"]').addClass('active');
                $('.widget-viewby').removeClass('active');

                $('.chartpreviewer-type').addClass('d-none');
                
                if (allowedCharts !== null) {
                    $.each(allowedCharts, function (key, chart) {
                        $('.chartpreviewer-type[data-charttype="'+chart+'"]').removeClass('d-none');
                    })
                } else {
                    $('.chartpreviewer-type').removeClass('d-none');
                }

                
                $(this).addClass('active');
                if ($('#widget_id').val() == '') {
                    $('#widgetForm [name="widget_name"]').val(getDefaultWidgetName(dashboardModule, $(this).data('measureby'), $(this).data('viewby')));
                }
                setChart('widgetPreviewer',defaultChart);
            });

            $(document).on('click', '.remove-widget', function () {
                var $item = $(this).closest(".grid-stack-item");
                var index = $(".grid-stack .grid-stack-item").index($item);
                $("#removeWidget").data('widget-id', index);
            })
            $('#removeWidget').on('click', function () {
                $('#removeWidgetModal').modal("hide");
                var el = document.getElementsByClassName("grid-stack-item")[$(this).data('widget-id')];
                el.remove();
                grid.removeWidget(el, false);
                saveDashboard();
            })

            $('.grid-stack').on('change', function (event, items) {
                saveDashboard();
            });





            $(".alphanumeric-input").on("input", function () {
                $(this).parent().find('.text-danger').html('');
                var currentValue = $(this).val();
                var sanitizedValue = currentValue.replace(/[^a-zA-Z0-9 ]/g, "");
                $(this).val(sanitizedValue);
            });

            $("#generatePdf").click(function () {
                CreatePDFfromHTML()
            });
        });

        $('#editDashboardForm').validate({
            rules: {
                dashboard_name: {
                    required: true,
                    minlength: 3,
                    maxlength: 100
                },
            },
            messages: {
                dashboard_name: {
                    required: "Please enter dashbaord name",
                    minlength: "Dashbaord name must be at least 3 characters",
                    maxlength: "Widget name must be maximum 100 characters"
                },
            },
            submitHandler: function (form) {
                var formData = {
                    Id: $(`#editDashboardForm [name="dashboard_id"]`).val(),
                    Name: $(`#editDashboardForm [name="dashboard_name"]`).val(),
                    Module: $(`#editDashboardForm [name="dashboard_module"]`).val(),
                    Widgets: '',
                }
                $.ajax({
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: "./DynamicDashboardWebService.asmx/SaveDashboard",
                    dataType: 'json',
                    data: JSON.stringify({ FormData: formData }),
                    success: function (data) {
                        var dashboard = data.d;
                        if (dashboard.ValidationErrors && Object.keys(dashboard.ValidationErrors).length > 0) {
                            $('#editDashboardForm').validate().resetForm();
                            $('#editDashboardForm').validate().showErrors(dashboard.ValidationErrors);
                        } else {
                            window.location.reload();
                        }
                    }
                });
            }
        });

        $.validator.addMethod("checkWidgetName", function (value, element) {
            // Get all the existing widget names from the data-title attribute
            var existingWidgetNames = $(".widget").map(function (index) {
                if ($('#widget_id').val() != index) {
                    return $(this).data("title");
                }

            }).get();

            // Check if the value exists in the array of existing widget names
            return existingWidgetNames.indexOf(value) === -1;
        }, "Widget name already exists");

        var widgetForm = $('#widgetForm').validate({
            rules: {
                widget_name: {
                    required: true,
                    minlength: 3,
                    maxlength: 100,
                    checkWidgetName: true,
                },
            },
            messages: {
                widget_name: {
                    required: "Please enter widget name",
                    minlength: "Widget name must be at least 3 characters",
                    maxlength: "Widget name must be maximum 100 characters"
                },
            },
            submitHandler: function (form) {
                var title = $("#widget_name").val();
                var chart = $(".chartpreviewer-type.active").data('charttype');
                var measureby = $(".widget-viewby.active").data('measureby');
                var viewby = $(".widget-viewby.active").data('viewby');
                var splitby = $('#widgetForm [name="split_by"]').val();

                var module = dashboardModule;

                var key = $('.grid-stack-item .grid-stack-item-content').length;
                for (let i = 0; i <= 5; i++) {
                    if ($(`#widget-container-id-${i}`).length == 0) {
                        key = i;
                        break;
                    }
                }
                if ($('#widget_id').val() != '') {
                    key = $('#widget_id').val();
                }
                let options = {
                    title:title,
                    module:module,
                    measureby:measureby,
                    viewby:viewby,
                    splitby:splitby,
                    chart:chart,
                }
                var content = widgetUi(key, options);

                if ($('#widget_id').val() == '') {
                    grid.addWidget({ w: 6, h: 4, content: content });
                } else {
                    var el = document.getElementsByClassName("grid-stack-item")[$('#widget_id').val()];
                    $(el).find('.grid-stack-item-content').html(content);
                    //el.innerHTML = '<div class="grid-stack-item-content">' + content + '</div>';
                }
                options.widgetfilters = {};
                $('#widgetForm .widget-form-filter:not(div)').each(function (index, element) {
                    options.widgetfilters[$(this).attr('name')] = $(this).val();
                });
                var content = getFilterInputsUi(key, options);
                widgetLoadingCount = 1;
                setChart(`widget-container-id-${key}`, chart, { module: module, measureby: measureby, viewby: viewby,splitby:splitby });
                setWidgetCount();
                saveDashboard();
                $('#widgetModal').modal('hide');
                var divTop = $(`#widget-container-id-${key}`).offset().top;
                $('html, body').animate({ scrollTop: divTop }, 'fast');
            }
        });

        $('#openAddWidgetModal').on('click', function (e) {
            if ($(this).hasClass("disabled")) {
                e.preventDefault();
                return;
            }
            widgetForm.resetForm();
            $('#widgetModal #widget_name').val('');
            $('#widgetModal #widget_id').val('');
            $('#widgetModal #widgetModalLabel').html("Add Widget");
            $('#widgetModal #addWidget').html('Add');

            resetWidgetSelection();
            $('#widgetModal').modal('show');
        })

        $(document).on('click', '.edit-widget', function () {
            widgetForm.resetForm();
            var widgetElement = $(this).closest(".widget");
            var widgetIndex = $(".grid-stack .grid-stack-item").index($(this).closest(".grid-stack-item"));
            $('#widgetModal #widgetModalLabel').html("Edit Widget");
            $('#widgetModal #addWidget').html('Update');
            $('#widgetModal #widget_name').val(widgetElement.data('title'));
            $('#widgetModal #widget_id').val(widgetIndex);

            var measureby = widgetElement.data('measureby');
            var viewby = widgetElement.data('viewby');
            var chart = widgetElement.data('chart');
            var splitby = widgetElement.data('splitby');
            $(`#widgetModalsSidebar .accordion-button.collapsed[data-bs-target="#accordion-${measureby}"]`).trigger('click');
            $(`.widget-viewby`).removeClass('active');
            $(`.widget-viewby[data-measureby="${measureby}"][data-viewby="${viewby}"]`).addClass('active').trigger('click');
            $(`#widgetForm [name="split_by"]`).val(splitby).trigger('click');
            $(`.chartpreviewer-type`).removeClass('active');
            $(`.chartpreviewer-type[data-charttype="${chart}"]`).trigger('click');
        });
        $('#deleteDashboard').on('click', function () {
            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                url: "./DynamicDashboardWebService.asmx/DeleteDashboard",
                dataType: 'json',
                data: JSON.stringify({Id:dashboardId}),
                success: function (data) {
                    var currentUrl = window.location.href;
                    var newUrl = currentUrl.split('?')[0];
                    window.location.href = newUrl;
                }
            });
        });

        $('#deleteDashboard').on('click', function () {
            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                url: "./DynamicDashboardWebService.asmx/DeleteDashboard",
                dataType: 'json',
                data: JSON.stringify({Id:dashboardId}),
                success: function (data) {
                    var currentUrl = window.location.href;
                    var newUrl = currentUrl.split('?')[0];
                    window.location.href = newUrl;
                }
            });
        });
        $('#applyFilter').on('click', function (e) {
            e.preventDefault();
            currentSfcode = $('#' + '<%= SalesForceList.ClientID %>').val();
            refreshWidgets();
        })

        $(document).on('click', '.applyWidgetFilters', function () {
            var currwidget = $(this).closest('.widget');
            widgetLoadingCount = 1;
            setChart($(this).closest('.widget-filter-wrapper').data('id'), currwidget.data('chart'), { module: currwidget.data('module'), measureby: currwidget.data('measureby'), viewby: currwidget.data('viewby'), splitby: currwidget.data('splitby') });
        })
        $(document).on('click', '.closewigetFilter', function () {
            $(this).closest('.widget-filter-wrapper').addClass('d-none');
        })

        $(document).on('click', '.openwidgetfilter', function () {
            $(this).next('.widget-filter-wrapper').toggleClass('d-none');
        })

        $('#widgetForm [name="split_by"]').on('click', function () {
            
            var allowedCharts = getAllowedCharts(dashboardModule, $('#widgetModalsSidebar .widget-viewby.active').data('measureby'), $('#widgetModalsSidebar .widget-viewby.active').data('viewby'));
            $('.chartpreviewer-type').addClass('d-none');
            if ($(this).val() =="") {
                $.each(allowedCharts, function (key, chart) {
                    $('.chartpreviewer-type[data-charttype="' + chart + '"]').removeClass('d-none');
                })
            } else {
                $.each(allowedCharts, function (key, chart) {
                    if (chart == "pie" || chart == "donut" || chart == "funnel") {
                        return true;
                    }
                    $('.chartpreviewer-type[data-charttype="' + chart + '"]').removeClass('d-none');
                })
                var $chartElements = $('.chartpreviewer-type.active:not(.d-none)');

                if ($chartElements.length === 0) {
                    $('.chartpreviewer-type:not(.d-none):first').click();
                }
            }
        })

    </script>
</asp:Content>

