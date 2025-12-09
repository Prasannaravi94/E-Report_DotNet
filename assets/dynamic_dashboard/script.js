$(document).ready(function() {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
      return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
        if (jqxhr.status === 401) {

            window.location.href = window.location.origin + '/index.aspx';
        }
    });

    /*$(document).on('changed.bs.select', '.selectpicker[data-min-options]', function () {
        var selectedOptions = $(this).val();
        var selectedCount = selectedOptions.length;
        if (selectedCount === 0) {
            var minOptions = parseInt($(this).data('min-options'))
            $(this).find('option').slice(0, minOptions).prop('selected', true);
            $(this).selectpicker();
        }
        
    });*/

});

function showLoader() {
    $("#main-loader").parent().show();
}

function hideLoader() {
    $("#main-loader").parent().hide();
}


