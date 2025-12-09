localStorage.setItem("kpiFilterOptionsBrands", "");
localStorage.setItem("kpiFilterOptionsDoctors", "");
localStorage.setItem("kpiFilterOptionsProducts", "");
localStorage.setItem("kpiFilterOptionsSubCategories", "");
localStorage.setItem("kpiFilterOptionsGroups", "");
localStorage.setItem("kpiFilterOptionsSpecialities", "");
localStorage.setItem("kpiFilterOptionsCategories", "");
localStorage.setItem("kpiFilterOptionsFieldForces", "");
localStorage.setItem("kpiFilterOptionsProductCategories", "");
localStorage.setItem("kpiFilterOptionsHQs", ""); 
localStorage.setItem("kpiFilterOptionsStates", ""); 



function getBrandOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetBrands', 'kpiFilterOptionsBrands');
}

function getDoctorOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetDoctors', 'kpiFilterOptionsDoctors');
}

function getProductOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetProducts', 'kpiFilterOptionsProducts');
}

function getGroupsOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetGroups', 'kpiFilterOptionsGroups');
}

function getSubCategoryOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetSubCategories', 'kpiFilterOptionsSubCategories');
}

function getSpecialityOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetSpecialities', 'kpiFilterOptionsSpecialities');
}
function getCategoryOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetCategories', 'kpiFilterOptionsCategories');
}

function getFieldForceOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetFieldForces', 'kpiFilterOptionsFieldForces');
}

function getProductCategoryOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetProductCategories', 'kpiFilterOptionsProductCategories');
}

function getHQOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetHQs', 'kpiFilterOptionsHQs');
}

function getStateOptions(filterwrapper, callback) {
    getFilterOptions(callback, 'GetStates', 'kpiFilterOptionsStates');
}
function getFilterOptions(callback, name, localstorageName) {
    var options = localStorage.getItem(localstorageName);
    if (options != '') {
        callback(options);
        return;
    }
    activeoptions = "";
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: "./DynamicDashboardWebService.asmx/" + name,
        dataType: 'json',
        data: JSON.stringify({ Data: FormData }),
        async: false,
        success: function (response) {
            var inactiveOptions = "";
            $.each(response.d, function (key, option) {
                if (option.Inactive == true) {
                    inactiveOptions += `<option value="${option.Id}">${option.Name}</option>`;

                } else {
                    options += `<option value="${option.Id}">${option.Name}</option>`;
                }
            });
            if (inactiveOptions != "") {
                options += `<optgroup label="Inactvies">${inactiveOptions}</optgroup>`;
            }
            localStorage.setItem(localstorageName, options);
            callback(options);

        }
    });
}