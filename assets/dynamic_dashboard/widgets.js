function initWidgetSidebar(module){
    $("#widgetModalsSidebar").html('<div class="accordion" id="accordionExample"></div>');
    var kpi = widgets[module];
    $.each(kpi, function(measurebyKey, measureby) {
        var acccordionhtml =`<div class="accordion-item">
        <h2 class="accordion-header" id="headingOne">
            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#accordion-${measurebyKey}"
                aria-expanded="false" aria-controls="accordion-${measurebyKey}">
                ${measureby.label}
            </button>
        </h2>
        <div id="accordion-${measurebyKey}" class="accordion-collapse collapse" aria-labelledby="headingOne"
            data-bs-parent="#accordionExample">
            <div class="accordion-body"><ul class="list-unstyled">`;
        $.each(measureby.views, function(viewbykey, viewby) {
            acccordionhtml +=`<li><a class="dropdown-item widget-viewby" style="cursor:pointer" data-measureby="${measurebyKey}" data-viewby="${viewbykey}">${viewby.label}</a></li>`;
        })
        acccordionhtml +=`</ul></div>
        </div>
    </div>`;
    $("#widgetModalsSidebar .accordion").append(acccordionhtml);
    })
}
function initWidegets(module){
    initWidgetSidebar(module);
}

function getMeasurebyName(module,measureby){
    return widgets[module][measureby].label;
}

function getViewbyName(module,measureby,viewby){
    return widgets[module][measureby].views[viewby].label;
}
function getSplitbyName(module, measureby, viewby,splitby) {
    return widgets[module][measureby].views[viewby].split_by[splitby].label;
}

function getSplitBy(module, measureby, viewby) {
    if (typeof widgets[module][measureby].views[viewby].split_by != 'undefined') {
        return widgets[module][measureby].views[viewby].split_by;
    } else {
        return null;
    }
    
}

function getFilters(module, measureby, viewby) {
    if (typeof widgets[module][measureby].views[viewby].filters != 'undefined') {
        return widgets[module][measureby].views[viewby].filters;
    } else {
        return null;
    }

}
function getAllowedCharts(module, measureby, viewby) {
    if (typeof widgets[module][measureby].views[viewby].allowed_charts != 'undefined') {
        return widgets[module][measureby].views[viewby].allowed_charts;
    } else {
        return null;
    }

}
function getDefaultChart(module, measureby, viewby) {
    if (typeof widgets[module][measureby].views[viewby].default_chart != 'undefined') {
        return widgets[module][measureby].views[viewby].default_chart;
    } else {
        return 'pie';
    }

}

function getDefaultWidgetName(module, measureby, viewby) {
    if (typeof widgets[module][measureby].views[viewby].widgetName != 'undefined') {
        return widgets[module][measureby].views[viewby].widgetName;
    } else {
        return '';
    }

}

function getWidgetFilterLabel(module, measureby, viewby,filter) {
    if (typeof widgets[module][measureby].views[viewby].filters[filter].label != 'undefined') {
        return widgets[module][measureby].views[viewby].filters[filter].label;
    } else {
        return '';
    }

}


var widgetLoadingCount = 0;
function refreshWidgets() {
    widgetLoadingCount = $('.widget-container').length;
    $('.widget-container').each(function (index, element) {
        const widgetcontainer = $(element);
        const widget = widgetcontainer.closest('.widget');
        setChart(widgetcontainer.attr('id'), widget.data('chart'), { module: widget.data('module'), measureby: widget.data('measureby'), viewby: widget.data('viewby'), splitby: widget.data('splitby') });
    });
}

function widgetUi(key, data) {
    filterinputs = '<div class="widget-custom-filter-wrapper"></div>';
    return `<div class="card h-100 border-0 widget" data-title="${data.title}" data-module="${data.module}" data-measureby="${data.measureby}" data-viewby="${data.viewby}" data-splitby="${data.splitby}" data-chart="${data.chart}" data-key="${key}">
    <div style="position:absolute;width:100%;height:100%;z-index:999;display:none">
        <div class="lds-ring-container" >
        <div class="lds-ring"><div></div><div></div><div></div><div></div></div>
        </div>
    </div>
    <div class="card-body"><div class="widget-toolbar d-flex justify-content-between">
                    <p class="fw-bold mb-0 text-singleline" data-bs-toggle="tooltip" data-bs-placement="top" title="${data.title}">${data.title}</p>
                    <div class="widget-toolbar-options">
                        <div class="d-flex">
                            <div class="me-2 widget-filter-option">
                                <a class="text-muted openwidgetfilter" type="button" ><i class="fa-solid fa-filter"></i></a>
                                <div class="card widget-filter-wrapper pt-0 d-none" data-id="widget-container-id-${key}"> 
                                        <div class="card-header">
                                            Filters
                                            <a href="javascript:void(0)" class="closewigetFilter float-end text-dark">
                                                <i class="fa-solid fa-xmark"></i>
                                            </a>
                                        </div>
                                        <div class="card-body">
                                            ${filterinputs}
                                            <div class="form-check form-switch mb-3">
                                              <input class="form-check-input" name="widget-filter-show-zero" id="widget-container-id-${key}-widget-filter-show-zero" type="checkbox" checked>
                                              <label class="form-check-label" for="widget-container-id-${key}-widget-filter-show-zero" >Show zero values</label>
                                            </div>
                                            <label class="form-label widget-filter-view-by-title">`+ getViewbyName(data.module, data.measureby, data.viewby)+`</label>
                                            <div class="widget-filter-view-by-wrapper">
                                            </div>
                                        </div>
                                </div>

                            </div>
                            <div class="dropdown">
                                <a class="text-muted dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false"><i class="fa-solid fa-gear"></i></a>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1"><li><a class="dropdown-item edit-widget" href="#" data-bs-toggle="modal" data-bs-target="#widgetModal">Edit</a></li><li><a class="dropdown-item  remove-widget" href="#" data-bs-toggle="modal" data-bs-target="#removeWidgetModal">Delete</a></li></ul>
                            </div>
                        </div>
                        
                    </div>

                    </div>
                    <small class="text-muted mb-0 text-singleline applied-filter-text"></small>

                    <div class="widget-container" style="height:calc( 100% - 45px )" id="widget-container-id-${key}"></div></div>
                    </div>`;
}

function getFilterInputsUi(widgetId, options) {
    if (widgetId == null) {
        
        var widgetfilterWrapper = `#widgetFormFiltersContainer`;
        $(widgetfilterWrapper).html("");
    } else {

        var widgetfilterWrapper = `.widget-filter-wrapper[data-id="widget-container-id-${widgetId}"] .widget-custom-filter-wrapper`;
    }
    var filters = getFilters(options.module, options.measureby, options.viewby);
    var filterInputs = '';
    if (filters !== null) {
        $.each(filters, function (key, filter) {
            if (filter.type == 'select') {
                var dataattr = '';
                //if (filter.alloption === true) {
                    //var defaultOption = '<option value="">All</option>';
                //}
                if (typeof filter.multiple != 'undefined' && filter.multiple === true) {
                    dataattr += ' multiple ';
                }
                if (typeof filter.alltext != 'undefined') {
                    dataattr += ' data-none-selected-text="' + filter.alltext +'" ';
                }
                if (typeof filter.max != 'undefined') {
                    dataattr += ' data-max-options="'+filter.max+'" ';
                }
                if (typeof filter.min != 'undefined') {
                    dataattr += ' data-min-options="' + filter.min + '" ';
                }

                if (typeof filter.required != 'undefined' && filter.required==true) {
                    dataattr += ' required ';
                }
                var additional_class = '';
                if (typeof filter.class != 'undefined') {
                    additional_class += filter.class ;
                }
                var defaultValue = '';
                if (typeof options.widgetfilters != 'undefined') {
                    if (key in options.widgetfilters) {
                        if (Array.isArray(options.widgetfilters[key])) {
                            defaultValue = options.widgetfilters[key].join(',');
                        }
                        else {
                            defaultValue = options.widgetfilters[key];
                        }
                    }
                    
                }
                
                $(widgetfilterWrapper).append(`
                    <div class="mb-3 input-wrapper">
                        <label for="${key}" class="form-label">${filter.label}</label>
                        <select class="form-select widget-form-filter selectpicker ${additional_class} " data-selected-text-format="count > 4" data-size="5" data-dropup-auto="false" data-value="${defaultValue}" name="${key}" ${dataattr} data-width="100%" data-live-search="true">
                        </select>
                    </div>
                `);
                filter.options(widgetfilterWrapper,function (optionsList) {
                    $(`${widgetfilterWrapper} .widget-form-filter[name="${key}"]`).html(optionsList);
                    let value = '';
                    if (typeof options.widgetfilters != 'undefined' && typeof options.widgetfilters[key] != 'undefined') {
                        value = options.widgetfilters[key];
                        if (typeof filter.multiple != 'undefined' && filter.multiple === true && Array.isArray(value) == false) {
                            value = value.toString().split(',').map(function (item) {
                                return item.trim();
                            });
                        }
                        
                        $(`${widgetfilterWrapper} .widget-form-filter[name="${key}"]`).val(value);

                    }
                    $(`${widgetfilterWrapper} .widget-form-filter[name="${key}"]`).selectpicker();
                }, key)
            }
            
            
        })
        if (widgetId != null) {
            $(`.widget-filter-wrapper[data-id="widget-container-id-${widgetId}"] .widget-custom-filter-wrapper`).append('<div class="text-end"><a class="btn btn-primary applyWidgetFilters"> Apply Filter</a></div> <hr>');
        }
    }
    return filterInputs;
}