var fmcg = angular.module('fmcg', ['ionic', 'fmcgServices', 'ngGrid', 'infinite-scroll', 'flexcalendar', 'pascalprecht.translate','angular.filter'])
.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    $stateProvider
    .state('signin', {
        url: "/sign-in",
        templateUrl: "partials/sign-in.html",
        controller: 'SignInCtrl'
    })
    .state('vacScr', {
        url: "/vacScr",
        templateUrl: "partials/VacPg.html",
        controller: 'VacPgCtrl'
    })
    .state('fmcgmenu', {
        url: "/fmcg",
        abstract: true,
        templateUrl: "partials/sidemenu.html",
        controller: "MainCtrl"
    })
    .state('fmcgmenu.home', {
        url: "/home",
        views: {
            'menuContent': {
                templateUrl: "partials/home.html",
                controller: "homeCtrl"
            }
        }
    })
                    .state('fmcgmenu.stockUpdation', {
                        url: "/stockUpdation",
                        views: {
                            'menuContent': {
                                templateUrl: "partials/stockUpdation.html",
                                controller: "stockUpdationCtrl"
                            }
                        }
                    })
         .state('fmcgmenu.mail', {
             url: "/mail",
             views: {
                 'menuContent': {
                     templateUrl: "partials/mail.html",
                     controller: "mailCtrl"
                 }
             }
         })
         .state('fmcgmenu.sendMail', {
             url: "/sendMail",
             views: {
                 'menuContent': {
                     templateUrl: "partials/sendMail.html",
                     controller: "sendMailCtrl"
                 }
             }
         })
         .state('fmcgmenu.medUpdateUpload', {
             url: "/medUpdateUpload",
             views: {
                 'menuContent': {
                     templateUrl: "partials/medUpdateUpload.html",
                     controller: "medUpdateUploadCtrl"
                 }
             }
         })
    .state('fmcgmenu.reloadMaster', {
        url: "/reload",
        views: {
            'menuContent': {
                templateUrl: "partials/reload.html",
                controller: "reloadCtrl"
            }
        }
    })
    .state('fmcgmenu.RCPA', {
        url: "/RCPA",
        views: {
            'menuContent': {
                templateUrl: "partials/RCPA.html",
                controller: "RCPACtrl"
            }
        }
    })
    .state('fmcgmenu.RMCL', {
        url: "/RMCL",
        views: {
            'menuContent': {
                templateUrl: "partials/CallReminder.html",
                controller: "RMCLCtrl"
            }
        }
    })
    .state('fmcgmenu.precall', {
        url: "/precall",
        views: {
            'menuContent': {
                templateUrl: "partials/PrecallAnalysis.html",
                controller: "precallCtrl"
            }
        }
    })
         .state('fmcgmenu.TourPlanLink', {
             url: "/TourPlanLink",
             views: {
                 'menuContent': {
                     templateUrl: "partials/TourPlanLink.html",
                     controller: "TourPlanLinkCtrl"
                 }
             }
         })
          .state('fmcgmenu.TourPlan', {
              url: "/TourPlan",
              views: {
                  'menuContent': {
                      templateUrl: "partials/TourPlan.html",
                      controller: "TourPlanCtrl"
                  }
              }
          })
         .state('fmcgmenu.ExpenseEntry', {
             url: "/ExpenseEntry",
             views: {
                 'menuContent': {
                     templateUrl: "partials/ExpenseEntry.html",
                     controller: "ExpenseEntryCtrl"
                 }
             }
                })
        .state('fmcgmenu.eExpense', {
                         url: "/eExpense",
                         views: {
                         'menuContent': {
                         templateUrl: "partials/webViewer.html",
                         controller: "eExpenseCtrl"
                         }
                         }
                         })
    .state('fmcgmenu.draft', {
        url: "/draft",
        views: {
            'menuContent': {
                templateUrl: "partials/draft.html",
                controller: "draftCtrl"
            }
        }
    })
    .state('fmcgmenu.outbox', {
        url: "/outbox",
        views: {
            'menuContent': {
                templateUrl: "partials/outbox.html",
                controller: "outboxCtrl"
            }
        }
    })

    .state('fmcgmenu.outboxView', {
        url: "/outboxView?myChoice",
        views: {
            'menuContent': {
                templateUrl: "partials/outboxView.html",
                controller: "outboxViewCtrl"
            }
        }
    })
    .state('fmcgmenu.addNew', {
        url: "/addNew",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-1.html",
                controller: "screen1Ctrl"
            }
        }
    })
    .state('fmcgmenu.screen2', {
        url: "/screen2",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-2.html",
                controller: "screen2Ctrl"
            }
        }
    })
         .state('fmcgmenu.screen2_doc', {
             url: "/screen2_doc",
             views: {
                 'menuContent': {
                     templateUrl: "partials/screen2_doc.html",
                     controller: "screen2docCtrl"
                 }
             }
         })
    .state('fmcgmenu.draftView', {
        url: "/draftView?myChoice",
        views: {
            'menuContent': {
                templateUrl: "partials/draftView.html",
                controller: "draftViewCtrl"
            }
        }
    })
    .state('fmcgmenu.screen3', {
        url: "/screen3",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-3.html",
                controller: "screen3Ctrl"
            }
        }
    })
    .state('fmcgmenu.screen4', {
        url: "/screen4",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-4.html",
                controller: "screen4Ctrl"
            }
        }
    })
    .state('fmcgmenu.screen4s', {
        url: "/screen4s",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-4s.html",
                controller: "screen4sCtrl"
            }
        }
    })
    .state('fmcgmenu.manageDataView', {
        url: "/manageDataView?myChoice",
        views: {
            'menuContent': {
                templateUrl: "partials/manageDataView.html",
                controller: "manageDataViewCtrl"
            }
        }
    })
    .state('fmcgmenu.manageDataTabView', {
        url: "/manageDataTabView?myChoice",
        views: {
            'menuContent': {
                templateUrl: "partials/manageDataTabView.html",
                controller: "manageDataViewCtrl"
            }
        }
    })
    .state('fmcgmenu.manageData', {
        url: "/manageData",
        views: {
            'menuContent': {
                templateUrl: "partials/manageData.html",
                controller: "manageCtrl"
            }
        }
    })
    .state('fmcgmenu.screen5', {
        url: "/screen5",
        views: {
            'menuContent': {
                templateUrl: "partials/screen-5.html",
                controller: "screen5Ctrl"
            }
        }
    })
    .state('fmcgmenu.dcr', {
        url: "/dcrdata",
        views: {
            'menuContent': {
                templateUrl: "partials/MnSummary.html", //dcr.html
                controller: "mnthSummary" //dcrData
            }
        }
    })
    .state('fmcgmenu.dcr1', {
        url: "/dayReport",
        views: {
            'menuContent': {
                templateUrl: "partials/dayReport.html",
                controller: "dayReport" //dcrData1
            }
        }
    })
    .state('fmcgmenu.myPlan', {
        url: "/myPlan",
        views: {
            'menuContent': {
                templateUrl: "partials/myTodayPlan.html",
                controller: "myTodyPlCtrl"
            }
        }
    })
    .state('fmcgmenu.tpview', {
        url: "/tpview",
        views: {
            'menuContent': {
                templateUrl: "partials/tpview.html",
                controller: 'tpviewCtrl'
            }
        }
    })
    .state('fmcgmenu.tpviewdt', {
        url: "/tpviewdt",
        views: {
            'menuContent': {
                templateUrl: "partials/TpViewDt.html",
                controller: 'tpviewdtCtrl'
            }
        }
    })
    .state('fmcgmenu.DayRemarks', {
        url: "/DayRemarks",
        views: {
            'menuContent': {
                templateUrl: "partials/DayActRemks.html",
                controller: "DyRmksCtrl"
            }
        }
    })
    .state('fmcgmenu.addULDoctor', {
        url: "/adduldoctor",
        views: {
            'menuContent': {
                templateUrl: "partials/addData.html",
                controller: "addULDocCtrl"
            }
        }
    })
           .state('fmcgmenu.vaccancyList', {
               url: "/vaccancyList",
               views: {
                   'menuContent': {
                       templateUrl: "partials/vaccancyList.html",
                       controller: "vaccancyList"
                   }
               }
           })
         .state('fmcgmenu.MCL_Details', {
             url: "/MCL_Details",
             views: {
                 'menuContent': {
                     templateUrl: "partials/MCL_Details.html",
                     controller: "MCL_DetailsCtrl"
                 }
             }
         })
    .state('fmcgmenu.addChemist', {
        url: "/addchemist",
        views: {
            'menuContent': {
                templateUrl: "partials/addData.html",
                controller: "addChemistCtrl"
            }
        }
    })
         .state('fmcgmenu.LeaveForm', {
             url: "/LeaveForm",
             views: {
                 'menuContent': {
                     templateUrl: "partials/LeaveForm.html",
                     controller: "LeaveForm"
                 }
             }
         })
         .state('fmcgmenu.ViewLeave', {
             url: "/ViewLeave",
             views: {
                 'menuContent': {
                     templateUrl: "partials/ViewLeave.html",
                     controller: "ViewLeave"
                 }
             }
         })
         .state('fmcgmenu.ViewDCR', {
             url: "/ViewDCR",
             views: {
                 'menuContent': {
                     templateUrl: "partials/ViewDCR.html",
                     controller: "ViewDCR"
                 }
             }
         })
          .state('fmcgmenu.viewTPApproval', {
              url: "/viewTPApproval",
              views: {
                  'menuContent': {
                      templateUrl: "partials/viewTPApproval.html",
                      controller: "viewTPApproval"
                  }
              }
          })
      .state('fmcgmenu.Approvals', {
          url: "/Approvals",
          views: {
              'menuContent': {
                  templateUrl: "partials/Approvals.html",
                  controller: "ApprovalsCtrl"
              }
          }
      })
      .state('fmcgmenu.LeaveApproval', {
          url: "/LeaveApproval",
          views: {
              'menuContent': {
                  templateUrl: "partials/LeaveApproval.html",
                  controller: "LeaveApproval"
              }
          }
      })
         .state('fmcgmenu.DCRApproval', {
             url: "/DCRApproval",
             views: {
                 'menuContent': {
                     templateUrl: "partials/DCRApproval.html",
                     controller: "DCRApproval"
                 }
             }
         })
      .state('fmcgmenu.TPApproval', {
          url: "/TPApproval",
          views: {
              'menuContent': {
                  templateUrl: "partials/TPApproval.html",
                  controller: "TPApproval"
              }
          }
      })
    
    .state('fmcgmenu.geoTag', {
        url: "/geoTag",
        views: {
            'menuContent': {
                templateUrl: "partials/geoTagList.html",
                controller: "geoTagCtrl"
            }
        }
    });
    $urlRouterProvider.otherwise("/sign-in");
}).run(function ($rootScope, $state, $ionicLoading, $ionicPopup) {
    $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
        var allow = ['/addNew', '/screen1', '/screen2', '/screen3', '/screen4', '/screen5']
        if ($rootScope.hasData)
        {
            if (allow.indexOf(toState.url) == -1)
            {

                $rootScope.deleteRecord = true;
                $rootScope.hasData = false;
                $state.go(toState.name);

            }

        }

    });
    $rootScope.$on('$stateChangeSuccess',function (event, toState, toParams, fromState, fromParams) {
        $ionicLoading.hide();
    });
})
.controller('VacPgCtrl', ['$rootScope', '$scope', '$state', function ($rootScope, $scope, $state) {
    $scope.GotoLogin = function () {
        $state.go('signin')
    }
}])
    .controller('SignInCtrl', ['$rootScope', '$scope', '$state', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.appVar="v 5.0.0"

    $scope.callback = function () {};
    $scope.process = false;
    $scope.login = true;
    $scope.error = "";
    var flag = false, temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    //fmcgLocalStorage.createData("events", []);
    if (userData && userData['user'] && userData['user']['valid'])
    {
        $scope.user = {};
        $scope.SFDispName = userData["sfName"] + ' ( ' + userData['user']['name'] + ' )';
        $scope.user['name'] = userData['user']['name'];
        $scope.user['valid'] = userData['user']['valid'];
    }
    $scope.signIn = function (user) {
        var loginInfo = {};
        $scope.process = true;
        $scope.login = false;
        $scope.error = "";
      if (__DevID != '') {
            user.DeviceRegId = localStorage.getItem('registrationId');
            user.AppDeviceRegId = __DevID;
        }else
      {

          $scope.cComputer = isComputer();
          if ($scope.cComputer == true) {
          }
          else {
              window.plugins.uniqueDeviceID.get(function success(uuid) {
                  __DevID = uuid;
              }, function () { });
          }
        }
     
      $scope.cComputer = isComputer();
      if ($scope.cComputer == true) 
          user.AppDeviceRegId = "1fb95de6-c9ca-c70e-3519-620702262428";
        if (userData != null)
        {
            var date2 = new Date();
            var date1 = new Date();

            date1.setTime(userData.lastLogin);
            date1.setUTCHours(0, 0, 0, 0);
            date2.setUTCHours(0, 0, 0, 0);

            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            if ((user.name).toLowerCase() === (userData.user.name).toLowerCase() && (user.password).toLowerCase() === (userData.user.password).toLowerCase() && (userData.user.username).toLowerCase() === (userData.user.current_username).toLowerCase())
            {
                flag = true;
                $state.go('fmcgmenu.home');
            }
            else
            {
                flag = false;
            }

        }
        if (!flag)
        {
            fmcgAPIservice.getPostData('POST', 'login&access=mobile', user).success(function (response) {
                if (response.success)
                {
                    window.localStorage.setItem("attendanceView", response.attendanceView);
                    var loginData = {};
                    var dat = new Date();
                    dat.setUTCHours(0, 0, 0, 0);
                    loginData.lastLogin = dat.getTime();
                    loginData.activeDays = response.days;
                    loginData.sfName = response.sfName;
                    loginData.Attendance = response.Attendance;
                    loginData.sfCode = response.sfCode;
                    loginData.curSFCode = response.sfCode;
                    loginData.desigCode = response.desigCode;
                    loginData.divisionCode = response.divisionCode;
                    loginData.callReport = response.call_report;
                    loginData.call_report_from_date = response.call_report_from_date;
                    loginData.call_report_to_date = response.call_report_to_date;
                    
                    loginData.GEOTagNeed = response.GEOTagNeed;
                  //  if (response.GeoChk == 0) loginData.GEOTagNeed = 1;  //For SANFFA
                    loginData.DisRad = response.DisRad;
                    loginData.SFStat = response.SFStat;
                    loginData.AppTyp = response.AppTyp; // 0 = Pharma. 1 = FMCG
                    loginData.TBase = response.TBase; 	// 0 = Territory Base. 1 = Cluster Base
                    loginData.GeoChk = response.GeoChk;  // 0 = Location Base. 1 = Non Location Base
                    loginData.MCLDet = response.MCLDet;  //0 =Need MCL Details,  1=No need
                    loginData.doctor_dobdow = response.doctor_dobdow;  //0 =Needed,  1=No need
                    if (loginData.GEOTagNeed == 1) loginData.GeoChk = 0;
                    loginData.ChmNeed = response.ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
                    loginData.No_of_TP_View = response.No_of_TP_View;
                    loginData.SFTPDate = response.SFTPDate;
                    loginData.circular = response.circular;
                    loginData.Doc_Pob_Mandatory_Need = response.Doc_Pob_Mandatory_Need;
                    loginData.Chm_Pob_Mandatory_Need = response.Chm_Pob_Mandatory_Need;
                    
					loginData.StkNeed=response.StkNeed;   // 0 = Stockist Needed. 1 = Not Needed
					loginData.UNLNeed=response.UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
                    loginData.DrCap=response.DrCap; 	// Listed Doctor Caption
					loginData.ChmCap=response.ChmCap;   // Chemist Caption
					loginData.StkCap=response.StkCap;   // Stockist Caption
					loginData.NLCap=response.NLCap;     // Unlisted DR. Caption

                    loginData.DPNeed = response.DPNeed; 	// 0 = Doctor Product Neeed. 1 = Not Needed
					loginData.DINeed=response.DINeed;       // 0 = Doctor Input Neeed. 1 = Not Needed
                    loginData.CPNeed = response.CPNeed; 	// 0 = Chemist Product Neeed. 1 = Not Needed
					loginData.CINeed=response.CINeed;       // 0 = Chemist Input Neeed. 1 = Not Needed
                    loginData.SPNeed = response.SPNeed; 	// 0 = Stockist Product Neeed. 1 = Not Needed
					loginData.SINeed=response.SINeed;       // 0 = Stockist Input Neeed. 1 = Not Needed
                    loginData.NPNeed = response.NPNeed; 	// 0 = UnListed Dr. Product Neeed. 1 = Not Needed
					loginData.NINeed=response.NINeed;       // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
					loginData.HlfNeed = response.HlfNeed;   // 0 = Halfday work Not Neeed. 1 >= Needed
                        
					loginData.DRxCap = response.DRxCap;     // Listed Doctor Rx Qty Caption
					loginData.DSmpCap = response.DSmpCap;   // Listed Doctor Sample Qty Caption
					loginData.CQCap = response.CQCap;       // Chemist Qty Caption
					loginData.SQCap = response.SQCap;       // Stockist Qty Caption
					loginData.NRxCap = response.NRxCap;     // UnListed Dr. Rx Qty Caption
					loginData.NSmpCap = response.NSmpCap;   // UnListed Dr. Sample Qty Caption
					user['valid'] = true;
					loginData.user = user;
					loginData.user.username = response.username;
					window.localStorage.setItem("loginInfo", JSON.stringify(loginData));
					flag = true;
					if (loginData.GeoChk == 0 && !$scope.cComputer && cordova.platformId!="ios") {
					    window.sangps.CheckGPS({ "gps": schkGPS });
					}
					if (response.SFStat != undefined && response.SFStat.toString() != '0')
                    {
					    $state.go('vacScr');
					} else {
					    $state.go('fmcgmenu.home');
					}
                }
                else
                {
                    $scope.process = false;
                    $scope.login = true;
                    $scope.error = response.msg;
                }
            }).error(
                function(){
                    Toast("connection failure...");
                    $scope.process = false;                        
                    $scope.login = true;                       
                }
            );
        }
        if (flag == true && diffDays > 0) {
            var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
            var dat = new Date();
            dat.setUTCHours(0, 0, 0, 0);
            loginInfo.lastLogin = dat.getTime();
            window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
            //window.localStorage.removeItem("mypln");

            window.localStorage.removeItem("MyDyRmks");
            window.localStorage.removeItem("MyDyRmksQ");
        }
    };
}])
.controller('MainCtrl', ['$rootScope', '$scope', '$state', '$ionicModal', '$ionicScrollDelegate', '$ionicPopup', '$location', 'fmcgLocalStorage', 'fmcgAPIservice', '$ionicSideMenuDelegate', 'geolocation', '$interval', 'notification', '$timeout', '$ionicLoading', '$http', function ($rootScope, $scope, $state, $ionicModal, $ionicScrollDelegate, $ionicPopup, $location, fmcgLocalStorage, fmcgAPIservice, $ionicSideMenuDelegate, geolocation, $interval, notification, $timeout, $ionicLoading,$http) {
    Exitflag = false;
    $scope.attendanceView = window.localStorage.getItem("attendanceView");
    var temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    var lastMsgShown = window.localStorage.getItem("msg_shown") || undefined;
    $scope.ClikCnt = 0;
    $scope.sendQData = function () {
        if ($scope.ClikCnt >= 3) {
            $scope.ClikCnt = 0;
            Toast("Sending Local Data To Support...");
            var draftData = window.localStorage.getItem("saveLater") || "";
            appendDS = "&sfCode=" + $scope.sfCode + "&msg=QueData";
            $http.defaults.useXDomain = true;
            return $http({
                url: baseURL + 'error/data' + appendDS,
                method: "POST",
                data: "data=" + draftData,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function mySuccess(response) {
                Toast("Data Sent");
            }, function myError(response) {
                console.log("fail");
            });
        }
        $scope.ClikCnt++;
    }
    $scope.$on('uSendQData', function (evnt) {
        $scope.sendQData();
    });
    $scope.view_MR = 0;
    $scope.data = {};
    $scope.attendance = function (update) {
        $scope.data.lat=_currentLocation.Latitude;
        $scope.data.long= _currentLocation.Longitude;
        fmcgAPIservice.addMAData('POST', 'dcr/save&update=' +update, "17", $scope.data).success(function (response) {
            if (response.success) {
                if(update==0)
                    window.localStorage.setItem("attendanceView", 1);
                else
                    window.localStorage.setItem("attendanceView", 0);
                  $scope.attendanceView = window.localStorage.getItem("attendanceView");
                 
                //console.log("TP Approval Submitted Successfully");
              //  $state.go('fmcgmenu.viewTPApproval');
            }
        });
    }
    /*if (__DevID != '') {
        app.push = PushNotification.init({
            "android": {
                "senderID": "582697596611"
            },
        });

        $interval(checkNotification,60000);   //1 minute 60000 millisec
        function checkNotification() {

            app.push.on('notification', function (data) {
                if (!data.additionalData.url) {
                    Toast(data.message);
                    return true;
                }
            });
        }
    }*/
    if (userData.desigCode.toLowerCase() == 'mr') $scope.view_MR = 1;
    //Setup variables Value Assign
    $scope.cDataID = '_' + userData.curSFCode;
    $scope.cMTPDId = '_' + userData.curSFCode;
    $scope.PlanChk = 0;
    $scope.AppEnver = ".Net";
    $scope.sfCode = userData.sfCode;
    $scope.desigCode = userData.desigCode;
    $scope.Attendance = userData.Attendance;
    $scope.GEOTagNeed = userData.GEOTagNeed;

    $scope.DisRad = userData.DisRad;
    $scope.AppTyp = userData.AppTyp;    // 0 = Pharma. 1 = FMCG
    $scope.TBase = userData.TBase;      // 0 = Territory Base. 1 = Cluster Base
    $scope.GeoChk = userData.GeoChk;    // 0 = Location Base. 1 = Non Location Base
    $scope.MCLDet = userData.MCLDet;  //0 =Need MCL Details,  1=No need
    $scope.doctor_dobdow = userData.doctor_dobdow;  //0 =Needed,  1=No need
    $scope.ChmNeed = userData.ChmNeed;  // 0 = Chemist Needed. 1 = Not Needed
    $scope.StkNeed = userData.StkNeed;  // 0 = Stockist Needed. 1 = Not Needed
    $scope.UNLNeed = userData.UNLNeed;  // 0 = Unlisted Needed. 1 = Not Needed
    $scope.DrCap = userData.DrCap;      // Listed Doctor Caption
    $scope.ChmCap = userData.ChmCap;    // Chemist Caption
    $scope.StkCap = userData.StkCap;    // Stockist Caption
    $scope.NLCap = userData.NLCap;      // Unlisted DR. Caption
    $scope.No_of_TP_View = userData.No_of_TP_View;
    $scope.SFTPDate = userData.SFTPDate;
    $scope.circular = userData.circular;
    $scope.Doc_Pob_Mandatory_Need = userData.Doc_Pob_Mandatory_Need;
    $scope.Chm_Pob_Mandatory_Need = userData.Chm_Pob_Mandatory_Need;
    
    $scope.DPNeed = userData.DPNeed;    // 0 = Doctor Product Neeed. 1 = Not Needed
    $scope.DINeed = userData.DINeed;    // 0 = Doctor Input Neeed. 1 = Not Needed
    $scope.CPNeed = userData.CPNeed;    // 0 = Chemist Product Neeed. 1 = Not Needed
    $scope.CINeed = userData.CINeed;    // 0 = Chemist Input Neeed. 1 = Not Needed
    $scope.SPNeed = userData.SPNeed;    // 0 = Stockist Product Neeed. 1 = Not Needed
    $scope.SINeed = userData.SINeed;    // 0 = Stockist Input Neeed. 1 = Not Needed
    $scope.NPNeed = userData.NPNeed;    // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
    $scope.NINeed = userData.NINeed;    // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
    $scope.HlfNeed = userData.HlfNeed;  // 0 = Halfday work Not Neeed. 1 >= Needed

    $scope.DRxCap = userData.DRxCap;    // Listed Doctor Rx Qty Caption
    $scope.DSmpCap = userData.DSmpCap;  // Listed Doctor Sample Qty Caption
    $scope.CQCap = userData.CQCap;      // Chemist Qty Caption
    $scope.SQCap = userData.SQCap;      // Stockist Qty Caption
    $scope.NRxCap = userData.NRxCap;    // UnListed Dr. Rx Qty Caption
    $scope.NSmpCap = userData.NSmpCap;  // UnListed Dr. Sample Qty Caption
    $scope.SFStat = userData.SFStat;
    if (userData.SFStat != undefined && userData.SFStat.toString() != '0') {
        $state.go('vacScr');
    }
    loadSetups = function () {

        var loginData = JSON.parse(localStorage.getItem("loginInfo"));

        var date2 = new Date();
        var date1 = new Date();

        date1.setTime(loginData.lastLogin);
        date1.setUTCHours(0, 0, 0, 0);
        date2.setUTCHours(0, 0, 0, 0);

        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        if (diffDays > 0) {
            var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
            var dat = new Date();
            dat.setUTCHours(0, 0, 0, 0);
            loginInfo.lastLogin = dat.getTime();
            window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
            window.localStorage.removeItem("mypln");
            window.localStorage.removeItem("MyDyRmks");
            window.localStorage.removeItem("MyDyRmksQ");
            
            $state.go('fmcgmenu.myPlan')
            $scope.PlanChk = 1;
        }


        fmcgAPIservice.getDataList('POST', 'get/setup', [])
        .success(function (response) {
            var loginData = JSON.parse(localStorage.getItem("loginInfo"));
            loginData.SFStat = response[0].SFStat;

            loginData.GEOTagNeed = response[0].GEOTagNeed;
            loginData.callReport = response[0].call_report;
            loginData.call_report_from_date = response[0].call_report_from_date;
            loginData.call_report_to_date = response[0].call_report_to_date;
            loginData.DisRad = response[0].DisRad;
            loginData.TBase = response[0].TBase; 	 // 0 = Territory Base. 1 = Cluster Base
            loginData.GeoChk = response[0].GeoChk;   // 0 = Location Base. 1 = Non Location Base
            loginData.MCLDet = response[0].MCLDet;  //0 =Need MCL Details,  1=No need
            loginData.doctor_dobdow = response[0].doctor_dobdow;  //0 =Needed,  1=No need
            
            //if (loginData.GeoChk == 0) loginData.GEOTagNeed = 1;  //For SANFFA
            if (loginData.GEOTagNeed == 1) loginData.GeoChk = 0;
            loginData.ChmNeed = response[0].ChmNeed; // 0 = Chemist Needed. 1 = Not Needed
            loginData.No_of_TP_View = response[0].No_of_TP_View;
            loginData.SFTPDate = response[0].SFTPDate;
            loginData.circular = response[0].circular;
            
            loginData.Doc_Pob_Mandatory_Need = response[0].Doc_Pob_Mandatory_Need;
            loginData.Chm_Pob_Mandatory_Need = response[0].Chm_Pob_Mandatory_Need;
            
            loginData.StkNeed = response[0].StkNeed; // 0 = Stockist Needed. 1 = Not Needed
            loginData.UNLNeed = response[0].UNLNeed; // 0 = Unlisted Needed. 1 = Not Needed
            loginData.DrCap = response[0].DrCap; 	 // Listed Doctor Caption
            loginData.ChmCap = response[0].ChmCap;   // Chemist Caption
            loginData.StkCap = response[0].StkCap;   // Stockist Caption
            loginData.NLCap = response[0].NLCap;     // Unlisted DR. Caption

            loginData.DPNeed = response[0].DPNeed; 	 // 0 = Doctor Product Neeed. 1 = Not Needed
            loginData.DINeed = response[0].DINeed;   // 0 = Doctor Input Neeed. 1 = Not Needed
            loginData.CPNeed = response[0].CPNeed; 	 // 0 = Chemist Product Neeed. 1 = Not Needed
            loginData.CINeed = response[0].CINeed;   // 0 = Chemist Input Neeed. 1 = Not Needed
            loginData.SPNeed = response[0].SPNeed; 	 // 0 = Stockist Product Neeed. 1 = Not Needed
            loginData.SINeed = response[0].SINeed;   // 0 = Stockist Input Neeed. 1 = Not Needed
            loginData.NPNeed = response[0].NPNeed; 	 // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
            loginData.NINeed = response[0].NINeed;   // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
            loginData.HlfNeed = (loginData.desigCode == 'MR') ? response[0].MRHlfDy : response[0].MGRHlfDy;   // 0 = Halfday work Not Neeed. 1 >= Needed

            loginData.DRxCap = response[0].DrRxQCap;     // Listed Doctor Rx Qty Caption
            loginData.DSmpCap = response[0].DrSmpQCap;   // Listed Doctor Sample Qty Caption
            loginData.CQCap = response[0].ChmQCap;       // Chemist Qty Caption
            loginData.SQCap = response[0].StkQCap;       // Stockist Qty Caption
            loginData.NRxCap = response[0].NLRxQCap;     // UnListed Dr. Rx Qty Caption
            loginData.NSmpCap = response[0].NLSmpQCap;   // UnListed Dr. Sample Qty Caption
            attendanceView = window.localStorage.getItem("attendanceView");
            if (attendanceView == 'undefined' || attendanceView == undefined)
                window.localStorage.setItem("attendanceView", 0);
            loginData.Attendance = response[0].Attendance;
            loginData.user.current_username= response[0].UsrDfd_UserName;
            loginData.user.password = response[0].sf_password;
            window.localStorage.setItem("loginInfo", JSON.stringify(loginData));

            $scope.SFStat = loginData.SFStat;
            $scope.Attendance = loginData.Attendance;
            $scope.GEOTagNeed = loginData.GEOTagNeed;
            $scope.DisRad = loginData.DisRad;
            $scope.TBase = loginData.TBase;      // 0 = Territory Base. 1 = Cluster Base
            $scope.GeoChk = loginData.GeoChk;    // 0 = Location Base. 1 = Non Location Base
            $scope.MCLDet = loginData.MCLDet;  //0 =Need MCL Details,  1=No need
            $scope.doctor_dobdow = loginData.doctor_dobdow;  //0 =Needed,  1=No need
            
            $scope.ChmNeed = loginData.ChmNeed;  // 0 = Chemist Needed. 1 = Not Needed
            $scope.StkNeed = loginData.StkNeed;  // 0 = Stockist Needed. 1 = Not Needed
            $scope.UNLNeed = loginData.UNLNeed;  // 0 = Unlisted Needed. 1 = Not Needed
            $scope.DrCap = loginData.DrCap;      // Listed Doctor Caption
            $scope.ChmCap = loginData.ChmCap;    // Chemist Caption
            $scope.StkCap = loginData.StkCap;    // Stockist Caption
            $scope.NLCap = loginData.NLCap;      // Unlisted DR. Caption
            $scope.SFTPDate = loginData.SFTPDate;
            $scope.circular = loginData.circular;
            $scope.Doc_Pob_Mandatory_Need = loginData.Doc_Pob_Mandatory_Need;
            $scope.Chm_Pob_Mandatory_Need = loginData.Chm_Pob_Mandatory_Need;
            $scope.No_of_TP_View = loginData.No_of_TP_View;

            $scope.DPNeed = loginData.DPNeed;    // 0 = Doctor Product Neeed. 1 = Not Needed
            $scope.DINeed = loginData.DINeed;    // 0 = Doctor Input Neeed. 1 = Not Needed
            $scope.CPNeed = loginData.CPNeed;    // 0 = Chemist Product Neeed. 1 = Not Needed
            $scope.CINeed = loginData.CINeed;    // 0 = Chemist Input Neeed. 1 = Not Needed
            $scope.SPNeed = loginData.SPNeed;    // 0 = Stockist Product Neeed. 1 = Not Needed
            $scope.SINeed = loginData.SINeed;    // 0 = Stockist Input Neeed. 1 = Not Needed
            $scope.NPNeed = loginData.NPNeed;    // 0 = UnListed Dr. Product Neeed. 1 = Not Needed
            $scope.NINeed = loginData.NINeed;    // 0 = UnListed Dr. Input Neeed. 1 = Not Needed
            $scope.HlfNeed = loginData.HlfNeed;  // 0 = Halfday work Not Neeed. 1 >= Needed

            $scope.DRxCap = loginData.DRxCap;    // Listed Doctor Rx Qty Caption
            $scope.DSmpCap = loginData.DSmpCap;  // Listed Doctor Sample Qty Caption
            $scope.CQCap = loginData.CQCap;      // Chemist Qty Caption
            $scope.SQCap = loginData.SQCap;      // Stockist Qty Caption
            $scope.NRxCap = loginData.NRxCap;    // UnListed Dr. Rx Qty Caption
            $scope.NSmpCap = loginData.NSmpCap;  // UnListed Dr. Sample Qty Caption

            if (loginData.GeoChk == 0 && !$scope.cComputer && cordova.platformId!="ios") {
                console.log("gps : " + loginData.GeoChk)
                //sangps.intiGPS({ "gps": schkGPS });
                window.sangps.CheckGPS({ "gps": loginData.GeoChk });
            }

            if (loginData.SFStat != undefined && loginData.SFStat.toString() != '0')
            {
                $state.go('vacScr');
            }
        })
        $timeout(function () { loadSetups(); }, 1000*(3600*2));
    }
    $timeout(function () { loadSetups(); }, 100);
    $scope.view_AddUMas = 0;
    if (userData.desigCode.toLowerCase() != 'mr') {
        $scope.view_AddUMas = 1;
        $scope.view_AddMas = 1;
    }
    $scope.loadr = new Array();
    $scope.ErrFnd = new Array();
    $scope.showFlash = function () {

        if (userData.callReport && userData.callReport.length > 0) {
            var dateFrom = userData.call_report_from_date;
            var dateTo = userData.call_report_to_date;

            var date = new Date();
            var dateCheck = ('0' + date.getDate()).slice(-2) + '/' + ('0' + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();

            var d1 = dateFrom.split("/");
            var d2 = dateTo.split("/");
            var c = dateCheck.split("/");

            var from = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11
            var to = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
            var check = new Date(c[2], parseInt(c[1]) - 1, c[0]);
        }
        if (userData.callReport && userData.callReport.length > 0 &&(check >= from && check <= to==true)) 
            $ionicPopup.show({
                title: 'Notification',
                content: userData.callReport,
                scope: $scope,
                buttons: [{ text: 'Close', type: 'button-assertive', onTap: function (e) { return true; } }, ]
            }).then(function (res) {

            }, function (err) {

            }, function (popup) {
                // If you need to access the popup directly, do it in the notify method
                // This is also where you can programatically close the popup:
                // popup.close();
            });
        
    }
    if (!lastMsgShown)
    {
        $scope.showFlash();
        var obj = {};
        var d = new Date();
        obj.date = d.getTime();
        window.localStorage.setItem("msg_shown", JSON.stringify(obj));

    } else {
        var data = JSON.parse(lastMsgShown);
        var date2 = new Date();
        var date1 = new Date();
        date1.setTime(data.date);
        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        if (diffDays > parseInt(1))
        {
            $scope.showFlash();
        }
    }

    $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
    $scope.LeaveTypes = fmcgLocalStorage.getData("LeaveTypes") || [];
    $scope.Folders = fmcgLocalStorage.getData("folders") || [];
    $scope.SubFolders = fmcgLocalStorage.getData("subfolders") || [];
    $scope.mailsStaff = fmcgLocalStorage.getData("mailsSF") || [];
    $scope.gender = [{ "id": "Male", "name": "Male" }, { "id": "Female", "name": "Female" }];
    $scope.preferredTimings = [ { "id": "00.00 to 6.00 am", "name": "00.00 to 6.00 am" }, { "id": "7.00 am to 1.00 pm", "name": "7.00 am to 1.00 pm" }, { "id": "2.00 pm to 7.00 pm", "name": "2.00 pm to 7.00 pm" }, { "id": "above 7pm", "name": "above 7pm" }];
    $scope.preferredDays = [ { "id": "Monday", "name": "Monday" }, { "id": "Tuesday", "name": "Tuesday" }, { "id": "Wednesday", "name": "Wednesday" }, { "id": "Thursday", "name": "Thursday" }, { "id": "Friday", "name": "Friday" }, { "id": "Saturday", "name": "Saturday" }];
    $scope.myTpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
    $scope.clusters = fmcgLocalStorage.getData("town_master" + $scope.cDataID) || [];

    $scope.stockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];
    $scope.allstockists = fmcgLocalStorage.getData("stockist_master" + $scope.cDataID) || [];
    $scope.chemists = fmcgLocalStorage.getData("chemist_master" + $scope.cDataID) || [];
    $scope.doctors = fmcgLocalStorage.getData("doctor_master" + $scope.cDataID) || [];
    $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master" + $scope.cDataID) || [];
    $scope.jointworks = fmcgLocalStorage.getData("salesforce_master" + $scope.cDataID) || [];

    $scope.categorys = fmcgLocalStorage.getData("Doctor_Category") || [];
    $scope.specialitys = fmcgLocalStorage.getData("Doctor_Specialty") || [];
    $scope.class_master = fmcgLocalStorage.getData("class_master") || [];
    $scope.qulifications = fmcgLocalStorage.getData("Qualifications") || [];
    $scope.brands = fmcgLocalStorage.getData("product_master") || [];
    $scope.subordinates = fmcgLocalStorage.getData("subordinate_master") || [];
    $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
    $scope.ProdByCat = [];
    $scope.ProdCategory = fmcgLocalStorage.getData("category_master") || [];
    $scope.products = fmcgLocalStorage.getData("product_master") || [];
    $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
    $scope.allsubs = fmcgLocalStorage.getData("subordinate") || [];
    $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
    $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];

    $scope.lcnt = 0;

    $scope.loadDatas = function (cMod, CDId) {
        if (!CDId) CDId = $scope.cDataID; 
        $scope.myTpTwns = fmcgLocalStorage.getData("town_master" + CDId) || [];
        $scope.clusters = fmcgLocalStorage.getData("town_master" + CDId) || [];
        $scope.stockists = fmcgLocalStorage.getData("stockist_master" + CDId) || [];
        $scope.chemists = fmcgLocalStorage.getData("chemist_master" + CDId) || [];
        $scope.doctors = fmcgLocalStorage.getData("doctor_master" + CDId) || [];
        $scope.uldoctors = fmcgLocalStorage.getData("unlisted_doctor_master" + CDId) || [];
        $scope.jointworks = fmcgLocalStorage.getData("salesforce_master" + CDId) || [];
        $scope.allstockists = fmcgLocalStorage.getData("stockist_master" + CDId) || [];

        $scope.categorys = fmcgLocalStorage.getData("Doctor_Category") || [];
        $scope.specialitys = fmcgLocalStorage.getData("Doctor_Specialty") || [];
        $scope.class_master = fmcgLocalStorage.getData("class_master") || [];
        $scope.qulifications = fmcgLocalStorage.getData("Qualifications") || [];
        $scope.ProdCategory = fmcgLocalStorage.getData("category_master") || [];
        $scope.brands = fmcgLocalStorage.getData("product_master") || [];
        $scope.subordinates = fmcgLocalStorage.getData("subordinate_master") || [];
        $scope.allsubs = fmcgLocalStorage.getData("subordinate") || [];
        $scope.worktypes = fmcgLocalStorage.getData("mas_worktype") || [];
        $scope.products = fmcgLocalStorage.getData("product_master") || [];
        $scope.gifts = fmcgLocalStorage.getData("gift_master") || [];
        $scope.reportTemplates = fmcgLocalStorage.getData("report_template_master") || [];
        $scope.nonreportTemplates = fmcgLocalStorage.getData("nonreport_template_master") || [];
        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        $scope.LeaveTypes = fmcgLocalStorage.getData("LeaveTypes") || [];
        $scope.Folders = fmcgLocalStorage.getData("folders") || [];
        $scope.SubFolders = fmcgLocalStorage.getData("subfolders") || [];
        $scope.mailsStaff = fmcgLocalStorage.getData("mailsSF") || [];
        $scope.preferredTimings = [{ "id": "00.00 to 6.00 am", "name": "00.00 to 6.00 am" }, { "id": "7.00 am to 1.00 pm", "name": "7.00 am to 1.00 pm" }, { "id": "2.00 pm to 7.00 pm", "name": "2.00 pm to 7.00 pm" }, { "id": "above 7pm", "name": "above 7pm" }];
        $scope.preferredDays = [ { "id": "Monday", "name": "Monday" }, { "id": "Tuesday", "name": "Tuesday" }, { "id": "Wednesday", "name": "Wednesday" }, { "id": "Thursday", "name": "Thursday" }, { "id": "Friday", "name": "Friday" }, { "id": "Saturday", "name": "Saturday" }];
        $scope.gender = [{ "id": "Male", "name": "Male" }, { "id": "Female", "name": "Female" }];

        if ($scope.Myplns.length == 0) {
            if (cMod == true) {
                $state.go('fmcgmenu.myPlan')
                $scope.PlanChk = 1;
            }
        } else {
            if (cMod == true) $state.go('fmcgmenu.home');
        }
        $scope.lcnt = 0;
        $ionicLoading.hide();
    }

    $scope.clearIdividual = function (value, total, CDId, cMod) {
        if (!cMod) cMod = false;
        if (!CDId) CDId = $scope.cDataID;      

        var temp = window.localStorage.getItem("loginInfo");
        var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
        $scope.loadr[value] = 1;
        $scope.ErrFnd[value] = 0;

        ReldDta = function (aid) { $scope.lcnt++; $scope.loadr[aid] = 0; if ($scope.lcnt == total) { if ($scope.DataLoaded == false) { Toast('No Internet Connection'); $scope.DataLoaded = true; } $scope.loadDatas(cMod, CDId); } }
        switch (value) {
            case 0:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwDoctor_Master_APP", '["doctor_code as id", "doctor_name as name","town_code","town_name","lat","long","addrs"]', , '["isnull(Doctor_Active_flag,0)=0"]'], CDId.replace(/_/g, ''))
                .success(function (response) {
                    window.localStorage.removeItem("doctor_master" + CDId);
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("doctor_master" + CDId, response);
                    ReldDta(0);
                })
                .error(function () { $scope.ErrFnd[0] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(0); });
                break;
            case 1:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwChemists_Master_APP",'["chemists_code as id", "chemists_name as name","town_code","town_name"]', , '["isnull(Chemist_Status,0)=0"]'], CDId.replace(/_/g, ''))
                .success(function (response) {
                    window.localStorage.removeItem("chemist_master" + CDId);
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("chemist_master" + CDId, response);
                    ReldDta(1);
                })
                .error(function () { $scope.ErrFnd[1] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(1); });
                break;
            case 2:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwstockiest_Master_APP",'["stockiest_code as id", "stockiest_name as name","town_code","town_name"]', , '["isnull(Stockist_Status,0)=0"]'], CDId.replace(/_/g, ''))
                .success(function (response) {
                    window.localStorage.removeItem("stockist_master" + CDId);
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("stockist_master" + CDId, response);
                    ReldDta(2);
                })
                .error(function () { $scope.ErrFnd[2] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(2); });
                break;
            case 3:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwunlisted_doctor_master_APP",'["unlisted_doctor_code as id", "unlisted_doctor_name as name","town_code","town_name"]', , '["isnull(unlisted_activation_flag,0)=0"]'], CDId.replace(/_/g, ''))
                .success(function (response) {
                    window.localStorage.removeItem("unlisted_doctor_master" + CDId);
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("unlisted_doctor_master" + CDId, response);
                    ReldDta(3);
                })
                .error(function () { $scope.ErrFnd[3] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(3); });
                break;
            case 4:
                if (cMod == true || total==1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",'["type_code as id", "Wtype as name"]', , '["isnull(Active_flag,0)=0"]', , , , 0]).success(function (response) {
                        window.localStorage.removeItem("mas_worktype");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("mas_worktype", response);
                        ReldDta(4);
                    })
                    .error(function () { $scope.ErrFnd[4] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(4); });
                } else { ReldDta(4); }
                break;
            case 5:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["product_master", '["product_code as id", "product_name as name", "Product_Sl_No as pSlNo", "Product_Category cateid"]', , '["isnull(Product_DeActivation_Flag,0)=0"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("product_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("product_master", response);
                        ReldDta(5);
                    })
                    .error(function () { $scope.ErrFnd[5] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(5); });
                } else { ReldDta(5); }
                break;
            case 6:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["gift_master", '["gift_code as id", "gift_name as name"]', , '["isnull(Gift_DeActivate_Flag,0)=0"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("gift_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("gift_master", response);
                        ReldDta(6);
                    })
                    .error(function () { $scope.ErrFnd[6] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(6); });
                } else { ReldDta(6); }
                break;
            case 7:
                fmcgAPIservice.getDataList('POST', 'get/jointwork', ["salesforce_master", '["sf_code as id", "sf_name as name"]'], CDId.replace(/_/g, ''))
                .success(function (response) {
                    window.localStorage.removeItem("salesforce_master" + CDId);
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("salesforce_master" + CDId, response);
                    ReldDta(7);
                })
                .error(function () { $scope.ErrFnd[7] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(7); });
                break;
            case 8:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwTown_Master_APP", '["town_code as id", "town_name as name"]', , '["isnull(Town_Activation_Flag,0)=0"]', , , , , , ], CDId.replace(/_/g, ''))
                .success(function (response) {
                    window.localStorage.removeItem("town_master" + CDId);
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("town_master" + CDId, response);
                    ReldDta(8);
                })
                .error(function () { $scope.ErrFnd[8] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(8); });
                break;
            case 9:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwFeedTemplate", '["id as id", "content as name"]', , '["isnull(ActFlag,0)=0"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("report_template_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("report_template_master", response);
                        ReldDta(9);
                    })
                    .error(function () { $scope.ErrFnd[9] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(9); });
                } else { ReldDta(9); }
                break;
            case 10:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwRmksTemplate",'["id as id", "content as name"]', , '["isnull(ActFlag,0)=0"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("nonreport_template_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("nonreport_template_master", response);
                        ReldDta(10);
                    })
                    .error(function () { $scope.ErrFnd[10] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(10); });
                } else { ReldDta(10); }
                break;
            case 11:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype", '["type_code as id", "Wtype as name","Hlfdy_flag as Hlfdy_flag"]', , '["isnull(Active_flag,0)=0","isnull(HalfDyNeed,0)=1"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("halfdayworks");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("halfdayworks", response);
                        ReldDta(11);
                    })
                    .error(function () { $scope.ErrFnd[11] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(11); });
                } else { ReldDta(11); }
                break;
            case 12:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["vwMyDayPlan", '["worktype","FWFlg","sf_member_code as subordinateid","cluster as clusterid","ClstrName","remarks"]', , ])
                    .success(function (response) {
                        window.localStorage.removeItem("mypln");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            window.localStorage.setItem("mypln", JSON.stringify(response));                            
                        ReldDta(12);
                    })
                    .error(function () { $scope.ErrFnd[12] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(12); });
                } else { ReldDta(12); }
                break;
            case 13:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["Doctor_Category", '["Cat_Code as id", "Cat_Name as name","Doc_Cat_SName as Doc_Cat_ShortName"]', , '["isnull(Cat_Flag,0)=0"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("Doctor_Category");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            window.localStorage.setItem("Doctor_Category", JSON.stringify(response));
                        ReldDta(13);
                    })
                    .error(function () { $scope.ErrFnd[13] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(13); });
                } else { ReldDta(13); }
                break;
            case 14:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["Doctor_Specialty", '["Specialty_Code as id", "Specialty_Name as name","Doc_Special_SName as Doc_Spec_ShortName"]', , '["isnull(Deactivate_flag,0)=0"]', , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("Doctor_Specialty");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            window.localStorage.setItem("Doctor_Specialty", JSON.stringify(response));
                        ReldDta(14);
                    })
                    .error(function () { $scope.ErrFnd[14] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(14) });
                } else { ReldDta(14); }
                break;                
            case 15:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'get/subordinate', ["subordinate_master",'["sf_code as id", "sf_name as name"]'])
                    .success(function (response) {
                        window.localStorage.removeItem("subordinate_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("subordinate_master", response);
                        ReldDta(15);
                    })
                    .error(function () { $scope.ErrFnd[15] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(15) });
                } else { ReldDta(15); }
                break;
            case 16:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'get/submgr', ["subordinate", '["sf_code as id", "sf_name as name"]'])
                    .success(function (response) {
                        window.localStorage.removeItem("subordinate");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("subordinate", response);
                        ReldDta(16);
                    })
                    .error(function () { $scope.ErrFnd[16] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(16) });
                } else { ReldDta(16); }
                break;
            case 17:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["category_master", '["Category_Code as id", "Category_Name as name"]', , , , , , 0])
                    .success(function (response) {
                        window.localStorage.removeItem("category_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("category_master", response);
                        ReldDta(17);
                    })
                    .error(function () { $scope.ErrFnd[17] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(17); });
                } else { ReldDta(17); }
                break;
            case 18:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["Mas_Doc_Class", '["Doc_ClsCode as id", "Doc_ClsSName as name"]'])
                    .success(function (response) {
                        window.localStorage.removeItem("class_master");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("class_master", response);
                        ReldDta(18);
                    })
                    .error(function () { $scope.ErrFnd[18] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(18) });
                } else { ReldDta(18); }
                break;
            case 19:
                if (cMod == true || total == 1) {
                    fmcgAPIservice.getDataList('POST', 'table/list', ["Mas_Doc_Qualification", '["sf_code as id", "sf_name as name"]'])
                    .success(function (response) {
                        window.localStorage.removeItem("Qualifications");
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("Qualifications", response);
                        ReldDta(19);
                    })
                    .error(function () { $scope.ErrFnd[19] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(19) });
                } else { ReldDta(19); }
                break;
            case 20:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwLeaveType", '["id","name","Leave_Name"]'])
               .success(function (response) {
                   window.localStorage.removeItem("LeaveTypes");
                   if (response.length && response.length > 0 && Array.isArray(response))
                       fmcgLocalStorage.createData("LeaveTypes", response);
                   ReldDta(20);
               })
               .error(function () { $scope.ErrFnd[20] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(20); });
                break;
            case 21:
                var viewApp = ($scope.view_MR == 1) ? "vwTour_Plan_App" : "vwTourPlan";

                fmcgAPIservice.getDataList('POST', 'table/list', ["vwTourPlan", '["date","remarks","worktype_code","worktype_name","RouteCode","RouteName","RouteCode2","RouteName2","RouteCode3","RouteName3","Worked_with_Code","Worked_with_Name","worktype_code2","worktype_name2","worktype_code3","worktype_name3"]'], CDId.replace(/_/g, ''))
                        .success(function (response) {
                            window.localStorage.removeItem("Tour_Plan_1");
                            window.localStorage.removeItem("Tour_Plan_2");
                            window.localStorage.removeItem("Tour_Plan_3");
                            if (response['next'].length && response['next'].length > 0 && Array.isArray(response['next'])) {
                                 fmcgLocalStorage.createData("Tour_Plan_3", response['next']);
                                
                            }
                            if (response['previous'].length && response['previous'].length > 0 && Array.isArray(response['previous'])) {
                                fmcgLocalStorage.createData("Tour_Plan_1", response['previous']);

                            }
                            if (response['current'].length && response['current'].length > 0 && Array.isArray(response['current'])) {
                                fmcgLocalStorage.createData("Tour_Plan_2", response['current']);
                            }
                            ReldDta(21);
                        })
                        .error(function () {
                            $scope.ErrFnd[21] = 1;
                            cMod = false;
                            $scope.DataLoaded = false;
                            ReldDta(21);
                        });
                break;
            case 22:
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwFolders", '["id", "name"]'])
                .success(function (response) {
                    window.localStorage.removeItem("folders");
                    window.localStorage.removeItem("subfolders");
                    if (response.length && response.length > 0 && Array.isArray(response)) {
                        fmcgLocalStorage.createData("folders", response);
                        var subfolder = response.slice(3);
                        fmcgLocalStorage.createData("subfolders", subfolder);
                    }
                    ReldDta(22);
                })
                .error(function () { $scope.ErrFnd[22] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(22); });
                break;
            case 23:
                fmcgAPIservice.getDataList('POST', 'table/list', ["GetMailSF", '[*]'])
                .success(function (response) {
                    window.localStorage.removeItem("mailsSF");
                    if (response.length && response.length > 0 && Array.isArray(response))
                        fmcgLocalStorage.createData("mailsSF", response);
                    ReldDta(23);
                })
                .error(function () { $scope.ErrFnd[23] = 1; cMod = false; $scope.DataLoaded = false; ReldDta(23); });
                break;
        }

        return true;
    }
    $scope.clearAll = function (cMod, CID) {
        if (!CID) CID = $scope.cDataID;
        n = 24;
        $scope.lcnt = 0;
        $scope.DataLoaded = true;
        for (var i = 0; i < n; i++)
            $scope.clearIdividual(i, n, CID, cMod);
    };

    if ($scope.worktypes.length == 0) {

        $state.go('fmcgmenu.reloadMaster');
        $scope.clearAll(true);
    }
    else
    {
        if ($scope.Myplns.length == 0 && $scope.SFStat == '0') {
            $ionicLoading.show({
                template: 'Validating Your Day Plan.<br>Please Wait...'
            });
            fmcgAPIservice.getDataList('POST', 'table/list', ["vwMyDayPlan", '["worktype","FWFlg","sf_member_code as subordinateid","cluster as clusterid","ClstrName","remarks"]', , ])
            .success(function (response) {
                if (response.length && response.length > 0 && Array.isArray(response))
                    window.localStorage.setItem("mypln", JSON.stringify(response));
                $scope.loadDatas(true);
                $ionicLoading.hide();
            })
            .error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
                $state.go('fmcgmenu.myPlan');
                $scope.PlanChk = 1;
            });
        }
    }
        
    $scope.onLine = isReachable();
    $scope.cComputer = isComputer();
    $scope.vibrate = function () {
        if (!$scope.cComputer)
            notification.vibrate("20");
    }
    $scope.HOURLY_COUNTER = 0;
    $scope.TrackingNeed = 1;
    setInterval(function () {
        if ($scope.TrackingNeed == 1) _userLoginStatus = 1;
        $scope.onLine = isReachable();
        $scope.cComputer = isComputer();
        $scope.HOURLY_COUNTER++;
        if ($scope.cComputer) {
            navigator.geolocation.getCurrentPosition(function (position) {
                if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                    $scope.fmcgData.location = position.coords.latitude + ":" + position.coords.longitude;
                }
                $scope.fmcgData.hasGps = isGpsEnabled();
            });
        }
        else {
            ele = document.querySelector(".ion-location");
            if (ele) ele.classList.remove("available");
            $scope.fmcgData.hasGps = isGpsEnabled(true);
            if ($scope.GeoChk == 0) {
                if (_currentLocation.Latitude != undefined) {
                    var dt = new Date();
                    var dt1 = new Date(_currentLocation.Time + ".000");
                    var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                    var resultInMinutes = Math.round(difference / 60000);
                    if (resultInMinutes > 10) {
                        _currentLocation = {};
                        return false;
                    }
                } else {
                    return false;
                }
            }
            if (_currentLocation.Latitude != undefined) {
                ele = document.querySelector(".ion-location");
                if (ele)
                    ele.classList.add("available");
                $scope.fmcgData.hasGps = isGpsEnabled();
                if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                    $scope.fmcgData.geoaddress = _currentLocation.address;
                    $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                }
            }
        }
    }, 1000);
    $scope.currentSelection = {};
    $scope.FilteredData = {};
    $scope.selectionType = 0;
    $scope.SelMulti = 0;
    $scope.SelMultiAll = 0;
    $scope.SlHlfDys = '';
    $scope.ShowMsg = true;
    $scope.selCate = '';

    $scope.selectedProduct = [];
    $scope.photosList = [];

    $ionicModal.fromTemplateUrl('partials/mapView.html', {
        id: '5',
        scope: $scope,
        animation: 'slide-in-up'
    }).then(function (modal) {
        $scope.modalMap = modal;
    });
    $ionicModal.fromTemplateUrl('partials/openImgLst.html', {
        id: '4',
        scope: $scope,
        animation: 'slide-in-up'
    }).then(function (modal) {
        $scope.modalPhotos = modal;
    });
  
    $ionicModal.fromTemplateUrl('partials/mnuOptions.html', {
        id: '3',
        scope: $scope,
        animation: 'slide-in-down'
    }).then(function (modal) {
        $scope.modalMnu = modal;
    });
   
    $scope.openOptMenu = function () {
        /*$scope.modalMnu.show();
        //$scope.pictureSource =;
        $scope.destinationType = Camera.DestinationType.FILE_URI;*/
        $scope.modalPhotos.show();
        if ($scope.photosList.length < 1) {
            $scope.openCamera()
        }
    }
    $scope.$on('ClearEvents', function () {
        $scope.photosList = [];
    })
    $scope.openCamera = function (source) {
        //$scope.modalPhotos.show();
        navigator.camera.getPicture(function (imgSrc) {
            imgLst = {};
            dt = new Date();
            imgLst.id = dt.getTime();
            imgLst.imgData = imgSrc;
            imgLst.uProgress = 0;
            imgLst.progressVisible = 1;
            imgLst.title = "";
            imgLst.remarks = "";
            $scope.photosList.push(imgLst);
            $timeout(function () { $scope.uploadPhoto(imgLst.id, imgLst.imgData); }, 200);
        }, function (msg) {
        }, { sourceType: 1, quality: 60, destinationType: $scope.destinationType, sourceType: source });
    }
    $scope.updateProgress = function (indx, value) {
        $scope.photosList[indx]["uProgress"] = value.toFixed(0);
    }
    $scope.uploadPhoto = function (imgId, imgData) {
        var imgUrl = imgData;
        item = $scope.photosList.filter(function (a) { return (a.id == imgId); });
        indx = $scope.photosList.indexOf(item[0]);
        var options = new FileUploadOptions();
        options.fileKey = "imgfile";
        options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
        options.mimeType = "image/jpeg";
        options.chunkedMode = false;
        var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
        var ft = new FileTransfer();
        ft.onprogress = function (progress) { $timeout(function () { $scope.updateProgress(indx, ((progress.loaded / progress.total) * 100)) }, 200); }
        ft.upload(imgUrl, uplUrl,
                function (response) {
                    $scope.photosList[indx]["progressVisible"] = 0;
                    console.log("Photo Uploaded....")
                },
                function () {
                }, options);
    }

    $scope.setEvntCap = function () {
        $scope.fmcgData.photosList = angular.copy($scope.photosList);
        $scope.modalMnu.hide();
    }
    $scope.deletePhoto = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
                    if (res) {
                        $scope.photosList.splice($scope.photosList.indexOf(item), 1);
                        Toast("Deleted Successfully...");
                    } else {
                        console.log('You are not sure');
                    }
                }
        );
    };
    $ionicModal.fromTemplateUrl('partials/selectModalProduct.html', {
        id: '1',
        scope: $scope,
        animation: 'slide-in-up'
    }).then(function (modal) {
        $scope.modalProd = modal;
    });
    $ionicModal.fromTemplateUrl('partials/selectModalCluster.html', {
        id: '1',
        scope: $scope,
        animation: 'slide-in-up'
    }).then(function (modal) {
        $scope.modalCluster = modal;
    });
    $ionicModal.fromTemplateUrl('partials/selectModal.html', {
        id:'2',
        scope: $scope,
        animation: 'slide-in-up'
    }).then(function (modal) {
        $scope.modal = modal;
    });
    $ionicModal.fromTemplateUrl('partials/selectModalStaffs.html', {
        id: '1',
        scope: $scope,
        animation: 'slide-in-up'
    }).then(function (modal) {
        $scope.modalStaffs = modal;
    });
    $scope.prdCloseMenu = function () {
        $('ion-side-menu-content').css(ionic.CSS.TRANSFORM, 'translate3d(0px, 0px, 0px)');
        if ($('body').hasClass('menu-open')) $('body').removeClass('menu-open');
    }
    $scope.clstrCloseMenu = function () {
        $('ion-side-menu-content').css(ionic.CSS.TRANSFORM, 'translate3d(0px, 0px, 0px)');
        if ($('body').hasClass('menu-open')) $('body').removeClass('menu-open');
    }
    $scope.filterProd = function (item) {        
        $scope.ProdByCat = {};
        $scope.ShowMsg = true;
        $scope.ProdByCat = $scope.products.filter(function (a) { return (a.cateid == item.id); });

        for (di = 0; di < $scope.ProdByCat.length; di++) {
            itm = $scope.selectedProduct.filter(function (a) { return (a.id == $scope.ProdByCat[di].id); });
            $scope.ProdByCat[di].checked = false;
            if (itm.length > 0) {
                $scope.ProdByCat[di].checked = true;
            }
        }
        $scope.selCate = item.name;
        if ($scope.ProdByCat.length < 1) { $scope.prodmsg = 'No Products Found'; } else {
            $scope.ShowMsg = false;
        }
        $ionicScrollDelegate.scrollTop();
    }

    $scope.roundNumber = function (number, precision) {
        precision = Math.abs(parseInt(precision)) || 0;
        var multiplier = Math.pow(10, precision);
        return (Math.round(number * multiplier) / multiplier);
    }
    $scope.cal_Distns = function (lat1, lon1, lat2, lon2, unit) {
        var radlat1 = Math.PI * lat1 / 180;
        var radlat2 = Math.PI * lat2 / 180;
        var radlon1 = Math.PI * lon1 / 180;
        var radlon2 = Math.PI * lon2 / 180;
        var theta = lon1 - lon2;
        var radtheta = Math.PI * theta / 180;
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        dist = Math.acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        if (unit == "K") { dist = dist * 1.609344 }
        if (unit == "N") { dist = dist * 0.8684 }
        return $scope.roundNumber(dist, 3);
    }

    $scope.openMailStaffs = function (sdata, choice, repId) {
        $scope.sData = sdata;
        $scope.selectionType = choice;
        if (choice == 201)
            data = $scope.fmcgData.staffSelectedList || [];
        //  console.log( $scope.fmcgData.staffSelectedList)
        if (choice == 202)
            data = $scope.fmcgData.staffSelectedListcc || [];
        if (choice == 203)
            data = $scope.fmcgData.staffSelectedListbcc || [];
        for (var di = 0; di < data.length; di++) {
            data[di].checked = true;
            data[di].id = data[di].id
            data[di].name = data[di].name
        }
        $scope.selectedProduct = data
        $scope.prdCloseMenu();
        for (di = 0; di < $scope.sData.length; di++) {
            itm = data.filter(function (a) { return (a.id == $scope.sData[di].id); });
            $scope.sData[di].checked = false;
            if (itm.length > 0) {
                if (itm[0].checked == true) {
                    $scope.sData[di].checked = true;
                }
            }
        }
        $scope.modalStaffs.show();
        $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();

    }
    $scope.openMProduct = function (sdata, choice, repId) {
        if ($scope.selCate == '') {
            //$scope.selCate = 'Select the Category';
            item=$scope.ProdCategory[0];
            $scope.filterProd(item);

        }
       

        $scope.selectionType = choice;
        if (choice == 9) data = $scope.fmcgData.productSelectedList;
        if (choice == 29) data = $scope.RCPA.productSelectedList;
        if (choice == 49) data = $scope.RMCL.productSelectedList;

        for (var di = 0; di < data.length; di++) {
            data[di].checked = true;

            data[di].id = data[di].product
            data[di].name = data[di].product_Nm
        }
        $scope.selectedProduct = data;
        $scope.prdCloseMenu();
        for (di = 0; di < $scope.ProdByCat.length; di++) {
            itm = data.filter(function (a) { return (a.id == $scope.ProdByCat[di].id); });
            $scope.ProdByCat[di].checked = false;
            if (itm.length > 0) {
                if (itm[0].checked == true) {
                    $scope.ProdByCat[di].checked = true;
                }
            }
        }
        $scope.modalProd.show();
        $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();
    }
    $scope.openTPClusterModal = function (sdata, choice, repId, name) {
        sdata = sdata + '_' + repId; //((',102,103,'.indexOf(',' + choice + ',') > -1) ? $scope.cMTPDId : $scope.cDataID);
        $scope.cData = fmcgLocalStorage.getData(sdata) || [];
        $scope.fmcgData.clstrData1 = $scope.fmcgData.clstrData1 || [];
        $scope.fmcgData.clstrData2= $scope.fmcgData.clstrData2 || [];
        tSdta = sdata;
        $scope.selCate = name;
        if (tSdta == 'town_master') { $scope['myTpTwns'] = data; }
        data2 = [];
        data1=$scope.fmcgData.clstrData1;
        $scope.selectionType = choice;
        for (var a = 0; a < data1.length; a++) {
            if (data1[a]['HQId'] == repId)
            data2 = data1[a]['cluster'];
        }

        for (var di = 0; di < data2.length; di++) {
            data2[di].checked = true;
            data2[di].id = data2[di].id
            data2[di].name = data2[di].name
        }
        $scope.selectedProduct = data2;
        repId = $scope.Mypln.subordinate.id;
        $scope.clstrCloseMenu();
        for (di = 0; di < $scope.cData.length; di++) {
            itm = data2.filter(function (a) { return (a.id == $scope.cData[di].id); });
            $scope.cData[di].checked = false;
            if (itm.length > 0) {
                if (itm[0].checked == true) {
                    $scope.cData[di].checked = true;
                }
            }
        }
        
        $scope.modalCluster.show();
        $ionicScrollDelegate.$getByHandle("ProdScroll").scrollTop();


    }
    $scope.openModal = function (sdata, choice, repId) {
        $scope.prdCloseMenu();
        $scope.clstrCloseMenu();
        $scope.modal.show();
        $scope.FilteredData = {};
        tSdta = sdata;
        showmsg_t = false;
        if (typeof (sdata) == 'string') {
            if ((",town_master,stockist_master,chemist_master,doctor_master,unlisted_doctor_master,salesforce_master,").indexOf(',' + sdata + ',') > -1)
                sdata = sdata + '_' + repId; //((',102,103,'.indexOf(',' + choice + ',') > -1) ? $scope.cMTPDId : $scope.cDataID);
            data = fmcgLocalStorage.getData(sdata) || [];        
            if (tSdta == 'town_master') { $scope['myTpTwns'] = data;}
        }
        else {
            data = sdata;
        }
        $scope.selectionType = choice;
        $scope.modal.searchText = '';
        $scope.SelMulti = 0;
        $scope.SelMultiAll = 0;
        var res = [];
        if (",51,61,".indexOf("," + choice + ",") > -1) {
            $scope.SelMultiAll = 1;
        }
        if (",5,6,7,25,26,36,".indexOf("," + choice + ",") > -1)
        {
            $scope.OrderFields = ['name'];
            tCode = '';//$scope.GEOTagNeed=0
            if (",6,".indexOf("," + choice + ",") > -1 && $scope.GEOTagNeed == 1) {
                if ($scope.fmcgData.location == undefined) {
                    Toast("Location not available. Please wait till get Location.")
                    $scope.FilteredData =[];
                    //return false;
                }else{
                    $scope.FilteredData = data.filter(function (a) {
                        var loc = $scope.fmcgData.location.split(':');
                        var crDis = $scope.cal_Distns(loc[0], loc[1], a.lat, a.long, 'K');
                        if (a.lat != '') {
                            a.dis = crDis;
                        }
                        return (crDis <= $scope.DisRad);
                    });
                }
                data = [];
            } else {
         
                if (",4,5,6,7,".indexOf("," + choice + ",") > -1) tCode = $scope.fmcgData.cluster.selected.id;
                if (",25,26,".indexOf("," + choice + ",") > -1) tCode = $scope.RCPA.cluster.selected.id;
                if (",36,".indexOf("," + choice + ",") > -1) tCode = $scope.precall.cluster.selected.id;
               
                if (tCode != '') {
                    $scope.FilteredData = data.filter(function (a) {
                        a.town_code = a.town_code + ',';
                        a.town_code = a.town_code.replace(',,', ',');
                        return ((',' + a.town_code).indexOf(',' + tCode + ',') > -1);
                    });
                }
               
                if ( tCode = '') {
                    showmsg_t = true;
                    ErrMsg = "select the Cluster...";
                }
                if ($scope.FilteredData.length < 1 && tCode != '') {
                    showmsg_t = true;
                    ErrMsg = "No Data in this Cluster...";
                }
                data = [];//data.filter(function (a) { return (a.town_code != $scope.fmcgData.cluster.selected.id); });
            }
        }
        else if (",4,14,".indexOf("," + choice + ",") > -1) {
            $scope.FilteredData=data.filter(function (a) { return (a.town_code === $scope.fmcgData.cluster.selected.id); });
            data=data.filter(function (a) { return (a.town_code != $scope.fmcgData.cluster.selected.id); });
        }
        else if (",8,9,10,29,41,49,51,61,".indexOf("," + choice + ",") > -1) {
            $scope.SelMulti = 1;
            if (choice == 8) slDta = $scope.fmcgData.jontWorkSelectedList;
            if (choice == 51) slDta = $scope.fmcgData.preferredDaysList;
            if (choice == 61) slDta = $scope.fmcgData.preferredTimingsList;
            if (choice == 10) slDta = $scope.fmcgData.giftSelectedList;


            if (choice == 41) slDta = $scope.RMCL.jontWorkSelectedList;
            if (choice == 9) slDta = $scope.fmcgData.productSelectedList;
            if (choice == 29) slDta = $scope.RCPA.productSelectedList;
            if (choice == 49) slDta = $scope.RMCL.productSelectedList;

            for (var di = 0; di < data.length; di++) {
                data[di].checked = false;
                for (var ii = 0; ii < slDta.length; ii++) {
                    if (",8,41,".indexOf("," + choice + ",") > -1) var slDtaid = slDta[ii].jointwork;
                    if (",51,61,".indexOf("," + choice + ",") > -1) {
                     
                        var slDtaid = slDta[ii].preferredId;
                    }
                    if (",9,29,49,".indexOf("," + choice + ",") > -1) var slDtaid = slDta[ii].product;
                    if (choice == 10) var slDtaid = slDta[ii].gift;
                    if (slDtaid == data[di].id) {
                        data[di].checked = true;
                        if (choice == 9) data[di].rx_qty = slDta[ii].rx_qty || 0;
                        if (",29,49,".indexOf("," + choice + ",") > -1) { data[di].qty = slDta[ii].qty || 0; }
                        if (choice == 9 || choice == 10) data[di].sample_qty = slDta[ii].sample_qty || 0;
                    }
                }
            }
            if (",9,29,".indexOf("," + choice + ",") > -1) $scope.OrderFields = ['pSlNo', 'name']; else $scope.OrderFields = ['name'];
        }
        else if (choice == 3) {
            $scope.OrderFields = ['id'];
        }
        else {
            $scope.OrderFields = ['name'];
            //data=data.sort(function(d1,d2){return  d1.name.toLowerCase()>d2.name.toLowerCase() ? 1 : (d1.name.toLowerCase()<d2.name.toLowerCase()) ? -1 : 0});                
        }
        $scope.currentSelection = data;
        $ionicScrollDelegate.$getByHandle("optsScroll").scrollTop();
            
        $timeout(function(){$("#tSearch").focus();}, 800);
    };
    $scope.closeModal = function () {
        $scope.modal.hide();
    };
    $scope.setSelProduct = function (item) {
        
        $scope.selectedProduct = $scope.selectedProduct.filter(function (a) { return(a.id != item.id) })
        if (item.checked == true) {
            $scope.selectedProduct.push(item);
        }
    }

    $scope.svSelect=function(CurrentData)
    {
        var SlTyp = $scope.selectionType;
        if (SlTyp == 8) $scope.fmcgData.jontWorkSelectedList = [];
        if (SlTyp == 51) {
            $scope.fmcgData.preferredDaysList = [];
            $scope.fmcgData.preferredDaysList.preferredIds = '';
            $scope.fmcgData.preferredDaysList.preferredNames = '';
        }
        if (SlTyp == 61) {
            $scope.fmcgData.preferredTimingsList = [];
            $scope.fmcgData.preferredTimingsList.preferredIds = '';
            $scope.fmcgData.preferredTimingsList.preferredNames = '';
        }
        if (SlTyp == 9) $scope.fmcgData.productSelectedList = [];
        if (SlTyp == 19) {
            repId = $scope.Mypln.subordinate.selected.id;
            repName = $scope.Mypln.subordinate.name;
           
            clstrData1 = [];
            $scope.fmcgData.Mypln = {};
            $scope.fmcgData.Mypln.TPEntry =$scope.fmcgData.Mypln.TPEntry|| [];

            $scope.fmcgData.selectedHQ = $scope.fmcgData.selectedHQ || [];
                $a = 1;
                for (var c = 0; c < $scope.fmcgData.selectedHQ.length; c++) {
                    if ($scope.fmcgData.selectedHQ[c]['id'] == $scope.Mypln.subordinate.selected.id) {
                        $a = 0;
                    }
                }
                if ($a != 0) {
                    hq = { "id": $scope.Mypln.subordinate.selected.id, "name": $scope.Mypln.subordinate.name };
                    $scope.fmcgData.selectedHQ.push(hq);
                }
                for (h = 0; h < $scope.fmcgData.selectedHQ.length; h++) {
                    $scope.fmcgData.Mypln.TPEntry.HQIds += $scope.fmcgData.selectedHQ[h].id + ', ';
                    $scope.fmcgData.Mypln.TPEntry.HQNms += $scope.fmcgData.selectedHQ[h].name + ', ';
                }
        }
        if (SlTyp == 29) { $scope.RCPA.productSelectedList = []; $scope.RCPA.OurBrndNms = ''; $scope.RCPA.OurBrndCds = ""; }
        if (SlTyp == 49) { $scope.RMCL.productSelectedList = []; $scope.RMCL.ProdNms = ''; $scope.RMCL.ProdCds = ""; }
        if (SlTyp == 41) { $scope.RMCL.jontWorkSelectedList = []; $scope.RMCL.JntWrkNms = ''; $scope.RMCL.JntWrkCds = ""; }
        if (SlTyp == 201) {
            $scope.fmcgData.staffSelectedList = []; $scope.fmcgData.staffNms = ''; $scope.fmcgData.staffIds = "";
        }
        if (SlTyp == 202) {
            $scope.fmcgData.staffSelectedListcc = []; $scope.fmcgData.ccstaffNms = ''; $scope.fmcgData.ccstaffIds = "";
        }
        if (SlTyp == 203) {
            $scope.fmcgData.staffSelectedListbcc = []; $scope.fmcgData.bccstaffNms = ''; $scope.fmcgData.bccstaffIds = "";
        }
        if (SlTyp == 10) $scope.fmcgData.giftSelectedList = [];

        for (var i = 0; i < CurrentData.length; i++)
        {
            if(CurrentData[i].checked==true)
            {
                if (",8,41,".indexOf("," + SlTyp + ",") > -1)
                {
                    var jontWorkData = {};
                    jontWorkData.jointwork = CurrentData[i].id;
                    jontWorkData.jointworkname = CurrentData[i].name;
                    if (SlTyp == 8) $scope.fmcgData.jontWorkSelectedList.push(jontWorkData);
                    if (SlTyp == 41) {
                        $scope.RMCL.JntWrkCds += CurrentData[i].id + ', ';
                        $scope.RMCL.JntWrkNms += CurrentData[i].name + ', ';
                        $scope.RMCL.jontWorkSelectedList.push(jontWorkData);
                    }
                }
               else if (",51,61,".indexOf("," + SlTyp + ",") > -1) {
                    var preferredData = {};
                    preferredData.preferredId= CurrentData[i].id;
                    preferredData.preferredName = CurrentData[i].name;
                    
                    if (SlTyp == 51) {
                        $scope.fmcgData.preferredDaysList.preferredIds += CurrentData[i].id + '/';
                        $scope.fmcgData.preferredDaysList.preferredNames += CurrentData[i].name + ',';
                        $scope.fmcgData.preferredDaysList.push(preferredData);
                    }
                    if (SlTyp == 61) {
                        $scope.fmcgData.preferredTimingsList.preferredIds += CurrentData[i].id + '/';
                        $scope.fmcgData.preferredTimingsList.preferredNames += CurrentData[i].name + ',';
                        $scope.fmcgData.preferredTimingsList.push(preferredData);
                    }
                }
                else if (",9,29,49,19,".indexOf("," + SlTyp + ",") > -1) {
                    var productData = {};
                    if (SlTyp == 19) {
                        productData.id = CurrentData[i].id;
                        productData.name = CurrentData[i].name;
                    }
                    else {
                        productData.product = CurrentData[i].id;
                        productData.product_Nm = CurrentData[i].name;
                    }
                    if (SlTyp == 9) {
                        productData.rx_qty = CurrentData[i].rx_qty || 0;
                        productData.sample_qty = CurrentData[i].sample_qty || 0;
                        $scope.fmcgData.productSelectedList.push(productData);
                    }
                    if (SlTyp == 19) {
                        clstrData1.push(productData);
                    }
                    if (SlTyp == 29) {
                        $scope.RCPA.OurBrndCds += CurrentData[i].id + ', ';
                        $scope.RCPA.OurBrndNms += CurrentData[i].name + ', ';
                        productData.qty = CurrentData[i].qty || 0;
                        $scope.RCPA.productSelectedList.push(productData);
                    }
                    if (SlTyp == 49) {
                        $scope.RMCL.ProdCds += CurrentData[i].id + ', ';
                        $scope.RMCL.ProdNms += CurrentData[i].name + ', ';
                        productData.qty = CurrentData[i].qty || 0;
                        $scope.RMCL.productSelectedList.push(productData);
                    }
                }
                else if (",201,202,203,".indexOf("," + SlTyp + ",") > -1) {
                    var staffData = {};
                    staffData.id = CurrentData[i].id;
                    staffData.name = CurrentData[i].name;
                    if (SlTyp == 201) {
                        $scope.fmcgData.staffIds += CurrentData[i].id + ', ';
                        $scope.fmcgData.staffNms += CurrentData[i].name + ', ';
                        $scope.fmcgData.staffSelectedList.push(staffData);

                    }
                    if (SlTyp == 202) {
                        $scope.fmcgData.ccstaffIds += CurrentData[i].id + ', ';
                        $scope.fmcgData.ccstaffNms += CurrentData[i].name + ', ';
                        $scope.fmcgData.staffSelectedListcc.push(staffData);

                    }
                    if (SlTyp == 203) {
                        $scope.fmcgData.bccstaffIds += CurrentData[i].id + ', ';
                        $scope.fmcgData.bccstaffNms += CurrentData[i].name + ', ';
                        $scope.fmcgData.staffSelectedListbcc.push(staffData);

                    }
                }
                else if (",10,".indexOf("," + SlTyp + ",") > -1) {
                    var giftData = {};
                    giftData.gift = CurrentData[i].id;
                    giftData.gift_Nm = CurrentData[i].name;
                    giftData.sample_qty = CurrentData[i].sample_qty || 1;
                    $scope.fmcgData.giftSelectedList.push(giftData);
                }
            }
        }
        if (",9,29,49,".indexOf("," + SlTyp + ",") > -1) {
            $scope.modalProd.hide();
        }
        else if (",201,202,203,".indexOf("," + SlTyp + ",") > -1)
            $scope.modalStaffs.hide();
        else if (",19,".indexOf("," + SlTyp + ",") > -1) {
            clusters = clstrData1;
            
            $scope.fmcgData.clstrData1 = $scope.fmcgData.clstrData1 || [];
            $scope.fmcgData.clstrData2 = $scope.fmcgData.clstrData2 || [];

            for (var i = $scope.fmcgData.clstrData1.length - 1; i >= 0; i--) {
                if ($scope.fmcgData.clstrData1[i]['HQId'] == repId)
                    $scope.fmcgData.clstrData1.splice(i, 1);
            }
            v = 0;
            for (var i = $scope.fmcgData.clstrData2.length - 1; i >= 0; i--) {
                if ($scope.fmcgData.clstrData2[i]['HQId'] == repId) {
                    v = 1;
                     $scope.fmcgData.clstrData2.splice(i, 1);
                }
            }
               $scope.modalCluster.hide();
               $scope.fmcgData.clstrData1.push({ "HQId": repId, "HQNm": repName, "cluster": clstrData1 });
               CLIds = ' ';
               CLNms = '';
               for (j = 0; j < clusters.length; j++) {
                   CLIds += clusters[j].id + ', ';
                   CLNms += clusters[j].name + ', ';
               }
              
             
               if (v == 0)
                   $scope.fmcgData.clstrData2.push({ "HQ": repName, "HQId": repId, "Cluster": CLNms, "ClusterId": CLIds })
               else {
                   setTimeout(function () {
                       $scope.fmcgData.clstrData2.push({ "HQ": repName, "HQId": repId, "Cluster": CLNms, "ClusterId": CLIds })
                   }, 10);
               }
             
        }
        else {

            $scope.modal.hide();
        }
            
    }
    $scope.selectPreferredData = function () {
        choice = $scope.selectionType;
        if ($scope.selectionType == 51) {
            $scope.fmcgData.preferredDaysList = $scope.preferredDays;
            data = $scope.fmcgData.preferredDaysList;
        }
        if ($scope.selectionType == 61) {
            $scope.fmcgData.preferredTimingsList = $scope.preferredTimings;
            data = $scope.fmcgData.preferredTimingsList;
        }
        for (var ii = 0; ii < data.length; ii++) {
             
                    data[ii].checked = true;
                   
        }
       // $ionicScrollDelegate.$getByHandle("optsScroll").scrollBottom();
    $scope.currentSelection = data;
   
    }
    $scope.selectData = function (item) {
        $rootScope.hasData=true;
        var key = ['worktype', 'cluster', 'customer', 'stockist', 'chemist', 'doctor', 'uldoctor', 'jointwork', 'worktype2', 'cluster2','worktype3', 'cluster3'];
        switch ($scope.selectionType)
        {
            case 1:
            case 101:
            case 102:
          
            default:

                if (typeof($scope.fmcgData.customer)!="undefined")
                {
                    if($scope.selectionType==3 && $scope.fmcgData.customer.selected.id.toString() != item.id.toString())
                    {
                        $rootScope.deleteRecord=true;
                        $scope.clearData();
                        $scope.data = {};
                        $scope.fmcgData.worktype = {};
                        $scope.fmcgData.worktype.selected = {};
                        var cus = item.id;
                        var CusTyp = (cus == 1) ? "D" : (cus == 2) ? "C" : (cus == 3) ? "S" : (cus == 4) ? "U" : "R";
                        var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function (a) { return (a.id == $scope.prvRptId) }) : $scope.worktypes.filter(function (a) { return (a.FWFlg == "F" && a.ETabs.indexOf(CusTyp)>-1) });
                        $scope.fmcgData.worktype.selected = Wtyps[0];   //{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
                        if ($scope.view_MR == 1)
                        {
                            $scope.fmcgData.subordinate = {};
                            $scope.fmcgData.subordinate.selected = {};
                            $scope.fmcgData.subordinate.selected.id = $scope.sfCode;
                        }
                    }
                }
                if ($scope.selectionType == 200) {
                    $scope.folder = {};
                    $scope.folder.name = item.name;
                    $scope.folder.selected = {};
                    $scope.folder.selected.id = item.id;
                    $scope.$broadcast('getFolders', { Details: item });
                }
                if ($scope.selectionType == 201) {
                    $scope.subfolder = {};
                    $scope.subfolder.name = item.name;
                    $scope.subfolder.selected = {};
                    $scope.subfolder.selected.id = item.id;
                    $scope.$broadcast('moveFolders', { Details: item });
                }
                if ($scope.selectionType > 100) {
                    $scope.Mypln[key[$scope.selectionType - 101]] = {};
                    $scope.Mypln[key[$scope.selectionType - 101]].name = item.name;
                    $scope.Mypln[key[$scope.selectionType - 101]].selected = {};
                    $scope.Mypln[key[$scope.selectionType - 101]].selected.id = item.id;
                    if ($scope.selectionType == 113) {
                        $scope.Mypln.cluster3= {};
                        $scope.Mypln.cluster3.name = item.name;
                        $scope.Mypln.cluster3.selected = {};
                        $scope.Mypln.cluster3.selected.id = item.id;
                    }
                    if ($scope.selectionType == 101) $scope.Mypln[key[0]].selected.FWFlg = item.FWFlg;
                    if ($scope.selectionType == 109) $scope.Mypln[key[8]].selected.FWFlg = item.FWFlg;
                    if ($scope.selectionType == 111) $scope.Mypln[key[10]].selected.FWFlg = item.FWFlg;

                }
                else if ($scope.selectionType > 40) {
                    $scope.RMCL[key[$scope.selectionType - 41]] = {};
                    $scope.RMCL[key[$scope.selectionType - 41]].name = item.name;
                    $scope.RMCL[key[$scope.selectionType - 41]].selected = {};
                    $scope.RMCL[key[$scope.selectionType - 41]].selected.id = item.id;

                    $scope.stockUpdation[key[$scope.selectionType - 41]] = {};
                    $scope.stockUpdation[key[$scope.selectionType - 41]].name = item.name;
                    $scope.stockUpdation[key[$scope.selectionType - 41]].selected = {};
                    $scope.stockUpdation[key[$scope.selectionType - 41]].selected.id = item.id;

                    if ($scope.selectionType == 42) {
                        $scope.LeaveTypes.type = {};
                        $scope.LeaveTypes.type.name = item.name;
                        $scope.LeaveTypes.type.selected= {};
                        $scope.LeaveTypes.type.selected.id = item.id;
                    }
                    if ($scope.selectionType == 44) {
                        stat = 0;
                        value = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                        for (key in value) {
                            if (value[key]['stockistCode'] == $scope.stockUpdation.stockist.selected.id) {
                                stat = 1;
                                key1 = key;
                            }
                        }
                        if (stat == 1)
                            $scope.LastUpdationDate = value[key1]['Last_Updation_Date'];
                        else
                            $scope.LastUpdationDate = '';
                        $scope.$broadcast('GetStocks');
                    }
                }
                else if ($scope.selectionType >30) {
                    $scope.precall[key[$scope.selectionType - 31]] = {};
                    $scope.precall[key[$scope.selectionType - 31]].name = item.name;
                    $scope.precall[key[$scope.selectionType - 31]].selected = {};
                    $scope.precall[key[$scope.selectionType - 31]].selected.id = item.id;
                    if ($scope.selectionType == 36) $scope.$broadcast('getPreCall', { DrId: item.id });
                }
                else if ($scope.selectionType > 20) {
                    if ($scope.selectionType == 22){
                        if ($scope.RCPA.cluster==undefined || $scope.RCPA.cluster.selected.id != item.id) {
                            $scope.RCPA.chemist = undefined;
                            $scope.RCPA.doctor = undefined;
                        }
                    }
                    $scope.RCPA[key[$scope.selectionType - 21]] = {};
                    $scope.RCPA[key[$scope.selectionType - 21]].name = item.name;
                    $scope.RCPA[key[$scope.selectionType - 21]].selected = {};
                    $scope.RCPA[key[$scope.selectionType - 21]].selected.id = item.id;
                }
                else
                {
                    $scope.fmcgData[key[$scope.selectionType - 1]] = {};
                    $scope.fmcgData[key[$scope.selectionType - 1]].name = item.name;
                    $scope.fmcgData[key[$scope.selectionType - 1]].selected = {};
                    $scope.fmcgData[key[$scope.selectionType - 1]].selected.id = item.id;
                    if ($scope.selectionType == 1) $scope.fmcgData[key[0]].selected.FWFlg = item.FWFlg;
                    if ($scope.selectionType == 6) {
                        $scope.fmcgData.visitingcardshow = 1;
                        $scope.$broadcast('getDocDetails', { DrId: item.id });
                    }
                    if ($scope.selectionType == 2)
                    $scope.$broadcast('getDoctors', { ClusterId: item.id });
                }
                    
                var temp = [4, 5, 6, 7];
                if (temp.indexOf($scope.selectionType) !== -1 && $scope.fmcgData.source === 0)
                {
                    $scope.fmcgData.cluster = {};
                    $scope.fmcgData.cluster.name = item.town_name;
                    $scope.fmcgData.cluster.selected = {};
                    $scope.fmcgData.cluster.selected.id = item.town_code;
                }
                break;
            case 8:
                var jontWorkData = {};
                jontWorkData.jointwork = item.id;
                jontWorkData.jointworkname = item.name;
                $scope.fmcgData.jontWorkSelectedList = $scope.fmcgData.jontWorkSelectedList || [];
                var len = $scope.fmcgData.jontWorkSelectedList.length;
                var flag = true;
                for (var i = 0; i < len; i++)
                {
                    if ($scope.fmcgData.jontWorkSelectedList[i]['jointwork'] === item.id)
                    {
                        flag = false;
                    }
                }
                if (flag)
                    $scope.fmcgData.jontWorkSelectedList.push(jontWorkData);
                else
                    Toast("JointWork Already Added", true);
                break;
            case 9:
                var productData = {};
                $scope.fmcgData.productSelectedList = $scope.fmcgData.productSelectedList || [];
                productData.product = item.id;
                productData.product_Nm = item.name;
                productData.rx_qty = 0;
                productData.sample_qty = 0;
                var len = $scope.fmcgData.productSelectedList.length;
                var flag = true;
                for (var i = 0; i < len; i++)
                {
                    if ($scope.fmcgData.productSelectedList[i]['product'] === item.id)
                    {
                        flag = false;
                    }
                }
                if (flag)
                    $scope.fmcgData.productSelectedList.push(productData);
                else
                    Toast("Product Already Added", true);
                break;
            case 10:
                var giftData = {};
                $scope.fmcgData.giftSelectedList = $scope.fmcgData.giftSelectedList || [];
                giftData.gift = item.id;
                giftData.gift_Nm = item.name;
                giftData.sample_qty = 1;
                var len = $scope.fmcgData.giftSelectedList.length;
                var flag = true;
                for (var i = 0; i < len; i++)
                {
                    if ($scope.fmcgData.giftSelectedList[i]['gift'] === item.id)
                    {
                        flag = false;
                    }
                }
                if (flag)
                    $scope.fmcgData.giftSelectedList.push(giftData);
                else
                    Toast("Input Already Added", true);
                break;
            case 19:
               // alert()
              //  var clstrData = {};
              /* repId = $scope.Mypln.subordinate.selected.id;
               $scope.fmcgData.clstrData[repId ]= $scope.fmcgData.clstrData[repId ]|| [];
                clstrData.repId.id = item.id;
                clstrData.repId.name = item.name;
                var len = $scope.fmcgData.clstrData.repId.length;
                var flag = true;
                for (var i = 0; i < len; i++) {
                    if ($scope.fmcgData.clstrData.repId[i]['id'] === item.id) {
                        flag = false;
                    }
                }
                if (flag)
                    $scope.fmcgData.clstrData.repId.push(clstrData);
                else
                    Toast("Cluster Already Added", true);*/
                break;
            case 12:
                $scope.fmcgData.rx_t = item.id;
                break;
            case 13:
                $scope.fmcgData.nrx_t = item.id;
                break;
            case 14: 
                $scope.fmcgData.OrdStk = {};
                $scope.fmcgData.OrdStk.name = item.name;
                $scope.fmcgData.OrdStk.selected = {};
                $scope.fmcgData.OrdStk.selected.id = item.id;
                break;
            case 15:
                $scope.fmcgData.remarks = item.name;
                break;
            case 112:
                $scope.fmcgData.qulification = {};
                $scope.fmcgData.qulification.name = item.name;
                $scope.fmcgData.qulification.selected = {};
                $scope.fmcgData.qulification.selected.id = item.id;
                break;
            case 113:
                $scope.fmcgData.class = {};
                $scope.fmcgData.class.name = item.name;
                $scope.fmcgData.class.selected = {};
                $scope.fmcgData.class.selected.id = item.id;
                break;
            case 115:
                $scope.fmcgData.gender = {};
                $scope.fmcgData.gender.name = item.name;
                $scope.fmcgData.gender.selected = {};
                $scope.fmcgData.gender.selected.id = item.id;
                break;
            case 16: case 17: case 18: case 28: case 38: case 40: case 50: case 52: case 58: case 48: case 103: case 208:
                var key={"c16":"speciality","c17":"category","c18":"subordinate"};

                if ($scope.selectionType == 50) {
                    $scope.tpview[key['c18']] = {};
                    $scope.tpview[key['c18']].name = item.name;
                    $scope.tpview[key['c18']].selected = {};
                    $scope.tpview[key['c18']].selected.id = item.id;
                    $scope.$broadcast('eGetTpEntry');
                    
                }
                else if ($scope.selectionType == 208) {
                    $scope.Reload[key['c18']] = {};
                    $scope.Reload[key['c18']].name = item.name;
                    $scope.Reload[key['c18']].selected = {};
                    $scope.Reload[key['c18']].selected.id = item.id;
                }
                else if ($scope.selectionType == 58) {
                    $scope.geoTag[key['c18']] = {};
                    $scope.geoTag[key['c18']].name = item.name;
                    $scope.geoTag[key['c18']].selected = {};
                    $scope.geoTag[key['c18']].selected.id = item.id;
                    $scope.geoTag['cluster'] = {};
                }
                else if ($scope.selectionType == 52) {
                    $scope.geoTag['cluster'] = {};
                    $scope.geoTag['cluster'].name = item.name;
                    $scope.geoTag['cluster'].selected = {};
                    $scope.geoTag['cluster'].selected.id = item.id;
                    $scope.$broadcast('setGeoData');
                }
                else if ($scope.selectionType == 103)
                {                    
                    $scope.Mypln[key['c18']] = {};
                    $scope.Mypln[key['c18']].name = item.name;
                    $scope.Mypln[key['c18']].selected = {};
                    $scope.Mypln[key['c18']].selected.id = item.id;
               
                    $scope.Mypln.cluster = {};
                }
                else if ($scope.selectionType == 40) {
                    $scope.MnthSumm[key['c18']] = {};
                    $scope.MnthSumm[key['c18']].name = item.name;
                    $scope.MnthSumm[key['c18']].selected = {};
                    $scope.MnthSumm[key['c18']].selected.id = item.id;

                    $scope.$broadcast('getMonthReport'); 
                }
                else if ($scope.selectionType == 38) {

                    $scope.precall[key['c18']] = {};
                    $scope.precall[key['c18']].name = item.name;
                    $scope.precall[key['c18']].selected = {};
                    $scope.precall[key['c18']].selected.id = item.id;
                }
                else if ($scope.selectionType == 48) {
                    $scope.RMCL[key['c18']] = {};
                    $scope.RMCL[key['c18']].name = item.name;
                    $scope.RMCL[key['c18']].selected = {};
                    $scope.RMCL[key['c18']].selected.id = item.id;
                    $scope.RMCL.doctor = undefined;
                }
                else if ($scope.selectionType == 28)
                {

                    $scope.RCPA[key['c18']] = {};
                    $scope.RCPA[key['c18']].name = item.name;
                    $scope.RCPA[key['c18']].selected = {};
                    $scope.RCPA[key['c18']].selected.id = item.id;

                    $scope.RCPA.chemist = undefined;
                    $scope.RCPA.doctor = undefined;
                }
                else
                {
                    $scope.fmcgData[key['c'+$scope.selectionType]] = {};
                    $scope.fmcgData[key['c'+$scope.selectionType]].name = item.name;
                    $scope.fmcgData[key['c'+$scope.selectionType]].selected = {};
                    $scope.fmcgData[key['c' + $scope.selectionType]].selected.id = item.id;
                    if ($scope.selectionType == 16) 
                        $scope.fmcgData[key['c' + $scope.selectionType]].Doc_Spec_ShortName = item.Doc_Spec_ShortName;
                    if ($scope.selectionType == 17)
                        $scope.fmcgData[key['c' + $scope.selectionType]].Doc_Cat_ShortName = item.Doc_Cat_ShortName;

                    if ($scope.selectionType == 18) {
                        $scope.fmcgData.stockist = undefined;
                        $scope.fmcgData.chemist = undefined;
                        $scope.fmcgData.doctor = undefined;
                        $scope.fmcgData.uldoctor = undefined;

                        $scope.fmcgData.cluster = undefined;
                    }
                }
                if (',18,28,38,48,103,'.indexOf(','+$scope.selectionType+',')>-1)
                {
                   
                    var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
                    loginInfo.curSFCode = item.id;
                    $scope.fmcgData.jontWorkSelectedList = [];
                    window.localStorage.setItem("loginInfo", JSON.stringify(loginInfo));
                    var TwnDet = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                    if ($scope.selectionType == 103) {
                        $scope.cMTPDId = '_' + item.id;
                        //$scope.myTpTwns =  fmcgLocalStorage.getData("town_master_" + item.id) || [];
                    } else {
                        $scope.cDataID = '_' + item.id;
                        //$scope.clusters = fmcgLocalStorage.getData("town_master_" + item.id) || [];
                    }
                    if (TwnDet.length > 0) {
                        $scope.loadDatas(false, '_' + item.id); 
                    }
                    else if (TwnDet.length <= 0) {
                        $ionicLoading.show({
                            template: 'Please Wait. Data is Sync...'
                        })
                        $scope.clearAll(false, '_' + item.id);
                    }
                }
                break;
        }
        $scope.modal.hide();
    };

    //Cleanup the modal when we're done with it!
    $scope.$on('$destroy', function () {
        $scope.modal.remove();
    });
    $scope.customers = [{
            'id': '1',
            'name': $scope.DrCap,
            'url': 'manageDoctorResult'
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap,
            'url': 'manageChemistResult'
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap,
            'url': 'manageStockistResult'
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap,
            'url': 'manageStockistResult'
        });
        $scope.nCI = al; al++;
    }

            

    $scope.DataLoaded = true;
    $scope.fmcgData = {};
    $scope.Reload = {};
    $scope.precall = {};
    $scope.RCPA = {};
    $scope.RMCL = {};
    $scope.Mypln = {};
    $scope.MnthSumm = {};
    $scope.stockUpdation = {};
    $scope.tpview = {};
    $scope.geoTag = {};

    $scope.go = function (path) {
        $location.path(path);
    };
    $scope.Mypln.worktype = {};
    $scope.Mypln.worktype.selected = {};
    $scope.prvRptId = '';
    var Wtyps = $scope.worktypes.filter(function (a) { return (a.FWFlg == "F") });
    $scope.Mypln.worktype.selected = Wtyps[0];
        
    $scope.showCounter = 0;
    $scope.draftCount = fmcgLocalStorage.getItemCount("draft");
    $scope.fmcgData.productSelectedList = [];
    $scope.fmcgData.doctorSelectedList = [];
    $scope.fmcgData.giftSelectedList = [];
    $scope.fmcgData.jontWorkSelectedList = [];
    $scope.fmcgData.preferredDaysList = [];
    $scope.fmcgData.preferredTimingsList = [];
    $scope.RCPA.productSelectedList = [];
    $scope.RMCL.jontWorkSelectedList = [];
    $scope.RMCL.productSelectedList = [];

    $scope.fmcgData.worktype = {};
    $scope.fmcgData.worktype.selected = {};
    $scope.fmcgData.worktype.selected= Wtyps[0];
        
    $rootScope.hasData = false;
    $scope.logout = function () {
        //window.localStorage.clear();
        $scope.toggleLeft();
        $state.go("signin");
    }
    $scope.toggleLeft = function () {
        //  notification.vibrate(15);
        //if ($('body').hasClass('menu-open')) {
        //}
        $ionicSideMenuDelegate.toggleLeft();
        $('body').removeClass('menu-open');
    };
    $scope.goToAddNew = function () {
        if ($scope.cComputer == false) {
            if ($scope.GeoChk == 0) {
                Toast("Checking for Location Services");
                if (_currentLocation.Latitude != undefined) {
                    var dt = new Date();
                    var dt1 = new Date(_currentLocation.Time);
                    var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                    var resultInMinutes = Math.round(difference / 60000);
                    if (resultInMinutes > 10) {
                        _currentLocation = {};
                        return false;
                    }
                } else {
                    // return false;
                }
            }
            if (_currentLocation.Latitude != undefined) {
                if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                    $scope.fmcgData.geoaddress = _currentLocation.address;
                    $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                }
            }
        }
        $state.go('fmcgmenu.addNew');
    };
    if ($scope.onLine)
    {

    }

    $scope._savingData = false;
    $scope.savingData = {};
    $scope.savingData.RCPA = false;
    $scope.savingData.RMCL = false;
    $scope.savingData.MyPL = false;
    $scope.savingData.Rmks = false;
    $scope.savingData.EVNT = false;
    function svQDatas() {
        $scope.onLine = isReachable();
        if ($scope.onLine && navigator.onLine)
        {
            var draftData = fmcgLocalStorage.getData("saveLater") || [];
            //console.log(draftData.length + ' : ' + $scope._savingData);
            if (draftData.length > 0 && $scope._savingData == false) {
                var data = draftData[0]; $scope._savingData = true;
                if (data.arc) {
                    var arc = data.arc;
                    var amc = data.amc;
                    fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, data, true).success(function (response) {
                        if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                            draftData.shift();
                        } else if (response['msg'] == 'Call Already Exist') {
                            draftData.shift();
                        }
                        else if (response['success']) {
                            draftData.shift();
                            Toast('Local calls synced', true);
                        } else {
                            var data = draftData.shift();
                            draftData.push(data);
                        }
                        fmcgLocalStorage.createData("saveLater", draftData);
                        $scope._savingData = false;
                    }).error(function () {
                        $scope._savingData = false;
                    });
                } else {
                    fmcgAPIservice.saveDCRData('POST', 'dcr/save', data, false)
                    .success(function (response) {
                        if (!response['success'] && response['msg'] !== 'Call Already Exist') {
                            draftData.shift();
                        } else if (response['msg'] == 'Call Already Exist') {
                            draftData.shift();
                        }
                        else if (response['success']) {
                            draftData.shift();
                            Toast('Local calls synced', true);
                        } else {
                            var data = draftData.shift();
                            draftData.push(data);
                        }
                        fmcgLocalStorage.createData("saveLater", draftData);
                        $scope._savingData = false;
                    })
                    .error(function () {
                        $scope._savingData = false;
                    });
                }
                        
            }
            
            var QDataEVNT = fmcgLocalStorage.getData("events") || [];
            //console.log(JSON.stringify(QDataEVNT));

            if (QDataEVNT.length > 0 && $scope.savingData.EVNT == false) {
                var dataEVNT = QDataEVNT[0];
                var imgUrl = dataEVNT.imgData; $scope.savingData.EVNT = true;
                var options = new FileUploadOptions();
                options.fileKey = "imgfile";
                options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/')+1);
                options.mimeType = "image/jpeg";
                options.chunkedMode = false;
                var uplUrl = baseURL + 'imgupload&sf_code=' + $scope.sfCode;
                var ft = new FileTransfer();
                ft.upload(imgUrl, uplUrl,
                function (response) {
                    QDataEVNT.shift();
                    fmcgLocalStorage.createData("events", QDataEVNT);
                    $scope.savingData.EVNT = false;
                },
                function () {
                    $scope.savingData.EVNT = false;
                }, options);
            }

            var QDataRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
            if (QDataRCPA.length > 0 && $scope.savingData.RCPA == false) {
                var dataRCPA = QDataRCPA[0]; $scope.savingData.RCPA = true;
                fmcgAPIservice.addMAData('POST', 'dcr/save', '4', dataRCPA)
                .success(function (response) {
                    if (response['success']) {
                        QDataRCPA.shift();
                        Toast('Local calls synced', true);
                    } else {
                        var dataRCPA = QDataRCPA.shift();
                        QDataRCPA.push(dataRCPA);
                    }
                    fmcgLocalStorage.createData("OutBx_RCPA", QDataRCPA);
                    $scope.savingData.RCPA = false;
                })
                .error(function () {
                    $scope.savingData.RCPA = false;
                });
            }

            var QDataRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
            if (QDataRMCL.length > 0 && $scope.savingData.RMCL == false) {
                var dataRMCL = QDataRMCL[0]; $scope.savingData.RMCL = true;
                fmcgAPIservice.addMAData('POST', 'dcr/save', '5', dataRMCL)
                .success(function (response) {
                    if (response['success']) {
                        QDataRMCL.shift();
                        Toast('Local calls synced', true);
                    } else {
                        var dataRMCL = QDataRMCL.shift();
                        QDataRMCL.push(dataRMCL);
                    }
                    fmcgLocalStorage.createData("OutBx_RMCL", QDataRMCL);
                    $scope.savingData.RMCL = false;
                })
                .error(function () {
                    $scope.savingData.RMCL = false;
                });
            }

            var QDataMyPL = fmcgLocalStorage.getData("myplnQ") || [];
            if (QDataMyPL.length > 0 && $scope.savingData.MyPL == false) {
                var dataMyPL = QDataMyPL[0]; $scope.savingData.MyPL = true;
                fmcgAPIservice.addMAData('POST', 'dcr/save', '3', dataMyPL)
                .success(function (response) {
                    if (response['success']) {
                        QDataMyPL.shift();
                        Toast('Local calls synced', true);
                    } else {
                        var dataMyPL = QDataMyPL.shift();
                        QDataMyPL.push(dataMyPL);
                    }
                    fmcgLocalStorage.createData("myplnQ", QDataMyPL);
                    $scope.savingData.MyPL = false;
                })
                .error(function () {
                    $scope.savingData.MyPL = false;
                });
            }

            var QDataRmks = fmcgLocalStorage.getData("MyDyRmksQ") || [];
            if (QDataRmks.length > 0 && $scope.savingData.Rmks == false) {
                var dataRmks = QDataRmks[0]; $scope.savingData.Rmks = true;
                fmcgAPIservice.updateRemark('POST', 'dcr/updRem',  dataRmks)
                .success(function (response) {
                    if (response['success']) {                       
                        Toast('Local calls synced', true);
                    }
                    QDataRmks.shift();
                    fmcgLocalStorage.createData("MyDyRmksQ", QDataRmks);
                    $scope.savingData.Rmks = false;
                })
                .error(function () {
                    $scope.savingData.Rmks = false;
                });
            }
        }
    }
    svQDataIntrvl = setInterval(function (){svQDatas();}, 500); //(1000*(60*5)));
    setInterval(function () {
        var QueData = fmcgLocalStorage.getData("saveLater") || [];
        $scope.outboxCount = QueData.length;
        var QueData = fmcgLocalStorage.getData("OutBx_RCPA") || [];
        $scope.outboxCount += QueData.length;
        var QueData = fmcgLocalStorage.getData("OutBx_RMCL") || [];
        $scope.outboxCount += QueData.length;
        var QueData = fmcgLocalStorage.getData("myplnQ") || [];
        $scope.outboxCount += QueData.length;
        var QueData = fmcgLocalStorage.getData("MyDyRmksQ") || [];
        $scope.outboxCount += QueData.length;
        
        var QueData = fmcgLocalStorage.getData("draft") || [];
        $scope.draftCount = QueData.length;
    }, 500);

    $scope.fmcgDataCopy = angular.copy($scope.fmcgData);
    $scope.clearData = function () {
        if ($scope.fmcgData)
        {
            if ($rootScope.deleteRecord)
            {
                if ($rootScope.saveToDraft || $scope.fmcgData.isDraft == true){fmcgLocalStorage.addData('draft', $scope.fmcgData);}
                var value = $scope.fmcgData;
                for (key in value){if (key){if(key!='jontWorkSelectedList') $scope.fmcgData[key] = undefined;}}
                $rootScope.hasData = false;
                $rootScope.hasEditData = false;
                $rootScope.deleteRecord = false;
                $rootScope.saveToDraft = false;
                $scope.fmcgData.isDraft = false;
            }
        }
    };
    $scope.saveToDraftO = function ()
    {
        var temp = fmcgLocalStorage.getData("draft");
        if (temp === null || temp.length == 0 || ($scope.fmcgData.worktype.selected.id).toString() === (temp[0]['worktype']['selected']['id']).toString())
        {
            $scope.fmcgData.isDraft = true;
            fmcgLocalStorage.addData('draft', $scope.fmcgData);
            var value = $scope.fmcgData;
            for (key in value)
            {
                if (key)
                {
                    if(key!='jontWorkSelectedList') $scope.fmcgData[key] = undefined;
                }
            }
            $state.go('fmcgmenu.home');
            Toast("Call saved to draft");
        }
        else
        {
            $ionicPopup.confirm({
                title: 'Call Conflict',
                content: 'You have call for other worktype in draft do you want to replace...?'
            })
                .then(function (res) {
                if (res) {
                    window.localStorage.removeItem("draft");
                    fmcgLocalStorage.addData('draft', $scope.fmcgData);
                    var value = $scope.$parent.fmcgData;
                    for (key in value)
                    {
                        if (key)
                        {
                            if(key!='jontWorkSelectedList') $scope.$parent.fmcgData[key] = undefined;
                        }
                    }
                    $state.go('fmcgmenu.home');
                    Toast("Data added to draft Successfully");
                } else {
                    console.log('You are not sure');
                }
            }
            );
        }
    };
    
    $scope.setAllow = function () {
        $rootScope.hasData = true;
        if ($scope.fmcgData.amc || $scope.fmcgData.arc)
        {
            $rootScope.hasEditData = true;
        }
    }
}])
.controller('myTodyPlCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$filter','$timeout', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $filter,$timeout) {
    $scope.$parent.navTitle = "My Day Plan";
    $scope.clearData();
    $scope.remarks ="";
    $scope.data = {};
    var temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    var $fl = 0;
    if ($scope.cComputer || cordova.platformId=="ios") {
        $scope.entryDate = new Date();
    } else {
          $timeout(function () {
            window.sangps.getDateTime(function (tDate) {
                $scope.entryDate = tDate;
            });
          }, 1000);
    }
    $scope.Mypl = fmcgLocalStorage.getData("mypln") || [];
    if ($scope.Mypl.length > 0) {
        if ($scope.Mypl[0].worktype != undefined) {
            $scope.Mypln.worktype = {};
            $scope.Mypln.worktype.selected = {};
            $scope.Mypln.worktype.selected.id = $scope.Mypl[0].worktype;
            $scope.Mypln.worktype.selected.FWFlg = $scope.Mypl[0].FWFlg;
            $scope.Mypln.subordinate = {};
            $scope.Mypln.subordinate.selected = {};
            if ($scope.view_MR == 1) {
                $scope.Mypln.subordinate.selected.id = $scope.sfCode;
            } else {
                $scope.Mypln.subordinate.selected.id = $scope.Mypl[0].subordinateid;
            }
            if ($scope.Mypl[0].FWFlg == 'F') {
                $scope.cMTPDId = '_' + $scope.Mypl[0].subordinateid;
                TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
                if (TpTwns.length < 1) {
                    $ionicLoading.show({
                        template: 'Please Wait. Data is Sync...'
                    }); $scope.clearAll(false, $scope.cMTPDId);
                } else { $scope.loadDatas(false, $scope.cMTPDId); }
            }
            $scope.Mypln.cluster = {};
            $scope.Mypln.cluster.selected = {};
            $scope.Mypln.cluster.selected.id = $scope.Mypl[0].clusterid;
            $scope.Mypln.remarks = $scope.Mypl[0].remarks;
            $fl = 1;
        }
    }
    if ($fl == 0) {

        $scope.Mypln.worktype = {};
        $scope.Mypln.worktype.selected = {};
        var Wtyps = $scope.worktypes.filter(function (a) { return (a.FWFlg == "F") });
        $scope.Mypln.worktype.selected = Wtyps[0];
        $scope.Mypln.subordinate = {};
        $scope.Mypln.subordinate.selected = {};
        if ($scope.view_MR == 1) {
            $scope.Mypln.subordinate.selected.id = $scope.sfCode; 
        }
        $scope.Mypln.cluster = {};
        $scope.Mypln.cluster.selected = {};
        $scope.Mypln.remarks = "";

    }
    $scope.save = function () {
        $scope.data.worktype = $scope.Mypln.worktype.selected.id;
        $scope.data.FWFlg = $scope.Mypln.worktype.selected.FWFlg;
        $scope.data.entryDate = $scope.entryDate;
        if ($scope.Mypln.worktype.selected.FWFlg == 'F')
        {
            if ($scope.view_MR!=1 && ($scope.Mypln.subordinate==undefined  || $scope.Mypln.subordinate.selected.id=='')){
                Toast('Select the field force...');
                return false;
            }                
            if($scope.Mypln.cluster==undefined || $scope.Mypln.cluster.selected.id=='')
            {
                Toast('Select the Cluster...');
                return false;
            }
            $scope.data.subordinateid = ($scope.view_MR == 1) ? userData.sfCode : $scope.Mypln.subordinate.selected.id;            
            $scope.data.clusterid = $scope.Mypln.cluster.selected.id;
            if ($scope.Mypln.cluster.name == undefined) $scope.Mypln.cluster.name = $filter('getValueforID')($scope.Mypln.cluster.selected.id, $scope.myTpTwns).name;
            $scope.data.ClstrName = $scope.Mypln.cluster.name;
        }else{
            $scope.data.subordinateid = '';
            $scope.data.clusterid = '';
            $scope.data.ClstrName= '';
        }
        $scope.data.remarks = $scope.Mypln.remarks;
        $ionicLoading.show({ template: 'Saving...' });
        window.localStorage.removeItem("mypln");
        window.localStorage.removeItem("myplnQ");
        $scope.Myplns = [];
        $scope.Myplns.push($scope.data);
        fmcgLocalStorage.createData("mypln", $scope.Myplns);
        fmcgAPIservice.addMAData('POST', 'dcr/save', "3", $scope.data).success(function (response) {
            if (response.success) {
                Toast("My Today Plan Submited Successfully");
                $ionicLoading.hide();
            }
            else {
                if (!response['success'])
                    Toast(response['msg'], true);
                $ionicLoading.hide();
            }
            $scope.$parent.Myplns = fmcgLocalStorage.getData("mypln") || [];
            $scope.PlanChk = 0;
            $scope.$parent.PlanChk = 0;
            $state.go('fmcgmenu.home');
            
        }).error(function () {
            window.localStorage.setItem("myplnQ", JSON.stringify($scope.Myplns));
            Toast("No Internet Connection! My Day Plan Saved in Outbox");
            $ionicLoading.hide();
            $state.go('fmcgmenu.home');
        });
    };

}])
.controller('DyRmksCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
    $scope.$parent.navTitle = "Day Activity Remarks";
    $scope.data = {};
    $scope.dyRmks={};
        
    $scope.MyDyRmks= fmcgLocalStorage.getData("MyDyRmks") || [];  
    if ($scope.MyDyRmks.length == 0)
    {
        fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_report",
            '["remarks","Half_Day_FW as Halfday"]', , '["Activity_Date=convert(varchar,getDate(),101)"]'
        ]).success(function (response) {
            $scope.MyDyRmks=response;
            if (response.length && response.length > 0 && Array.isArray(response))
            {
                fmcgLocalStorage.createData("MyDyRmks", response);                    
                $scope.dyRmks.remarks = response[0].remarks;
                $scope.SlHlfDys = response[0].Halfday;
            }
        }).error(function () {
            Toast("No Internet Connection!.");
        });
    }
    else{
        $scope.dyRmks.remarks = $scope.MyDyRmks[0].remarks;
        $scope.SlHlfDys = $scope.MyDyRmks[0].Halfday;
    }

HalfdayWorks = fmcgLocalStorage.getData("halfdayworks") || [];
$scope.HalfdayWorks = HalfdayWorks.filter(function (a) {
    if (a.Hlfdy_flag == 1) {
        return a.Hlfdy_flag;
    }
    })

    $scope.setHlfDy = function (item) {
        for (var di = 0; di < $scope.HalfdayWorks.length; di++) {
            $scope.HalfdayWorks[di].checked = false;
            if ($scope.SlHlfDys.indexOf($scope.HalfdayWorks[di].id) > -1)
                $scope.HalfdayWorks[di].checked = true;
        }
    }
    if ($scope.HalfdayWorks.length == 0) {
        fmcgAPIservice.getDataList('POST', 'table/list', ["mas_worktype",
            '["type_code as id", "Wtype as name"]'
                    , , '["isnull(Active_flag,0)=0","isnull(HalfDyNeed,0)=1"]', , , , 0]).success(function (response) {
                        $scope.HalfdayWorks = response;
                        $scope.setHlfDy();
                        if (response.length && response.length > 0 && Array.isArray(response))
                            fmcgLocalStorage.createData("halfdayworks", response);
                    });
    } else {
        $scope.setHlfDy();
    }
    $scope.selHlfDy = function (item)
    {
        if (item.checked) {
            if ($scope.AppEnver != ".Net") {
                if ($scope.SlHlfDys.indexOf(item.id + ',') < 0) $scope.SlHlfDys += item.id + ',';
            }
            else {
                if ($scope.SlHlfDys.indexOf(item.name + ' ( ' + item.id + ' ),') < 0) {
                    $scope.SlHlfDys += item.name + ' ( ' + item.id + ' ),';
                }
            }
        }
        else
        {
            $scope.SlHlfDys = $scope.SlHlfDys.replace((($scope.AppEnver != ".Net")?item.id:item.name + ' ( ' + item.id + ' )')+',', '');
        }
    }
    $scope.save = function () {
        if ($scope.dyRmks.remarks==undefined || $scope.dyRmks.remarks=='')
        {
            Toast('Enter Day Activity Remarks...')
        }
        else{
            $scope.data.remarks = $scope.dyRmks.remarks;
            $scope.data.Halfday = $scope.SlHlfDys;
            
            MyDyRmks=[{}];
            MyDyRmks[0]["remarks"] = $scope.dyRmks.remarks;
            MyDyRmks[0]["Halfday"] = $scope.SlHlfDys
            window.localStorage.removeItem("MyDyRmks");
            fmcgLocalStorage.createData("MyDyRmks", MyDyRmks);

            fmcgAPIservice.updateRemark('POST', 'dcr/updRem', $scope.data).success(function (response) {
                if (response.success)
                {
                    Toast("Day Activity Remarks Updated Successfully");
                }
                else
                    Toast(response['msg'], true);
            })
            .error(function () {
                window.localStorage.setItem("MyDyRmksQ", JSON.stringify(MyDyRmks));
                Toast("No Internet Connection! Saved in Outbox");
                $state.go('fmcgmenu.home');
            })
            ;
            $state.go('fmcgmenu.home');
        }
    }
}])
.controller('stockUpdationCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
    $scope.$parent.navTitle = "SS Entry";
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.stockUpdation";
    $scope.myplan = fmcgLocalStorage.getData("mypln") || [];
    if ($scope.$parent.fmcgData.editable != 1) {
        $scope.stockUpdation.stockist = {};
        $scope.stockUpdation.stockist.selected = {};
    }

    $scope.Last_Updation_Date = [];
    $scope.tdate = $filter('date')(new Date(), 'dd/MM/yyyy');
    $scope.fmcgData.value1 = 0;
    $scope.fmcgData.value2 = 0;
    $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
    $scope.setAllow();
    $scope.$on('GetStocks', function (evnt) {
        $scope.GetCurrentStock();
    });
    $scope.GetCurrentStock = function () {
        getStock();
    }
    if ($scope.view_STOCKIST == 1)
        getStock();
    function getStock() {
        if ($scope.view_STOCKIST == 1)
            $scope.LastUpdationDate = $scope.Last_Updation_Date[0]['Last_Updation_Date'];
        else
            $scope.$parent.fmcgData.productSelectedList = [];

        if ($scope.LastUpdationDate == $scope.tdate)
            $scope.editing = 0;
        else
            $scope.editing = 1;
        if ($scope.view_STOCKIST != 1 && $scope.stockUpdation.stockist.selected != undefined)
            $sCode = $scope.stockUpdation.stockist.selected.id
        else
            $sCode = "0";
        fmcgAPIservice.getPostData('POST', 'get/currentStock&scode=' + $sCode, []).success(function (response) {
            $scope.currentStocks = response;
            $ionicLoading.hide();
        }).error(function () {
            Toast("No Internet Connection!");
            $ionicLoading.hide();
        });
    }
    $scope.edit = function (item) {
        var value = $scope.currentStocks;
        $scope.$parent.fmcgData.editable = 1;
        $scope.$parent.fmcgData.productSelectedList = value;
        $state.go('fmcgmenu.stockUpdation');
    };
    $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];
    $scope.addProduct = function(selected) {
        var productData = {};
        $scope.desigCode = $scope.desigCode;
        productData.product = selected;
        productData.product_Nm = (selected.toString() != "") ? $filter('getValueforID')(selected, $scope.products).name : "";
        productData.rx_qty = 0;
        productData.sample_qty = 0;
        var len = $scope.$parent.fmcgData.productSelectedList.length;
        var flag = true;
        for (var i = 0; i < len; i++)
        {
            if ($scope.$parent.fmcgData.productSelectedList[i]['product'] === selected)
            {
                flag = false;
            }
        }
        if (flag)
            $scope.$parent.fmcgData.productSelectedList.push(productData);
    }
    scrTyp = 1
    if (scrTyp == 1 || scrTyp == 4)
    {

        RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        if ($scope.closing == 0)
            $scope.condClosing = false;
        else
            $scope.condClosing = true;
       // if ($scope.recv == 0)
            $scope.condRecv = false;
      /*  else
            $scope.condRecv = true;*/
        $scope.gridOptions = {
            data: 'fmcgData.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html'},
                { field: 'recv_qty', displayName: "Receipt", enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 90, "visible": $scope.condClosing },
                { field: 'cb_qty', displayName: "CB", enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 95, "visible": true},
                { field: 'pieces', displayName: "Base UOM", enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate4.html", width: 90, "visible": $scope.condRecv },
                {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]

        };

    }


    $scope.find = function () {
        if (this.row.entity.pieces >= this.row.entity.conversionQty) {
           // var str = "Enter less than Conversion Quantity (" + this.row.entity.conversionQty + ")";
            alert("Enter less than Conversion Quantity (" + this.row.entity.conversionQty+")");
            this.row.entity.pieces = 0;
        }
    }
    $scope.fmcgData.value = 0;
    $scope.removeRow = function() {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
    };
    $scope.save = function () {

        var products = $scope.$parent.fmcgData.productSelectedList;
        if ($scope.$parent.fmcgData.editable == 1)
            var save = 'dcr/save&editable=' + 1;
        else
            save = 'dcr/save&editable=' + 0;
        if ($scope.SF_type == 1)
            $sCode=$scope.stockUpdation.stockist.selected.id
        else
            $sCode = "0";
        save = save + '&sCode=' + $sCode;
        if ($scope.view_STOCKIST == 0) {
            if ($scope.stockUpdation.stockist.selected.id==undefined) {
                Toast("Please Select Distributors");
                return false;
            }
        }
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.addMAData('POST', save, '8', products)
                .success(function(response) {
                  /*  if (response['success']) {
                        var date = new Date();
                        if ($scope.view_STOCKIST == 1) {
                            localStorage.removeItem("Last_Updation_Date");
                            var Last_Updation_Date = [{ "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy') }]
                        }
                        else {
                            var Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                            stat = 0;
                            for (key in Last_Updation_Date) {
                                if (Last_Updation_Date[key]['stockistCode'] == $sCode) {
                                    stat = 1;
                                    Last_Updation_Date[key]['Last_Updation_Date'] = $filter('date')(new Date(), 'dd/MM/yyyy');
                                }
                            }
                            if (stat == 0)
                                Last_Updation_Date.push({ "Last_Updation_Date": $filter('date')(new Date(), 'dd/MM/yyyy'), "stockistCode": $sCode });
                        }
                        window.localStorage.setItem("Last_Updation_Date", JSON.stringify(Last_Updation_Date));
                        $scope.Last_Updation_Date = fmcgLocalStorage.getData("Last_Updation_Date") || [];
                        $state.go('fmcgmenu.transCurrentStocks');
                        $ionicLoading.hide();
                        Toast('Stock Updated', true);
                    }
//                                        fmcgLocalStorage.createData("myplnQ", QDataMyPL);
//                                        $scope.savingData.MyPL = false;*/
                })
                .error(function() {
                    //  $scope.savingData.MyPL = false;
                });
//                $state.go('fmcgmenu.home');
    }



})
.controller('addULDocCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
    $scope.$parent.navTitle = "Add " + $scope.NLCap;
    $scope.clearData();
    $scope.data = {};
    $scope.NmCap = $scope.NLCap;
    $scope.save = function () {
        if ($scope.data.name == "" || $scope.data.name == undefined) {
            Toast('Enter the Name...');
            return false;
        }
        $scope.data.cluster = {};
        $scope.data.cluster.selected = {};
        if ($scope.fmcgData.cluster == undefined || $scope.fmcgData.cluster.selected == undefined) {
            Toast('Select the Cluster...');
            return false;
        }
        $scope.data.cluster.selected.id = $scope.fmcgData.cluster.selected.id;

        $scope.data.qulification = {};
        $scope.data.qulification.selected = {};
        if ($scope.fmcgData.qulification == undefined || $scope.fmcgData.qulification.selected == undefined) {
            Toast('Select the Qulification...');
            return false;
        }
        $scope.data.qulification.selected.id = $scope.fmcgData.qulification.selected.id;

        $scope.data.class = {};
        $scope.data.class.selected = {};
        if ($scope.fmcgData.class == undefined || $scope.fmcgData.class.selected == undefined) {
            Toast('Select the Category...');
            return false;
        }
        $scope.data.class.selected.id = $scope.fmcgData.class.selected.id;

        $scope.data.speciality = {};
        $scope.data.speciality.selected = {};
        if ($scope.fmcgData.speciality == undefined || $scope.fmcgData.speciality.selected == undefined) {
            Toast('Select the Speciality...');
            return false;
        }
        $scope.data.speciality.selected.id = $scope.fmcgData.speciality.selected.id;

        $scope.data.category = {};
        $scope.data.category.selected = {};
        if ($scope.fmcgData.category == undefined || $scope.fmcgData.category.selected == undefined) {
            Toast('Select the Category...');
            return false;
        }
        $scope.data.category.selected.id = $scope.fmcgData.category.selected.id;

        fmcgAPIservice.addMAData('POST', 'dcr/save', "2", $scope.data).success(function (response) {
            if (response.success)
                Toast($scope.NLCap + " Added Successfully");
            $scope.data = {};
            if ($scope.AppTyp == 1) {
                $scope.clearIdividual(0, 1)
            }
            else { $scope.clearIdividual(3, 1); }
            $scope.data = {};
            if ($scope.fmcgData.cluster == undefined) $scope.fmcgData.cluster = {};
            if ($scope.fmcgData.speciality == undefined) $scope.fmcgData.cluster = {};
            if ($scope.fmcgData.category == undefined) $scope.fmcgData.cluster = {};

            $scope.fmcgData.cluster.selected = {};
            $scope.fmcgData.speciality.selected = {};
            $scope.fmcgData.category.selected = {};

        });
        $state.go('fmcgmenu.home');
    };
}])
 .controller('vaccancyList', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.$parent.navTitle = "Vaccancy List";

         $ionicLoading.show({
             template: 'Loading...'
         });
         fmcgAPIservice.getDataList('POST', 'vaccancyList', [])
              .success(function (response) {
                  $scope.vaccancyLists = response;
                  $ionicLoading.hide();
              }).error(function () {
                  $ionicLoading.hide();
                  Toast('No Internet Connection.');
              });
        
     })

.controller('MCL_DetailsCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$timeout', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $timeout) {
    $scope.$parent.navTitle = "MCL Details";
    $scope.clearData();
      $scope.data = {};
    $scope.NmCap = $scope.NLCap;

    $scope.cDataID = '_' + $scope.sfCode;

    $scope.fmcgData.subordinate = {};
    $scope.fmcgData.subordinate.selected = {};
    $scope.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g, '');
    $scope.fmcgData.doctor = undefined;
    function failNative(e) {
        $ionicLoading.hide();
        console.error('Houston, we have a big problem :(');
    }
    $scope.openCamera = function (source) {
        $scope.destinationType = Camera.DestinationType.FILE_URI;
        $ionicLoading.show({
            template: 'Loading...'
        });
        navigator.camera.getPicture(function (imgSrc) {
            $scope.fmcgData.visiting_card_url = imgSrc;
            $ionicLoading.hide();
            uploadFile(imgSrc)


        }, function (msg) {}, { sourceType: 1, quality: 60, destinationType: $scope.destinationType, sourceType: source });
        $timeout(function () {
            $scope.fmcgData.visitingcardshow = 0;
        }, 3000);
        $ionicLoading.hide();
    }

    $scope.Attachment = function () {

        fileChooser.open(function (uri) {
            var uripath = uri;
            $ionicLoading.show({
                template: 'Loading...'
            });
            $scope.fmcgData.visitingcardshow = 1;
            window.FilePath.resolveNativePath(uripath, successNative, failNative);
        });
    }
    $scope.fileName = '';
    $scope.finalPath = '';
    function uploadFile(imgUrl) {
        var imgUrl = imgUrl;
        var options = new FileUploadOptions();
        options.fileKey = "imgfile";
        options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
        options.chunkedMode = false;
        options.mimeType = "image/jpeg";
        var uplUrl = baseURL + 'fileAttachment&doctor_code=' + $scope.fmcgData.doctor.selected.id;
        var ft = new FileTransfer();
        ft.upload(imgUrl, uplUrl,
           function (response) {
               $scope.finalPath = '';
           },
           function () {
           }, options);
        var date = new Date();
        $scope.FromDate = ('0' + date.getDate()).slice(-2) + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + date.getFullYear();
        $scope.fmcgData.Visiting_Card = $scope.fmcgData.doctor.selected.id + "_" + $scope.FromDate + "_" + imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
            $scope.fmcgData.visiting_card_url = imgUrl;
            $scope.fmcgData.visitingcardshow = 0;
      //  alert($scope.fmcgData.visiting_card_url )
    }
    function successNative(finalPath) {
        // var path = 'file://' + finalPath;
        $scope.finalPath = finalPath;
        $scope.NameOfFile = finalPath.substr(finalPath.lastIndexOf('/') + 1);
        $scope.fmcgData.visitingcard = $scope.fmcgData.doctor.selected.id + "_" + $scope.NameOfFile;
        uploadFile(finalPath);
        $ionicLoading.hide();

    }
    
    $scope.fmcgData.visitingcardshow = 1;
    $scope.$on('getDocDetails', function (evnt, DrCd) {
        $scope.fmcgData.preferredDaysList =[];
        $scope.fmcgData.preferredTimingsList =[];
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getPostData('POST', 'get/doctor&Msl_No=' + DrCd.DrId, []).success(function (response) {
            $scope.fmcgData.visitingcardshow = 0;
            $scope.data = response[0];
            $scope.fmcgData.qulification = {};
            $scope.fmcgData.qulification.selected = {};
            $scope.fmcgData.qulification.selected.id = $scope.data.Doc_QuaCode;
            $scope.fmcgData.qulification.name = $scope.data.Doc_Qua_Name;
            $scope.fmcgData.speciality = {};
            $scope.fmcgData.speciality.selected = {};
            $scope.fmcgData.speciality.selected.id = $scope.data.Doc_Special_Code;
            $scope.fmcgData.speciality.Doc_Spec_ShortName = $scope.data.Doc_Spec_ShortName;
            $scope.fmcgData.category = {};
            $scope.fmcgData.category.selected = {};
            $scope.fmcgData.category.selected.id = $scope.data.Doc_Cat_Code;
            $scope.fmcgData.category.Doc_Cat_ShortName = $scope.data.Doc_Cat_ShortName;
            $scope.fmcgData.gender = {};
            $scope.fmcgData.gender.selected = {};
            $scope.fmcgData.gender.selected.id = $scope.data.ListedDr_Sex;
            $scope.data.Clinic_Address = $scope.data.ListedDr_Address3;
            $scope.fmcgData.visiting_card_url = visitingcardUrl + $scope.data.Visiting_Card;

            $scope.fmcgData.Visiting_Card = $scope.data.Visiting_Card;
            $scope.fmcgData.preferredDaysList.preferredIds = $scope.data.visit_days;
            $scope.fmcgData.preferredTimingsList.preferredIds = $scope.data.visit_hours;
            $scope.fmcgData.preferredDaysList.preferredNames = $scope.data.visit_days1;
            $scope.fmcgData.preferredTimingsList.preferredNames = $scope.data.visit_hours1;
            if ($scope.data.visit_hours1 != null && $scope.data.visit_hours1 != '') {
                visithours = $scope.data.visit_hours1.split(',');
                for (i = 0; i < visithours.length; i++) {
                    var preferredData = {};
                    preferredData.preferredId = visithours[i];
                    preferredData.preferredName = visithours[i];
                    $scope.fmcgData.preferredTimingsList.push(preferredData);
                }
           }
            if ($scope.data.visit_days1!=null&&$scope.data.visit_days1!='') {
               visitdays = $scope.data.visit_days1.split(',');
               for (i = 0; i < visitdays.length; i++) {
                   var preferredData = {};
                   preferredData.preferredId = visitdays[i];
                   preferredData.preferredName = visitdays[i];
                   $scope.fmcgData.preferredDaysList.push(preferredData);
               }
           }
            $ionicLoading.hide();
        }).error(function () {
            Toast("No Internet Connection!");
            $ionicLoading.hide();
        });
    });
    $scope.save = function () {
        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.Myplns.length > 0) {
            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
        }
        if ($scope.fmcgData.doctor == undefined || $scope.fmcgData.doctor.selected == undefined) {
            Toast('Select the Doctor...');
            return false;
        }
        $scope.data.Visiting_Card = $scope.fmcgData.Visiting_Card;
        $scope.data.visit_hours=$scope.fmcgData.preferredTimingsList.preferredIds;
        $scope.data.visit_days = $scope.fmcgData.preferredDaysList.preferredIds;
        $scope.data.doctorCode = $scope.fmcgData.doctor.selected.id;
        if ($scope.fmcgData.gender != undefined && $scope.fmcgData.gender.selected != undefined)
        $scope.data.ListedDr_Sex = $scope.fmcgData.gender.selected.id;
        $scope.data.qulification = {};
        $scope.data.qulification.selected = {};
        if ($scope.fmcgData.qulification == undefined || $scope.fmcgData.qulification.selected == undefined) {
            Toast('Select the Qulification...');
            return false;
        }
        $scope.data.Doc_QuaCode = $scope.fmcgData.qulification.selected.id;
        $scope.data.Doc_Qua_Name = $scope.fmcgData.qulification.name;

        $scope.data.speciality = {};
        $scope.data.speciality.selected = {};            
        if($scope.fmcgData.speciality==undefined || $scope.fmcgData.speciality.selected==undefined)
        {
            Toast('Select the Speciality...');
            return false;
        }
        $scope.data.Doc_Special_Code = $scope.fmcgData.speciality.selected.id;
        $scope.data.Doc_Spec_ShortName = $scope.fmcgData.speciality.Doc_Spec_ShortName;

        $scope.data.category = {};
        $scope.data.category.selected = {};
        if($scope.fmcgData.category==undefined || $scope.fmcgData.category.selected==undefined)
        {
            Toast('Select the Category...');
            return false;
        }
      
        $scope.data.Doc_Cat_Code = $scope.fmcgData.category.selected.id;
        $scope.data.Doc_Cat_ShortName = $scope.fmcgData.category.Doc_Cat_ShortName;
        fmcgAPIservice.addMAData('POST', 'dcr/save', "18", $scope.data).success(function (response) {
            if (response.success) {
                Toast("MCL Details Updated Successfully");
                // $scope.clearIdividual(0, 1);
            $scope.data = {};                
            /*  if ($scope.AppTyp == 1) {
                  $scope.clearIdividual(0,1)
              }
              else { $scope.clearIdividual(3,1); }*/
        }
            if (!response['success']) {
              //  $state.go('fmcgmenu.home');
                Toast(response['msg']);
                $ionicLoading.hide();
            }
        })
        .error(function () {
            Toast("No Internet Connection!..");
        });
        $state.go('fmcgmenu.home');
    };
}])

.controller('addChemistCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification) {
    $scope.$parent.navTitle = "Add "+$scope.ChmCap;
    $scope.clearData();
    $scope.view_chemist=1;
    $scope.NmCap = $scope.ChmCap;
    $scope.data = {};
    $scope.save = function () {
        $scope.data.cluster = {};
        $scope.data.cluster.selected = {};
        if($scope.data.name=="" || $scope.data.name==undefined)
        {
            Toast('Enter the Name...');
            return false;
        }
        if($scope.fmcgData.cluster.selected.id==undefined)
        {
            Toast('Select the Cluster...');
            return false;
        }
        if($scope.data.address=="" || $scope.data.address==undefined)
        {
            Toast('Enter the Address...');
            return false;
        }
        if($scope.data.phone=="" || $scope.data.phone==undefined)
        {
            Toast('Enter the Phone...');
            return false;
        }
        $scope.data.cluster.selected.id = $scope.fmcgData.cluster.selected.id;
        fmcgAPIservice.addMAData('POST', 'dcr/save', "1", $scope.data).success(function (response) {
            if (response.success)
                Toast($scope.ChmCap+" Added Successfully");
            //write as service
            $scope.clearIdividual(1, 1);
            $scope.data = {};
            $scope.fmcgData.cluster.selected.id=undefined;
        });
        $state.go('fmcgmenu.home');
    };
}])
.controller('homeCtrl', ['$rootScope', '$scope', '$state', '$ionicPopup', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', '$ionicLoading', function ($rootScope, $scope, $state, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.customers = [{
            'id': '1',
            'name': $scope.DrCap,
            'url': 'manageDoctorResult'
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap,
            'url': 'manageChemistResult'
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap,
            'url': 'manageStockistResult'
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap,
            'url': 'manageStockistResult'
        });
        $scope.nCI = al; al++;
    }

    $scope.clearData();
    $scope.$parent.navTitle = "";
    var dobdow = JSON.parse(localStorage.getItem("doctor_dob_dow"));
    var date = new Date();
    var today_date = ('0' + date.getDate()).slice(-2) + '/' + ('0' + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();
    if ($scope.doctor_dobdow == 0) {
        if (dobdow != null) {
            if (dobdow.today == today_date) {
                $scope.dob = dobdow.dob;
                $scope.dow = dobdow.dow;
            }
            else {
                $scope.dob = [];
                $scope.dow = [];
                getDoctorDet();
            }
        }
        else {
            $scope.dob = [];
            $scope.dow = [];
            getDoctorDet();
        }
    }
    function getDoctorDet() {
        fmcgAPIservice.getDataList('POST', 'getDoctor_Dob_Dow', [])
             .success(function (response) {
                 dob = [];
                 dow = [];
                 for (i = 0; i < response.length; i++) {
                     if (response[i]['type'] == "birthday")
                         dob.push(response[i]);
                     else
                         dow.push(response[i]);
                 }
                 $scope.dob = dob;
                 $scope.dow = dow;
                 var date = new Date();
                 var dt = ('0' + date.getDate()).slice(-2) + '/' + ('0' + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();

                 dobdow = {};
                 dobdow.dob = $scope.dob;
                 dobdow.dow = $scope.dow;
                 dobdow.today = dt;
                 localStorage.removeItem("doctor_dob_dow");
                 window.localStorage.setItem("doctor_dob_dow", JSON.stringify(dobdow));
                 $ionicLoading.hide();
             }).error(function () {
                 $ionicLoading.hide();
                 $scope.dob = [];
                 $scope.dow = [];
                 Toast('No Internet Connection.');
             });
    }
    
    var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
    $scope.$parent.fieldforceName = loginInfo.sfName;
    var eles=document.querySelectorAll(".fieldforce-name");
    for(var i=0;i<eles.length;i++){
        eles[i].innerHTML=$scope.$parent.fieldforceName;
    }
   /* if ($scope.Myplns.length == 0 && $scope.PlanChk == 1) {
        $state.go('fmcgmenu.myPlan');
    }

    else */
    if ($scope.Myplns.length > 0) {

        if ($scope.view_MR == 1) $scope.Myplns[0].subordinateid = $scope.sfCode;
        if ($scope.Myplns[0].subordinateid != "") {
            $scope.cDataID = '_' + $scope.Myplns[0].subordinateid;
            TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cDataID) || [];
            if (TpTwns.length < 1) {
                $ionicLoading.show({
                    template: 'Please Wait. Day Plan Details is Sync...'
                });
                $scope.clearAll(false, $scope.cDataID);
            } else { $scope.loadDatas(false, $scope.cDataID); }
        }
    }
        
    $scope.gotoCust=function(cus)
    {

        $scope.$parent.fmcgData.customer = {};
        $scope.$parent.fmcgData.worktype = {};

        var CusTyp = (cus == 1) ? "D" : (cus == 2) ? "C" : (cus == 3) ? "S" : (cus == 4) ? "U" : "R";
        var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function (a) { return (a.id == $scope.prvRptId) }) : $scope.worktypes.filter(function (a) { return (a.FWFlg == "F" && a.ETabs.indexOf(CusTyp) > -1) });
        
        if (Wtyps.length > 0) {
            $scope.$parent.fmcgData.worktype.selected = Wtyps[0]; //{ "id": "field work", "name": "Field Work","FWFlg":"F"}
            switch (cus) {
                case 1:
                    $scope.$parent.fmcgData.customer.selected = $scope.customers[0];
                    break;
                case 2:
                    if ($scope.ChmNeed != 1) $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.cCI];
                    break;
                case 3:
                    if ($scope.StkNeed != 1) $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.sCI];
                    break;
                case 4:
                    if ($scope.UNLNeed != 1) $scope.$parent.fmcgData.customer.selected = $scope.customers[$scope.nCI];
                    break;
            }
            $scope.$parent.fmcgData.source = 0;
        }
        
        var pln = fmcgLocalStorage.getData("mypln") || [];
        if (pln.length > 0)
        {
            if ( pln[0].FWFlg == 'F')
            {        
                $state.go('fmcgmenu.screen2');
            }
            else {
                $state.go('fmcgmenu.addNew');
            }
        }
    };

    $scope.goToCustomer = function (cus) {            
        if ($scope.cComputer == false) {
            if ($scope.GeoChk == 0) {
                Toast("Checking for Location Services");
                if (_currentLocation.Latitude != undefined) {
                    var dt = new Date();
                    var dt1 = new Date(_currentLocation.Time);
                    var difference = dt.getTime() - dt1.getTime(); // This will give difference in milliseconds
                    var resultInMinutes = Math.round(difference / 60000);
                    if (resultInMinutes > 10) {
                        _currentLocation = {};
                        return false;
                    }
                } else {
                    // return false;
                }
            }
            if (_currentLocation.Latitude != undefined) {
                if (!$scope.fmcgData.arc && !$scope.fmcgData.isDraft) {
                    $scope.fmcgData.geoaddress = _currentLocation.address;
                    $scope.fmcgData.location = _currentLocation.Latitude + ":" + _currentLocation.Longitude;
                }
            }
        }
        $scope.gotoCust(cus);        
    };
}])
.controller('manageCtrl', ['$rootScope', '$scope', '$state', '$ionicLoading', 'fmcgAPIservice', 'fmcgLocalStorage', 'notification', function ($rootScope, $scope, $state, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.success = false;
    $scope.owsuccess = false;
    $scope.owTypeData = [];
    $scope.$parent.navTitle = "Submitted Calls";
    $scope.customers = [{
            'id': '1',
            'name': $scope.DrCap,
            'url': 'manageDoctorResult'
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap,
            'url': 'manageChemistResult'
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap,
            'url': 'manageStockistResult'
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap,
            'url': 'manageStockistResult'
        });
        $scope.nCI = al; al++;
    }    
    fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function (response) {
        if (response['success'])
        {
            $scope.success = true;
            $scope.customers[0].count = response['data'][0]['doctor_count'];
            if ($scope.ChmNeed != 1) $scope.customers[$scope.cCI].count = response['data'][1]['chemist_count'];
            if ($scope.StkNeed != 1) $scope.customers[$scope.sCI].count = response['data'][2]['stockist_count'];
            if ($scope.UNLNeed != 1) $scope.customers[$scope.nCI].count = response['data'][3]['uldoctor_count'];
            $scope.owTypeData.daywise_remarks = response['data'][4]['remarks'];
            $scope.owTypeData.HlfDayWrk = response['data'][5]['halfdaywrk'];
        }
        else
        {
            $scope.owsuccess = true;
            $scope.owTypeData = response['data'][0];
        }
        $ionicLoading.hide();
    }).error(function () {
        $ionicLoading.hide();
        Toast('No Internet Connection.');
    });
}])

    .controller('screen2docCtrl', function ($rootScope, $scope, $ionicPopup, $stateParams, $state, fmcgAPIservice, fmcgLocalStorage, generateUID, notification, $ionicScrollDelegate) {
        $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];
        if ($scope.Myplns.length == 0) {
            $state.go('fmcgmenu.myPlan');
        }
        $scope.toggleLeft = function () {
            $ionicSideMenuDelegate.toggleLeft();
        };
        $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen2_doc";
        $scope.$parent.navTitle = "Screen2";
        $scope.$parent.fmcgData.doctorSelectedList = $scope.$parent.fmcgData.doctorSelectedList || [];
        //console.log($scope.$parent.fmcgData.doctorSelectedList)

        if ($scope.view_MR == 1) {
            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false, '_' + $scope.sfCode)
        }
        $scope.$on('getDoctors', function (evnt, Cluster) {
            tCode=Cluster.ClusterId
            getDoctorList(tCode);
        })
        tCode = $scope.$parent.fmcgData.cluster.selected.id;
        getDoctorList(tCode);

        function getDoctorList(tCode) {
        repId=$scope.$parent.fmcgData.subordinate.selected.id;
        sdata = 'doctor_master_' + repId; //((',102,103,'.indexOf(',' + choice + ',') > -1) ? $scope.cMTPDId : $scope.cDataID);
        alldata = fmcgLocalStorage.getData(sdata) || [];
        if (tCode != '') {
            $scope.DocByCluster = alldata.filter(function (a) {
                a.town_code = a.town_code + ',';
                a.town_code = a.town_code.replace(',,', ',');
                return ((',' + a.town_code).indexOf(',' + tCode + ',') > -1);
            });
        }
        data = $scope.$parent.fmcgData.doctorSelectedList;
        for (di = 0; di < $scope.DocByCluster.length; di++) {
            itm = data.filter(function (a) { return (a.id == $scope.DocByCluster[di].id); });
            $scope.DocByCluster[di].checked = false;
            if (itm.length > 0) {
                if (itm[0].checked == true) {
                    $scope.DocByCluster[di].checked = true;
                }
            }
        }
    }
        $scope.setSelProduct = function (item) {
            $scope.$parent.fmcgData.doctorSelectedList = $scope.$parent.fmcgData.doctorSelectedList.filter(function (a) { return (a.id != item.id) })
            if (item.checked == true) {
                $scope.$parent.fmcgData.doctorSelectedList.push(item);
            }
        }
       
        $scope.goBack = function () {
            $state.go('fmcgmenu.addNew');
        }
     
    })
.controller('screen1Ctrl', function ($rootScope, $scope, $ionicPopup, $stateParams, $state, fmcgAPIservice, fmcgLocalStorage, generateUID, notification) {
    $scope.Myplns = fmcgLocalStorage.getData("mypln") || [];  
    if ($scope.Myplns.length == 0){
            $state.go('fmcgmenu.myPlan');
    }
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.addNew";
    $scope.$parent.navTitle = "Call Report";

    if ($scope.view_MR == 1) {
        $scope.$parent.fmcgData.subordinate = {};
        $scope.$parent.fmcgData.subordinate.selected = {};
        $scope.$parent.fmcgData.subordinate.selected.id = $scope.sfCode;
        $scope.loadDatas(false, '_'+$scope.sfCode)
    }
    $scope.$parent.fmcgData.worktype = {};
    $scope.$parent.fmcgData.worktype.selected = {};
    var Wtyps = ($scope.prvRptId != '') ? $scope.worktypes.filter(function (a) { return (a.id == $scope.prvRptId) }) : $scope.worktypes.filter(function (a) { return (a.FWFlg == "F") });
    $scope.$parent.fmcgData.worktype.selected = Wtyps[0];//{ "id": "field work", "name": "Field Work","FWFlg":"F"}        
    var id = $stateParams.customerId;
    if (id != null && id.length)
    {
        var Wtyps = $scope.worktypes.filter(function (a) { return (a.FWFlg == "F") });
        $scope.worktype = {};
        $scope.worktype.selected = Wtyps[0];
    }
    $scope.goBack = function () {
        if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor)
        {
            $ionicPopup.confirm(
                    {
                        title: 'Confirm Navigation',
                        content: 'You have unsaved record do you want to save it in draft...?'
                    }).then(function (res) {
                if (res) {
                    fmcgLocalStorage.addData('draft', $scope.fmcgData);
                    var value = $scope.$parent.fmcgData;
                    for (key in value)
                    {
                        if (key)
                        {
                            if(key!='jontWorkSelectedList') $scope.$parent.fmcgData[key] = undefined;

                        }
                    }
                    $state.go('fmcgmenu.home');

                } else {
                    $state.go('fmcgmenu.home');
                    var value = $scope.$parent.fmcgData;
                    for (key in value)
                    {
                        if (key)
                        {
                            if(key!='jontWorkSelectedList') $scope.$parent.fmcgData[key] = undefined;

                        }
                    }
                }
            }
            );
        }
        else
        {
            $state.go('fmcgmenu.home');
            var value = $scope.$parent.fmcgData;
            for (key in value)
            {
                if (key)
                {
                    if(key!='jontWorkSelectedList') $scope.$parent.fmcgData[key] = undefined;

                }
            }
        }

    };
    $scope.goNext = function () {
        if ($scope.$parent.fmcgData.worktype.selected.FWFlg != "F" || $scope.view_MR == 1) {
            $scope.fmcgData.subordinate = {}
            $scope.fmcgData.subordinate.selected = {}
            $scope.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g,'');
        }
        if ($scope.$parent.fmcgData.worktype.selected.FWFlg == "F")
        {
            var proceed = true;
            var msg = "";
            if (!$scope.fmcgData.subordinate && proceed && $scope.view_MR!=1)
            {
                msg = "Please Select Headquarters      ";
                proceed = false;
            }
            if (!$scope.fmcgData.cluster && proceed)
            {
                msg = "Please Select Cluster       ";
                proceed = false;
            }
            if (!$scope.fmcgData.customer && proceed)
            {
                msg = msg + "Please Select Visit To         "
                proceed = false;
            }
            if (proceed)
            {
                $scope.$parent.fmcgData.source = 1;
                $state.go('fmcgmenu.screen2');
            }
            else
            {
                Toast(msg, true);
            }
        }
        else
            $state.go('fmcgmenu.screen5');
    };
})
.controller('screen4Ctrl', function ($rootScope, $scope, $state, $filter, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.setAllow();
    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen4";
    $scope.$parent.fmcgData.giftSelectedList = $scope.$parent.fmcgData.giftSelectedList || [];
    $scope.addProduct = function (selected) {
        var giftData = {};
        giftData.gift = selected;
        giftData.sample_qty = 1;
        $scope.$parent.fmcgData.giftSelectedList.push(giftData);
    }

    $scope.gridOptions = {
        data: 'fmcgData.giftSelectedList',
        rowHeight: 50,
        enableRowSelection: false,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{field: 'gift', displayName: 'Input Name', enableCellEdit: false, cellTemplate: 'partials/giftCellTemplate.html'},
            {field: 'sample_qty', displayName: 'Input Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate1.html", width: 90},
            {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
    };
    $scope.removeRow = function () {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.giftSelectedList.splice(index, 1);
    };
    $scope.goBack = function () {
        if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed==0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed==0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed==0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed==0))
            $state.go('fmcgmenu.screen3');
        else
            $state.go('fmcgmenu.screen2');
    };
    $scope.goNext = function () {
        $state.go('fmcgmenu.screen5');
    };
    $scope.save = function () {
        $scope.saveToDraftO();
    }
})
.controller('AppController', function ($scope, $ionicSideMenuDelegate) {
    $scope.toggleLeft = function () {
        $ionicSideMenuDelegate.toggleLeft();
    };
})
.controller('screen3Ctrl', function ($rootScope, $scope, $filter, $state, $ionicModal, $ionicSideMenuDelegate, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen3";
    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();

    $scope.$parent.fmcgData.productSelectedList = $scope.$parent.fmcgData.productSelectedList || [];

    $scope.addProduct = function (selected) {
        var productData = {};
        productData.product = selected;
        productData.product_Nm = (selected.toString()!="")?$filter('getValueforID')(selected, $scope.products).name:"";
        productData.rx_qty = 0;
        productData.sample_qty = 0;
        var len = $scope.$parent.fmcgData.productSelectedList.length;
        var flag = true;
        for (var i = 0; i < len; i++)
        {
            if ($scope.$parent.fmcgData.productSelectedList[i]['product'] === selected)
            {
                flag = false;
            }
        }
        if (flag)
            $scope.$parent.fmcgData.productSelectedList.push(productData);
    }
    scrTyp=$scope.$parent.fmcgData.customer.selected.id
    if (scrTyp == 1 || scrTyp == 4)
    {
        
        RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        $scope.gridOptions = {
            data: 'fmcgData.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html'},
                {field: 'rx_qty', displayName: RxCap, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60, visible:false},
                {field: 'sample_qty', displayName: SmplCap, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate1.html", width: 90 },
                {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
        };
    }
    else
    {
        QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
        $scope.gridOptions = {
            data: 'fmcgData.productSelectedList',
            rowHeight: 50,
            rowTemplate: 'rowTemplate.html',
            enableCellSelection: true,
            enableColumnResize: true,
            enableRowSelection: false,
            plugins: [new ngGridFlexibleHeightPlugin()],
            showFooter: true,
            columnDefs: [{field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html'},
                {field: 'rx_qty', displayName: QCap, enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
                {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
        };
    }
    $scope.removeRow = function () {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.productSelectedList.splice(index, 1);
    };
    $scope.save = function () {
        $scope.saveToDraftO();
        $state.go('fmcgmenu.home');
    }
    $scope.goBack = function () {

        $state.go('fmcgmenu.screen2');
    };
    $scope.goNext = function () {
        if ((',1,4,').indexOf(',' + $scope.fmcgData.customer.selected.id + ',') > -1 && $scope.AppTyp == 1) {
            if ($scope.fmcgData.productSelectedList.length > 0) {
                if ($scope.fmcgData.OrdStk == undefined || $scope.fmcgData.OrdStk.selected.id == "") {
                    Toast('Select the ' + $scope.StkCap + ' Name');
                    return false;
                }
            }
        }
        if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed==0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed==0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed==0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed==0))
            $state.go('fmcgmenu.screen4');
        else
            $state.go('fmcgmenu.screen5');
    };
})
.controller('screen2Ctrl', function ($rootScope, $scope, $state, $timeout, $filter, $ionicModal, fmcgAPIservice, fmcgLocalStorage, notification) 
{
    if ($scope.Myplns.length == 0) {
        $state.go('fmcgmenu.myPlan');
    }
    if (!($scope.$parent.fmcgData.cluster)) {
        if ($scope.Myplns[0].subordinateid == "" && $scope.view_MR != 1) {
            $state.go('fmcgmenu.addNew');
        } else {
            if ($scope.view_MR == 1)
                $scope.cDataID = '_' + $scope.sfCode;
            else
                $scope.cDataID = '_' + $scope.Myplns[0].subordinateid; //alert($scope.cDataID);
            $scope.loadDatas(false, $scope.cDataID);

            $scope.$parent.fmcgData.subordinate = {};
            $scope.$parent.fmcgData.subordinate.selected = {};
            $scope.$parent.fmcgData.subordinate.selected.id = $scope.cDataID.replace(/_/g, '');

            $scope.$parent.fmcgData.cluster = {};
            $scope.$parent.fmcgData.cluster.selected = {};
            $scope.$parent.fmcgData.cluster.selected.id = $scope.Myplns[0].clusterid;
            
        }
    }
    $scope.modal = $ionicModal;
    var tDate = new Date();
    if (!$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.arc && !$scope.$parent.fmcgData.isDraft) {
        if ($scope.cComputer || cordova.platformId=="ios") {
            $scope.$parent.fmcgData.entryDate = tDate;
            $scope.$parent.fmcgData.modifiedDate = tDate;
        } else {
            window.sangps.getDateTime(function (tDate) {
                $scope.$parent.fmcgData.entryDate = tDate;
                $scope.$parent.fmcgData.modifiedDate = tDate;
            });
        }
    }
    else {
        if ($scope.cComputer || cordova.platformId=="ios") {
            $scope.$parent.fmcgData.modifiedDate = tDate;
        } else {
            window.sangps.getDateTime(function (tDate) {
                $scope.$parent.fmcgData.modifiedDate = tDate;
            });
        }
        var tDate = new Date($scope.$parent.fmcgData.entryDate);
        $scope.$parent.fmcgData.entryDate = tDate;
    }

    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();
    $scope.$parent.fmcgData.currentLocation = "fmcgmenu.screen2";
    $scope.customers = [{
            'id': '1',
            'name': $scope.DrCap
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap
        });
        $scope.nCI = al; al++;
    }
    
    $scope.save = function () {
        if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor)
        {
            $scope.saveToDraftO();
        }
        else
        {
            Toast("No data to save in draft", true);
        }
    };
    $scope.$parent.fmcgData.jontWorkSelectedList = $scope.$parent.fmcgData.jontWorkSelectedList || [];
    $scope.addProduct = function (selected)
    {
        var jontWorkData = {};
        jontWorkData.jointwork = selected;
        $scope.$parent.fmcgData.jontWorkSelectedList.push(jontWorkData);
    }
    ;
    $scope.gridOptions = {
        data: 'fmcgData.jontWorkSelectedList',
        rowHeight: 50,
        enableRowSelection: false,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{field: 'jointworkname', displayName: 'Jointwork', enableCellEdit: false, cellTemplate: 'partials/jointworkCellTemplate.html'},
            {field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50}]
    };
    $scope.removeRow = function () {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.fmcgData.jontWorkSelectedList.splice(index, 1);
    };
    $scope.goNext = function () {
        var temp;
        var msg = "";
        var proceed = true;
        if ($scope.fmcgData.doctor || $scope.fmcgData.chemist || $scope.fmcgData.stockist || $scope.fmcgData.uldoctor)
        {
        }
        else
        {
            proceed = false;
            msg = "Please Select " + $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
        }
        /* 
        var pobT = $scope.fmcgData.pob ? parseInt($scope.fmcgData.pob) : 0;
        if (isNaN(pobT) || pobT > 999999)
        {
            $scope.fmcgData.pob = "";
            proceed = false;
            msg = msg + "Please Check the input pob";
        }*/
        if ($scope.fmcgData.pob == undefined && $scope.Doc_Pob_Mandatory_Need == 0 && $scope.fmcgData.customer.selected.id == 1) {
            proceed = false;
            msg = msg + "Enter the input pob..";
        }
        if ($scope.fmcgData.pob == undefined && $scope.Chm_Pob_Mandatory_Need == 0 && $scope.fmcgData.customer.selected.id == 2) {
            proceed = false;
            msg = msg + "Enter the input pob..";
        }
        if (proceed)
        {
            if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed == 0))
                $state.go('fmcgmenu.screen3');
            else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed == 0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed == 0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed == 0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed == 0))
                $state.go('fmcgmenu.screen4')
            else
                $state.go('fmcgmenu.screen5');
        }
        else
        {
            Toast(msg, true);
        }
    };
    $scope.goBack = function () {
        $state.go('fmcgmenu.addNew');
    };

}).controller('screen4sCtrl', function ($scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {

}).controller('screen5Ctrl', function ($rootScope, $scope, $state, $ionicPopup, $filter, geolocation, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {

    if ($scope.$parent.fmcgData.customer) {
        $scope.$parent.navTitle = $filter('getValueforID')($scope.$parent.fmcgData.customer.selected.id, $scope.customers).name;
    } else {
        $scope.$parent.navTitle = "";
    }
    $scope.setAllow();
    $scope.updateValue = function (value) {
        $scope.$parent.fmcgData.remarks = value.name;
    };
    $scope.saveD = function () {

        $scope.fmcgData.toBeSync = false;
        $scope.saveToDraftO();
    };
    $scope.save = function () {
        /*if (($scope.fmcgData.location == undefined || $scope.fmcgData.location == "") && $scope.GeoChk == 0) {
            alert("Location Not Found,. Try Again...");
            return false;
        }*/
        function _clrData(scope) {
            var value = scope.fmcgData;
            for (key in value) {
                if (key) {
                    if (key != 'jontWorkSelectedList') scope.fmcgData[key] = undefined;
                }
            }
        }
        function _savLocal(scope, stat) {
            scope.fmcgData.toBeSync = true;
            fmcgLocalStorage.addData('saveLater', scope.fmcgData);
            _clrData(scope);
            stat.go('fmcgmenu.home');

            Toast("No Connection! Call Saved Locally", true);
        };

        $rootScope.hasData = false;
        $rootScope.hasEditData = false;
        if ($scope.fmcgData.worktype.selected.FWFlg == "F") {
            if ($scope.reportTemplates.length > 0) {
                if ($scope.fmcgData.rx_t == undefined && $scope.$parent.fmcgData.customer.selected.id == "1") {
                    Toast('Select Call Feedback....');
                    return false;
                }
            }
            if ($scope.nonreportTemplates.length > 0) {
                if (($scope.fmcgData.remarks == undefined || $scope.fmcgData.remarks == '') && $scope.$parent.fmcgData.customer.selected.id == "1") {
                    Toast('Select the Template ( or ) Enter the Remarks....');
                    return false;
                }
            }
        }
        if ($scope.photosList.length > 0)
        {
            fmcgLocalStorage.addData("events", $scope.photosList);
        }
        $scope.$emit('ClearEvents');
        if ($scope.fmcgData.arc) {
            var arc = $scope.fmcgData.arc;
            var amc = $scope.fmcgData.amc;
            $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
            fmcgAPIservice.saveDCRData('POST', 'dcr/updateEntry&arc=' + arc + '&amc=' + amc, $scope.fmcgData, true)
            .success(function (response) {
                _clrData($scope);
                Toast("Call updated SuccessFully");
                $state.go('fmcgmenu.home'); $ionicLoading.hide();
            }).error(function () {
                _savLocal($scope, $state);
                $state.go('fmcgmenu.home'); $ionicLoading.hide();
            });
        } else {
            $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
            fmcgAPIservice.saveDCRData('POST', 'dcr/save', $scope.fmcgData, false).success(function (response) {                
                if (!response['success'] && response['type'] == 2) {
                    $state.go('fmcgmenu.home');
                    Toast(response['msg']);
                    $ionicLoading.hide();
                }
            else if (!response['success'] && response['type'] == 1) {
                    $scope.showConfirm = function () {
                        var confirmPopup = $ionicPopup.confirm({
                            title: 'Warning !',
                            template: response['msg']
                        });
                        confirmPopup.then(function (res) {
                            if (res) {
                                $ionicLoading.show({ template: 'Call Submitting. Please wait...' });
                                fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false).success(function (response) {                                    
                                    if (!response['success']) {
                                        Toast(response['msg'], true);
                                        _clrData($scope);
                                        $state.go('fmcgmenu.home'); $ionicLoading.hide();
                                    }
                                    else {
                                        Toast("call Updated Successfully");
                                        if ($scope.fmcgData.customer.selected.id == 1) {
                                            $scope.RCPA.subordinate = {};
                                            $scope.RCPA.subordinate.selected = {};
                                            $scope.RCPA.cluster = {};
                                            $scope.RCPA.cluster.selected = {};
                                            $scope.RCPA.doctor = {};
                                            $scope.RCPA.doctor.selected = {};
                                            if ($scope.view_MR != 1) {
                                                $scope.RCPA.subordinate.selected.id = $scope.fmcgData.subordinate.selected.id;
                                                $scope.RCPA.subordinate.name = $scope.fmcgData.subordinate.name;
                                            }
                                            $scope.RCPA.cluster.selected.id = $scope.fmcgData.cluster.selected.id.replace(',', '');
                                            $scope.RCPA.cluster.name = $scope.fmcgData.cluster.name;
                                            $scope.RCPA.doctor.selected.id = $scope.fmcgData.doctor.selected.id;
                                            $scope.RCPA.doctor.name = $scope.fmcgData.doctor.name;
                                            _clrData($scope);
                                            $state.go('fmcgmenu.RCPA'); $ionicLoading.hide();
                                        }
                                        else {
                                            _clrData($scope);
                                            $state.go('fmcgmenu.home'); $ionicLoading.hide();
                                        }
                                    }
                                   
                                })

                                .error(function () {
                                    Toast("call Updation Failed...");
                                    _clrData($scope);
                                    $state.go('fmcgmenu.home'); $ionicLoading.hide();
                                });
                            } else {
                                _clrData($scope);
                                $state.go('fmcgmenu.home');$ionicLoading.hide();
                            }
                        });
                    };
                    $ionicLoading.hide();
                    $scope.showConfirm();
                }
                else
                {
                if (!response['success']) {
                    Toast(response['msg'], true);
                    _clrData($scope);
                    $state.go('fmcgmenu.home'); $ionicLoading.hide();
                }
                else {
                    Toast("call Submited Successfully");
                    if ($scope.fmcgData.customer.selected.id == 1) {
                        $scope.RCPA.subordinate = {};
                        $scope.RCPA.subordinate.selected = {};
                        $scope.RCPA.cluster = {};
                        $scope.RCPA.cluster.selected = {};
                        $scope.RCPA.doctor = {};
                        $scope.RCPA.doctor.selected = {};
                        $scope.RCPA.chemist = {};
                        $scope.RCPA.chemist.selected = {};
                        //if ($scope.view_MR != 1) {
                            $scope.RCPA.subordinate.selected.id = $scope.fmcgData.subordinate.selected.id;
                            $scope.RCPA.subordinate.name = $scope.fmcgData.subordinate.name;
                       // }
                        $scope.RCPA.cluster.selected.id = $scope.fmcgData.cluster.selected.id.replace(',', '');
                        $scope.RCPA.cluster.name = $scope.fmcgData.cluster.name;
                        $scope.RCPA.doctor.selected.id = $scope.fmcgData.doctor.selected.id;
                        $scope.RCPA.doctor.name = $scope.fmcgData.doctor.name;
                       console.log("chemist_master_"+$scope.RCPA.subordinate.selected.id);
                        data = fmcgLocalStorage.getData("chemist_master_"+$scope.RCPA.subordinate.selected.id) || [];
                        console.log('data length'+data.length)
                        $scope.FilteredData = data.filter(function (a) {
                             a.town_code = a.town_code + ',';
                             a.town_code = a.town_code.replace(',,', ',');
                             console.log('(,' + a.town_code+').indexof(,' + $scope.RCPA.cluster.selected.id + ',)');
                             return ((',' + a.town_code).indexOf(',' + $scope.RCPA.cluster.selected.id + ',') > -1);
                        });
                        console.log($scope.FilteredData.length);
                        if($scope.FilteredData.length>0){
                             $scope.RCPA.chemist.selected.id = $scope.FilteredData[0].id;
                             $scope.RCPA.chemist.name = $scope.FilteredData[0].name;
                        }
                        console.log($scope.fmcgData.cluster.selected.id);
                        _clrData($scope);
                        $state.go('fmcgmenu.RCPA');
                        $ionicLoading.hide();
                    }
                    else {
                        _clrData($scope);
                        $state.go('fmcgmenu.home'); $ionicLoading.hide();
                    }
                }
                   
                }
            })
            .error(function () {
                _savLocal($scope, $state);
                $state.go('fmcgmenu.home'); $ionicLoading.hide();
            });
        }
        
    };
    $scope.goBack = function () {
        if ($scope.fmcgData.worktype.selected.FWFlg.toString() !== "F")
        {
            $state.go('fmcgmenu.addNew')
        }
        else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DINeed==0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CINeed==0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SINeed==0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NINeed==0))
            $state.go('fmcgmenu.screen4')
        else if (($scope.fmcgData.customer.selected.id == 1 && $scope.DPNeed==0) || ($scope.fmcgData.customer.selected.id == 2 && $scope.CPNeed==0) || ($scope.fmcgData.customer.selected.id == 3 && $scope.SPNeed==0) || ($scope.fmcgData.customer.selected.id == 4 && $scope.NPNeed==0))
            $state.go('fmcgmenu.screen3');
        else
            $state.go('fmcgmenu.screen2');
    };
})
.controller('RCPACtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage) {
    $scope.$parent.navTitle = "RCPA Entry";
    $scope.gridOptions = {
        data: 'RCPA.productSelectedList',
        rowHeight: 50,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        enableRowSelection: false,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
            { field: 'qty', displayName: 'Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
            { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
    };

    if ($scope.cComputer || cordova.platformId=="ios") {
        $scope.RCPA.eDate = new Date();
    } else {
        window.sangps.getDateTime(function (tDate) {
            $scope.RCPA.eDate = tDate;
        });
    }
    $scope.removeRow = function () {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.RCPA.productSelectedList.splice(index, 1);
    };
    $scope.clear = function () {
        if ($scope.view_MR == 1) {
            $scope.RCPA.subordinate = {};
            $scope.RCPA.subordinate.selected = {};
            $scope.RCPA.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false,'_'+ $scope.sfCode)
        } else {
            if ($scope.RCPA.subordinate == undefined)
            $scope.RCPA.subordinate = undefined;
        }
        if($scope.RCPA.cluster==undefined) $scope.RCPA.cluster = undefined;
        if($scope.RCPA.chemist==undefined) $scope.RCPA.chemist = undefined;
        if ($scope.RCPA.doctor == undefined) $scope.RCPA.doctor = undefined;
        $scope.RCPA.OurBrndCds = "";
        $scope.RCPA.OurBrndNms = "";
        $scope.RCPA.productSelectedList = [];

        $scope.RCPA.CpmptName = "";
        $scope.RCPA.CpmptBrnd = "";
        $scope.RCPA.CpmptQty = "";
        $scope.RCPA.CpmptPOB = "";
        $scope.RCPA.CpmptRate = "";
        $scope.RCPA.Precrip = "";
    }
    $scope.clear();
    $scope.save = function () {
        $scope.data = {};
        if ($scope.view_MR != 1) {
            if ($scope.RCPA.subordinate == undefined || $scope.RCPA.subordinate.selected.id == "")
            {
                Toast('Select the Headquarters');
                return false;
            }
        }

        if ($scope.AppTyp != 1) {
            if ($scope.RCPA.chemist == undefined || $scope.RCPA.chemist.selected.id == "") {
                Toast('Select the ' + $scope.ChmCap + ' Name');
                return false;
            }
            else
                $scope.RCPA.chemist.selected.id = ' ';
        }
        if ($scope.RCPA.doctor == undefined || $scope.RCPA.doctor.selected.id == "") {
            Toast('Select the ' + $scope.DrCap + ' Name');
            return false;
        }
        tmPrd = $scope.RCPA.productSelectedList;
        if (tmPrd.length<1) {
            Toast('Select the Our Brand');
            return false;
        }
        RCPAProdQty = "";
        RCPAProdNmQty = "";
        for (i = 0; i < tmPrd.length; i++) {
            RCPAProdQty += tmPrd[i].product + " ( " + tmPrd[i].qty + " ), ";
            RCPAProdNmQty += tmPrd[i].product_Nm + " ( " + tmPrd[i].qty + " ), ";
        }
        $scope.RCPA.OurBrndCds = RCPAProdQty;
        $scope.RCPA.OurBrndNms = RCPAProdNmQty;

        if ($scope.RCPA.CpmptName == undefined || $scope.RCPA.CpmptName == "") {
            Toast('Enter the Competitor Name');
            return false;
        }
        if ($scope.RCPA.CpmptBrnd == undefined || $scope.RCPA.CpmptBrnd == "") {
            Toast('Enter the Competitor Brand');
            return false;
        }

        $scope.data.RCPADt= new Date();
        if ($scope.AppTyp != 1) {
            $scope.data.ChmId = $scope.RCPA.chemist.selected.id;
            $scope.data.ChmName = $scope.RCPA.chemist.name;
        } else {
            $scope.data.ChmId = '';
            $scope.data.ChmName = '';
        }
        $scope.data.RCPADt = $scope.RCPA.eDate;
        $scope.data.DrId = $scope.RCPA.doctor.selected.id;
        $scope.data.DrName = $scope.RCPA.doctor.name;
        $scope.data.ourBrnd = $scope.RCPA.OurBrndCds;
        $scope.data.ourBrndNm = $scope.RCPA.OurBrndNms;

        $scope.data.CmptrName = $scope.RCPA.CpmptName;
        $scope.data.CmptrBrnd = $scope.RCPA.CpmptBrnd;
        $scope.data.CmptrQty = $scope.RCPA.CpmptQty;
        $scope.data.CmptrPOB = $scope.RCPA.CpmptPOB;
        $scope.data.CmptrPriz = $scope.RCPA.CpmptRate;         
        $scope.data.Remark=$scope.RCPA.Precrip;
        $scope.clear();
        
        fmcgAPIservice.addMAData('POST', 'dcr/save', "4", $scope.data).success(function (response) {
            if (response.success) Toast("RCPA Entry Submited Successfully");
        }).error(function () {
            $scope.OutRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
            $scope.OutRCPA.push($scope.data);
            localStorage.removeItem("OutBx_RCPA");
            fmcgLocalStorage.createData("OutBx_RCPA", $scope.OutRCPA);
            Toast("No Internet Connection! RCPA Entry Saved in Outbox");
        });
        $state.go('fmcgmenu.home');
    };
})
.controller('RMCLCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage) {
    $scope.$parent.navTitle = "Reminder Calls";
    $scope.gridOptions = {
        data: 'RMCL.productSelectedList',
        rowHeight: 50,
        rowTemplate: 'rowTemplate.html',
        enableCellSelection: true,
        enableColumnResize: true,
        enableRowSelection: false,
        plugins: [new ngGridFlexibleHeightPlugin()],
        showFooter: true,
        columnDefs: [{ field: 'product_Nm', displayName: 'Product', enableCellEdit: false, cellTemplate: 'partials/productCellTemplate.html' },
            { field: 'qty', displayName: 'Qty', enableCellEdit: true, editableCellTemplate: "partials/cellEditTemplate.html", width: 60 },
            { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }]
    };

    if ($scope.cComputer || cordova.platformId=="ios") {
        $scope.RMCL.eDate = new Date();
    } else {
        window.sangps.getDateTime(function (tDate) {
            $scope.RMCL.eDate = tDate;
        });
    }
    $scope.removeRow = function () {
        var index = this.row.rowIndex;
        $scope.gridOptions.selectItem(index, false);
        $scope.$parent.RMCL.productSelectedList.splice(index, 1);
    };
    $scope.clear = function () {
        if ($scope.view_MR == 1) {
            $scope.RMCL.subordinate = {};
            $scope.RMCL.subordinate.selected = {};
            $scope.RMCL.subordinate.selected.id = $scope.sfCode;
            $scope.loadDatas(false, '_' + $scope.sfCode)
        } else {
            $scope.RMCL.subordinate = undefined;
        }
        $scope.RMCL.doctor = undefined;
        $scope.RMCL.JntWrkCds = "";
        $scope.RMCL.JntWrkNms = "";
        $scope.RMCL.jontWorkSelectedList = [];
        $scope.RMCL.ProdCds = "";
        $scope.RMCL.ProdNms = "";
        $scope.RMCL.productSelectedList = [];
        $scope.RMCL.remarks = "";
    }
    $scope.clear();
    $scope.save = function () {
        $scope.data = {};
        if ($scope.view_MR != 1) {
            if ($scope.RMCL.subordinate == undefined || $scope.RMCL.subordinate.selected.id == "") {
                Toast('Select the Headquarters');
                return false;
            }
        }
        if ($scope.RMCL.doctor == undefined || $scope.RMCL.doctor.selected.id == "") {
            Toast('Select the ' + $scope.DrCap + ' Name');
            return false;
        }
        if ($scope.RMCL.JntWrkCds == undefined) $scope.RMCL.JntWrkCds = '';
        /*if ($scope.RMCL.JntWrkCds == undefined || $scope.RMCL.JntWrkCds == "") {
            Toast('Select the Joint Work');
            return false;
        }*/
        if ($scope.RMCL.ProdCds == undefined || $scope.RMCL.ProdCds == "") {
            Toast('Select the Product');
            return false;
        }

        $scope.data.RMCLDt = $scope.RMCL.eDate;
        $scope.data.DrId = $scope.RMCL.doctor.selected.id;
        $scope.data.DrName = $scope.RMCL.doctor.name;
        $scope.data.JntWrkCds = $scope.RMCL.JntWrkCds;
        $scope.data.JntWrkNms = $scope.RMCL.JntWrkNms;
        tmPrd = $scope.RMCL.productSelectedList;
        RMCLProdQty = "";
        RMCLProdNmQty = "";
        for (i = 0; i < tmPrd.length; i++) {
            RMCLProdQty += tmPrd[i].product + " ( " + tmPrd[i].qty + " ), ";
            RMCLProdNmQty += tmPrd[i].product_Nm + " ( " + tmPrd[i].qty + " ), ";
        }
        if (RMCLProdQty == '') { Toast('Select the Product'); return false; }
        $scope.data.ProdCds = RMCLProdQty;
        $scope.data.ProdNms = RMCLProdNmQty;
        $scope.data.Remark = $scope.RMCL.remarks;
        $scope.clear();

        fmcgAPIservice.addMAData('POST', 'dcr/save', "5", $scope.data).success(function (response) {
            if (response.success) Toast("Reminder Calls Submited Successfully");
        }).error(function () {
            $scope.OutRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
            $scope.OutRMCL.push($scope.data);
            localStorage.removeItem("OutBx_RMCL");
            fmcgLocalStorage.createData("OutBx_RMCL", $scope.OutRMCL);
            Toast("No Internet Connection!.\n Reminder Calls Saved in Outbox");
        });
        $state.go('fmcgmenu.home');
    };
})
.controller('precallCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, $ionicLoading) {
    $scope.$parent.navTitle = "Precall Analysis";
    if ($scope.view_MR == 1) {
        $scope.precall.subordinate = {};
        $scope.precall.subordinate.selected = {};
        $scope.precall.subordinate.selected.id = $scope.sfCode;
        $scope.loadDatas(false, '_' + $scope.sfCode)
    }
    else {
        $scope.precall.subordinate = undefined
    }

    $scope.precall.doctor = undefined
    $scope.$on('getPreCall', function (evnt, DrCd) {
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getPostData('POST', 'get/precall&Msl_No=' + DrCd.DrId, []).success(function (response) {
            $scope.vwPreCall = response;
            $ionicLoading.hide();
        }).error(function () {
            Toast("No Internet Connection!");
            $ionicLoading.hide();
        });
    });
})
      .controller('ExpenseEntryCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {


          $scope.update = 0;
          $scope.data = {};
          $scope.$parent.navTitle = "Expense Entry ";
          $scope.getTP = {};
          $scope.getExpense = {};
          $scope.extraDetails = [{ "parameter": "Courier Charges", "type": true, "amount": "" },
              { "parameter": "Xerox Charges", "type": true, "amount": "" },
          { "parameter": "Printout Charges", "type": true, "amount": "" },
          { "parameter": "Sample and Clearing", "type": true, "amount": "" },
          { "parameter": "Miscellaneous Charges", "type": true, "amount": "" }, ];
          $scope.grandTotal = 0;

          view();
          function view() {
              $ionicLoading.show({
                  template: 'Loading...'
              });
              fmcgAPIservice.getDataList('POST', 'table/list', ["getTPDet"])
                                    .success(function (response) {
                                        window.localStorage.removeItem("getTP");
                                        fmcgLocalStorage.createData("getTP", response);
                                        if (response.length == 0)
                                            console.log("no record");
                                        else {
                                            $scope.getTPlength = response.length;
                                            if ($scope.getTPlength != 0) {
                                                $scope.worktype = response[0].WorkType_Name;
                                                $scope.place = response[0].place;
                                            }
                                        }
                                    });

              fmcgAPIservice.getDataList('POST', 'table/list', ["getExpenseDet"])
                                       .success(function (response) {
                                           window.localStorage.removeItem("getExpense");

                                           if (response.length == 0) {
                                               console.log("no records");
                                               $ionicLoading.hide();
                                           }
                                           else {
                                               $scope.update = 1;
                                               expenseDet = response.extraDetails;
                                               head = response.head;
                                               $scope.data.tot = head.Expense_Total;
                                               $scope.data.allowance = head.Expense_Allowance;
                                               $scope.data.fare = head.Expense_Fare;
                                               $scope.data.distance = head.Expense_Distance;
                                               $scope.amttot = 0;
                                               for (var key in expenseDet) {
                                                   if (expenseDet[key]['type'] == 0) {
                                                       expenseDet[key]['type'] = true;
                                                       amt = expenseDet[key]['amount'];

                                                   }
                                                   else {
                                                       amt = -expenseDet[key]['amount'];
                                                       expenseDet[key]['type'] = false;
                                                   }
                                                   $scope.amttot = amt + $scope.amttot;
                                                  $scope.grandTotal= $scope.amttot + $scope.data.tot;
                                               }
                                               if (expenseDet.length != 0)
                                                   $scope.extraDetails = expenseDet;
                                               $ionicLoading.hide();
                                           }
                                       });
          }

          var date = new Date();
          $scope.dat = $filter('date')(new Date(), 'dd/MM/yyyy');

          $scope.date = $scope.dat;

          $scope.calculateTot = function () {
              var extraDetails = $scope.extraDetails;
              var amtTot = 0;
              for (var key in extraDetails) {
                  if (extraDetails[key]['type'] == true)
                      amtTot = extraDetails[key]['amount'] + amtTot;
                  if (extraDetails[key]['type'] == false)
                      amtTot = -extraDetails[key]['amount'] + amtTot;
              }
              $scope.amttot = amtTot;
              $scope.grandTotal = $scope.amttot + $scope.data.tot;
          }
          $scope.total = function () {
              if (this.data.fare == undefined)
                  $scope.data.tot = 0 + this.data.allowance;
              else if (this.data.allowance == undefined)
                  $scope.data.tot = this.data.fare + 0;
              else
                  $scope.data.tot = this.data.fare + this.data.allowance;
              $scope.grandTotal = $scope.amttot + $scope.data.tot;
          }
          $scope.addFields = function () {
              $scope.extraDetails.push({ parameter: '', type: true, amount: '' });
          }
          $scope.delete = function (index) {
              $scope.extraDetails.splice(index, 1);
          }
          //  $scope.data.inactive = true;

          //ng-disabled="data.inactive"
          $scope.getTP = fmcgLocalStorage.getData("getTP") || [];

          $scope.getTPlength = $scope.getTP.length;
          $scope.submit = function () {
              $scope.getTP = fmcgLocalStorage.getData("getTP") || [];

              $scope.getTPlength = $scope.getTP.length;
              $scope.data.worktype = addQuotes($scope.getTP[0].Work_Type);
              if ($scope.getTP[0].Work_Type == undefined) {
                  Toast('Not update Dcr Entry...');
                  return false;
              }
              $ionicLoading.show({
                  template: 'Loading...'
              });
              $scope.data.worktype_name = addQuotes($scope.getTP[0].WorkType_Name);
              $scope.data.placeno = addQuotes($scope.getTP[0].placeNo);
              $scope.data.place = addQuotes($scope.getTP[0].place);
              $scope.data.sfCode = addQuotes($scope.getTP[0].Sf_Code);
              $scope.data.sfName = addQuotes($scope.getTP[0].Sf_Name);
              $scope.data.date = addQuotes($scope.date);
              $scope.data.expense_total = $scope.tot;
              $scope.data.extraDetails = $scope.extraDetails;
              $scope.data.additionalTot = $scope.amttot;
              $scope.data.allowance = $scope.data.allowance;
              $scope.data.fare = $scope.data.fare;
              $scope.data.distance = $scope.data.distance;
              fmcgAPIservice.addMAData('POST', 'dcr/save&update=' + $scope.update, "20", $scope.data).success(function (response) {
                  if (response.success) {
                      $ionicLoading.hide();
                      Toast("Expense Entered Successfully");
                      $state.go('fmcgmenu.home');
                  }
              }).error(function () {
                  Toast("No Internet Connection! Try Again.");
                  $ionicLoading.hide();
              });
          }


      }])
      .controller('eExpenseCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
             $scope.$parent.navTitle = "Expense Entry ";
             //$scope.iframeURL="http://sanffa.info/masterfiles/mr/RptAutoExpense_RowWise.aspx?App=1&mon=2&year=2017&sf_code=MR0397";
             $("#ifrm").attr("src","http://sanffa.info/masterfiles/mr/RptAutoExpense_RowWise.aspx?App=1&mon=2&year=2017&sf_code=MR0397");
                                   $("div.backdrop").css("display","none");
                                   console.log($scope.iframeURL);
      }])

.controller('TourPlanCtrl', ['$rootScope', '$scope', '$state', '$location', 'fmcgAPIservice', 'fmcgLocalStorage', '$ionicSideMenuDelegate', 'notification', '$ionicLoading', '$ionicModal', '$filter', function ($rootScope, $scope, $state, $location, fmcgAPIservice, fmcgLocalStorage, $ionicSideMenuDelegate, notification, $ionicLoading, $ionicModal, $filter) {
         $scope.$parent.navTitle = "Add TP Entry";
         //   $scope.No_of_TP_View=2;
         
         var temp = window.localStorage.getItem("loginInfo");
         var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
         var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
         $scope.clearIdividual(21, 1, '_' + sRSF);
         $scope.update = 0;
         $scope.$parent.fmcgData.clstrData2 = $scope.$parent.fmcgData.clstrData2 || [];
         $scope.$on('changeGrid', function (e,i) {
            $scope.gridOptions.selectItem(i, false);
            $scope.fmcgData.clstrData2.splice(i, 1);
        });
         $scope.changeMonth = function () {
            $state.go('fmcgmenu.TourPlanLink');
         }
         $scope.gridOptions = {
         data: 'fmcgData.clstrData2',
         rowHeight: 50,
         rowTemplate: 'rowTemplate.html',
         enableCellSelection: true,
         enableColumnResize: true,
         enableRowSelection: false,
         plugins: [new ngGridFlexibleHeightPlugin()],
         showFooter: true,
         columnDefs: [{ field: 'HQ', displayName: 'Headquarters', enableCellEdit: false, cellTemplate: 'partials/hqCellTemplate.html' },
                      { field: 'Cluster', displayName: 'Clusters', enableCellEdit: false, cellTemplate: "partials/clusterCellTemplate.html" },
                      { field: 'remove', displayName: '', enableCellEdit: false, cellTemplate: 'partials/removeButton.html', width: 50 }
                      ]
         };
         $scope.removeRow = function () {
         var index = this.row.rowIndex;
         $scope.gridOptions.selectItem(index, false);
         $scope.fmcgData.clstrData2.splice(index, 1);
         $scope.fmcgData.clstrData1.splice(index, 1);
         };
         $scope.Mypln.worktype = {};
         $scope.Mypln.worktype.selected = {};
         $scope.Mypln.worktype2 = {};
         $scope.Mypln.worktype2.selected = {};
         $scope.Mypln.worktype3 = {};
         $scope.Mypln.worktype3.selected = {};
         $scope.Mypln.stockist = {};
         $scope.Mypln.stockist.selected = {};
         $scope.bac = "red";
         //  var Wtyps = $scope.worktypes.filter(function (a) {
         //   return (a.FWFlg == "F")
         // });
         // $scope.Mypln.worktype.selected = Wtyps[0];
         $scope.Mypln.subordinate = {};
         $scope.Mypln.subordinate.selected = {};
         if ($scope.view_MR == 1) {
         $scope.Mypln.subordinate.selected.id = $scope.sfCode;
         } else {
         $scope.Mypln.subordinate.selected.id = undefined;
         }
         
         if (($scope.Mypln.worktype.selected.FWFlg == 'F' || $scope.Mypln.worktype2.selected.FWFlg == 'F' || $scope.Mypln.worktype3.selected.FWFlg == 'F') && $scope.Mypln.subordinate.selected.id !== undefined) {
         $scope.cMTPDId = '_' + $scope.Mypln.subordinate.selected.id;
         TpTwns = fmcgLocalStorage.getData("town_master" + $scope.cMTPDId) || [];
         if (TpTwns.length < 1) {
         $ionicLoading.show({
                            template: 'Please Wait. Data is Sync...'
                            });
         $scope.clearAll(false, $scope.cMTPDId);
         } else {
         $scope.loadDatas(false, $scope.cMTPDId);
         }
         }
         
         $scope.Mypln.cluster = {};
         $scope.Mypln.cluster.selected = {};
         $scope.Mypln.cluster2 = {};
         $scope.Mypln.cluster2.selected = {};
         $scope.Mypln.cluster3 = {};
         $scope.Mypln.cluster3.selected = {};
         var date = new Date();
         var date1 = new Date($scope.SFTPDate.date);
         //    date1 = new Date("2016-12-16")
         $scope.disableMonth = date1.getMonth() + 1;
         $scope.disableYear = date1.getFullYear();
         $scope.disableDay = date1.getDate();
         if ($scope.fmcgData.TourPlanLink_id == 1)
         $scope.month = date.getMonth() ;
         else if ($scope.fmcgData.TourPlanLink_id == 2)
         $scope.month = date.getMonth() + 1;
         else
         $scope.month = date.getMonth() + 2;
         $scope.year = date.getFullYear();
         if ($scope.month > 12) {
         $scope.month = 1;
         $scope.year = date.getFullYear()+1;
         }
         
         $scope.day = date.getDate();
         $scope.noOfMonth = new Date($scope.year, $scope.month, 0).getDate();
         
         var disableDates = [];
         if ($scope.disableYear == $scope.year) {
         if ($scope.disableMonth == $scope.month) {
         for ($i = 01; $i < $scope.disableDay; $i++) {
         disableDates.push($scope.year + '-' + $scope.month + '-' + $i);
         }
         }
         if ($scope.disableMonth > $scope.month) {
         for ($i = 01; $i <= $scope.noOfMonth; $i++) {
         disableDates.push($scope.year + '-' + $scope.month + '-' + $i);
         }
         }
         }
         
         if ($scope.disableYear > $scope.year) {
         for ($i = 01; $i <= $scope.noOfMonth; $i++) {
         disableDates.push($scope.year + '-' + $scope.month + '-' + $i);
         }
         }
         $scope.noOfMonthsubmit = $scope.noOfMonth - disableDates.length;
         var tourPlan = fmcgLocalStorage.getData("Tour_Plan_" + $scope.fmcgData.TourPlanLink_id) || [];
         $scope.submitApproval = 0;
         if (tourPlan.length == $scope.noOfMonthsubmit)
         $scope.submitApproval = 1;
         $scope.show = 0;
         // $scope.clearData();
         $scope.data = {};
         $scope.modal = $ionicModal;
         
         $scope.modal.fromTemplateUrl('partials/TourPlanModal.html', function (modal) {
                                      $scope.modal = modal;
                                      }, {
                                      animation: 'slide-in-up',
                                      focusFirstInput: true
                                      });
         var temp = window.localStorage.getItem("loginInfo");
         var tourPlan = fmcgLocalStorage.getData("Tour_Plan_" + $scope.fmcgData.TourPlanLink_id) || [];
         if(tourPlan.length>0)
         $scope.Confirmed = tourPlan[0].Confirmed;
         if ($scope.Confirmed == null)
         $scope.Confirmed = 0;
         var $fl = 0;
         $scope.datefn = function () {
         $scope.options = {
         defaultDate: new Date($scope.year,$scope.month,$scope.day),
         minDate: new Date($scope.year,$scope.month,1),
         maxDate: new Date($scope.year,$scope.month,$scope.noOfMonth),
         //  disabledDates: [],
         dayNamesLength: 3, // 1 for "M", 2 for "Mo", 3 for "Mon"; 9 will show full day names. Default is 1.
         mondayIsFirstDay: true, //set monday as first day of week. Default is false
         disabledDates: disableDates,
         eventClick: function (date) {
         if (date.day < 10)
         date.day = "0" + date.day;
         date.month = date.month + 1;
         if (date.month < 10)
         date.month = "0" + date.month;
         
         $scope.modal.update = function () {
         $scope.show = 1;
         updateData();
         }
         function updateData() {
         
         $scope.modal.view_MR = $scope.view_MR;
         $scope.modal.No_of_TP_View=$scope.No_of_TP_View;
         $scope.data.date = date.year + "-" + date.month + "-" + date.day;
         $scope.modal.date = date.day + "/" + date.month + "/" + date.year;
         $scope.clickdate = date.day + "/" + date.month + "/" + date.year;
         var tourPlan = fmcgLocalStorage.getData("Tour_Plan_" + $scope.fmcgData.TourPlanLink_id) || [];
         for (var key in tourPlan) {
         if ($scope.data.date === (tourPlan[key]['date'])) {
         $scope.modal.SF_type = $scope.SF_type;
         $scope.modal.tourPlan = tourPlan[key];
         $scope.Mypln.subordinate.selected.id = $scope.sfCode;
         $scope.data.remarks = tourPlan[key]['remarks'];
         $scope.Mypln.worktype.selected.id = tourPlan[key]['worktype_code'];
         $scope.Mypln.worktype.name = tourPlan[key]['worktype_name'];
         $scope.Mypln.worktype2.selected.id = tourPlan[key]['worktype_code2'];
         $scope.Mypln.worktype2.name = tourPlan[key]['worktype_name2'];
         $scope.Mypln.worktype3.selected.id = tourPlan[key]['worktype_code3'];
         $scope.Mypln.worktype3.name = tourPlan[key]['worktype_name3'];
         
         
         if ($scope.view_MR == 0) {
         if (tourPlan[key]['worktype_name'] == "Field Work") {
         $scope.Mypln.worktype.selected.FWFlg = 'F';
         
         $scope.Mypln.subordinate.selected.id = tourPlan[key]['Worked_with_Code'];
         $scope.cMTPDId = '_' + $scope.Mypln.subordinate.selected.id;
         
         $scope.Mypln.subordinate.name = tourPlan[key]['Worked_with_Name'];
         HQId = tourPlan[key]['Worked_with_Code'].split(',');
         HQNms = tourPlan[key]['Worked_with_Name'].split(',');
         CLIds = tourPlan[key]['RouteCode'].split(',');
         CLNms = tourPlan[key]['RouteName'].split(',');
         $scope.fmcgData.clstrData1 = [];
         $scope.fmcgData.clstrData2 = [];
         for (var i = 0; i < HQId.length; i++) {
         HQId1 = HQId[i].trim();
         HQNm1 = HQNms[i].trim();
         if (HQId1 != "") {
         sdata = "town_master" + '_' + HQId1;
         $scope.cData = fmcgLocalStorage.getData(sdata) || [];
         cluster1 = [];
         for (di = 0; di < $scope.cData.length; di++) {
         clusterId = CLIds.filter(function (a) {
                                  return (a.trim() == $scope.cData[di].id);
                                  });
         
         cluster = CLNms.filter(function (b) {
                                
                                return (b.trim() == $scope.cData[di].name);
                                });
         if (clusterId.length > 0) {
         cluster1.push({ "id": clusterId[0].trim(), "name": cluster[0].trim() })
         }
         
         }
         CLIds1 = ' ';
         CLNms1 = '';
         for (j = 0; j < cluster1.length; j++) {
         CLIds1 += cluster1[j].id + ', ';
         CLNms1 += cluster1[j].name + ', ';
         }
         $scope.fmcgData.clstrData2.push({ "HQ": HQNm1, "HQId": HQId1, "Cluster": CLNms1, "ClusterId": CLIds1 })
         $scope.fmcgData.clstrData1.push({ "HQId": HQId1, "HQNm": HQNm1, "cluster": cluster1 })
         }
         }
         
         }
         else {
         $scope.Mypln.worktype.selected.FWFlg = 'A';
         $scope.fmcgData.clstrData1 = [];
         $scope.fmcgData.clstrData2 = [];
         }
         }
         else {
         if (tourPlan[key]['worktype_name'] == "Field Work" || tourPlan[key]['worktype_name2'] == "Field Work" || tourPlan[key]['worktype_name3'] == "Field Work") {
         
         if (tourPlan[key]['worktype_name'] == "Field Work") {
         $scope.Mypln.worktype.selected.FWFlg = 'F';
         $scope.Mypln.cluster.selected.id = Number(tourPlan[key]['RouteCode']);
         $scope.Mypln.cluster.name = tourPlan[key]['RouteName'];
         }
         if (tourPlan[key]['worktype_name2'] == "Field Work") {
         $scope.Mypln.worktype2.selected.FWFlg = 'F';
         $scope.Mypln.cluster2.selected.id = Number(tourPlan[key]['RouteCode2']);
         $scope.Mypln.cluster2.name = tourPlan[key]['RouteName2'];
         }
         if (tourPlan[key]['worktype_name3'] == "Field Work") {
         $scope.Mypln.worktype3.selected.FWFlg = 'F';
         $scope.Mypln.cluster3.selected.id = Number(tourPlan[key]['RouteCode3']);
         $scope.Mypln.cluster3.name = tourPlan[key]['RouteName3'];
         }
         }
         else {
         $scope.Mypln.worktype.selected.FWFlg = 'A';
         $scope.Mypln.worktype2.selected.FWFlg = 'A';
         $scope.Mypln.worktype3.selected.FWFlg = 'A';
         $scope.Mypln.cluster.selected.id = '';
         $scope.Mypln.cluster.name = '';
         $scope.Mypln.cluster2.selected.id = '';
         $scope.Mypln.cluster2.name = '';
         $scope.Mypln.cluster3.selected.id = '';
         $scope.Mypln.cluster3.name = '';
         }
         }
         }
         }
         }
         
         updateData();
         $scope.modal.show();
         
         },
         dateClick: function (date) {
         $scope.Mypln.worktype = {};
         $scope.Mypln.worktype.selected = {};
         $scope.Mypln.worktype2 = {};
         $scope.Mypln.worktype2.selected = {};
         $scope.Mypln.worktype3 = {};
         $scope.Mypln.worktype3.selected = {};
         $scope.Mypln.stockist = {};
         $scope.Mypln.stockist.selected = {};
         $scope.Mypln.cluster = {};
         $scope.Mypln.cluster.selected = {};
         $scope.Mypln.cluster2 = {};
         $scope.Mypln.cluster2.selected = {};
         $scope.Mypln.cluster3 = {};
         $scope.Mypln.cluster3.selected = {};
         $scope.Mypln.subordinate.selected.id = $scope.sfCode;
         $scope.data.remarks = '';
         //  $scope.data.market = '';
         $scope.show = 1;
         if (date.day < 10) date.day = "0" + date.day;
         date.month = date.month + 1;
         if (date.month < 10) date.month = "0" + date.month;
         $scope.data.date = date.year + "-" + date.month + "-" + date.day;
         $scope.clickdate = date.day + "/" + date.month + "/" + date.year;
         $scope.fmcgData.clstrData1 = [];
         $scope.fmcgData.clstrData2 = [];
         },
         changeMonth: function (month, year) {
         
         },
         };
         $scope.Tour_Plan = fmcgLocalStorage.getData("Tour_Plan_" + $scope.fmcgData.TourPlanLink_id) || [];
         $scope.events = $scope.Tour_Plan;
         }
         $scope.datefn();
         
         
         

         $scope.close = function () {
         $scope.show = 0;
         }
         $scope.submit=function(){
         fmcgAPIservice.addMAData('POST', 'dcr/save&month=' + $scope.month+'&year=' + $scope.year, "12", $scope.data).success(function (response) {
                      if (response.success) {
                          Toast("Submitted For Approval");
                          $state.go('fmcgmenu.home');
                      }
                })
         }
         $scope.save = function () {
         if ($scope.Mypln.worktype.selected == undefined || $scope.Mypln.worktype.selected.id == "" || $scope.Mypln.worktype.selected.id == undefined) {
         Toast('Select the Work Type...');
         return false;
         }
         var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
         $scope.data.sfName = loginInfo.sfName;
         $scope.data.SF_type = $scope.SF_type;
         $scope.data.worktype_code = $scope.Mypln.worktype.selected.id;
         $scope.data.worktype_name = $scope.Mypln.worktype.name;
         $scope.data.worktype_code2 = $scope.Mypln.worktype2.selected.id;
         $scope.data.worktype_name2 = $scope.Mypln.worktype2.name;
         $scope.data.worktype_code3= $scope.Mypln.worktype3.selected.id;
         $scope.data.worktype_name3= $scope.Mypln.worktype3.name;
         if ($scope.data.worktype_name == 'Field Work' || $scope.data.worktype_name2 == 'Field Work' || $scope.data.worktype_name3 == 'Field Work') {
         
         if ($scope.Mypln.subordinate.selected == undefined || $scope.Mypln.subordinate.selected.id == "") {
         Toast('Select the Headquarter...');
         return false;
         }
         if ($scope.view_MR==1) {
         if ($scope.data.worktype_name == 'Field Work'&&($scope.Mypln.cluster.selected == undefined || $scope.Mypln.cluster.selected.id == "" || $scope.Mypln.cluster.selected.id == undefined)) {
         Toast('Select the Cluster...');
         return false;
         }
         if ($scope.data.worktype_name2 == 'Field Work' && ($scope.Mypln.cluster2.selected == undefined || $scope.Mypln.cluster2.selected.id == "" || $scope.Mypln.cluster2.selected.id == undefined)) {
         Toast('Select the Cluster2...');
         return false;
         }
         if ($scope.data.worktype_name3 == 'Field Work' && ($scope.Mypln.cluster3.selected == undefined || $scope.Mypln.cluster3.selected.id == "" || $scope.Mypln.cluster3.selected.id == undefined)) {
         Toast('Select the Cluster3...');
         return false;
         }
         $scope.data.Worked_with_Code = $scope.Mypln.subordinate.selected.id;
         $scope.data.Worked_with_Name = $scope.Mypln.subordinate.name;
         $scope.data.RouteCode = $scope.Mypln.cluster.selected.id;
         $scope.data.RouteName = $scope.Mypln.cluster.name;
         $scope.data.RouteCode2 = $scope.Mypln.cluster2.selected.id;
         $scope.data.RouteName2 = $scope.Mypln.cluster2.name;
         $scope.data.RouteCode3 = $scope.Mypln.cluster3.selected.id;
         $scope.data.RouteName3 = $scope.Mypln.cluster3.name;
         }
         
         
         } else {
         $scope.data.Worked_with_Code = '';
         $scope.data.Worked_with_Name = '';
         $scope.data.RouteCode = '';
         $scope.data.RouteName = '';
         $scope.fmcgData.clstrData1 = [];
         $scope.fmcgData.clstrData2 = [];
         }
         
         
         $ionicLoading.show({
                            template: 'Saving...'
                            });
         if (!$scope.view_MR) {
         $scope.data.worktype_code = $scope.Mypln.worktype.selected.id;
         $scope.data.worktype_name = $scope.data.worktype_name;
         $scope.data.sfName = $scope.data.sfName;
         $scope.data.date = $scope.data.date;
         $scope.data.date = $scope.data.date;
         clusters = $scope.fmcgData.clstrData2;
         CLIds = '';
         CLNms = '';
         HQId = '';
         HQNms = '';
         for (j = 0; j < clusters.length; j++) {
         CLIds += clusters[j].ClusterId;
         CLNms += clusters[j].Cluster;
         HQId += clusters[j].HQId + ', ';
         HQNms += clusters[j].HQ + ', ';
         }
         $scope.data.RouteCode = CLIds;
         $scope.data.RouteName =CLNms;
         $scope.data.Worked_with_Code =HQId;
         $scope.data.Worked_with_Name =HQNms;
         if ($scope.data.worktype_name == 'Field Work' && CLIds=='') {
         Toast('Select the Cluster...');
         return false;
         }
         }
        //console.log($scope.fmcgData.clstrData1);
         fmcgAPIservice.addMAData('POST', 'dcr/save', "11", $scope.data).success(function (response) {
             if (response.success) {
             //  var tourPlan = {};
             tourPlan = fmcgLocalStorage.getData("Tour_Plan_" + $scope.fmcgData.TourPlanLink_id, tourPlan) || [];
             for (var key in tourPlan) {
                 if ($scope.data.date == (tourPlan[key]['date'])) {
                     tourPlan.splice(key, 1);
                 }
             }
             $scope.data.Confirmed = 0;
             tourPlan.push($scope.data);
             $scope.show = 0;
             fmcgLocalStorage.createData("Tour_Plan_" + $scope.fmcgData.TourPlanLink_id, tourPlan);
             if (tourPlan.length == $scope.noOfMonthsubmit)
             $scope.submitApproval = 1;
             $scope.fmcgData.clstrData1 = [];
             $scope.fmcgData.clstrData2 = [];
             $scope.data = {};
             $scope.datefn();
             Toast("Plan Submited Successfully");
             $ionicLoading.hide();
             $state.go('fmcgmenu.TourPlan');
             }
             }).error(function () {
                      //window.localStorage.setItem("Tour_Plan", $scope.Tour_Plan);
             Toast("No Internet Connection! Try Again.");
             $ionicLoading.hide();
             //$state.go('fmcgmenu.TourPlan');
        });
    }
}])
.controller('draftCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.navTitle = "Draft Calls";
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.clearData();
    $scope.success = false;
    $scope.nodata = false;
    $scope.owsuccess = false;
    $scope.owTypeData = [];
    $scope.deleteEntry = function () {
        $ionicPopup.confirm({
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
        })
        .then(function (res) {
            if (res) {
                localStorage.removeItem("draft");
                $state.go('fmcgmenu.home');
                Toast("draft deleted successfully");
            } else {
                console.log('You are not sure');
            }
        });
    };
    $scope.editEntry = function () {
        var value = fmcgLocalStorage.getData("draft");
        for (key in value[0])
        {
            if (key)
            {
                $scope.$parent.fmcgData[key] = value[0][key];
            }
        }
        localStorage.removeItem("draft");
        $state.go('fmcgmenu.addNew');
    }
    $scope.saveEntry = function () {
        var temp = fmcgLocalStorage.getData("draft");
        fmcgAPIservice.saveDCRData('POST', 'dcr/save', temp[0], false).success(function (response) {
            if (!response['success'] && response['type'] == 1)
            {
                $scope.showConfirm = function () {
                    var confirmPopup = $ionicPopup.confirm({
                        title: 'Warning !',
                        template: response['msg']
                    });
                    confirmPopup.then(function (res) {
                        if (res) {
                            fmcgAPIservice.updateDCRData('POST', 'dcr/save&replace', response['data'], false).success(function (response) {
                                if (!response['success'])
                                {
                                    Toast(response['msg'], true);
                                }
                                else
                                {
                                    Toast("call Updated Successfully");
                                }
                            });
                            localStorage.removeItem("draft");
                        } else {
                            localStorage.removeItem("draft");
                        }
                    });
                };
                $scope.showConfirm();
            }
            else if (!response['success'])
            {
                Toast(response['msg'], true);
                localStorage.removeItem("draft");
            }
            else
            {
                Toast("call Submited Successfully");
                localStorage.removeItem("draft");
            }
        });

    }

    //$scope.$parent.navTitle = "Submitted Calls";
    $scope.customers = [{
            'id': '1',
            'name': $scope.DrCap,
            'url': 'manageDoctorResult'
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap,
            'url': 'manageChemistResult'
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap,
            'url': 'manageStockistResult'
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap,
            'url': 'manageStockistResult'
        });
        $scope.nCI = al; al++;
    }
    $scope.getData = function () {
        response = fmcgLocalStorage.getEntryCount();
        if (response['success'])
        {
            $scope.success = true;
            $scope.customers[0].count = response['data']['doctor_count'];
            if ($scope.ChmNeed != 1) $scope.customers[$scope.cCI].count = response['data']['chemist_count'];
            if ($scope.StkNeed != 1) $scope.customers[$scope.sCI].count = response['data']['stockist_count'];
            if ($scope.UNLNeed != 1) $scope.customers[$scope.nCI].count = response['data']['uldoctor_count'];
        }
        else if (response['ndata'])
        {
            $scope.nodata = true;
        }
        else
        {
            $scope.owsuccess = true;
            $scope.owTypeData = response['data'];
        }
        $ionicLoading.hide();
    };
    $scope.getData();
})
.controller('outboxCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.navTitle = "Outbox Calls";
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.clearData();
    $scope.success = false;
    $scope.nodata = false;
    $scope.owsuccess = false;
    $scope.owTypeData = [];

    $scope.oxRmks = fmcgLocalStorage.getData("MyDyRmksQ") || [];
    $scope.oxMyPln = fmcgLocalStorage.getData("myplnQ") || [];
    $scope.delOutbxMyPln = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
                    if (res) {
                        $scope.oxMyPln.splice($scope.oxMyPln.indexOf(item), 1);
                        fmcgLocalStorage.createData('myplnQ', $scope.oxMyPln);
                        Toast("My day plan successfully deleted from Outbox");
                    } else {
                        console.log('You are not sure');
                    }
                }
        );
    };
    $scope.oxRCPA = fmcgLocalStorage.getData("OutBx_RCPA") || [];
    $scope.deleteOutbox = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
                    if (res) {
                        $scope.oxRCPA.splice($scope.oxRCPA.indexOf(item), 1);
                        fmcgLocalStorage.createData('OutBx_RCPA', $scope.oxRCPA);
                        Toast("RCPA successfully deleted from Outbox");
                    } else {
                        console.log('You are not sure');
                    }
                }
        );
    };
    $scope.oxRMCL = fmcgLocalStorage.getData("OutBx_RMCL") || [];
    $scope.delOutbxRMCL = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
                    if (res) {
                        $scope.oxRMCL.splice($scope.oxRMCL.indexOf(item), 1);
                        fmcgLocalStorage.createData('OutBx_RMCL', $scope.oxRMCL);
                        Toast("Reminder call successfully deleted from Outbox");
                    } else {
                        console.log('You are not sure');
                    }
                }
        );
    };


    $scope.deleteEntry = function (DelType) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {

                    if (res) {
                        localStorage.removeItem("saveLater");
                        $state.go('fmcgmenu.home');
                        Toast("draft deleted successfully");
                    } else {
                        console.log('You are not sure');
                    }
                }
        );
    };
    $scope.customers = [{
        'id': '1',
        'name': $scope.DrCap,
        'url': 'manageDoctorResult'
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap,
            'url': 'manageChemistResult'
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap,
            'url': 'manageStockistResult'
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap,
            'url': 'manageStockistResult'
        });
        $scope.nCI = al; al++;
    }
    $scope.getData = function () {
        response = fmcgLocalStorage.getOutboxCount();
        if (response['success']) {
            $scope.success = true;
            $scope.customers[0].count = response['data']['doctor_count'];
            if ($scope.ChmNeed != 1) $scope.customers[$scope.cCI].count = response['data']['chemist_count'];
            if ($scope.StkNeed != 1) $scope.customers[$scope.sCI].count = response['data']['stockist_count'];
            if ($scope.UNLNeed != 1) $scope.customers[$scope.nCI].count = response['data']['uldoctor_count'];
        }
        else if (response['ndata']) {
            $scope.nodata = true;
        }
        else {
            $scope.owsuccess = true;
            $scope.owTypeData = response['data'];
        }
        $ionicLoading.hide();
    };
    $scope.getData();
    if ($scope.oxRCPA.length > 0 || $scope.oxRMCL.length > 0 || $scope.oxMyPln.length > 0 || $scope.oxRmks.length > 0) {
        $scope.nodata = false;
    }
    $scope.nDtaRCPA = ($scope.oxRCPA.length > 0) ? false : true;
    $scope.nDtaRMCL = ($scope.oxRMCL.length > 0) ? false : true;
    $scope.nDtaMyPL = ($scope.oxMyPln.length > 0) ? false : true;
    $scope.nDtaRmks = ($scope.oxRmks.length > 0) ? false : true;
    
})
.controller('reloadCtrl', function ($rootScope, $scope, $state, $ionicPopup, $ionicLoading, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.navTitle = "Master Sync";
    $scope.countInc = 0;

    var temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    $scope.RefreshData = function () {
        var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id == "") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
        $scope.clearAll(false, '_' + sRSF);
    }
    $scope.clearItem = function (value) {
        var sRSF = (userData.desigCode.toLowerCase() == 'mr' || $scope.Reload.subordinate == undefined || $scope.Reload.subordinate.selected.id=="") ? userData.sfCode : $scope.Reload.subordinate.selected.id;
        $scope.clearIdividual(value, 1, '_' + sRSF);      
    }; 
    
})
.controller('draftViewCtrl', function ($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/manageDataViewModal.html', function (modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.data = {
        showDelete: false
    };
    $scope.customerDatas;
    $scope.myChoice = $stateParams.myChoice;
    switch ($scope.myChoice)
    {
        case "1":
            $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
            break;
        case "3":
            $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
            break;
        case "2":
            $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
            break;
        case "4":
            $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
            break;
    }    
    $scope.draftButtons = [
        {
            text: 'Edit',
            type: 'button-calm',
            onTap: function (item) {
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                var value = item;

                for (key in value)
                {
                    if (key)
                    {
                        $scope.$parent.fmcgData[key] = item[key];
                    }
                }
                fmcgLocalStorage.createData('draft', $scope.drafts);
                $state.go('fmcgmenu.addNew');
            }
        }
    ];
    
    $scope.edit = function (item) {
        $scope.loadDatas(false, '_' + item.subordinate.selected.id);
        $scope.fmcgData.isDraft = true;
        $scope.drafts.splice($scope.drafts.indexOf(item), 1);
        var value = item;

        for (key in value)
        {
            if (key)
            {
                $scope.$parent.fmcgData[key] = item[key];
            }
        }
        fmcgLocalStorage.createData('draft', $scope.drafts);
        $state.go('fmcgmenu.screen2');
    };
    $scope.save = function (item) {

        fmcgAPIservice.saveDCRData('POST', 'dcr/save', item, false).success(function (response) {
            if (!response['success'])
            {
                Toast(response['msg'], true);
            }
            else{
                Toast("call Submited Successfully")
            }
            $scope.drafts.splice($scope.drafts.indexOf(item), 1);
            fmcgLocalStorage.createData('draft', $scope.drafts);
        });

    };
    $scope.preView = function (item) {
        scrTyp = item.customer.selected.id;
        $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
        $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        $scope.modal.par = $scope;        
        $scope.modal.draft = item;
        $scope.modal.show();
    };
    $scope.deleteDrftList = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
            if (res) {
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                fmcgLocalStorage.createData('draft', $scope.drafts);                
                Toast("Call deleted successfully from draft");
            } else {
                console.log('You are not sure');
            }
        }
        );
    };
    $scope.drafts = [];
    $scope.drafts = fmcgLocalStorage.getData("draft");
})
.controller('outboxViewCtrl', function ($rootScope, $scope, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.outboxs = fmcgLocalStorage.getData("saveLater");
    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/manageDataViewModal.html', function (modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.data = {
        showDelete: false
    };
    $scope.customerDatas;
    $scope.myChoice = $stateParams.myChoice;
    switch ($scope.myChoice) {
        case "1":
            $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
            break;
        case "3":
            $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
            break;
        case "2":
            $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
            break;
        case "4":
            $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
            break;
    }
    $scope.preView = function (item) {
        scrTyp = item.customer.selected.id;
        $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
        $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        $scope.modal.par = $scope;
        $scope.modal.draft = item;
        $scope.modal.show();
    };
    $scope.deleteOutbxList = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
                    if (res) {
                        $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                        fmcgLocalStorage.createData('saveLater', $scope.drafts);
                        Toast("Call deleted successfully from Outbox");
                    } else {
                        console.log('You are not sure');
                    }
                }
        );
    };
    $scope.drafts = [];
    $scope.drafts = fmcgLocalStorage.getData("saveLater");
})
.controller('manageDataViewCtrl', function ($rootScope, $scope, $ionicLoading, $stateParams, $state, $ionicModal, $ionicPopup, fmcgAPIservice, fmcgLocalStorage, notification, $filter) {
    $ionicLoading.show({
        template: 'Loading...'
    });
    
    $scope.drafts = [];
    var tm = [];

    $scope.myChoice = $stateParams.myChoice;
    var ch=$scope.myChoice;
    $scope.$parent.navTitle = "Submitted "+ ((ch==1)?$scope.DrCap:(ch==2)?$scope.ChmCap:(ch==2)?$scope.StkCap:(ch==2)?$scope.NLCap:'calls');
    $scope.clearData();
    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/dataViewModal.html', function (modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });
    $scope.data = {
        showDelete: false
    };
    $scope.customerDatas;
    fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_report", '["*"]', , , 1, , 1, , "[\"activity_date asc\"]"]).success(function (response) {
        var tm = [];
        var pklist = [];
        for (var i = 0, len = response.length; i < len; i++)
        {
            var tempData = {};
            tempData['worktype'] = {};
            tempData['worktype']['selected'] = {};
            tempData['cluster'] = {};
            tempData['cluster']['selected'] = {};
            tempData['remarks'] = "";
            tempData['arc'] = "";
            tempData.worktype.selected.id = response[i]['Work_Type'];
            tempData.worktype.selected.FWFlg = response[i]['FWFlg'];
            tempData.arc = response[i]["Trans_SlNo"];

            pklist.push("Trans_SlNo=\'" + tempData.arc + "\'");
            $scope.loadEdDet(pklist, tempData);
        }
    }).error(function () {
        $ionicLoading.hide();
        Toast('No Internet Connection.');
    });
    $scope.loadEdDet=function(pklist,tempData)
    {
        switch ($scope.myChoice) {
            case "1":
                $scope.customerDatas = fmcgLocalStorage.getData("doctor_master") || [];
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwactivity_msl_details", '["*,cast(time as datetime) tm"]', , JSON.stringify(pklist), , 1, , , '["cast(time as datetime)"]']).success(function (response1) {
                    for (var j = 0, le = response1.length; j < le; j++) {
                        tData = {};
                        tData = angular.copy(tempData);
                        tData['amc'] = response1[j]['Trans_Detail_Slno'];
                        tData['pob'] = parseInt(response1[j]['POB']);
                        tData['customer'] = {};
                        tData['customer']['selected'] = {}
                        tData['customer']['selected']['id'] = "1";
                        tData['doctor'] = {};
                        tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                        tData['doctor']['selected'] = {}
                        tData['doctor']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                        tData['doctor']['name'] = response1[j]['Trans_Detail_Name'];

                        tData.cluster.selected.id = response1[j]['SDP'];
                        tData.cluster.name = response1[j]['SDP_Name'];
                        tData.remarks = response1[j]["Activity_Remarks"];
                        tData.rx = response1[j]['Rx'];
                        tData['rx_t'] = response1[j]['Rx'];

                        /****
                        if (tData.rx)
                            tData.rx_t = response1[j]['rx_t'];
                        else
                            tData.nrx_t = response1[j]['nrx_t'];
                        ****/

                        tData['subordinate'] = {};
                        tData['subordinate']['selected'] = {};
                        tData.subordinate.selected.id = response1[j]['DataSF'];

                        /**** 
    
                        tData['OrdStk'] = {};
                        tData['OrdStk']['selected'] = {}
                        tData['OrdStk']['selected']['id'] = response1[j]['Order_Stk'];
                        tData['OrderNo'] = parseInt(response1[j]['Order_No']);
    
                        ****/

                        var response2 = response1[j]['Worked_with_Code'].split("$$");
                        var jw = response1[j]['Worked_with_Name'].split(",");
                        for (var m = 0, leg = response2.length; m < leg; m++) {
                            tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                            var pTemp = {};
                            pTemp.jointwork = response2[m].toString();
                            pTemp.jointworkname = jw[m].toString();
                            if (pTemp.jointwork.length !== 0) tData['jontWorkSelectedList'].push(pTemp);
                        }

                        var prods = (response1[j]['Product_Code'] + ((response1[j]['Product_Code'] == '') ? '' : '#') + response1[j]['Additional_Prod_Code']).split("#");
                        var prodNms = (response1[j]['Product_Detail'] + ((response1[j]['Product_Detail'] == '') ? '' : '#') + response1[j]['Additional_Prod_Dtls']).split("#");
                        for (var m = 0, leg = prods.length; m < leg; m++) {
                            if (prods[m].length > 0) {
                                tData['productSelectedList'] = tData['productSelectedList'] || [];
                                var pTemp = {};
                                var prod = prods[m].split('~');
                                var prodNm = prodNms[m].split('~');
                                pTemp.product = prod[0].toString();
                                pTemp.product_Nm = prodNm[0].toString();
                                var prodQ = prod[1].split('$');
                                pTemp.sample_qty = prodQ[0];
                                pTemp.rx_qty = prodQ[1];

                                if (pTemp.product.length !== 0) tData['productSelectedList'].push(pTemp);
                            }
                        }


                        var gfts = (response1[j]['Gift_Code'] + ((response1[j]['Gift_Code'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Code'].replace(/$/g, '')).split("#");
                        var gftNms = (response1[j]['Gift_Name'] + ((response1[j]['Gift_Name'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Dtl'].replace(/$/g, '')).split("#");
                        for (var m = 0, leg = gfts.length; m < leg; m++) {
                            if (gfts[m].length > 0) {
                                tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                var giftTemp = {};
                                var gft = gfts[m].split('~');
                                var gftNm = gftNms[m].split('~');
                                giftTemp.gift = gft[0];
                                giftTemp.gift_Nm = gftNm[0];
                                giftTemp.sample_qty = gft[1];
                                if (giftTemp.gift.length !== 0) tData['giftSelectedList'].push(giftTemp);
                            }
                        }

                        tData['entryDate'] = new Date(response1[j]['tm']['date']);
                        tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                        tm.push(tData);
                    }
                    $scope.drafts = tm;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });

                break;
            case "3":
                $scope.customerDatas = fmcgLocalStorage.getData("stockist_master") || [];
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_CSH_Detail", '["*"]', , JSON.stringify(pklist), , 3, , , '["stk_meet_time"]']).success(function (response1) {
                    for (var j = 0, le = response1.length; j < le; j++) {
                        tData = {};
                        tData = angular.copy(tempData);

                        tData['amc'] = response1[j]['Trans_Detail_Slno'];
                        tData['pob'] = parseInt(response1[j]['POB']);
                        tData['customer'] = {};
                        tData['customer']['selected'] = {}
                        tData['customer']['selected']['id'] = "3";
                        tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                        tData['stockist'] = {};
                        tData['stockist']['selected'] = {}
                        tData['stockist']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                        tData['stockist']['name'] = response1[j]['Trans_Detail_Name'];

                        tData.cluster.selected.id = response1[j]['SDP'];
                        tData.cluster.name = response1[j]['SDP_Name'];
                        tData.remarks = response1[j]["Activity_Remarks"];
                        tData.rx = response1[j]['Rx'];
                        tData['rx_t'] = response1[j]['Rx'];

                        /****
                        if (tData.rx)
                            tData.rx_t = response1[j]['rx_t'];
                        else
                            tData.nrx_t = response1[j]['nrx_t'];
                        ****/

                        tData['subordinate'] = {};
                        tData['subordinate']['selected'] = {};
                        tData.subordinate.selected.id = response1[j]['DataSF'];

                        var response2 = response1[j]['Worked_with_Code'].split("$$");
                        var jw = response1[j]['Worked_with_Name'].split(",");
                        for (var m = 0, leg = response2.length; m < leg; m++) {
                            tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                            var pTemp = {};
                            pTemp.jointwork = response2[m].toString();
                            pTemp.jointworkname = jw[m].toString();
                            if (pTemp.jointwork.length !== 0) tData['jontWorkSelectedList'].push(pTemp);
                        }


                        var prods = response1[j]['Additional_Prod_Code'].split("#");
                        var prodNms = response1[j]['Additional_Prod_Dtls'].split("#");
                        for (var m = 0, leg = prods.length; m < leg; m++) {
                            if (prods[m].length > 0) {
                                tData['productSelectedList'] = tData['productSelectedList'] || [];
                                var pTemp = {};
                                var prod = prods[m].split('~');
                                var prodNm = prodNms[m].split('~');
                                pTemp.product = prod[0].toString();
                                pTemp.product_Nm = prodNm[0].toString();
                                pTemp.rx_qty = prod[1];
                                if (pTemp.product.length !== 0) tData['productSelectedList'].push(pTemp);
                            }
                        }


                        var gfts = response1[j]['Additional_Gift_Code'].split("#");
                        var gftNms = response1[j]['Additional_Gift_Dtl'].split("#");
                        for (var m = 0, leg = gfts.length; m < leg; m++) {
                            if (gfts[m].length > 0) {
                                tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                var giftTemp = {};
                                var gft = gfts[m].split('~');
                                var gftNm = gftNms[m].split('~');
                                giftTemp.gift = gft[0];
                                giftTemp.gift_Nm = gftNm[0];
                                giftTemp.sample_qty = gft[1];
                                if (giftTemp.gift.length !== 0) tData['giftSelectedList'].push(giftTemp);
                            }
                        }

                        tData['entryDate'] = new Date(response1[j]['vstTime']['date']);
                        tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                        tm.push(tData);
                    }
                    $scope.drafts = tm;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
                break;
            case "2":
                $scope.customerDatas = fmcgLocalStorage.getData("chemist_master") || [];
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_CSH_Detail", '["*"]', , JSON.stringify(pklist), , 2, , , '["chm_meet_time"]']).success(function (response1) {
                    for (var j = 0, le = response1.length; j < le; j++) {
                        tData = {};
                        tData = angular.copy(tempData);

                        tData['amc'] = response1[j]['Trans_Detail_Slno'];
                        tData['pob'] = parseInt(response1[j]['POB']);
                        tData['customer'] = {};
                        tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                        tData['customer']['selected'] = {}
                        tData['customer']['selected']['id'] = "2";
                        tData['chemist'] = {};
                        tData['chemist']['selected'] = {}
                        tData['chemist']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                        tData['chemist']['name'] = response1[j]['Trans_Detail_Name'];

                        tData.cluster.selected.id = response1[j]['SDP'];
                        tData.cluster.name = response1[j]['SDP_Name'];
                        tData.remarks = response1[j]["Activity_Remarks"];
                        tData.rx = response1[j]['Rx'];
                        tData['rx_t'] = response1[j]['Rx'];

                        /****
                        if (tData.rx)
                            tData.rx_t = response1[j]['rx_t'];
                        else
                            tData.nrx_t = response1[j]['nrx_t'];
                        ****/

                        tData['subordinate'] = {};
                        tData['subordinate']['selected'] = {};
                        tData.subordinate.selected.id = response1[j]['DataSF'];


                        var response2 = response1[j]['Worked_with_Code'].split("$$");
                        var jw = response1[j]['Worked_with_Name'].split(",");
                        for (var m = 0, leg = response2.length; m < leg; m++) {
                            tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                            var pTemp = {};
                            pTemp.jointwork = response2[m].toString();
                            pTemp.jointworkname = jw[m].toString();
                            if (pTemp.jointwork.length !== 0) tData['jontWorkSelectedList'].push(pTemp);
                        }


                        var prods = response1[j]['Additional_Prod_Code'].split("#");
                        var prodNms = response1[j]['Additional_Prod_Dtls'].split("#");
                        for (var m = 0, leg = prods.length; m < leg; m++) {
                            if (prods[m].length > 0) {
                                tData['productSelectedList'] = tData['productSelectedList'] || [];
                                var pTemp = {};
                                var prod = prods[m].split('~');
                                var prodNm = prodNms[m].split('~');
                                pTemp.product = prod[0].toString();
                                pTemp.product_Nm = prodNm[0].toString();
                                pTemp.rx_qty = prod[1];
                                if (pTemp.product.length !== 0) tData['productSelectedList'].push(pTemp);
                            }
                        }


                        var gfts = response1[j]['Additional_Gift_Code'].split("#");
                        var gftNms = response1[j]['Additional_Gift_Dtl'].split("#");
                        for (var m = 0, leg = gfts.length; m < leg; m++) {
                            if (gfts[m].length > 0) {
                                tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                var giftTemp = {};
                                var gft = gfts[m].split('~');
                                var gftNm = gftNms[m].split('~');
                                giftTemp.gift = gft[0];
                                giftTemp.gift_Nm = gftNm[0];
                                giftTemp.sample_qty = gft[1];
                                if (giftTemp.gift.length !== 0) tData['giftSelectedList'].push(giftTemp);
                            }
                        }

                        tData['entryDate'] = new Date(response1[j]['vstTime']['date']);
                        tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                        tm.push(tData);
                    }
                    $scope.drafts = tm;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
                break;
            case "4":
                $scope.customerDatas = fmcgLocalStorage.getData("unlisted_doctor_master") || [];
                fmcgAPIservice.getDataList('POST', 'table/list', ["vwActivity_Unlst_Detail", '["*,cast(time as datetime) tm"]', , JSON.stringify(pklist), , 1, , , '["cast(time as datetime)"]']).success(function (response1) {
                    for (var j = 0, le = response1.length; j < le; j++) {

                        tData = {};
                        tData = angular.copy(tempData);

                        tData['amc'] = response1[j]['Trans_Detail_Slno'];
                        tData['pob'] = parseInt(response1[j]['POB']);
                        tData['customer'] = {};
                        tData['location'] = response1[j]['lati'] + ':' + response1[j]['long'];
                        tData['customer']['selected'] = {}
                        tData['customer']['selected']['id'] = "4";
                        tData['uldoctor'] = {};
                        tData['uldoctor']['selected'] = {}
                        tData['uldoctor']['selected']['id'] = response1[j]['Trans_Detail_Info_Code'];
                        tData['uldoctor']['name'] = response1[j]['Trans_Detail_Name'];

                        tData.cluster.selected.id = response1[j]['SDP'];
                        tData.cluster.name = response1[j]['SDP_Name'];
                        tData.remarks = response1[j]["Activity_Remarks"];
                        tData.rx = response1[j]['Rx'];
                        tData['rx_t'] = response1[j]['Rx'];

                        /****
                        if (tData.rx)
                            tData.rx_t = response1[j]['rx_t'];
                        else
                            tData.nrx_t = response1[j]['nrx_t'];
                        ****/

                        tData['subordinate'] = {};
                        tData['subordinate']['selected'] = {};
                        tData.subordinate.selected.id = response1[j]['DataSF'];

                        var response2 = response1[j]['Worked_with_Code'].split("$$");
                        var jw = response1[j]['Worked_with_Name'].split(",");
                        for (var m = 0, leg = response2.length; m < leg; m++) {
                            tData['jontWorkSelectedList'] = tData['jontWorkSelectedList'] || [];
                            var pTemp = {};
                            pTemp.jointwork = response2[m].toString();
                            pTemp.jointworkname = jw[m].toString();
                            if (pTemp.jointwork.length !== 0) tData['jontWorkSelectedList'].push(pTemp);
                        }

                        var prods = (response1[j]['Product_Code'] + ((response1[j]['Product_Code'] == '') ? '' : '#') + response1[j]['Additional_Prod_Code']).split("#");
                        var prodNms = (response1[j]['Product_Detail'] + ((response1[j]['Product_Detail'] == '') ? '' : '#') + response1[j]['Additional_Prod_Dtls']).split("#");
                        for (var m = 0, leg = prods.length; m < leg; m++) {
                            if (prods[m].length > 0) {
                                tData['productSelectedList'] = tData['productSelectedList'] || [];
                                var pTemp = {};
                                var prod = prods[m].split('~');
                                var prodNm = prodNms[m].split('~');
                                pTemp.product = prod[0].toString();
                                pTemp.product_Nm = prodNm[0].toString();
                                var prodQ = prod[1].split('$');
                                pTemp.sample_qty = prodQ[0];
                                pTemp.rx_qty = prodQ[1];

                                if (pTemp.product.length !== 0) tData['productSelectedList'].push(pTemp);
                            }
                        }


                        var gfts = (response1[j]['Gift_Code'] + ((response1[j]['Gift_Code'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Code'].replace(/$/g, '')).split("#");
                        var gftNms = (response1[j]['Gift_Name'] + ((response1[j]['Gift_Name'] == '') ? '' : '~' + response1[j]['Gift_Qty'] + '#') + response1[j]['Additional_Gift_Dtl'].replace(/$/g, '')).split("#");
                        for (var m = 0, leg = gfts.length; m < leg; m++) {
                            if (gfts[m].length > 0) {
                                tData['giftSelectedList'] = tData['giftSelectedList'] || [];
                                var giftTemp = {};
                                var gft = gfts[m].split('~');
                                var gftNm = gftNms[m].split('~');
                                giftTemp.gift = gft[0];
                                giftTemp.gift_Nm = gftNm[0];
                                giftTemp.sample_qty = gft[1];
                                if (giftTemp.gift.length !== 0) tData['giftSelectedList'].push(giftTemp);
                            }
                        }

                        tData['entryDate'] = new Date(response1[j]['tm']['date']);
                        tData['modifiedDate'] = new Date(response1[j]['ModTime']['date']);

                        tm.push(tData);
                    }
                    $scope.drafts = tm;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
                break;
        }
    }

    $scope.edit = function (item) {
        var TwnDet = fmcgLocalStorage.getData("town_master_" + item.subordinate.selected.id) || [];
        if (TwnDet.length > 0) {
            $scope.loadDatas(false, '_' + item.subordinate.selected.id);
        } 
        else if (TwnDet.length <= 0) {
            $scope.clearAll(false, '_' + item.subordinate.selected.id);
        }
        
        $scope.drafts.splice($scope.drafts.indexOf(item), 1);
        var value = item;
        for (key in value)
        {
            if (key)
            {
                $scope.$parent.fmcgData[key] = item[key];
            }
        }
        //fmcgLocalStorage.createData('draft', $scope.drafts);
        $state.go('fmcgmenu.screen2');
    };
    $scope.preView = function (item) {
        scrTyp = item.customer.selected.id
        $scope.QCap = (scrTyp == 2) ? $scope.CQCap : $scope.SQCap;
        $scope.RxCap = (scrTyp == 1) ? $scope.DRxCap : $scope.NRxCap;
        $scope.SmplCap = (scrTyp == 1) ? $scope.DSmpCap : $scope.NSmpCap;
        $scope.modal.par = $scope;
        $scope.modal.draft = item;
        $scope.modal.show();
    };
    $scope.deleteDraft = function (item) {
        $ionicPopup.confirm(
                {
                    title: 'Call Delete',
                    content: 'Are you sure you want to Delete?'
                }).then(function (res) {
            if (res) {
                $scope.drafts.splice($scope.drafts.indexOf(item), 1);
                fmcgAPIservice.deleteEntry('POST', 'deleteEntry', item).success(function (response) {
                Toast("call deleted successfully");                
                });
            } else {
                console.log('You are not sure');
            }
        });
    };
}).controller('dcrData', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.navTitle = "Monthly Summary";
    $scope.clearData();
    $scope.dcrDataCur = {};
    fmcgAPIservice.getDataList('POST', 'dcr/callReport', []).success(function (response) {
        var data = response;
        var temp = [];
        for (var i = 0; i < data.length; i++)
        {
            var flag = true;
            for (var j = 0; j < temp.length; j++)
            {
                if (data[i]['day_dcr'] == temp[j]['day_dcr'])
                {
                    temp[j]['town_code'] = temp[j]['town_code'] + ',\n' + data[i]['town_code'];
                    temp[j]['chm_count'] = parseInt(temp[j]['chm_count']) + parseInt(data[i]['chm_count']);
                    temp[j]['stk_count'] = parseInt(temp[j]['stk_count']) + parseInt(data[i]['stk_count']);
                    temp[j]['doc_count'] = parseInt(temp[j]['doc_count']) + parseInt(data[i]['doc_count']);
                    temp[j]['uldoc_count'] = parseInt(temp[j]['uldoc_count']) + parseInt(data[i]['uldoc_count']);

                    flag = false;
                }

            }

            if (flag)
            {
                temp.push(data[i]);
            }
        }
        $scope.dcrDataCur = temp;
    }).error(function () { Toast('No Internet Connection.');});

})
.controller('TourPlanLinkCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
    $scope.$parent.navTitle = "Tour Plan";
    $scope.goToLink = function (cus) {
        $scope.fmcgData.TourPlanLink_id = cus;
        $state.go('fmcgmenu.TourPlan');

    }
})

      .controller('ApprovalsCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
          $scope.$parent.navTitle = "Approvals";
          $scope.goToApproval = function (cus) {
              if(cus==1)
                  $state.go('fmcgmenu.ViewLeave');
              if (cus == 2)
                  $state.go('fmcgmenu.viewTPApproval');
              if (cus == 3)
                  $state.go('fmcgmenu.ViewDCR');
          }
      })
       .controller('viewTPApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
           $scope.$parent.navTitle = "View TourPlan";
           $ionicLoading.show({
               template: 'Loading...'
           });
           fmcgAPIservice.getDataList('POST', 'vwChkTransApproval', [])
                .success(function (response) {
                    $scope.TP = response;
                    $ionicLoading.hide();
                }).error(function () {
                    $ionicLoading.hide();
                    Toast('No Internet Connection.');
                });
           $scope.approval = function (code, month, year) {
               $scope.$parent.TPCode = code;
               $scope.$parent.TPMonth = month;
               $scope.$parent.TPYear = year;
               $state.go('fmcgmenu.TPApproval');
           }
           $scope.goBack = function () {
               $state.go('fmcgmenu.Approvals');
           }
       })
      .controller('TPApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
          $scope.data = {};
          $ionicLoading.show({
              template: 'Loading...'
          });

          $scope.rejection = 0;
          $scope.$parent.navTitle = "TP Approval";
          fmcgAPIservice.getDataList('POST', 'vwChkTransApprovalOne&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, [])
              .success(function (response) {
                  $scope.TPSf = response;
                  $ionicLoading.hide();
              }).error(function () {
                  $ionicLoading.hide();
                  Toast('No Internet Connection.');
              });
          $scope.approve = function () {
              fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, "13", $scope.data).success(function (response) {
                  if (response.success) {
                      Toast("TP Approval Submitted Successfully");
                      $state.go('fmcgmenu.viewTPApproval');
                  }
              });
          }
          $scope.reject = function () {
              $scope.rejection = 1;
          }
          $scope.goBack = function () {
              $state.go('fmcgmenu.viewTPApproval');
          }
          $scope.rejSend = function () {
              if ($scope.data.reason == undefined || $scope.data.reason == '') {
                  Toast('Enter the Reason...');
                  return false;
              }
              fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.TPCode + "&month=" + $scope.$parent.TPMonth + "&year=" + $scope.$parent.TPYear, "14", $scope.data).success(function (response) {
                  if (response.success) {
                      Toast("TP Reject Successfully");
                      $state.go('fmcgmenu.viewTPApproval');
                  }
              });
          }

      })
     .controller('ViewLeave', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.$parent.navTitle = "View Leave";
         $ionicLoading.show({
             template: 'Loading...'
         });
         fmcgAPIservice.getDataList('POST', 'vwLeave', [])
              .success(function (response) {
                $scope.leaves=response;
                  $ionicLoading.hide();
              }).error(function () {
                  $ionicLoading.hide();
                  Toast('No Internet Connection.');
              });
         $scope.approval = function (id) {
            leaves= $scope.leaves;
            for (key in leaves) {
                if (leaves[key]['Leave_Id'] == id)
                    $scope.$parent.LA = leaves[key];
            }
            
             $state.go('fmcgmenu.LeaveApproval');
         }
         $scope.goBack = function () {
             $state.go('fmcgmenu.Approvals');
         }
     })
    .controller('ViewDCR', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
        $scope.$parent.navTitle = "View DCR";
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'vwDcr', [])
             .success(function (response) {
                 $scope.DCR = response;
                 $ionicLoading.hide();
             }).error(function () {
                 $ionicLoading.hide();
                 Toast('No Internet Connection.');
             });
        $scope.approval = function (sfCode, ActivityDate, FieldWork_Indicator, worktype, sfName, Trans_SlNo,PlanName) {
            $scope.$parent.code = sfCode;
            $scope.$parent.activity_Date = ActivityDate;
            $scope.$parent.FieldWork_Indicator = FieldWork_Indicator;
            $scope.$parent.worktype = worktype;
            $scope.$parent.sfName = sfName;
            $scope.$parent.Trans_SlNo = Trans_SlNo;
            $scope.$parent.PlanName = PlanName;
            $state.go('fmcgmenu.DCRApproval');
        }
        $scope.goBack = function () {
            $state.go('fmcgmenu.Approvals');
        }
    })
     .controller('DCRApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.data = {};
         $scope.rejection = 0;
         $scope.$parent.navTitle = "DCR Approval";
       
         if ($scope.$parent.FieldWork_Indicator == 'F') {
             $ionicLoading.show({
                 template: 'Loading...'
             });
             fmcgAPIservice.getDataList('POST', 'vwDcrOne&Trans_SlNo=' + $scope.$parent.Trans_SlNo, [])
                  .success(function (response) {
                      $scope.DCRSf = response;
                      $ionicLoading.hide();
                  }).error(function () {
                      $ionicLoading.hide();
                      Toast('No Internet Connection.');
                  });
         }
         $scope.approve = function () {
             fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.code + "&date=" + $scope.$parent.activity_Date, "15", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("DCR Approval Submitted Successfully");
                     $state.go('fmcgmenu.ViewDCR');
                 }
             });
         }
         $scope.goBack = function () {
             $state.go('fmcgmenu.ViewDCR');
         }
         $scope.reject = function () {
             $scope.rejection = 1;
         }
         $scope.rejSend = function () {
             if ($scope.data.reason == undefined || $scope.data.reason == '') {
                 Toast('Enter the Reason...');
                 return false;
             }
             fmcgAPIservice.addMAData('POST', 'dcr/save&code=' + $scope.$parent.code + "&date=" + $scope.$parent.activity_Date, "16", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("DCR Reject Successfully");
                     $state.go('fmcgmenu.ViewDCR');
                 }
             });
         }

     })
     .controller('LeaveApproval', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
         $scope.data = {};

         $scope.rejection = 0;
         $scope.$parent.navTitle = "Leave Approval";
         $scope.approve = function () {
             $scope.data.LA = $scope.$parent.LA;
             fmcgAPIservice.addMAData('POST', 'dcr/save&leaveid=' + $scope.$parent.LA.Leave_Id, "9", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("Leave Approval Submitted Successfully");
                     $state.go('fmcgmenu.ViewLeave');
                 }
             });
         }
         $scope.reject = function () {
             $scope.rejection = 1;
         }
         $scope.rejSend = function () {
             $scope.data.LA = $scope.$parent.LA;
             if ($scope.data.reason == undefined || $scope.data.reason=='') {
                 Toast('Enter the Reason...');
                 return false;
             }
             fmcgAPIservice.addMAData('POST', 'dcr/save&leaveid=' + $scope.$parent.LA.Leave_Id, "10", $scope.data).success(function (response) {
                 if (response.success) {
                     Toast("Leave Reject Successfully");
                     $state.go('fmcgmenu.ViewLeave');
                 }
             });
         }
         $scope.goBack = function () {
             $state.go('fmcgmenu.ViewLeave');
         }
     })
 .controller('LeaveForm', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification,$ionicLoading) {
     $scope.$parent.navTitle = "Leave Forms";
     $scope.LeaveTypes.type = {};
     $scope.LeaveTypes.type.selected = {};
     $scope.data = {};
     $scope.campaign = {
     };
     $scope.campaign.dateRange = [];
             $scope.leaves = [];
             fmcgAPIservice.getDataList('POST', 'vwLeaveStatus', [])
             .success(function (response) {
                      $scope.leaves = response;
                      $ionicLoading.hide();
                      }).error(function () {
                               $ionicLoading.hide();
                               Toast('No Internet Connection.');
                               });
             if ($scope.leaves.length == 2) {
             $scope.ll = 50;
             }
             if($scope.leaves.length == 3) {
             $scope.ll = 25;
             }
             if ($scope.leaves.length == 4)
             $scope.ll = 20;
             $scope.leaveCheck = [{ "Name": "Eligible" },
                                  { "Name": "Taken" },
                                  { "Name": "Available" },
                                  ]
     // __DevID
     $ionicLoading.show({
         template: 'Loading...'
     });
     $scope.campaign.add_months = function (dt, n) {
         if (dt != undefined) {
             dateArray = dt.split("/");
             dt = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0])
             return new Date(dt.setMonth(dt.getMonth() + n));
         }
     }

     fmcgAPIservice.getDataList('POST', 'vwCheckLeave', [])
             .success(function (response) {
                 $scope.campaign.dateRange = response;
                 $ionicLoading.hide();
             }).error(function () {
                 $ionicLoading.hide();
                 Toast('No Internet Connection.');
             });

     $scope.save = function () {
         $scope.data.Leave_Type = $scope.LeaveTypes.type.selected.id;
         if ($scope.data.Leave_Type == undefined) {
             Toast('Select the Leave Type...');
             return false;
         }
         $scope.data.From_Date= $scope.campaign.start_at;
         if ($scope.data.From_Date == undefined) {
             Toast('Enter the From Date...');
             return false;
         }
         dateArray = ($scope.data.From_Date).split("/");
         $scope.data.From_Date = dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0];
         $scope.data.To_Date = $scope.campaign.end_at||0;
         if ($scope.data.To_Date == undefined || $scope.data.To_Date ==0) {
             Toast('Enter the To Date...');
             return false;
         }
         dateArray = ($scope.data.To_Date).split("/");
         $scope.data.To_Date = dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0];
           $ionicLoading.show({
            template: 'Loading...'
        });
         var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
         var fieldforceName = loginInfo.sfName;
         $scope.sf_name = fieldforceName;
         fmcgAPIservice.addMAData('POST', 'dcr/save&sf_name='+fieldforceName, "8", $scope.data).success(function (response) {
             if (response.success) {
                 Toast("Leave Form Submitted Successfully");
                 $ionicLoading.hide();
                 $state.go('fmcgmenu.home');
             }
         });
     }
  
 })
 .directive("startDateCalendar", [
  function () {
      return function (scope, element, attrs) {
          scope.$watch("campaign.dateRange", (function (newValue, oldValue) {
              element.datepicker({
                  beforeShowDay: function (date) {
                      var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                      return [scope.campaign.dateRange.indexOf(dateString) == -1];
                  }
              })
          }), true)
          return element.datepicker({
              dateFormat: "dd/mm/yy",
              numberOfMonths: 1,
              minDate: new Date(),
              //maxDate: scope.campaign.end_at,
              //beforeShowDay: $.datepicker.noWeekends,
              beforeShowDay: function (date) {
                  var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                  return [scope.campaign.dateRange.indexOf(dateString) == -1];
              },
              onSelect: function (date) {
                  scope.campaign.start_at = date;
                  scope.data.No_of_Days = 0;

                  scope.$apply();
                  scope.campaign.endDat = scope.campaign.add_months(date, 1);

              }
          });
      };
  }

 ]).directive("endDateCalendar", [
  function () {
      return function (scope, element, attrs) {
          scope.$watch("campaign.dateRange", (function (newValue, oldValue) {
              element.datepicker({
                  beforeShowDay: function (date) {
                      var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                      return [scope.campaign.dateRange.indexOf(dateString) == -1];
                  }
              })
          }), true)
          scope.$watch("campaign.start_at", (function (newValue, oldValue) {
              k = 0;
              scope.campaign.end_at = '';
              scope.campaign.endDat = scope.campaign.add_months(newValue, 1);
              for (i = 0; i <= scope.campaign.dateRange.length; i++) {
                  dat = scope.campaign.dateRange[i];

                  if (dat != undefined && newValue != undefined) {
                      dateArray = newValue.split("/");
                      dt1 = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0]);
                      dateArray = dat.split("/");
                      dt2 = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0])
                      if (dt1 < dt2) {
                          element.datepicker("option", "maxDate", scope.campaign.dateRange[i]);
                          k = 1;
                          i = scope.campaign.dateRange.length;
                      }
                  }
              }

              element.datepicker("option", "minDate", newValue);;
              if (k == 0)
                  element.datepicker("option", "maxDate", scope.campaign.endDat);
          }), true);

          return element.datepicker({
              dateFormat: "dd/mm/yy",
              numberOfMonths: 1,
              minDate: scope.campaign.start_at,
              defaultDate: scope.campaign.end_at,
              maxDate: scope.campaign.endDat,
              beforeShowDay: function (date) {
                  var dateString = jQuery.datepicker.formatDate('dd/mm/yy', date);
                  return [scope.campaign.dateRange.indexOf(dateString) == -1];
              }
,
              onSelect: function (date) {
                  var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                  dateArray = scope.campaign.start_at.split("/");
                  var firstDate = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0]);
                  dateArray = date.split("/");
                  var secondDate = new Date(dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0]);
                  var diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
                  scope.data.No_of_Days = diffDays + 1;
                  scope.campaign.end_at = date;
                  return scope.$apply();
              }
          });
      };
  }

 ])

.controller('tpviewCtrl', function ($scope, $ionicLoading, fmcgAPIservice) {
    $scope.$parent.navTitle = "Tour plan view";
    $scope.tpvwlists = {};
    $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

    $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
    $scope.CntvwTop = (($scope.view_MR == 1) ? '88' : '130') + 'px';
    $scope.$on('eGetTpEntry', function (evnt) {
        $scope.GetTpEntry();
    });

    var temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    $scope.tpview.subordinate = {};
    $scope.tpview.subordinate.name = userData.sfName;
    $scope.tpview.subordinate.selected = {};
    $scope.tpview.subordinate.selected.id = userData.sfCode;

    $scope.GetTpEntry = function () {
        $ionicLoading.show({
            template: 'Loading...'
        });
        var params = {};
        $scope.error = "";
        params.sfCode = ($scope.desigCode=='MR')?$scope.sfCode:$scope.tpview.subordinate.selected.id;
        params.mnthYr = $scope.mnthDt;
        fmcgAPIservice.getPostData('POST', 'tpview', params).success(
      function (response) {
          $scope.tpvwlists = JSON.parse(JSON.stringify(response));
          if ($scope.tpvwlists.length <= 0) {
              $scope.error = "TP Entry Not Yet Updated...";
          };
          $ionicLoading.hide();
      }).error(function () {
          $ionicLoading.hide();
      });
    }
    $scope.GetTpEntry();
})
.controller('tpviewdtCtrl', function ($scope, $ionicLoading, fmcgAPIservice) {
    $scope.$parent.navTitle = "Datewise Tour plan view";
    $scope.tpDate = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
    $scope.dtTPvwlists = {};

    $scope.$on('GetTpEntryDt', function (evnt) {
        $scope.GetTpEntryDtws();
    });

    $scope.GetTpEntryDtws = function () {
        $ionicLoading.show({
            template: 'Loading...'
        });
        var params = {};
        $scope.error = "";
        var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
        params.tpDate = $scope.tpDate;
        params.sfCode = $scope.sfCode;
        fmcgAPIservice.getPostData('POST', 'tpviewdt', params)
        .success(function (response) {
            $scope.dtTPvwlists = JSON.parse(JSON.stringify(response));
            if ($scope.dtTPvwlists.length <= 0) {
                $scope.error = "TP Entry Not Yet Updated...";
            };
            $ionicLoading.hide();
        }).error(function () {
            $ionicLoading.hide();
        });
    }
    $scope.GetTpEntryDtws();
})
.controller('mnthSummary', function ($rootScope, $ionicLoading, $ionicScrollDelegate, $ionicModal, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function (modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });

    $scope.ViewDetail = function (Acd, SlTyp, Adt) {
        $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Visit Details For : ' + Adt;
        $scope.modal.show();

        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.modal.vwVstlists = [];
        fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
            .success(function (response) {
                $scope.modal.vwVstlists = response;
                $ionicLoading.hide();
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $ionicScrollDelegate.scrollTop();
    };
    $scope.$parent.navTitle = "Monthly Summary";
    $scope.clearData();
    $scope.mnthDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');

    var temp = window.localStorage.getItem("loginInfo");
    var userData = (temp != null && temp.length > 0) ? JSON.parse(temp) : null;
    $scope.MnthSumm.subordinate = {};
    $scope.MnthSumm.subordinate.name = userData.sfName;
    $scope.MnthSumm.subordinate.selected = {};
    $scope.MnthSumm.subordinate.selected.id = userData.sfCode;

    $scope.$on('getMonthReport', function (evnt) {
        $scope.getMonthReports();
    });
    
    $scope.subhdTop = (($scope.view_MR == 1) ? '44' : '88') + 'px';
    $scope.CntvwTop = (($scope.view_MR == 1) ? '75' : '130')+'px';
    $scope.getMonthReports = function () {
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'get/MnthSumm&rptSF=' + $scope.MnthSumm.subordinate.selected.id + '&rptDt=' + $scope.mnthDt, [])
            .success(function (response) {
                $scope.MnthDCRList = response;
                $ionicLoading.hide();
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
    }
    $scope.getMonthReports();
})
      .controller('sendMailCtrl', function ($rootScope, $scope, $ionicScrollDelegate, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal) {
          function failNative(e) {
              console.error('Houston, we have a big problem :(');
          }
         
          $scope.Attachment = function () {
                  console.log('event fired...');
              fileChooser.open(function (uri) {
                  var uripath = uri;
                  $ionicLoading.show({
                      template: 'Loading...'
                  });
                  window.FilePath.resolveNativePath(uripath, successNative, failNative);
              });
          }
          function successNative(finalPath) {
              // var path = 'file://' + finalPath;
              $scope.finalPath = finalPath;
              $scope.NameOfFile = finalPath.substr(finalPath.lastIndexOf('/') + 1);
              $ionicLoading.hide();

          }
          $scope.fileName = '';
          $scope.finalPath = '';
          function uploadFile() {
              var imgUrl = $scope.finalPath;
              var options = new FileUploadOptions();
              options.fileKey = "imgfile";
              options.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
              options.chunkedMode = false;
              options.mimeType = "image/jpeg";
              var uplUrl = baseURL + 'fileAttachment&sf_code=' + $scope.sfCode;
              var ft = new FileTransfer();
              ft.upload(imgUrl, uplUrl,
                 function (response) {

                     $scope.finalPath = '';
                 },
                 function () {
                 }, options);
          }
          // console.log($scope.myFile);
          data = {};


          $scope.goBack = function () {
              $state.go('fmcgmenu.mail');
          }
          $scope.sendMail = function () {
              $scope.data = {};
              if ($scope.finalPath != "") {
                  uploadFile();
                  var imgUrl = $scope.finalPath;
                  $scope.fileName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
              }
              var fd = new FormData();
              fd.append('fileName', $scope.fileName);
              fd.append('to', $scope.fmcgData.staffNms);
              fd.append('cc', addQuotes($scope.fmcgData.ccstaffNms));
              fd.append('bcc', addQuotes($scope.fmcgData.bccstaffNms));
              msg = $("[name=subject]").val();
              msg1 = msg.replace(/</g, '&lt;');
              msg2 = msg1.replace(/>/g, '&gt;');
              fd.append('subject', encodeURIComponent(msg2));
              msg = $("[name=content]").val();
              if ($scope.fmcgData.ccstaffIds == undefined)
                  $scope.fmcgData.ccstaffIds = "";
              if ($scope.fmcgData.bccstaffIds == undefined)
                  $scope.fmcgData.bccstaffIds = "";
              ToCcBcc = $scope.fmcgData.staffIds + $scope.fmcgData.ccstaffIds + $scope.fmcgData.bccstaffIds;
              fd.append('to_id', addQuotes($scope.fmcgData.staffIds));
              fd.append('cc_id', addQuotes($scope.fmcgData.ccstaffIds));
              fd.append('bcc_id', addQuotes($scope.fmcgData.bccstaffIds));
              msg1 = msg.replace(/</g, '&lt;');
              msg2 = msg1.replace(/>/g, '&gt;');
              fd.append('message', encodeURIComponent(msg2));
              fd.append('ToCcBcc', ToCcBcc);
              var loginInfo = JSON.parse(localStorage.getItem("loginInfo"));
              fd.append('from', loginInfo.sfName);
              if ($scope.fmcgData.staffNms == '' || $scope.fmcgData.staffNms == undefined) {
                  Toast("Select To");
                  return false;
              }
              $ionicLoading.show({
                  template: 'Loading...'
              });
              fmcgAPIservice.addMAData('POST', 'dcr/save', "19", fd).success(function (response) {
                  if (response.success) {
                      $state.go('fmcgmenu.mail');
                      Toast("Mail Successfully Submitted");
                      $ionicLoading.hide();

                  }
              });
          }
      })

    .directive('contenteditable', function () {
        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;
                ngModel.$render = function () {
                    element.html(ngModel.$viewValue || '');
                };
                element.on('blur keyup change', function () {
                    scope.$apply(read);
                });
                read();
                function read() {
                    var html = element.html();
                    if (attrs.stripBr && html == '<br>') {
                        html = '';
                    }
                    ngModel.$setViewValue(html);
                }
            }
        };
    })

      .controller('mailCtrl', function ($rootScope, $scope, $ionicScrollDelegate, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading, $ionicModal) {
          $scope.$parent.navTitle = "Mail";
          $scope.folder = {};
          var date1 = new Date();
          //    date1 = new Date("2016-12-16")
          $scope.month = date1.getMonth() + 1;
          $scope.year = date1.getFullYear();
          years = [];
          for (i = 2014; i <= $scope.year; i++) {
              years.push({ "val": i });
          }
          $scope.years = years;

          $scope.folder.selected = {};
          $scope.folder.selected.id = "inbox";
          $scope.folder.name= "Inbox";
          $scope.compose = function () {
              $scope.fmcgData.staffSelectedList = []; $scope.fmcgData.staffNms = ''; $scope.fmcgData.staffIds = "";
              $scope.fmcgData.staffSelectedListcc = []; $scope.fmcgData.ccstaffNms = ''; $scope.fmcgData.ccstaffIds = "";
              $scope.fmcgData.staffSelectedListbcc = []; $scope.fmcgData.bccstaffNms = ''; $scope.fmcgData.bccstaffIds = "";

              $scope.fmcgData.msg = '';
              $scope.fmcgData.Attach = '';
              $scope.fmcgData.subject = '';
              $scope.fmcgData.AttachName = '';
              $state.go('fmcgmenu.sendMail');
          }
          $scope.mailType = 'viewFolders';
          if ($scope.folder.selected.id != undefined) {
              viewDetails($scope.folder.name, $scope.month, $scope.year);
          }
          $scope.viewMail = function (Staffid_Id, Mail_vc_ViewFlag, Mail_int_Det_No, date, Attach, from, to, cc, sub, msg,bcc) {
              if ($scope.folder.selected.id == "inbox" && $scope.folder.selected.id != "sent") {
                  fmcgAPIservice.addMAData('POST', 'mailView&id=' + $scope.fmcgData.Mail_int_Det_No).success(function (response) {
                      if (response.success) {
                          //$scope.mailType = "viewFolders";
                      }
                  });
              }
              $scope.fmcgData.msg = "";
              $ionicScrollDelegate.scrollTop();
              $scope.mailType = 'View';
              msg1 = msg.replace(/&lt;/g, '<');
              msg2 = msg1.replace(/&gt;/g, '>');
              $scope.fmcgData.Mail_int_Det_No = Mail_int_Det_No;
              $scope.fmcgData.msg = msg2;
              $scope.fmcgData.date = date;
              $scope.fmcgData.Attach = MailPath + Attach;
              $scope.fmcgData.AttachName = Attach;
              $scope.fmcgData.Staffid_Id = Staffid_Id;
              if ($scope.folder.selected.id == "Sent") {
                  $scope.fmcgData.to = from;
                  $scope.fmcgData.from = to;
              }
              else {
                  $scope.fmcgData.from = from;
                  $scope.fmcgData.to = to;
              }
              $scope.fmcgData.cc = cc;
              $scope.fmcgData.bcc = bcc;
              $scope.fmcgData.sub = sub;
          }
          $scope.view = function () {
              $ionicLoading.show({
                  template: 'Loading...'
              });

              var fileTransfer = new FileTransfer();
              var uri = encodeURI($scope.fmcgData.Attach);
              var fileURL = cordova.file.externalCacheDirectory;
              fileTransfer.download(
                  uri,
                   fileURL + $scope.fmcgData.AttachName,
                  function (entry) {
                      $ionicLoading.hide();
                      filePath = fileURL + $scope.fmcgData.AttachName;
                      fileName = $scope.fmcgData.AttachName;
                      var extn = fileName.split(".").pop();
                      if (extn == "png" || extn == "jpg")
                          mimetype = 'image/png';
                      else
                          mimetype = 'application/' + extn;
                      cordova.plugins.fileOpener2.open(
                     filePath,
                    mimetype,
                    {
                        error: function () {
                            //alert('Error status: ' + e.status + ' - Error message: ' + e.message);
                        },
                        success: function () {
                            // alert("Success");
                        }
                    }
                    );
                  },
                  function (error) {
                  },
                  false,
                  {
                      headers: {
                          "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                      }
                  }
              );
          }
          $scope.downloadFile = function () {
              $ionicLoading.show({
                  template: 'Loading...'
              });
              var fileTransfer = new FileTransfer();
              var uri = encodeURI($scope.fmcgData.Attach);
              var fileURL = cordova.file.externalRootDirectory + "/Download/";
              fileTransfer.download(
                  uri,
                   fileURL + $scope.fmcgData.AttachName,

                  function (entry) {
                      $ionicLoading.hide();
                      alert("Download Completed");
                      //  alert("download complete: " + entry.toURL());
                  },
                  function (error) {
                      // alert("download error source " + error.source);
                      // alert("download error target " + error.target);
                      // alert("download error code" + error.code);
                  },
                  false,
                  {
                      headers: {
                          "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                      }
                  }
              );
          }
          $scope.$on('moveFolders', function (evnt, det) {
              $scope.subfolder.selected.id = det.Details['id'];
              fmcgAPIservice.addMAData('POST', 'mailMove&id=' + $scope.fmcgData.Mail_int_Det_No + "&folder=" + $scope.subfolder.name).success(function (response) {
                  if (response.success) {
                      $scope.mailType = "viewFolders";
                      Toast("Mail Moved");
                  }
              });
          });
          $scope.showSelectMonth = function (val) {
              $scope.month = val;
              viewDetails($scope.folder.name, $scope.month, $scope.year);

          }
          $scope.showSelectYear = function (val) {
              $scope.year = val;
              viewDetails($scope.folder.name, $scope.month, $scope.year);

          }
          $scope.$on('getFolders', function (evnt, det) {
              $scope.folder.selected.id = det.Details['id'];

              viewDetails(det.Details['name'], $scope.month, $scope.year);
          });

          $scope.reply = function () {
              $scope.fmcgData.staffIds = $scope.fmcgData.Staffid_Id;
              $scope.fmcgData.staffNms = $scope.fmcgData.from+',';
              $scope.$parent.mailsStaff.push({ "id": $scope.fmcgData.Staffid_Id, "name": $scope.fmcgData.from });
              $scope.fmcgData.staffSelectedList = [];
              $scope.$parent.fmcgData.staffSelectedList.push({ "id": $scope.fmcgData.Staffid_Id, "name": $scope.fmcgData.from, "checked": true });
              $scope.fmcgData.subject = 're:' + $scope.fmcgData.sub;
              $scope.fmcgData.message = '';
              $state.go('fmcgmenu.sendMail');
          }
          $scope.delete = function () {
              fmcgAPIservice.addMAData('POST', 'mailDel&id=' + $scope.fmcgData.Mail_int_Det_No + "&folder=" + $scope.folder.selected.id).success(function (response) {
                  if (response.success) {
                      $scope.mailType = "viewFolders";

                  }
              });
          }
          $scope.forward = function () {
              str = "<div>----- Forwarded Message ----</div><div>From : " + $scope.fmcgData.from + "</div><div>To :" + $scope.fmcgData.to + "</div><div>Sent : " + $scope.fmcgData.date + "</div><div><br></div><div><br></div>";
              $scope.fmcgData.message = str + $scope.fmcgData.msg;
              $scope.fmcgData.subject = "Fwd:" + $scope.fmcgData.sub;
              $state.go('fmcgmenu.sendMail');
          }
          $scope.goBack = function () {
              $scope.mailType = 'viewFolders';
          }
          function viewDetails(folder, month, year) {
              $ionicLoading.show({
                  template: 'Loading...'
              });
              fmcgAPIservice.getDataList('POST', 'getMailsApp&folder=' + folder + "&month=" + month + "&year=" + year, [])
               .success(function (response) {
                   $scope.Mails = response;
                   $ionicLoading.hide();
               }).error(function () {
                   $ionicLoading.hide();
                   Toast('No Internet Connection.');
               })
          }


      })
         .controller('medUpdateUploadCtrl', function ($rootScope, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification, $ionicLoading) {
             $scope.$parent.navTitle = "Circular";
            
             $ionicLoading.show({
                 template: 'Loading...'
             });
             $scope.download = function (fileName) {

                 Attach = UploadPath + fileName;

                 var fileTransfer = new FileTransfer();
                 var uri = encodeURI(Attach);
                 var fileURL = cordova.file.externalRootDirectory + "/Download/";

                 $ionicLoading.show({
                     template: 'Loading...'
                 });
                 fileTransfer.download(
                     uri,
                      fileURL + fileName,

                     function (entry) {
                         $ionicLoading.hide();
                         alert("Download Completed");
                     },
                     function (error) {
                     },
                     false,
                     {
                         headers: {
                             "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                         }
                     }
                 );
             }
             $scope.view = function (fileName) {
                 $ionicLoading.show({
                     template: 'Loading...'
                 });
                 Attach = UploadPath + fileName;
                 var fileTransfer = new FileTransfer();
                 var uri = encodeURI(Attach);
                 var fileURL = cordova.file.externalCacheDirectory;
                 fileTransfer.download(
                     uri,
                      fileURL + fileName,
                     function (entry) {
                         $ionicLoading.hide();
                         filePath = fileURL + fileName;
                         var extn = fileName.split(".").pop();

                         if (extn == "png" || extn == "jpg")
                             mimetype = 'image/png';
                         else
                             mimetype = 'application/' + extn;
                         cordova.plugins.fileOpener2.open(
                        filePath,
                       mimetype,
                       {
                           error: function () {
                               //alert('Error status: ' + e.status + ' - Error message: ' + e.message);
                           },
                           success: function () {
                               // alert("Success");
                           }
                       }
                       );
                     },
                     function (error) {
                     },
                     false,
                     {
                         headers: {
                             "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=="
                         }
                     }
                 );
             }
             fmcgAPIservice.getDataList('POST', 'vwMedUpdateUpload', [])
                   .success(function (response) {
                       $scope.MissedDoctors = response;
                       $ionicLoading.hide();
                   }).error(function () {
                       $ionicLoading.hide();
                       Toast('No Internet Connection.');

                   })

         })
.controller('dayReport', function ($rootScope, $ionicLoading, $ionicModal, $ionicScrollDelegate, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.modal = $ionicModal;
    $scope.modal.fromTemplateUrl('partials/ViewVstDetails.html', function (modal) {
        $scope.modal = modal;
    }, {
        animation: 'slide-in-up',
        focusFirstInput: true
    });

    $scope.ViewDetail = function (Acd, SlTyp, Adt) {
        $scope.modal.rptTitle = ((SlTyp == 1) ? $scope.DrCap : (SlTyp == 2) ? $scope.ChmCap : (SlTyp == 3) ? $scope.StkCap : (SlTyp == 4) ? $scope.NLCap : '') + ' Visit Details For : ' + Adt;
        $scope.modal.show();

        $ionicLoading.show({
            template: 'Loading...'
        });
        $scope.modal.vwVstlists = [];
        fmcgAPIservice.getDataList('POST', 'get/vwVstDet&ACd=' + Acd + '&typ=' + SlTyp, [])
            .success(function (response) {
                $scope.modal.vwVstlists = response;
                $ionicLoading.hide();
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
        $ionicScrollDelegate.scrollTop();
    };
    $scope.$parent.navTitle = "Day Report";
    $scope.clearData();
    $scope.dyRptDt = new Date().toISOString().slice(0, 10).replace(/-/g, '-');
    $scope.getDayReports = function () {
        $ionicLoading.show({
            template: 'Loading...'
        });
        fmcgAPIservice.getDataList('POST', 'get/DayRpt&rptDt=' + $scope.dyRptDt, [])
            .success(function (response) {
                $scope.dayDCRList = response;
                $ionicLoading.hide();
            }).error(function () {
                $ionicLoading.hide();
                Toast('No Internet Connection.');
            });
    }
    $scope.getDayReports();
})
.controller('dcrData1', function ($rootScope, $ionicLoading, $scope, $state, fmcgAPIservice, fmcgLocalStorage, notification) {
    $scope.$parent.navTitle = "Day Report";
    $scope.clearData();
    $ionicLoading.show({
        template: 'Loading...'
    });
    $scope.success = false;
    $scope.owsuccess = false;
    $scope.owTypeData = [];
    //$scope.$parent.navTitle = "Submitted Calls";
    
    $scope.customers = [{
            'id': '1',
            'name': $scope.DrCap,
            'url': 'manageDoctorResult'
    }];
    var al = 1;
    if ($scope.ChmNeed != 1) {
        $scope.customers.push({
            'id': '2',
            'name': $scope.ChmCap,
            'url': 'manageChemistResult'
        });
        $scope.cCI = al; al++;
    }
    if ($scope.StkNeed != 1) {
        $scope.customers.push({
            'id': '3',
            'name': $scope.StkCap,
            'url': 'manageStockistResult'
        });
        $scope.sCI = al; al++;
    }
    if ($scope.UNLNeed != 1) {
        $scope.customers.push({
            'id': '4',
            'name': $scope.NLCap,
            'url': 'manageStockistResult'
        });
        $scope.nCI = al; al++;
    }
    fmcgAPIservice.getDataList('POST', 'entry/count', []).success(function (response) {
        if (response['success'])
        {
            $scope.success = true;
            $scope.customers[0].count = response['data'][0]['doctor_count'];
            if ($scope.ChmNeed != 1) $scope.customers[$scope.cCI].count = response['data'][1]['chemist_count'];
            if ($scope.StkNeed != 1) $scope.customers[$scope.sCI].count = response['data'][2]['stockist_count'];
            if ($scope.UNLNeed != 1) $scope.customers[$scope.nCI].count = response['data'][3]['uldoctor_count'];
            $scope.owTypeData.daywise_remarks = response['data'][4]['remarks'];
            $scope.owTypeData.HlfDayWrk = response['data'][5]['halfdaywrk'];
        }
        else
        {
            $scope.owsuccess = true;
            $scope.owTypeData = response['data'][0];
        }
        $ionicLoading.hide();
    }).error(function () {
        $ionicLoading.hide();
        Toast('No Internet Connection.');
    });
})
.controller('geoTagCtrl', function ($rootScope, $scope, fmcgLocalStorage, fmcgAPIservice, $compile, $ionicModal) {
    if ($scope.view_MR == 1) {
        $scope.geoTag.subordinate = {};
        $scope.geoTag.subordinate.selected = {};
        $scope.geoTag.subordinate.selected.id = $scope.sfCode;
        $scope.loadDatas(false, '_' + $scope.sfCode);

        $scope.subhdTop = '44px';
        $scope.CntvwTop = '88px';
    } else {
        $scope.geoTag.subordinate = undefined;
        $scope.subhdTop = '88px';
        $scope.CntvwTop = '132px';
    }
    $scope.geoTag.cluster = {};
    $scope.GeoTagDr = {};
    $scope.geoCustomers = [];
    $scope.unGeoCustomers = [];
    $scope.$on('setGeoData', function (e) {
        var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];
        $scope.geoCustomers = data.filter(function (a) {
            a.town_code = a.town_code + ',';
            a.town_code=a.town_code.replace( ',,', ',');
                return (a.lat != '' && (',' + a.town_code).indexOf(',' + $scope.geoTag.cluster.selected.id + ',') > -1);
            });
        
        $scope.unGeoCustomers = data.filter(function (a) {
            a.town_code = a.town_code + ',';
            a.town_code = a.town_code.replace(',,', ',');
                return (a.lat == '' && (',' + a.town_code).indexOf(',' + $scope.geoTag.cluster.selected.id + ',') > -1);
            });
           });
    var map; var myLatlng; setPosFlag = false; var marker;

    $scope.mapLocation = function (item, m_Flag) {
        if (typeof google === 'undefined' || typeof google === undefined) {
            $.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyCOY8BqGMFqo7Ij2n1O8b4RcL443zjoI_o&libraries=places');
        }

        $scope.modalMap.show();
        $scope.GeoTagDr = item;
        $scope.modalMap.vTyp = m_Flag;

        mapWin = $('.modal-backdrop .active');
        
        $("#map_canvas").closest('.scroll').addClass("fitHeight");
        $("#map_canvas").closest('.scroll').removeClass("scroll");
        $(".fitHeight").closest('.scroll-content').removeClass("ionic-scroll");
        $(".fitHeight").closest('.scroll-content').removeClass("scroll-content");
        mapWin.css("left", "0px");
        mapWin.css("top", "0px");
        mapWin.css("width", "100%");
        mapWin.css("height", "100%");

        myLatlng = null;
        tLatLng = new google.maps.LatLng(13.0460985, 80.2228998)
        if (m_Flag == 1) {
            myLatlng = new google.maps.LatLng(item.lat, item.long);
            setPosFlag = true;
        } else {
            if (_currentLocation.Latitude != undefined) {
                myLatlng = new google.maps.LatLng(_currentLocation.Latitude, _currentLocation.Longitude);
                setPosFlag = true;
            }
        }

        var mapOptions = {
            center: ((myLatlng == null) ? tLatLng : myLatlng),
            zoom: ((myLatlng == null) ? 5 : (17 - ((m_Flag == 1) ? $scope.roundNumber($scope.DisRad, 0) : 0))),
            panControl: true,
            zoomControl: true,
            mapTypeControl: false,
            scaleControl: true,
            streetViewControl: true,
            overviewMapControl: true,
            rotateControl: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        if ($scope.mymap == undefined) {
            var myDiv = document.getElementById("map_canvas");
            var mymap = new google.maps.Map(myDiv, mapOptions);

            // Create the search box and link it to the UI element.
            var input = document.getElementById('pac-input');
            input.value = "";
            var searchBox = new google.maps.places.Autocomplete(input);
            searchBox.setTypes(['geocode']);
            mymap.addListener('bounds_changed', function () {
                searchBox.setBounds(mymap.getBounds());
                setTimeout(function () {
                    plcs = $('.pac-container');
                    $('.pac-container').remove();
                    plcs.appendTo(".modal-backdrop .active");
                }, 500);
            });

            $scope.map = mymap;

            searchBox.addListener('place_changed', function () {
                var place = searchBox.getPlace();
                if (place.geometry.viewport) {
                    mymap.fitBounds(place.geometry.viewport);
                } else {
                    mymap.setCenter(place.geometry.location);
                    mymap.setZoom(17);
                }
                marker.setPosition(place.geometry.location);
                var address = '';
                if (place.address_components) {
                    address = [
                      (place.address_components[0] && place.address_components[0].short_name || ''),
                      (place.address_components[1] && place.address_components[1].short_name || ''),
                      (place.address_components[2] && place.address_components[2].short_name || '')
                    ].join(' ');
                }

                plcID = document.getElementById('plcAddr');
                if (plcID != null) plcID.innerHTML = address;
                infowindow.open(mymap, marker);
            });
        }

        if (marker != undefined) marker.setMap(null);

        if (m_Flag == 1) {
            marker = new google.maps.Circle({
                center: myLatlng,
                map: $scope.map,
                strokeColor: '#cccccc',
                strokeWeight: 2,
                strokeOpacity: 0.5,
                fillColor: '#0000df',
                fillOpacity: 0.5,
                radius: $scope.DisRad * 1000
            });
        }
        marker = new google.maps.Marker({
            map: $scope.map,
            draggable: (m_Flag == 1) ? false : true,
            animation: google.maps.Animation.DROP,
            position: myLatlng
        });

        var geocoder = new google.maps.Geocoder();

        function geocodePosition(pos) {
            $scope.GeoTagDr.lat = pos.lat();
            $scope.GeoTagDr.long = pos.lng();
            geocoder.geocode({
                latLng: pos
            },
            function (responses) {
                plcID = document.getElementById('plcAddr')
                if (responses && responses.length > 0) {
                    if (plcID != null) plcID.innerHTML = responses[0].formatted_address;
                    $scope.GeoTagDr.addrs = responses[0].formatted_address;
                } else {
                    if (plcID != null) plcID.innerHTML = 'Cannot determine address at this location.';
                    $scope.GeoTagDr.addrs = '';
                }
            });
        }
        var infowindow = new google.maps.InfoWindow({
            content: "<br><b>Doctor :</b> " + $scope.GeoTagDr.name + "<br><b>Address :</b> <span id='plcAddr'></span><br><div style='display:" + ((m_Flag == 0) ? 'block' : 'none') + ";text-align:center;padding-top:15px;'><button class='button buttonClear  button-balanced pull-right' onclick='SvLocation()'>Tag This Place</button></div>",
        });
        if (m_Flag == 0) {
            google.maps.event.addListener(marker, 'dragend', function () {
                geocodePosition(marker.getPosition());
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open($scope.map, marker);
                geocodePosition(marker.getPosition());
            });
        }


        marker.setAnimation(google.maps.Animation.BOUNCE);

        if (myLatlng != null) {
            infowindow.open($scope.map, marker);
            geocodePosition(myLatlng);
        }
        infowindow.addListener('domready', function () {
            SvLocation = function () {
                fmcgAPIservice.addMAData('POST', 'dcr/save', "6", $scope.GeoTagDr).success(function (response) {
                    if (response.success) Toast("GEO Tagged Successfully");
                }).error(function () {
                    $scope.OutGEOTag = fmcgLocalStorage.getData("OutBx_GEOTag") || [];
                    $scope.OutGEOTag.push($scope.GeoTagDr);
                    localStorage.removeItem("OutBx_GEOTag");
                    fmcgLocalStorage.createData("OutBx_GEOTag", $scope.OutGEOTag);
                    Toast("No Internet Connection! GEO Tagged in Outbox");

                });
                var data = fmcgLocalStorage.getData('doctor_master_' + $scope.geoTag.subordinate.selected.id) || [];
                var drRw = data.filter(function (a) { return (a.id == $scope.GeoTagDr.id); });
                var $indx = data.indexOf(drRw[0]);
                data[$indx].lat = $scope.GeoTagDr.lat;
                data[$indx].long = $scope.GeoTagDr.long;
                data[$indx].addrs = $scope.GeoTagDr.addrs;
                fmcgLocalStorage.createData('doctor_master_' + $scope.geoTag.subordinate.selected.id, data);
                $scope.$broadcast('setGeoData');

                $scope.modalMap.hide();
            }
        });
        marker.addListener('click', function () {

        });
    }

})
    .filter('selectMap', function () {

    return function (input, data) {
        if (data)
        {
            for (var i = 0, len = data['collection'].length; i < len; i++)
            {
                if (parseInt(data['collection'][i].id) === parseInt(input))
                {
                    input = data['collection'][i].name;
                    break;
                }
            }
        }
        return input;
    };
}).filter('reverseGeoCode', function () {
    return function (input) {
        return input;
    };
}).filter('getValueforID', function () {
    return function (id, collection) {
        var arrayToReturn = [];
        if (collection && id)
        {

            var len;
            try {
                len = collection.length;
            }
            catch (m)
            {
                len = 0;
            }
            for (var i = 0; i < len; i++) {

                if (id === collection[i].id) {
                    return collection[i];
                    break;
                }
            }
        }
        return null;
    }
    ;
}).filter('searchF', function () {
    return function (id, collection) {
        var arrayToReturn = [];
        if (collection && id)
        {
            collection = collection;
            var len;
            try {
                len = collection.length;
            }
            catch (m)
            {
                len = 0;
            }
            for (var i = 0; i < len; i++) {

                if (id.toString() === collection[i].town_code.toString()) {
                    arrayToReturn.push(collection[i]);

                }
            }
            return arrayToReturn;
        }
        return [];
    }
    ;
}).filter('getValueforIDD', function () {
    return function (id, collection) {
        var arrayToReturn = []; 
        if (collection && id)
        {
            collection = collection.collection;

            var len;
            try {
                len = collection.length;
            }
            catch (m)
            {
                len = 0;
            }
            for (var i = 0; i < len; i++) {
                if (id.toString() === collection[i].id.toString()) {
                    return collection[i];
                    break;
                }
            }
        }
        return null;
    }
    ;
}).directive('reverseGeocode', function () {
    return {
        restrict: 'E',
        template: '<div></div>',
        link: function (scope, element, attrs) {
            var geocoder = new google.maps.Geocoder();
            var results = attrs.latlng.split(":");
            if (results.length == 2)
            {
                var latlng = new google.maps.LatLng(results[0], results[1]);
                geocoder.geocode({'latLng': latlng}, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[1]) {
                            element.text(results[1].formatted_address);
                        } else {
                            element.text('Location not found');
                        }
                    } else {
                        element.text('Geocoder failed due to: ' + status);
                    }
                });
            }
        },
        replace: true
    }
});
var isReachable = function ()
{
    if (navigator.connection)
    {
       if (isComputer() == true)
            return true;
        else {
            var networkState = navigator.connection ? navigator.connection.type : Connection.NONE;
            var states = {};
            states[Connection.UNKNOWN] = 'Unknown connection';
            states[Connection.ETHERNET] = 'Ethernet connection';
            states[Connection.WIFI] = 'WiFi connection';
            states[Connection.CELL_2G] = 'Cell 2G connection';
            states[Connection.CELL_3G] = 'Cell 3G connection';
            states[Connection.CELL_4G] = 'Cell 4G connection';
            states[Connection.NONE] = 'No network connection';
            if (Connection.NONE == networkState)
                return false;
            else
                return true;
        }
    }
    else
    {
        return navigator.onLine;
    }
}
var isComputer = function ()
{

    if (navigator.platform === "Win32")
    {
        return true;
    }
    return false;
}
var isGpsEnabled = function (PositionError)
{
    if (isComputer())
        return true;
    else if (PositionError)
    {
        return false;
    }
    return true;
}
var Toast = function (message, type)
{
    if (window.plugins)
    {
        window.plugins.toast.showShortCenter(message, function (a) {
            console.log('toast success: ' + a)
        }, function (b) {
            alert('toast error: ' + b)
        });
    }
    else
    {
        var data = {};
        data.message = message;
        data.extraClasses = 'messenger-fixed messenger-on-left messenger-on-top';
        if (type)
            data.type = "error";
        Messenger().post(data);
    }
};


function enableLight() {
    if (window.watcher) {
        navigator.geolocation.clearWatch(window.watcher);
    }
    var ele = document.querySelector(".ion-location");
    window.watcher = navigator.geolocation.watchPosition(function (position) {
        ele && ele.classList.add("available");
    },
    function (error) {
        if (ele) {
            ele.classList.remove("available");
        } else {
            console.log("Notifier icon not found");
        }
    }, {maximumAge: 0, timeout: 5000, enableHighAccuracy: true})
}
document.addEventListener("deviceready", function () {
    enableLight();
});
document.addEventListener("resume", function () {
//    alert("Resume"+window.watcher);
    enableLight();
//    if (window.watch) {
//        navigator.geolocation.clearWatch(window.watch);
//    }
}, false);
