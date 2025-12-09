Apex.colors = [
    "#008FFB", "#00E396", "#FEB019", "#FF4560", "#775DD0",
    '#FF5733',
    '#33FF57',
    '#5733FF',
    '#FF3366',
    '#33FF66',
    '#6633FF',
    '#FF33CC',
    '#33FFCC',
    '#CC33FF',
    '#FF6699',
    '#99FF66',
    '#9966FF',
    '#FF3399',
    '#33FF99',
    '#9933FF',
    '#FF66CC',
    '#CCFF66',
    '#CC66FF',
    '#FF33FF',
    '#33FF33',
    '#FFCC33',
    '#FF99CC',
    '#CC99FF',
    '#FF66FF',
    '#99FF33',
    '#FFCC66',
    '#FFCC99',
    '#66CCFF',
    '#CCFF99',
    '#99CCFF',
    '#FF99FF',
    '#FFCCCC',
    '#CCCCCC',
    '#FF6600',
    '#00CCFF',
    '#FF9933',
    '#CC00FF',
    '#33CCFF',
    '#FF3300',
    '#00FFCC',
    '#3300FF',
    '#FF9900',
    '#00FF99',
    '#9900FF',
    '#FF0000',
    '#00FF00',
    '#0000FF',
    '#FFFF00',
    '#00FFFF',
    '#FF00FF',
    '#990000',
    '#009900',
    '#000099',
    '#999900',
    '#009999',
    '#990099',
    '#663300',
    '#336600',
    '#003366',
    '#660033',
    '#336633',
    '#633300',
    '#CC9900',
    '#00CC99',
    '#9900CC',
    '#CC0099',
    '#99CC00',
    '#0099CC',
    '#996600',
    '#669900',
    '#009966',
    '#660099',
    '#996633',
    '#339966',
    '#669933',
    '#339999',
    '#993399',
    '#999933',
    '#339933',
    '#993366',
    '#339933',
    '#993366',
    '#339933',
    '#993366',
    '#333333',
    '#666666',
    '#999999',
];




// You can use this 'colors' array in your ApexCharts configuration.


var AppliedCharts = {

}
var fetchedData = {

}
var chartClickCount = {};

function allValuesAreZero(array) {
    return array.every(value => value === 0);
}
function seriresHasOnlyZero(data) {
    return data.every(series => allValuesAreZero(series.data));
}
function callChart(id, type, params, data) {
    if (data.series.length == 0 || seriresHasOnlyZero(data.series)) {
        $(`#${id}`).html(`<div class="h-100 d-flex justify-content-center align-items-center"><div class="text-center"><p><i class="fa-solid fa-circle-notch fa-2xl"></i></p><p>No data found</p></div></div>`);
        return;
    }
    switch (type) {
        case 'pie':
            var options = setPieChart(data, params);
            break;
        case 'donut':
            var options = setDonutChart(data, params);
            break;
        case 'column':
            let stacked = true;
            var options = setColumnChart(data, params, stacked);
            break;
        case 'linecolumn':
            if (typeof params == 'undefined') {
                data.series= [
                    {
                        name: "Count1",
                        data: [1380, 1100, 990, 880, 740, 548, 330, 200],
                    },
                    {
                        name: "Count 2",
                        data: [1380, 110, 690, 880, 540, 248, 30, 130],
                    },
                    {
                        name: "Percentage",
                        data: [100, 10, 70, 100, 80, 50, 10, 100],
                    }
                ]
            }
            var options = setLineColumnChart(data, params)
            break;
        case 'line':
            var options = setLineChart(data, params);
            break;
        case 'area':
            var options = setAreaChart(data, params);
            break;
        case 'funnel':
            // Your data object
            // Your data object
            var dataObject = data

            // Get the index order based on the sorted data in descending order
            var indexOrder = dataObject.series[0].data.map(function (_, index) {
                return index;
            }).sort(function (a, b) {
                return dataObject.series[0].data[b] - dataObject.series[0].data[a];
            });

            // Sort the series.data array in descending order
            dataObject.series[0].data = indexOrder.map(function (index) {
                return dataObject.series[0].data[index];
            });

            // Sort the corresponding values in labels, labelIds, and fulllabelIds
            dataObject.labels = indexOrder.map(function (index) {
                return dataObject.labels[index];
            });
            dataObject.labelIds = indexOrder.map(function (index) {
                return dataObject.labelIds[index];
            });
            dataObject.fulllabelIds = indexOrder.map(function (index) {
                return dataObject.fulllabelIds[index];
            });
            var options = setFunnelChart(data,params);
            break;
        case 'forcetable':
        case 'table':

            
            if (typeof params != 'undefined') {
                var tablehtml = '<div class="table-responsive"><table class="table table-bordered table-responsive mt-3" data-module="' + params.module + '" data-measureby="' + params.measureby + '" data-viewby="' + params.viewby + '" data-splitby="' + params.splitby + '">';
             //   var title = getViewbyName(params.module, params.measureby, params.viewby);
                var title = data.view_by_title;
            } else {
                var tablehtml = '<div class="table-responsive"><table class="table table-bordered table-responsive mt-3">';
                title = "Widget Name";
            }
            tablehtml += '<thead>';
            tablehtml += '<tr>';
            if (typeof params != 'undefined' && typeof params.splitby != 'undefined' && params.splitby) {
                tablehtml += '<th rowspan="2">';
            } else {
                tablehtml += '<th>';
            }
            
            tablehtml += title;
            tablehtml += '</th>';
            if (typeof params != 'undefined' && typeof params.splitby != 'undefined' && params.splitby) {   
                tablehtml += '<th colspan="' + data.series.length + '" style="text-align: center;">';
              //  tablehtml += getSplitbyName(params.module, params.measureby, params.viewby, params.splitby);
                tablehtml += data.measure_by_title;
                tablehtml += '</th>';
                tablehtml += '</tr>';
                tablehtml += '<tr>';
            }
            $.each(data.series, function (index, seriesItem) {
                tablehtml += '<th>';
                tablehtml += seriesItem.name;
                tablehtml += '</th>';
            });
            
            tablehtml += '</tr>';
            tablehtml += '</thead>';

            tablehtml += '<tbody>';
            $.each(data.labels, function (lableid, label) {
                tablehtml += '<tr>';
                tablehtml += '<td>';
                tablehtml += label;
                tablehtml += '</td>';
                $.each(data.series, function (index, seriesItem) {
                    tablehtml += '<td>';
                    if (typeof params != 'undefined') {
                        tablehtml += '<a class="table-drilldown-link" data-viewquery="' + data.labelIds[lableid] + '" data-splitquery="' + data.seriesIds[index] + '" data-viewbyname="' + label + '" data-splitbyname="' + seriesItem.name + '"  href="javascript:void(0)">' + seriesItem.data[lableid] + '</a>';
                    } else {
                        tablehtml += seriesItem.data[lableid];
                    }
                    
                    tablehtml += '</td>';
                });
                
                tablehtml += '</tr>';
            });
            tablehtml += '<tbody>';

            tablehtml += '</table></div>';
            if (typeof AppliedCharts[id] != 'undefined') {
                AppliedCharts[id].destroy();
            }
            $(`#${id}`).html(tablehtml);
            break;
        case 'score':
            
            $(`#${id}`).html(SetScoreCard(data, params));
            $('[data-bs-toggle="tooltip"]').tooltip();
            break;
        default:
            var options = setPieChart(data,params);
            break;
    }

    if (type != 'table' && type != 'forcetable' && type !='score') {
        if (typeof AppliedCharts[id] != 'undefined') {
            AppliedCharts[id].destroy();
        }
        $(`#${id}`).html('');

        if (typeof params != 'undefined') {
            fetchedData[id] = data;
            var viewbyTitle = $(`#${id}`).closest('.widget').find('.widget-filter-view-by-title');
            viewbyTitle.html(data.view_by_title);
            var viewbyWrapper = $(`#${id}`).closest('.widget').find('.widget-filter-view-by-wrapper');
            let labels = [];
            if (typeof options.labels != 'undefined') {
                labels = options.labels
            } else {
                labels = options.xaxis.categories;
            }
            viewbyWrapper.html("");
            $.each(labels, function (index, label) {
                viewbyWrapper.append(`
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" value="${index}" id="${id}-showed-view-by-${index}" name="showed-view-by" checked>
                    <label class="form-check-label" for="${id}-showed-view-by-${index}">${label}</label>
                </div>`)
            });
        }

        AppliedCharts[id] = new ApexCharts(document.querySelector(`#${id}`), options);
        AppliedCharts[id].render();
        
    }
    if (type == 'forcetable') {
        $(`#${id} .table-responsive`).hide();
        $(`#${id}`).append(`<div class="h-100 w-100 d-flex justify-content-center align-items-center show-table-view-dialog" style="
    position: absolute;top:0px"><div class="text-center"><p><i class="fa-solid fa-circle-notch fa-2xl"></i></p><p>There is huge data to show chart view.</p><a class="btn btn-primary show-table-view">Show table view</a></div></div>`);
    }
}

function handleDataPointClick(chartId, module, measureby, viewby, splitby, viewquery, viewbyname = '',splitquery='',splitbyname='') {
    //if (module != 'master_kpi') {
    //    return;
    //}
    if (typeof chartClickCount[chartId] == 'undefined') {
        chartClickCount[chartId] = 0;
    }
    chartClickCount[chartId]++;
    if (chartClickCount[chartId] === 1) {
        timeout = setTimeout(function () {
            chartClickCount[chartId] = 0;
        }, 300);
    } else if (chartClickCount[chartId] === 2) {
        if (module == 'master_kpi') {
            let sfcode = currentSfcode;
            let sfname = $(sfcodeDropdownSelector + ' option[value="' + sfcode + '"]').html();
            let queryString = `?sfcode=${sfcode}&sfname=${sfname}&module=${module}&measureby=${measureby}&viewby=${viewby}&splitby=${splitby}&viewquery=${viewquery}&splitquery=${splitquery}&viewbyname=${viewbyname}&splitbyname=${splitbyname}`;
            clearTimeout(timeout);
            chartClickCount[chartId] = 0;

            window.open(drillDownLinkMaster(module, measureby, queryString));
        } else {
            clearTimeout(timeout);
            chartClickCount[chartId] = 0;

            var FormData = {
                dashboardFilters: {
                    sfcode: currentSfcode,
                    module: module,
                },
                widgetFilters: {
                    viewquery: viewquery,
                    splitquery: splitquery,
                    measureby: measureby,
                    viewby: viewby,
                    splitby: splitby,
                },
            }
            $('.dashboard-filter').each(function (index, element) {
                FormData.dashboardFilters[$(this).attr('name')] = $(this).val();
            });
            $('#apexcharts' + chartId).closest('.widget').find(`.widget-filter-wrapper .widget-form-filter:not(div)`).each(function (index, element) {
                var value = $(this).val();
                if (Array.isArray(value)) {
                    var value = value.join(',');
                }
                FormData.widgetFilters[$(this).attr('name')] = value;
            });
            window.open(drillDownLink(module, measureby, FormData));
        }

    }
}

function drillDownLink(module, measureby, FormData) {
    if (module == 'master_kpi') {
        if (measureby == 'listed_doctor') {
            return 'DrillDown/ListedDoctors.aspx' + $.param(FormData);
        } else if (measureby == 'un_listed_doctor') {
            return 'DrillDown/UnListedDoctors.aspx' + $.param(FormData);
        } else if (measureby == 'chemist') {
            return 'DrillDown/Chemist.aspx' + $.param(FormData);
        } else if (measureby == 'stockist') {
            return 'DrillDown/Stockist.aspx' + $.param(FormData);
        } else if (measureby == 'fieldforce') {
            return 'DrillDown/FieldForces.aspx' + $.param(FormData);
        } else if (measureby == 'product') {
            return 'DrillDown/Products.aspx' + $.param(FormData);
        } else if (measureby == 'holiday') {
            return 'DrillDown/Holidays.aspx' + $.param(FormData);
        }
    }
    else if(module =='marketing_kpi')
        return 'DrillDown/Marketing.aspx?' + $.param(FormData);
    else if (module == 'sales_kpi')
        return 'DrillDown/Sales.aspx?' + $.param(FormData);
}
function drillDownLinkMaster(module, measureby, queryString) {
    if (module == 'master_kpi') {
        if (measureby == 'listed_doctor') {
            return 'DrillDown/ListedDoctors.aspx' + queryString;
        } else if (measureby == 'un_listed_doctor') {
            return 'DrillDown/UnListedDoctors.aspx' + queryString;
        } else if (measureby == 'chemist') {
            return 'DrillDown/Chemist.aspx' + queryString;
        } else if (measureby == 'stockist') {
            return 'DrillDown/Stockist.aspx' + queryString;
        } else if (measureby == 'fieldforce') {
            return 'DrillDown/FieldForces.aspx' + queryString;
        } else if (measureby == 'product') {
            return 'DrillDown/Products.aspx' + queryString;
        } else if (measureby == 'holiday') {
            return 'DrillDown/Holidays.aspx' + queryString;
        }
    }
}
function setChart(id, type, params) {
    if (typeof params != 'undefined') {

        //showLoader();
        $('#DashboardFilter .btn').addClass("disabled");
        $('.page-loader').show();
        $('#' + id).closest('.widget').find('.lds-ring-container').parent().show();
        var data = {
            id: id,
            series: [],
            labels: [],
            seriesIds: [],
            labelsIds: [],
            title:'',
            measure_by_title: 'Measure By',
            view_by_title: 'View By',
        }
        var FormData = {
            Module: params.module,
            MeasureBy: params.measureby,
            ViewBy: params.viewby,
            SplitBy: params.splitby,
            SFCode: currentSfcode,
            Filters: {},
            WidgetFiters: {},
        }
        $('.dashboard-filter').each(function (index, element) {
            FormData.Filters[$(this).attr('name')] = $(this).val();
        });
        var allAppliedFiltersText = '';
        $(`.widget-filter-wrapper[data-id="${id}"] .widget-form-filter:not(div)`).each(function (index, element) {
            var value = $(this).val();
            if(Array.isArray(value)) {
                var value = value.join(',');
            }
            FormData.WidgetFiters[$(this).attr('name')] = value;

            if (typeof params != 'undefined') {
                if (value == "") {
                    var appliedFilterText = $(this).data('none-selected-text');
                } else {
                    var selectedLabels = $(this).find(`option:selected`).map(function () {
                        return $(this).text();
                    }).get();

                    var appliedFilterText = selectedLabels.join(', ');
                }
                allAppliedFiltersText += getWidgetFilterLabel(params.module, params.measureby, params.viewby, $(this).attr('name'))+": "+appliedFilterText + " | ";
                
                
            }
            if (index+1 == $(`.widget-filter-wrapper[data-id="${id}"] .widget-form-filter:not(div)`).length) {
                $(this).closest('.widget').find('.applied-filter-text').html('<span data-bs-toggle="tooltip" data-bs-placement="top" title="' + allAppliedFiltersText + '">' + allAppliedFiltersText + '</span>');
                $('[data-bs-toggle="tooltip"]').tooltip();
            }
            
        });
        
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: "./DynamicDashboardWebService.asmx/GetWidgetData",
            dataType: 'json',
            data: JSON.stringify({ Data: FormData }),
            success: function (response) {
                var result = response.d;
                if (result) {
                    
                    if (result.Series != null && result.Series.length > 0) {
                        data.labels = result.Labels;
                        data.labelIds = result.LabelIds;
                        data.seriesIds = result.SeriesIds;
                        data.fulllabelIds = result.LabelIds;
                        data.measure_by_title = result.MeasureByTitle;
                        data.view_by_title = result.ViewByTitle;
                        $.each(result.Series, function (index, seriesItem) {
                            // Assuming each seriesItem has properties "name" and "data"
                            var series = {
                                name: seriesItem.name,
                                data: seriesItem.data,
                            };
                            data.series.push(series);
                        });
                        if (data.labels.length > 30) {  
                            type = 'forcetable';
                        }
                        callChart(id, type, params, data);
                    } else {
                        $(`#${id}`).html(`<div class="h-100 d-flex justify-content-center align-items-center"><div class="text-center"><p><i class="fa-solid fa-circle-notch fa-2xl"></i></p><p>No data found</p></div></div>`);
                    }
                    
                }
                $('#' + id).closest('.widget').find('.lds-ring-container').parent().hide();
                widgetLoadingCount--;
                if (widgetLoadingCount == 0) {
                    //hideLoader();
                    $('#DashboardFilter .btn').removeClass("disabled");
                    $('.page-loader').hide();
                }
                
                
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#' + id).html(`<div class="h-100 d-flex justify-content-center align-items-center"><div class="text-center"><p><i class="fa-solid fa-exclamation-triangle fa-2xl"></i></p><p>An error occurred while loading data</p></div></div>`);

                $('#' + id).closest('.widget').find('.lds-ring-container').parent().hide();
                widgetLoadingCount--;
                if (widgetLoadingCount == 0) {
                    //hideLoader();
                    $('#DashboardFilter .btn').removeClass("disabled");
                    $('.page-loader').hide();
                }
            }
        });
    }else{
        data = {
            series: [
                {
                    name: "Count",
                    data: [1380, 1100, 990, 880, 740, 548, 330, 200],
                }
            ],
            labels: ['Sourced', 'Screened', 'Assessed', 'HR Interview', 'Technical', 'Verify', 'Offered', 'Hired',],
            title:''
        }
        callChart(id, type, params, data);
    }
}


function setPieChart(data, params) {
    events = {}
    if (typeof params !== 'undefined') {
        events = {
            dataPointSelection: function (event, chartContext, config) {
                event.stopPropagation();
                handleDataPointClick(config.w.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.w.globals.labels[config.dataPointIndex])
            }
        }
    }
    var options = {
        series: data.series[0].data,
        chart: {
            type: 'pie',
            events: events
        },
        labels: data.labels,
        legend: {
            position: 'bottom'
        },
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                },
                legend: {
                    position: 'bottom'
                }
            }
        }],
    };
    return options;
}

function setDonutChart(data,params) {
    events = {}
    if (typeof params !== 'undefined') {
        
        events = {
            dataPointSelection: function (event, chartContext, config) {
                event.stopPropagation();
                handleDataPointClick(config.w.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.w.globals.labels[config.dataPointIndex])
            }
        }
    }
    var options = {
        series: data.series[0].data,
        chart: {
            type: 'donut',
            events: events,
        },
        labels: data.labels,
        legend: {
            position: 'bottom'
        },
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };
    return options;
}

function setLineColumnChart(data, params) {
    events = {}
    if (typeof params !== 'undefined') {

        events = {
            dataPointSelection: function (event, chartContext, config) {
                event.stopPropagation();
                if (config.seriesIndex >= 0 && config.dataPointIndex >= 0) {
                    handleDataPointClick(config.w.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.w.globals.labels[config.dataPointIndex], fetchedData[data.id].seriesIds[config.seriesIndex], config.w.globals.seriesNames[config.seriesIndex])
                }
            }
        }
    }
    var options = {};
    if (dashboardModule == 'marketing_kpi') {
        options=MarketingKpiLineColumnChartOptions(data, params);
    }
    else if (dashboardModule == 'sales_kpi') {
        options = SalesKpiLineColumnChartOptions(data, params);
    }
    return options;
}

function setColumnChart(data,params,stacked=true) {
    events = {}
    if (typeof params !== 'undefined') {
        
        events = {
            dataPointSelection: function (event, chartContext, config) {
                event.stopPropagation();
                if (config.seriesIndex >= 0 && config.dataPointIndex >= 0) {
                    handleDataPointClick(config.w.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.w.globals.labels[config.dataPointIndex], fetchedData[data.id].seriesIds[config.seriesIndex], config.w.globals.seriesNames[config.seriesIndex])
                }
            }
        }
    }
    var options = {
        tooltip: {
            shared: true,
            intersect:false,
        },
        series: data.series,
        chart: {
            stacked: stacked,
            type: 'bar',
            toolbar: {
                show: false,
            },
            events: events,
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '55%',
                endingShape: 'rounded'
            },
        },
        dataLabels: {
            enabled: true,
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: data.labels,
            title: {
                text: data.view_by_title
            },
        },
        yaxis: {
            title: {
                text: data.measure_by_title
            },
        },
        fill: {
            opacity: 1
        },
    };

    return options;
}

function setLineChart(data, params) {
    events = {}
    if (typeof params !== 'undefined') {

        events = {
            click: function (event, chartContext, config) {
                event.stopPropagation();
                if (config.seriesIndex >= 0 && config.dataPointIndex >= 0) {
                    handleDataPointClick(config.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.globals.categoryLabels[config.dataPointIndex], fetchedData[data.id].seriesIds[config.seriesIndex], config.globals.seriesNames[config.seriesIndex])
                }
            }
        }
    }
    var options = {
        series: data.series,
        chart: {
            type: 'line',
            zoom: {
                enabled: false
            },
            toolbar: {
                show: false,
            },
            events: events,
        },
        dataLabels: {
            enabled: true,
        },
        stroke: {
            curve: 'smooth'
        },
        xaxis: {
            categories: data.labels,
            title: {
                text: data.view_by_title
            },
        },
        yaxis: {
            title: {
                text: data.measure_by_title
            },
        },
    };

    return options
}

function setAreaChart(data,params) {
    events = {}
    if (typeof params !== 'undefined') {

        events = {
            click: function (event, chartContext, config) {
                event.stopPropagation();
                if (config.seriesIndex >= 0 && config.dataPointIndex >= 0) {
                    handleDataPointClick(config.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.globals.categoryLabels[config.dataPointIndex], fetchedData[data.id].seriesIds[config.seriesIndex], config.globals.seriesNames[config.seriesIndex])
                }
            }
        }
    }
    var options = {
        series: data.series,
        chart: {
            type: 'area',
            zoom: {
                enabled: false
            },
            toolbar: {
                show: false,
            },
            events: events,
        },
        dataLabels: {
            enabled: true
        },
        stroke: {
            curve: 'smooth'
        },
        labels: data.labels,
        xaxis: {
            title: {
                text: data.view_by_title
            },
        },
        yaxis: {
            title: {
                text: data.measure_by_title
            },
        },
        legend: {
            horizontalAlign: 'left'
        },

    };

    return options
}

function setFunnelChart(data, params) {
    events = {}
    if (typeof params !== 'undefined') {
        
        events = {
            dataPointSelection: function (event, chartContext, config) {
                event.stopPropagation();
                if (config.seriesIndex >= 0 && config.dataPointIndex >= 0) {
                    handleDataPointClick(config.w.globals.chartID, params.module, params.measureby, params.viewby, params.splitby, fetchedData[data.id].labelIds[config.dataPointIndex], config.w.globals.labels[config.dataPointIndex], fetchedData[data.id].seriesIds[config.seriesIndex], config.w.globals.seriesNames[config.seriesIndex])
                }
            }
        }
    }

    var options = {
        series: data.series,
        chart: {
            type: 'bar',
            toolbar: {
                show: false,
            },
            events: events,
        },
        plotOptions: {
            bar: {
                borderRadius: 0,
                horizontal: true,
                barHeight: '80%',
                isFunnel: true,
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val, opt) {
                return opt.w.globals.labels[opt.dataPointIndex] + ':  ' + val
            },
            dropShadow: {
                enabled: true,
            },
        },
        xaxis: {
            categories: data.labels,
        },
        legend: {
            show: false,
        },
    };

    return options;
}

function updateChartData(ele) {
    let widgetId = ele.data('id');
    let data = fetchedData[widgetId];
    let labels = [];
    let series = [];
    let showZeros = false;

    fetchedData[widgetId].labelIds = [];
    if (ele.find('input[name="widget-filter-show-zero"]').prop('checked')) {
        showZeros = true;

    }
    ele.find('input[name="showed-view-by"]:checked').each(function (showedIndex) {
        showViewById = $(this).val();
        labels.push(data.labels[showViewById]);
        fetchedData[widgetId].labelIds.push(fetchedData[widgetId].fulllabelIds[showViewById]);
        let hasValue = false;
        $.each(data.series, function (index, seriesItem) {
            if (showedIndex == 0) {
                series.push({
                    name: seriesItem.name,
                    data: [],
                });
            }
            if (seriesItem.data[showViewById] !=0) {
                hasValue = true;
            }
            series[index].data.push(seriesItem.data[showViewById])
        });
        if (showZeros == false && hasValue == false) {
            labels.pop();
            fetchedData[widgetId].labelIds.pop();
            $.each(data.series, function (index, seriesItem) {
                series[index].data.pop();
            });
        }
        
    });
    let options = {};
    let charttype = ele.closest('.widget').data('chart');
    switch (charttype) {
        case "pie":
        case "donut":
            options = {
                series: series[0].data,
                labels: labels,
            }
            break;
        default:
            options = {
                series: series,
                xaxis: {
                    categories: labels,
                },
            }
            break;
    }
    AppliedCharts[widgetId].updateOptions(options);
}

$(function () {
    $(document).on("click", '[name="showed-view-by"]', function () {
        updateChartData($(this).closest('.widget-filter-wrapper'));
    })
    $(document).on("click", '[name="widget-filter-show-zero"]', function () {
        updateChartData($(this).closest('.widget-filter-wrapper'));
    })

    $(document).on("click", '.table-drilldown-link', function () {
        let module = $(this).closest('table').data("module");
        if (module == 'master_kpi') {
            let sfcode = currentSfcode;
            let sfname = $(sfcodeDropdownSelector + ' option[value="' + sfcode + '"]').html();

            let viewquery = $(this).data("viewquery");
            let splitquery = $(this).data("splitquery");
            let viewbyname = $(this).data("viewbyname");
            let splitbyname = $(this).data("splitbyname");
            
            let measureby = $(this).closest('table').data("measureby");
            let viewby = $(this).closest('table').data("viewby");
            let splitby = $(this).closest('table').data("splitby");

            let queryString = `?sfcode=${sfcode}&sfname=${sfname}&module=${module}&measureby=${measureby}&viewby=${viewby}&splitby=${splitby}&viewquery=${viewquery}&splitquery=${splitquery}&viewbyname=${viewbyname}&splitbyname=${splitbyname}`;

            window.open(drillDownLinkMaster(module, measureby, queryString));
        } else {

            var FormData = {
                dashboardFilters: {
                    sfcode: currentSfcode,
                    module: $(this).closest('table').data("module"),
                },
                widgetFilters: {
                    viewquery: $(this).data("viewquery"),
                    splitquery: $(this).data("splitquery"),
                    measureby: $(this).closest('table').data("measureby"),
                    viewby: $(this).closest('table').data("viewby"),
                    splitby: $(this).closest('table').data("splitby"),
                },
            }
            $('.dashboard-filter').each(function (index, element) {
                FormData.dashboardFilters[$(this).attr('name')] = $(this).val();
            });
            //$(`.widget-filter-wrapper[data-id="${id}"] .widget-form-filter:not(div)`).each(function (index, element) {
            //    var value = $(this).val();
            //    if (Array.isArray(value)) {
            //        var value = value.join(',');
            //    }
            //    FormData.widgetFilters[$(this).attr('name')] = value;
            //});
            window.open(drillDownLink($(this).closest('table').data("module"), $(this).closest('table').data("measureby"), FormData));
        }
    })
    $(document).on("click", '.show-table-view', function () {
        $(this).closest('.widget-container').find('.table-responsive').show();
        $(this).closest('.widget-container').find('.show-table-view-dialog').remove();
    })

});