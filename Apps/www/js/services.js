var fmcgServices = angular.module('fmcgServices', []);
fmcgServices.factory('phonegapReady', function($rootScope) {
    return function(fn) {
        var queue = [];
        var impl = function() {
            queue.push(Array.prototype.slice.call(arguments));
        };
        document.addEventListener('deviceready', function() {
            queue.forEach(function(args) {
                fn.apply(this, args);
            });
            impl = fn;
        }, false);
        return function() {
            return impl.apply(this, arguments);
        };
    };
});
fmcgServices.factory('navSvc', function($navigate) {
    return {
        slidePage: function(path, type) {
            $navigate.go(path, type);
        },
        back: function() {
            $navigate.back();
        }
    }
});
fmcgServices.factory('geolocation', function($rootScope, phonegapReady) {
    return {
        getCurrentPosition: phonegapReady(function(onSuccess, onError, options) {
            navigator.geolocation.getCurrentPosition(function() {
                var that = this,
                        args = arguments;
                if (onSuccess) {
                    $rootScope.$apply(function() {
                        onSuccess.apply(that, args);
                    });
                }
            }, function() {
                var that = this,
                        args = arguments;
                if (onError) {
                    $rootScope.$apply(function() {
                        onError.apply(that, args);
                    });
                }
            },
                    options);
        })
    };
});
fmcgServices.factory('notification', function($rootScope, phonegapReady) {
    return {
        alert: phonegapReady(function(message, alertCallback, title, buttonName) {
            navigator.notification.alert(message, function() {
                var that = this,
                        args = arguments;
                $rootScope.$apply(function() {
                    alertCallback.apply(that, args);
                });
            }, title, buttonName);
        }),
        confirm: phonegapReady(function(message, confirmCallback, title, buttonLabels) {
            navigator.notification.confirm(message, function() {
                var that = this,
                        args = arguments;
                $rootScope.$apply(function() {
                    confirmCallback.apply(that, args);
                });
            }, title, buttonLabels);
        }),
        beep: function(times) {
            navigator.notification.beep(times);
        },
        vibrate: function(milliseconds) {
            navigator.notification.vibrate(milliseconds);
        }
    };
});


fmcgServices.factory('fmcgAPIservice', function ($http, fmcgLocalStorage) {
    var fmcgAPI = {};
    $http.defaults.useXDomain = true;
    $http.defaults.headers.common['X-Requested-With'];
    fmcgAPI.deleteEntry = function (method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        var str = {
            arc: data.arc,
            amc: data.amc
        };
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(str)), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }
    fmcgAPI.getDataList = function (method, url, data, sSF) {
        var tablistNSL = ["vwactivity_report", "vwactivity_msl_details", "vwActivity_CSH_Detail", "activity_stockist_report", "vwActivity_Unlst_Detail", "activity_doctor_report", "activity_Chemist_report", "activity_stockist_report", "activity_unlisteddoctor_Report", "vwactivity_Report_APP", "vwactivity_doctor_report_App", "vwactivity_Chemist_report_App", "vwactivity_stockist_report_App", "vwactivity_unlisteddoctor_Report_App", "activity_unlistedsample_Report", "activity_sample_report", "activity_input_report", "activity_chm_sample_report", "speciality_master", "vwActivity_Report", "Activity_POB_Report", "vwMyDayPlan"];
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        if (!sSF) sSF = ((userData.desigCode.toLowerCase() == 'mr' || (tablistNSL.indexOf(data[0]) > -1) || data.length == 0) ? userData.sfCode : userData.curSFCode)
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + sSF + "&rSF=" + userData.sfCode + "&username=" + userData.user.username;
        }
        var str = {
            tableName: data[0],
            coloumns: data[1],
            today: data[4],
            join: data[2],
            where: data[3],
            or: data[5],
            wt: data[6],
            sfCode: data[7],
            orderBy: data[8]
        };
        if (tablistNSL.indexOf(data[0]) === -1) {
            str.orderBy = "[\"name asc\"]";
        }
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(str)), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    fmcgAPI.getPostData = function (method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }

        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + encodeURIComponent(JSON.stringify(data)), //"tableName=" + data[0] + "&coloumns=" + data[1] + "&join=" + encodeURI(data[2]) + "&where=" + data[3],
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    };
    pushKey = function (obj1, obj2) {
        obj1['f_key'] = obj2;
        return obj1;
    }
    addQuotes = function (data) {
        data = data ? data : "";
        return "\'" + data.toString().replace(/\'/g,"\'\'") + "\'";
    }
    fmcgAPI.updateRemark = function (method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        resultData = [];
        if (userData) {
            var UpdRemark = {};
            UpdRemark.SF_code = addQuotes(userData.sfCode);
            UpdRemark.remarks = addQuotes(data.remarks);
            UpdRemark.HalfDay_FW_Type = addQuotes(data.Halfday);
            resultData.push({ 'Activity_Report': UpdRemark });
            $http.defaults.useXDomain = true;
            return $http({
                method: method,
                url: baseURL + url,
                data: "data=" + JSON.stringify(resultData),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
        else { alert('Kindly Login Again !...'); }
    }
    fmcgAPI.sendData= function (method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode;
        }
        $http.defaults.useXDomain = true;
        return $http({
            method: method,
            url: baseURL + url + appendDS,
            data: "data=" + JSON.stringify(data),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }
    fmcgAPI.updateDCRData = function (method, url, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode + "&username=" + userData.user.username;
        }
        $http.defaults.useXDomain = true;
        return $http({
            method: method,
            url: baseURL + url + appendDS,
            data: "data=" + JSON.stringify(data),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }
    fmcgAPI.addMAData = function (method, url, choice, data) {
        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode + "&username=" + userData.user.username;
        }
        var resultData = [];
        switch (choice) {
            case "1":
                var chemists_master = {};
                chemists_master.town_code = addQuotes(data.cluster.selected.id);
                chemists_master.chemists_name = addQuotes(data.name);
                chemists_master.Chemists_Address1 = addQuotes(data.address);
                chemists_master.Chemists_Phone = addQuotes(data.phone);
                resultData.push({ 'chemists_master': chemists_master });
                break;
            case "2":
                var unlisted_doctor_master = {};
                unlisted_doctor_master.town_code = addQuotes(data.cluster.selected.id);
                unlisted_doctor_master.unlisted_doctor_name = addQuotes(data.name);
                unlisted_doctor_master.unlisted_cat_code = addQuotes(data.category.selected.id);
                unlisted_doctor_master.unlisted_specialty_code = addQuotes(data.speciality.selected.id);
                unlisted_doctor_master.unlisted_qulifi = addQuotes(data.qulification.selected.id);
                unlisted_doctor_master.unlisted_class = addQuotes(data.class.selected.id);
                resultData.push({ 'unlisted_doctor_master': unlisted_doctor_master });
                break;
            case "3":
                var tbMyDayPlan = {};
                tbMyDayPlan.wtype = addQuotes(data.worktype);
                tbMyDayPlan.sf_member_code = addQuotes(data.subordinateid);
                tbMyDayPlan.cluster = addQuotes(data.clusterid);
                tbMyDayPlan.remarks = addQuotes(data.remarks);
                tbMyDayPlan.FWFlg = addQuotes(data.FWFlg);
                tbMyDayPlan.ClstrName = addQuotes(data.ClstrName);
                var endate = new Date(data['entryDate']);
                var eDate = endate.getFullYear() + "-" + ('0' + (endate.getMonth() + 1)).slice(-2) + "-" + ('0' + endate.getDate()).slice(-2) + " 00:00:00";

                tbMyDayPlan.dcr_activity_date= eDate;
                resultData.push({ 'tbMyDayPlan': tbMyDayPlan });
                break;
            case "4":
                var endate = new Date(data['RCPADt']);
                var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
                var tbRCPADetails = {};
                tbRCPADetails.ChmId = addQuotes(data.ChmId);
                tbRCPADetails.ChmName = addQuotes(data.ChmName);
                tbRCPADetails.DrId = addQuotes(data.DrId);
                tbRCPADetails.DrName = addQuotes(data.DrName);
                tbRCPADetails.RCPADt = addQuotes(dateStr);
                tbRCPADetails.ourBrnd = addQuotes(data.ourBrnd);
                tbRCPADetails.ourBrndNm = addQuotes(data.ourBrndNm);

                tbRCPADetails.CmptrName = addQuotes(data.CmptrName);
                tbRCPADetails.CmptrBrnd = addQuotes(data.CmptrBrnd);
                tbRCPADetails.CmptrQty = addQuotes(data.CmptrQty);
                tbRCPADetails.CmptrPOB = addQuotes(data.CmptrPOB);
                tbRCPADetails.CmptrPriz = addQuotes(data.CmptrPriz);
                tbRCPADetails.Remark = addQuotes(data.Remark);
                resultData.push({ 'tbRCPADetails': tbRCPADetails });
                break;
            case "5":
                var tbRemdrCall = {};
                tbRemdrCall.Doctor_ID = addQuotes(data.DrId);
                tbRemdrCall.WWith = addQuotes(data.JntWrkCds);
                tbRemdrCall.WWithNm = addQuotes(data.JntWrkNms);
                tbRemdrCall.Prods = addQuotes(data.ProdCds);
                tbRemdrCall.ProdsNm = addQuotes(data.ProdNms);
                tbRemdrCall.Remarks = addQuotes(data.Remark);
                tbRemdrCall.location = addQuotes(data.location);
                resultData.push({ 'tbRemdrCall': tbRemdrCall });
                break;
            case "6":
                var Map_GEO_Customers = {};
                Map_GEO_Customers.Cust_Code = addQuotes(data.id);
                Map_GEO_Customers.lat = addQuotes(data.lat);
                Map_GEO_Customers.long = addQuotes(data.long);
                Map_GEO_Customers.StatFlag = '0';
                resultData.push({ 'Map_GEO_Customers': Map_GEO_Customers });
                break;
            case "7":
                var endate = new Date(data['DtTm']);
                var tbTrackLoction = {};
                var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
                tbTrackLoction.SF_code = addQuotes(data.SF_code);
                tbTrackLoction.SF_Name = addQuotes(data.SF_Name);
                tbTrackLoction.DtTm = addQuotes(dateStr);
                tbTrackLoction.Lat = addQuotes(data.Lat);
                tbTrackLoction.Lon = addQuotes(data.Lon);
                resultData.push({ 'tbTrackLoction': tbTrackLoction });
                break;
            case "8":
                var LeaveForm = {};
                LeaveForm.Leave_Type = addQuotes(data.Leave_Type);
                LeaveForm.From_Date = data.From_Date;
                LeaveForm.To_Date = addQuotes(data.To_Date);
                LeaveForm.Reason = addQuotes(data.Reason);
                LeaveForm.address = addQuotes(data.address);
                LeaveForm.No_of_Days = data.No_of_Days;
              
                resultData.push({ 'LeaveForm': LeaveForm });
                break;
            case "9":
                var LeaveApproval = {};
                LeaveApproval.From_Date = data.LA.From_Date;
                LeaveApproval.To_Date = addQuotes(data.LA.To_Date);
                LeaveApproval.No_of_Days = data.LA.LeaveDays;
                LeaveApproval.Sf_Code = data.LA.Sf_Code;
                resultData.push({ 'LeaveApproval': LeaveApproval });
                break;
            case "10":
                var LeaveReject = {};
                LeaveReject.From_Date = data.LA.From_Date;
                LeaveReject.To_Date = addQuotes(data.LA.To_Date);
                LeaveReject.No_of_Days = data.LA.LeaveDays;
                LeaveReject.reason = addQuotes(data.reason);
                LeaveReject.Sf_Code = data.LA.Sf_Code;


                resultData.push({ 'LeaveReject': LeaveReject });
                break;
            case "11":
                var Tour_Plan = {};
                Tour_Plan.worktype_code = addQuotes(data.worktype_code);
                Tour_Plan.worktype_name = addQuotes(data.worktype_name);
                Tour_Plan.worktype_code2 = addQuotes(data.worktype_code2);
                Tour_Plan.worktype_name2 = addQuotes(data.worktype_name2);
                Tour_Plan.worktype_code3 = addQuotes(data.worktype_code3);
                Tour_Plan.worktype_name3 = addQuotes(data.worktype_name3);
                Tour_Plan.Worked_with_Code = addQuotes(data.Worked_with_Code);
                Tour_Plan.Worked_with_Name = addQuotes(data.Worked_with_Name);
                Tour_Plan.RouteCode = addQuotes(data.RouteCode);
                Tour_Plan.RouteName = addQuotes(data.RouteName);
                Tour_Plan.RouteCode2 = addQuotes(data.RouteCode2);
                Tour_Plan.RouteName2 = addQuotes(data.RouteName2);
                Tour_Plan.RouteCode3 = addQuotes(data.RouteCode3);
                Tour_Plan.RouteName3 = addQuotes(data.RouteName3);
                Tour_Plan.sfName = addQuotes(data.sfName);
                Tour_Plan.objective = addQuotes(data.remarks);
                Tour_Plan.Tour_Date = addQuotes(data.date);
                //  Tour_Plan.Market = addQuotes(data.market);
                resultData.push({ 'Tour_Plan': Tour_Plan });
                break;
            case "12":
                var TourPlanSubmit = {};
                resultData.push({ 'TourPlanSubmit': TourPlanSubmit });
                break;
            case "13":
                var TPApproval = {};
                resultData.push({ 'TPApproval': TPApproval });
                break;
            case "14":
                var TPReject = {};
                TPReject.reason = addQuotes(data.reason);
                resultData.push({ 'TPReject': TPReject });
                break;
            case "15":
                var DCRApproval = {};
                resultData.push({ 'DCRApproval': DCRApproval });
                break;
            case "16":
                var DCRReject = {};
                DCRReject.reason = addQuotes(data.reason);
                resultData.push({ 'DCRReject': DCRReject });
                break;
            case "17":
                var TP_Attendance = {}
                TP_Attendance.lat = addQuotes(data.lat);
                TP_Attendance.long = addQuotes(data.long);
                resultData.push({ 'TP_Attendance': TP_Attendance });

            case "18":
                var MCL_Details = {};
                MCL_Details.doctorCode = addQuotes(data.doctorCode);
                MCL_Details.Doc_Cat_Code = addQuotes(data.Doc_Cat_Code);
                MCL_Details.Doc_Cat_ShortName = addQuotes(data.Doc_Cat_ShortName);
                MCL_Details.Doc_QuaCode = addQuotes(data.Doc_QuaCode);
                MCL_Details.Doc_Qua_Name = addQuotes(data.Doc_Qua_Name);
                MCL_Details.Doc_Special_Code = addQuotes(data.Doc_Special_Code);
                MCL_Details.Doc_Spec_ShortName = addQuotes(data.Doc_Spec_ShortName);
                MCL_Details.Hospital_Address = addQuotes(data.Hospital_Address);
                MCL_Details.ListedDr_DOB = addQuotes(data.ListedDr_DOB);
                MCL_Details.ListedDr_DOW = addQuotes(data.ListedDr_DOW);
                MCL_Details.ListedDr_Hospital = addQuotes(data.ListedDr_Hospital);
                MCL_Details.ListedDr_RegNo = addQuotes(data.ListedDr_RegNo);
                MCL_Details.ListedDr_Sex = addQuotes(data.ListedDr_Sex);
                MCL_Details.Dr_Potential = addQuotes(data.Dr_Potential);
                MCL_Details.Dr_Contribution = addQuotes(data.Dr_Contribution);
                MCL_Details.visit_hours = addQuotes(data.visit_hours);
                MCL_Details.visit_days = addQuotes(data.visit_days);
                MCL_Details.ListedDr_Address3 = addQuotes(data.Clinic_Address);
               //MCL_Details.Visiting_Card = addQuotes("110036_IMG-20170812-WA0001.jpg");
                MCL_Details.Visiting_Card = addQuotes(data.Visiting_Card);
                resultData.push({ 'MCL_Details': MCL_Details });
            case "19":
                break;
            case "20":
                resultData.push({ 'expense': data });
        }
        $http.defaults.useXDomain = true;
        if (choice == 19) {
            return $http({
                url: baseURL + 'createMail' + appendDS,
                method: method,
                data: data,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }

            });
        }
        else {
            return $http({
                url: baseURL + url + appendDS,
                method: method,
                data: "data=" + JSON.stringify(resultData),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
    }
    fmcgAPI.saveDCRData = function (method, url, data, update) {
        var temp = window.localStorage.getItem("loginInfo");
       
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        var appendDS = "";
        if (userData) {
            appendDS = appendDS + "&divisionCode=" + userData.divisionCode + "&sfCode=" + userData.sfCode + "&username=" + userData.user.username;
        }
        if (data['worktype'] == undefined) {
            $http.defaults.useXDomain = true;
            return $http({
                url: baseURL,
                method: method,
                data: "data=",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
        var activity_report_APP = {};
        activity_report_APP['Worktype_code'] = addQuotes(data['worktype']['selected']['id']);

        try {
            activity_report_APP['Town_code'] = addQuotes(data['cluster']['selected']['id']);
        }
        catch (err) {

        }
        activity_report_APP['username'] = userData.user.username;
        activity_report_APP['app_device_id'] = __DevID;
        activity_report_APP['Daywise_Remarks'] = addQuotes(data['remarks']);
        activity_report_APP['rx'] = addQuotes(data['rx']);
        if (data['rx'])
            activity_report_APP['rx_t'] = addQuotes(data['rx_t']);
        else
            activity_report_APP['nrx_t'] = addQuotes(data['nrx_t']);
        activity_report_APP['DataSF'] = addQuotes(data['subordinate']['selected']['id']);
        var endate = new Date(data['entryDate']);
        var moddate = new Date(data['modifiedDate']);
        var dateStr = endate.getFullYear() + "-" + (endate.getMonth() + 1) + "-" + endate.getDate() + " " + endate.getHours() + ":" + endate.getMinutes() + ":" + endate.getSeconds();
        var mdateStr = moddate.getFullYear() + "-" + (moddate.getMonth() + 1) + "-" + moddate.getDate() + " " + moddate.getHours() + ":" + moddate.getMinutes() + ":" + moddate.getSeconds();
        var eDate = endate.getFullYear() + "-" + ('0' + (endate.getMonth() + 1)).slice(-2) + "-" + ('0' + endate.getDate()).slice(-2) + " 00:00:00";
        activity_report_APP['dcr_activity_date'] = eDate;
        //to be s
        var activity_sample_reports = [];
        var activity_input_reports = [];
        var activity_pob_reports = [];
        var Activity_Event_Captures = [];
        var resultData = [];
        resultData.push({ 'Activity_Report_APP': activity_report_APP });
      
        var selectedC = "";
        var jointWorkString = "";
        try {
            selectedC = data['customer']['selected']['id'];
        }
        catch (err) {

        }
        if ((data['worktype']['selected']['FWFlg']).toString() !== "F") {
            selectedC = "10";
        }
        else {
            for (var t = 0, jwlen = data.jontWorkSelectedList.length; t < jwlen; t++) {
                if (t != 0)
                    jointWorkString += "$$";
                jointWorkString += data.jontWorkSelectedList[t].jointwork;

            }
        }

        var sOrdStk = '', sOrdNo = '0';
        if ((',1,4,').indexOf(',' + selectedC + ',') > -1 && userData.AppTyp == 1) {
            if (data.OrdStk != undefined) {
                if (data.OrdStk.selected != undefined)
                {
                    sOrdStk = data['OrdStk']['selected']['id'];
                    sOrdNo = data['OrderNo'];
                    if (sOrdNo == '') sOrdNo = 0;
                }
            }
        }        

        var tempD = parseInt(data['pob']);
        switch (selectedC) {
            case "1":
                var activity_doctor_rep = {};

                activity_doctor_rep['Doctor_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_doctor_rep['Worked_With'] = addQuotes(jointWorkString);
                activity_doctor_rep['Doc_Meet_Time'] = addQuotes(dateStr);
                activity_doctor_rep['modified_time'] = addQuotes(mdateStr);
                activity_doctor_rep['location'] = addQuotes(data['location']);
                activity_doctor_rep['geoaddress'] = '';///['geoaddress'].replace(/\//g,'\\\/');
                if (userData.AppTyp == 1) {
                    activity_doctor_rep['Order_Stk'] = addQuotes(sOrdStk);
                    activity_doctor_rep['Order_No'] = addQuotes(sOrdNo);
                }
                activity_doctor_rep['doctor_code'] = addQuotes(data['doctor']['selected']['id']);
                pushKey(activity_doctor_rep, { "Activity_Report_Code": "'Activity_Report_APP'" });
                resultData.push({ 'Activity_Doctor_Report': activity_doctor_rep });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty']; tempOb['Product_Rx_Qty'] = (Qty == null) ? 0 : Qty;
                        Qty = data['productSelectedList'][i]['sample_qty']; tempOb['Product_Sample_Qty'] = (Qty == null) ? 0 : Qty;
                        activity_sample_reports.push(pushKey(tempOb, { "Activity_MSL_Code": "Activity_Doctor_Report" }));
                    }
                }
                resultData.push({ 'Activity_Sample_Report': activity_sample_reports });
                data['giftSelectedList'] = data['giftSelectedList'] || [];
                for (var i = 0, len = data['giftSelectedList'].length; i < len; i++) {
                    var tempOb = {};
                    var gfcd = data['giftSelectedList'][i]['gift'];
                    tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                    if (Envrmnt == ".Net") tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                    tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                    activity_input_reports.push(pushKey(tempOb, { "Activity_MSL_Code": "Activity_Doctor_Report" }));
                }
                resultData.push({ 'Activity_Input_Report': activity_input_reports });
                break;
            case "2":
                var activity_chemist_rep = {};
                activity_chemist_rep['Chemist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_chemist_rep['chemist_code'] = addQuotes(data['chemist']['selected']['id']);
                activity_chemist_rep['Worked_With'] = addQuotes(jointWorkString);
                activity_chemist_rep['location'] = addQuotes(data['location']);
                activity_chemist_rep['geoaddress'] = ''; //data['geoaddress'].replace(/\//g, '\\\/');
                activity_chemist_rep['Chm_Meet_Time'] = addQuotes(dateStr);
                activity_chemist_rep['modified_time'] = addQuotes(mdateStr);
                pushKey(activity_chemist_rep, { "Activity_Report_Code": "'Activity_Report_APP'" });
                resultData.push({ 'Activity_Chemist_Report': activity_chemist_rep });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty']; tempOb['Qty'] = (Qty == null) ? 0 : Qty;
                        activity_pob_reports.push(pushKey(tempOb, { "activity_chemist_code": "Activity_Chemist_Report" }));
                    }
                }
                resultData.push({ 'Activity_POB_Report': activity_pob_reports });
                if (data.giftSelectedList != undefined) {
                    for (var i = 0, len = data.giftSelectedList.length; i < len; i++) {
                        var tempOb = {};
                        var gfcd = data['giftSelectedList'][i]['gift'];
                        tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                        if (Envrmnt == ".Net") tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                        tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                        activity_input_reports.push(pushKey(tempOb, { "activity_chemist_code": "Activity_Chemist_Report" }));
                    }
                }
                resultData.push({ 'Activity_Chm_Sample_Report': activity_input_reports });
                break;
            case "3":
                var activity_stockist_Rep = {};
                activity_stockist_Rep['Stockist_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_stockist_Rep['Worked_With'] = addQuotes(jointWorkString);
                activity_stockist_Rep['location'] = addQuotes(data['location']);
                activity_stockist_Rep['geoaddress'] = '';//data['geoaddress'].replace(/\//g, '\\\/');
                activity_stockist_Rep['stockist_code'] = addQuotes(data['stockist']['selected']['id']);
                activity_stockist_Rep['Stk_Meet_Time'] = addQuotes(dateStr);
                activity_stockist_Rep['modified_time'] = addQuotes(mdateStr);
                pushKey(activity_stockist_Rep, { "Activity_Report_Code": "'Activity_Report_APP'" });
                resultData.push({ 'Activity_Stockist_Report': activity_stockist_Rep });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net") ? spcd : addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        Qty = data['productSelectedList'][i]['rx_qty']; tempOb['Qty'] = (Qty == null) ? 0 : Qty;
                        activity_pob_reports.push(pushKey(tempOb, { "activity_stockist_code": "Activity_Stockist_Report" }));
                    }
                }
                resultData.push({ 'Activity_Stk_POB_Report': activity_pob_reports });
                if (data.giftSelectedList != undefined) {
                    for (var i = 0, len = data.giftSelectedList.length; i < len; i++) {
                        var tempOb = {};
                        var gfcd = data['giftSelectedList'][i]['gift'];
                        tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                        if (Envrmnt == ".Net") tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                        tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                        activity_input_reports.push(pushKey(tempOb, { "activity_stockist_code": "Activity_Stockist_Report" }));
                    }
                }
                resultData.push({ 'Activity_Stk_Sample_Report': activity_input_reports });
                break;
            case "4":
                var activity_ulDoctor_Rep = {};
                activity_ulDoctor_Rep['UnListed_Doctor_POB'] = isNaN(tempD) ? 0 : tempD;
                activity_ulDoctor_Rep['Worked_With'] = addQuotes(jointWorkString);
                activity_ulDoctor_Rep['location'] = addQuotes(data['location']);
                activity_ulDoctor_Rep['geoaddress'] = '';//data['geoaddress'].replace(/\//g, '\\\/');
                activity_ulDoctor_Rep['UnListed_Doc_Meet_Time'] = addQuotes(dateStr);
                activity_ulDoctor_Rep['modified_time'] = addQuotes(mdateStr);

                if (userData.AppTyp == 1) {
                    activity_ulDoctor_Rep['Order_Stk'] = sOrdStk;
                    activity_ulDoctor_Rep['Order_No'] = sOrdNo;
                }
                activity_ulDoctor_Rep['uldoctor_code'] = addQuotes(data['uldoctor']['selected']['id']);
                pushKey(activity_ulDoctor_Rep, { "Activity_Report_Code": "'Activity_Report_APP'" });
                resultData.push({ 'Activity_UnListedDoctor_Report': activity_ulDoctor_Rep });
                if (data.productSelectedList != undefined) {
                    for (var i = 0, len = data['productSelectedList'].length; i < len; i++) {
                        var tempOb = {};
                        var spcd = data['productSelectedList'][i]['product'];
                        tempOb['product_code'] = (Envrmnt == ".Net")?spcd:addQuotes(spcd);
                        if (Envrmnt == ".Net") tempOb['product_Name'] = data['productSelectedList'][i]['product_Nm'];
                        tempOb['Product_Rx_Qty'] = data['productSelectedList'][i]['rx_qty'];
                        tempOb['Product_Sample_Qty'] = data['productSelectedList'][i]['sample_qty'];
                        activity_sample_reports.push(pushKey(tempOb, { "activity_msl_code": "Activity_UnListedDoctor_Report" }));
                    }
                }
                resultData.push({ 'Activity_Unlistedsample_Report': activity_sample_reports });
                data['giftSelectedList'] = data['giftSelectedList'] || [];
                for (var i = 0, len = data['giftSelectedList'].length; i < len; i++) {
                    var tempOb = {};
                    var gfcd = data['giftSelectedList'][i]['gift'];
                    tempOb['Gift_Code'] = (Envrmnt == ".Net") ? gfcd : addQuotes(gfcd);
                    if (Envrmnt == ".Net") tempOb['Gift_Name'] = data['giftSelectedList'][i]['gift_Nm'];
                    tempOb['Gift_Qty'] = data['giftSelectedList'][i]['sample_qty'];
                    activity_input_reports.push(pushKey(tempOb, { "activity_msl_code": "Activity_UnListedDoctor_Report" }));
                }
                resultData.push({ 'activity_unlistedGift_Report': activity_input_reports });
                break;
            default:

                break;
        }

        if (data.photosList != undefined) {
            for (var i = 0, len = data['photosList'].length; i < len; i++) {
                var tempOb = {};
                var imgUrl = data['photosList'][i]['imgData'];
                tempOb['imgurl'] =imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
                tempOb['title'] = addQuotes(data['photosList'][i]['title']);
                tempOb['remarks'] = addQuotes(data['photosList'][i]['remarks']);
                Activity_Event_Captures.push(pushKey(tempOb, { "Activity_Report_Code": "Activity_Report_APP" }));
            }
            //resultData.push({'Activity_Event_Captures': Activity_Event_Captures});
        }
        resultData.push({ 'Activity_Event_Captures': Activity_Event_Captures });
        $http.defaults.useXDomain = true;
        return $http({
            url: baseURL + url + appendDS,
            method: method,
            data: "data=" + JSON.stringify(resultData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }


    return fmcgAPI;
});
fmcgServices.factory('generateUID', function($http) {
    var gUID = {};
    gUID.generate = function(separator) {
        var delim = separator || "-";
        function S4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        return (S4() + S4() + delim + S4() + delim + S4() + delim + S4() + delim + S4() + S4() + S4());
    };
    return gUID;
});
fmcgServices.factory('fmcgLocalStorage', function() {
    var fmcgLocalStorage = {};
    fmcgLocalStorage.addData = function(key, value)
    {
        var predata = window.localStorage.getItem(key) || "[]";
        predata = JSON.parse(predata);
        predata.push(value);
        window.localStorage.setItem(key, JSON.stringify(predata));
        return true;
    }
    fmcgLocalStorage.createData = function(key, value)
    {
        window.localStorage.setItem(key, JSON.stringify(value));
        return true;
    }

    fmcgLocalStorage.getData = function(key) {
        var temp = window.localStorage.getItem(key);
        var ugData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        return ugData;
    }
    fmcgLocalStorage.getItemCount = function(key) {
        var data = window.localStorage.getItem(key);
        if (data == null || data == undefined)
            return 0;
        var count = 0;
        count = JSON.parse(data).length;
        return count;
    }
    fmcgLocalStorage.getEntryCount = function() {
        var data = window.localStorage.getItem("draft");
        var temp = JSON.parse(data);
        var result = {};
        var obj = {'doctor_count': 0,'chemist_count': 0, 'stockist_count': 0, 'uldoctor_count': 0};
        if (temp != null && temp.length > 0)
        {
            if ((temp[0]['worktype']['selected']['FWFlg']).toString() === "F")
            {
                for (var i = 0, len = temp.length; i < len; i++)
                {
                    if(temp[i]['customer'])
                    switch (temp[i]['customer']['selected']['id'])
                    {
                        case "1":
                            obj['doctor_count']++;
                            break;
                        case "2":
                            obj['chemist_count']++;
                            break;
                        case "3":
                            obj['stockist_count']++;
                            break;
                        case "4":
                            obj['uldoctor_count']++;
                            break;
                    }
                }
                result['data'] = obj;
                result['success'] = true;
            }
            else
            {
                result['success'] = false;
                result['data'] = {};
                result['data']['daywise_remarks'] = temp[0]['remarks'];
                result['data']['worktype_code'] = temp[0]['worktype']['selected']['id'];
            }
        }
        else
        {
            result['ndata'] = true;
        }

        return result;
    }

    fmcgLocalStorage.getOutboxCount = function () {
        var data = window.localStorage.getItem("saveLater");
        var temp = JSON.parse(data);
        var result = {};
        var obj = { 'doctor_count': 0, 'chemist_count': 0, 'stockist_count': 0, 'uldoctor_count': 0 };
        if (temp != null && temp.length > 0) {
            if ((temp[0]['worktype']['selected']['FWFlg']).toString() === "F") {
                for (var i = 0, len = temp.length; i < len; i++) {
                    if (temp[i]['customer'])
                        switch (temp[i]['customer']['selected']['id']) {
                            case "1":
                                obj['doctor_count']++;
                                break;
                            case "2":
                                obj['chemist_count']++;
                                break;
                            case "3":
                                obj['stockist_count']++;
                                break;
                            case "4":
                                obj['uldoctor_count']++;
                                break;
                        }
                }
                result['data'] = obj;
                result['success'] = true;
            }
            else {
                result['success'] = false;
                result['data'] = {};
                result['data']['daywise_remarks'] = temp[0]['remarks'];
                result['data']['worktype_code'] = temp[0]['worktype']['selected']['id'];
            }
        }
        else {
            result['ndata'] = true;
        }

        return result;
    }
    return fmcgLocalStorage;
});