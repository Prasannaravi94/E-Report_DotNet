// scrollableTable.js

var ScrollableTable = (function () {
    ScrollableTable.prototype.showLoader =function () {
        $("#main-loader").parent().show();
    }

    ScrollableTable.prototype.hideLoader = function () {
        $("#main-loader").parent().hide();
    }

    ScrollableTable.prototype.debounce = function(func, wait) {
        let timeout;
        return function executedFunction() {
            const later = () => {
                clearTimeout(timeout);
                func.apply(this, arguments);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }

    function ScrollableTable(tableId, config) {

        ScrollableTable.prototype.prefixName = "virtual-scroller-table"
        ScrollableTable.prototype.tableId = tableId
        ScrollableTable.prototype.wrapperSelector = `#${ScrollableTable.prototype.tableId}-${ScrollableTable.prototype.prefixName}-wrapper`
        ScrollableTable.prototype.infoSelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}-info`
        ScrollableTable.prototype.scrollerSelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}`
        ScrollableTable.prototype.headerWrapperSelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}-header-wrapper`
        ScrollableTable.prototype.bodyWrapperSelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}-body-wrapper`
        ScrollableTable.prototype.bodyBeforeSelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}-body-wrapper .${ScrollableTable.prototype.prefixName}-body-before`
        ScrollableTable.prototype.bodySelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}-body-wrapper .${ScrollableTable.prototype.prefixName}-body`
        ScrollableTable.prototype.bodyAfterSelector = `${ScrollableTable.prototype.wrapperSelector} .${ScrollableTable.prototype.prefixName}-body-wrapper .${ScrollableTable.prototype.prefixName}-body-after`

        ScrollableTable.prototype.remoteUrl = config.remoteUrl;
        ScrollableTable.prototype.totalRecords = config.totalRecords;
        ScrollableTable.prototype.pageLimit = "200000";
        ScrollableTable.prototype.pages = Math.ceil(ScrollableTable.prototype.totalRecords / ScrollableTable.prototype.pageLimit);
        ScrollableTable.prototype.calculatePageData(1)

        ScrollableTable.prototype.rowHeight = 40.8;
        ScrollableTable.prototype.displayLength = 100;
        ScrollableTable.prototype.currentRow = 1;
        ScrollableTable.prototype.startRow = 1;
        ScrollableTable.prototype.endRow = ScrollableTable.prototype.displayLength;
        ScrollableTable.prototype.scrollEnabled = true;
        ScrollableTable.prototype.filters = config.filters || null;
        ScrollableTable.prototype.columns = config.columns || null;
        ScrollableTable.prototype.visibleColumns = config.visibleColumns || null;
        ScrollableTable.prototype.preferenceName = config.preferenceName || "";
        ScrollableTable.prototype.init();

    }
    ScrollableTable.prototype.calculatePageData = function (page) {
        ScrollableTable.prototype.currentPage = page
        ScrollableTable.prototype.currentPageStart = (ScrollableTable.prototype.currentPage - 1) * ScrollableTable.prototype.pageLimit;
        ScrollableTable.prototype.currentPageEnd = Math.min(ScrollableTable.prototype.currentPage * ScrollableTable.prototype.pageLimit, ScrollableTable.prototype.totalRecords);
        ScrollableTable.prototype.currentPageTotalRecords = ScrollableTable.prototype.currentPageEnd - ScrollableTable.prototype.currentPageStart
    }
    ScrollableTable.prototype.loadData = function (start, end, currentRow = 1, firstLoad = false) {
        ScrollableTable.prototype.showLoader();
        ScrollableTable.prototype.scrollEnabled = false;
        var Filters = (typeof ScrollableTable.prototype.filters === 'function') ? ScrollableTable.prototype.filters() : ScrollableTable.prototype.filters;
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: ScrollableTable.prototype.remoteUrl,
            dataType: 'json',
            data: JSON.stringify({ Start: ScrollableTable.prototype.currentPageStart + start, End: ScrollableTable.prototype.currentPageStart + end, Filters: Filters }),
            success: function (response) {
                var result = response.d;
                if (result) {
                    $(`${ScrollableTable.prototype.bodySelector}`).html(result);
                    $(`${ScrollableTable.prototype.bodyBeforeSelector}`).css('height', ((start - 1) * ScrollableTable.prototype.rowHeight) + 'px');
                    $(`${ScrollableTable.prototype.bodyAfterSelector}`).css('height', ((ScrollableTable.prototype.currentPageTotalRecords - end) * ScrollableTable.prototype.rowHeight) + 'px');

                    if (firstLoad) {
                        ScrollableTable.prototype.synchronizeHeaderColumnWidths();
                        ScrollableTable.prototype.synchronizeBodyColumnWidths();
                        ScrollableTable.prototype.setTableInfo(1)
                    } else {
                        ScrollableTable.prototype.synchronizeHeaderColumnWidths();
                        ScrollableTable.prototype.synchronizeBodyColumnWidths();
                    }
                    var scrollTo = (currentRow - 1) * ScrollableTable.prototype.rowHeight;
                    $(`${ScrollableTable.prototype.scrollerSelector}`).scrollTop(scrollTo);
                    
                    
                    setTimeout(function () {

                        ScrollableTable.prototype.scrollEnabled =true

                    }, 1200);

                }
                ScrollableTable.prototype.hideLoader();

            }
        });
    };

    ScrollableTable.prototype.updateVisibleRows = function () {
        currentRow = Math.floor(($(ScrollableTable.prototype.scrollerSelector).scrollTop()) / ScrollableTable.prototype.rowHeight) + 1;

        previousRows = currentRow - (ScrollableTable.prototype.displayLength / 2);
        nextRows = currentRow + (ScrollableTable.prototype.displayLength / 2);
        if (nextRows > ScrollableTable.prototype.currentPageTotalRecords) {
            currentstartRow = ScrollableTable.prototype.currentPageTotalRecords - ScrollableTable.prototype.displayLength
            currentendRow = ScrollableTable.prototype.currentPageTotalRecords

        } else {
            currentstartRow = previousRows
            currentendRow = nextRows;
        }
        if (currentstartRow < 1) {
            currentendRow += -1 * (currentstartRow)
            if (currentendRow > ScrollableTable.prototype.currentPageTotalRecords) {
                currentendRow = ScrollableTable.prototype.currentPageTotalRecords
            }
            currentstartRow = 1;
        }
        if ((ScrollableTable.prototype.startRow == 1 && currentRow < (ScrollableTable.prototype.displayLength - 10)) || (ScrollableTable.prototype.endRow == ScrollableTable.prototype.currentPageTotalRecords && currentRow > ScrollableTable.prototype.currentPageTotalRecords - (ScrollableTable.prototype.displayLength - 10)) || (currentRow > (ScrollableTable.prototype.startRow + 10) && (currentRow < ScrollableTable.prototype.endRow - 10))) {
            return;
        }

        ScrollableTable.prototype.startRow = currentstartRow;
        ScrollableTable.prototype.endRow = currentendRow;
        ScrollableTable.prototype.loadData(ScrollableTable.prototype.startRow, ScrollableTable.prototype.endRow, currentRow);
    };

    ScrollableTable.prototype.setTableInfo = function (currentRow = 1) {
        
        var toRows = currentRow + Math.round(($(`${ScrollableTable.prototype.scrollerSelector}`).height() / ScrollableTable.prototype.rowHeight)) - 2;
        if (toRows > ScrollableTable.prototype.currentPageTotalRecords) {
            toRows = ScrollableTable.prototype.currentPageTotalRecords;
        }
        currentRow += ScrollableTable.prototype.currentPageStart;
        toRows += ScrollableTable.prototype.currentPageStart;
        $(`${ScrollableTable.prototype.infoSelector}`).html(`Showing ${currentRow} to ${toRows} of ${ScrollableTable.prototype.totalRecords} entries`);
    };

    ScrollableTable.prototype.synchronizeHeaderColumnWidths = function () {
        
        var headerColumns = $(`${ScrollableTable.prototype.headerWrapperSelector} th`);
        var bodyColumns = $(`${ScrollableTable.prototype.bodySelector} tr:first-child td`);
        headerColumns.each(function (index) {
            var bodyColumnWidth = $(bodyColumns[index]).outerWidth();
            $(this).css('min-width', bodyColumnWidth);
            $(this).css('min-height', ScrollableTable.prototype.rowHeight);
            $(this).css('max-height', ScrollableTable.prototype.rowHeight);
        });
    };

    ScrollableTable.prototype.synchronizeBodyColumnWidths = function () {
        var headerColumns = $(`${ScrollableTable.prototype.headerWrapperSelector} th`);
        headerColumns.each(function (index) {
            var bodyColumnWidth = $(this).outerWidth();
            $(`${ScrollableTable.prototype.bodySelector} td:nth-child(${(index + 1)})`).css('min-width', bodyColumnWidth);
            $(`${ScrollableTable.prototype.bodySelector} td:nth-child(${(index + 1)})`).css('max-width', bodyColumnWidth);
            $(headerColumns[index]).css('min-width', bodyColumnWidth);
        });
    };

    ScrollableTable.prototype.updateListOrder =function() {
        var checkedItems = $("#columnshowshidecanvas .form-check-input:checked").closest("li");
        var uncheckedItems = $("#columnshowshidecanvas  .form-check-input:not(:checked)").closest("li");

        // Append unchecked items after checked items
        $("#sortable-list").append(checkedItems).append(uncheckedItems);

        // Refresh the sortable list
        $("#sortable-list").sortable("refresh");
    }
    ScrollableTable.prototype.getColumnShowHideOffcanvas = function () {
        var offCanvas = `<div class="offcanvas offcanvas-end" tabindex="-1" id="columnshowshidecanvas" >
        <div class="offcanvas-header border-bottom">
            <h5 class="offcanvas-title">Show / Hide columns</h5>
            <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body"><ul id="sortable-list" class="list-unstyled">`;

        $.each(ScrollableTable.prototype.visibleColumns, function (key,value) {
            offCanvas += `<li class="border-bottom border-light-subtle"><div class="form-check py-1">
                <input class="form-check-input" type="checkbox" name="${value}" checked>
                    <label class="form-check-label">
                        ${ScrollableTable.prototype.columns[value]}
                    </label>
            </div></li>`;
        });
        $.each(ScrollableTable.prototype.columns, function (key, value) {
            if (ScrollableTable.prototype.visibleColumns.includes(key)) {
                return;
            }
            offCanvas += `<li class="border-bottom border-light-subtle"><div class="form-check py-1">
                <input class="form-check-input" type="checkbox" name="${key}">
                    <label class="form-check-label">
                        ${value}
                    </label>
            </div></li>`;
        });
        offCanvas += `</ul></div>
        <div class="offcanvas-footer p-2 border-top d-flex justify-content-between">
            <button type="button" class="btn" data-bs-dismiss="offcanvas">Cancel</button>
            <button type="submit" id="${ScrollableTable.prototype.prefixName}-saveColumns" class="btn btn-primary">Apply</button>
        </div>
    </div>`;
        return offCanvas;
    };
    ScrollableTable.prototype.init = function () {
        var tableHeader = $(`#${ScrollableTable.prototype.tableId}`).prop('outerHTML');
        var pagination = ``;
        if (ScrollableTable.prototype.pages > 1) {
            pagination = `
            <nav aria-label="Page navigation example">
                <ul class="pagination justify-content-end">`
            for (var i = 1; i <= ScrollableTable.prototype.pages; i++) {
                let active = '';
                if (i == ScrollableTable.prototype.currentPage) {
                    active = 'active';
                }
                
                pagination += `<li class="page-item ${active}"><a class="page-link ${ScrollableTable.prototype.prefixName}-page-link" data-page="${i}" href="javascript:void(0)">${i}</a></li>`
            }
            pagination += `
                </ul>
            </nav>`
        }
        var currentUrl = window.location.href;
        var separator = currentUrl.includes('?') ? '&' : '?';
        var exportUrl = currentUrl + separator + 'export=1';

        var tableStructure = `
        <div id="${ScrollableTable.prototype.tableId}-${ScrollableTable.prototype.prefixName}-wrapper" class="${ScrollableTable.prototype.prefixName}-wrapper">
            <div class="d-flex justify-content-between mb-1">
                <div class="${ScrollableTable.prototype.prefixName}-info"></div>
                <div>
                <a class="btn btn-success" href="${exportUrl}" style="background-color: #1D6F42"><i class="fa fa-file-excel-o mr-2" aria-hidden="true"></i>Export Excel</a>
                <a class="btn btn-secondary" data-bs-toggle="offcanvas" href="#columnshowshidecanvas" ><i class="fa-solid fa-table-cells"></i></a>
                </div>
            </div>
            
            <div class="${ScrollableTable.prototype.prefixName} table-responsive bg-white">
                <div class="${ScrollableTable.prototype.prefixName}-header-wrapper">
                    ${tableHeader}
                </div>
                <div class="${ScrollableTable.prototype.prefixName}-body-wrapper">
                    <div class="${ScrollableTable.prototype.prefixName}-body-before"></div>
                    <table class="table table-bordered table-striped">
                        <tbody class="${ScrollableTable.prototype.prefixName}-body">
                    
                        </tbody>
                    </table>
                    <div class="${ScrollableTable.prototype.prefixName}-body-after"></div>
                </div>
            </div>
            ${pagination}
        </div>
        `;
        $('#offCanvasWrapper').html(ScrollableTable.prototype.getColumnShowHideOffcanvas());
        $(`#${ScrollableTable.prototype.tableId}`).replaceWith(tableStructure);
        $("#sortable-list").sortable();
        ScrollableTable.prototype.loadData(1, ScrollableTable.prototype.displayLength, 1, true)

        $(`${ScrollableTable.prototype.scrollerSelector}`).on('scroll', ScrollableTable.prototype.debounce(function () {
            if (ScrollableTable.prototype.currentPageTotalRecords <= ScrollableTable.prototype.displayLength || !ScrollableTable.prototype.scrollEnabled) {
                return;
            }
            ScrollableTable.prototype.updateVisibleRows();
        }, 1000));

        $(`${ScrollableTable.prototype.scrollerSelector}`).on('scroll', function () {
            currentRow = Math.floor(($(this).scrollTop()) / ScrollableTable.prototype.rowHeight) + 1;
            ScrollableTable.prototype.setTableInfo(currentRow);
        });

        $(window).on('resize', function () {
            ScrollableTable.prototype.synchronizeBodyColumnWidths();
        });


        $(`.${ScrollableTable.prototype.prefixName}-page-link`).click(function () {
            $(`.${ScrollableTable.prototype.prefixName}-page-link`).parent().removeClass('active')
            $(this).parent().addClass('active')
            ScrollableTable.prototype.calculatePageData($(this).data('page'))
            ScrollableTable.prototype.loadData(1, ScrollableTable.prototype.displayLength, 1, true)
        })

        $(`#${ScrollableTable.prototype.prefixName}-saveColumns`).click(function () {
            $('#columnshowshidecanvas').toggle();
            ScrollableTable.prototype.showLoader();
            var checkedNames = [];

            // Iterate over checked checkboxes and push their names into the array
            $('#columnshowshidecanvas .offcanvas-body .form-check-input:checked').each(function () {
                checkedNames.push($(this).attr('name'));
            });

            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                url: "./DrillDownWebService.asmx/saveColumns",
                dataType: 'json',
                data: JSON.stringify({ Name: ScrollableTable.prototype.preferenceName, Values: checkedNames }),
                success: function (response) {
                    location.reload();
                }
            });
        })
        $("#columnshowshidecanvas .form-check-input").on("change", function () {
            ScrollableTable.prototype.updateListOrder();
        });
    };

    return ScrollableTable;
})();
