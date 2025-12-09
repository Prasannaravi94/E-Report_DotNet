<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard_PreCall_Analysis.aspx.cs" Inherits="MasterFiles_DashBoard_PreCall_Analysis" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
        integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/css/bootstrap-select.min.css" />
    <link href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap.min.css"
        rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />
    <link href="../css/pace/themes/blue/pace-theme-flash.css" rel="stylesheet" />

    <style>
        .blockUI.blockMsg.blockElement {
            border: none !important;
        }

        #loader {
            position: fixed;
            top: 50%;
            right: 50%;
            margin: auto;
        }

            #loader .dot {
                bottom: 0;
                height: 100%;
                left: 0;
                margin: auto;
                position: absolute;
                right: 0;
                top: 0;
                width: 87.5px;
            }

                #loader .dot::before {
                    border-radius: 100%;
                    content: "";
                    height: 87.5px;
                    left: 0;
                    position: absolute;
                    right: 0;
                    top: 0;
                    transform: scale(0);
                    width: 87.5px;
                }

                #loader .dot:nth-child(7n+1) {
                    transform: rotate(45deg);
                }

                    #loader .dot:nth-child(7n+1)::before {
                        animation: 0.8s linear 0.1s normal none infinite running load;
                        background: #00ff80 none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+2) {
                    transform: rotate(90deg);
                }

                    #loader .dot:nth-child(7n+2)::before {
                        animation: 0.8s linear 0.2s normal none infinite running load;
                        background: #00ffea none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+3) {
                    transform: rotate(135deg);
                }

                    #loader .dot:nth-child(7n+3)::before {
                        animation: 0.8s linear 0.3s normal none infinite running load;
                        background: #00aaff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+4) {
                    transform: rotate(180deg);
                }

                    #loader .dot:nth-child(7n+4)::before {
                        animation: 0.8s linear 0.4s normal none infinite running load;
                        background: #0040ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+5) {
                    transform: rotate(225deg);
                }

                    #loader .dot:nth-child(7n+5)::before {
                        animation: 0.8s linear 0.5s normal none infinite running load;
                        background: #2a00ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+6) {
                    transform: rotate(270deg);
                }

                    #loader .dot:nth-child(7n+6)::before {
                        animation: 0.8s linear 0.6s normal none infinite running load;
                        background: #9500ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+7) {
                    transform: rotate(315deg);
                }

                    #loader .dot:nth-child(7n+7)::before {
                        animation: 0.8s linear 0.7s normal none infinite running load;
                        background: magenta none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+8) {
                    transform: rotate(360deg);
                }

                    #loader .dot:nth-child(7n+8)::before {
                        animation: 0.8s linear 0.8s normal none infinite running load;
                        background: #ff0095 none repeat scroll 0 0;
                    }

            #loader .lading {
                background-image: url("../images/loading.gif");
                background-position: 50% 50%;
                background-repeat: no-repeat;
                bottom: -40px;
                height: 20px;
                left: 0;
                position: absolute;
                right: 0;
                width: 180px;
            }

        @keyframes load {
            100% {
                opacity: 0;
                transform: scale(1);
            }
        }

        @keyframes load {
            100% {
                opacity: 0;
                transform: scale(1);
            }
        }

        #tblMsgInfo > tbody > tr > td {
            border: 1px solid #aba3a3;
        }
        /* Default Header Styles */
        .dynamic-header {
            background-color: #4cb6c4;
            color: #fff;
            text-align: center;
            padding: 10px;
            font-size: 16px;
        }

        /* Responsive Header for Tablets */
        @media (max-width: 768px) {
            .dynamic-header {
                font-size: 14px;
                padding: 8px;
            }
        }

        /* Responsive Header for Mobile */
        @media (max-width: 480px) {
            .dynamic-header {
                font-size: 12px;
                padding: 6px;
            }
        }

        table.data-table {
            width: 80%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 12px;
            text-align: left;
        }

            table.data-table th, table.data-table td {
                padding: 10px;
                border: 1px solid #ddd;
                font-size: 12px;
                font-family: Verdana;
            }


            table.data-table th {
                background-color: #f4f4f4;
                font-size: 12px;
                font-family: Verdana;
            }

            table.data-table td {
                background-color: #f9f9f9;
                font-size: 11px;
                font-family: Verdana;
            }

        /* Mobile responsiveness */
        @media only screen and (max-width: 600px) {
            table.data-table {
                font-size: 12px;
                font-family: Verdana;
            }

                table.data-table th, table.data-table td {
                    padding: 8px;
                    font-size: 11px;
                }
        }

        btn2 {
            display: inline-block;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            padding: .375rem .75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: .25rem;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .btn2:focus, .btn2:hover {
            text-decoration: none;
        }

        .btn2.focus, .btn2:focus {
            outline: 0;
            box-shadow: 0 0 0 .2rem rgba(0,123,255,.25);
        }

        .btn2.disabled, .btn2:disabled {
            opacity: .65;
        }

        .btn2:not(:disabled):not(.disabled) {
            cursor: pointer;
        }

            .btn2:not(:disabled):not(.disabled).active, .btn2:not(:disabled):not(.disabled):active {
                background-image: none;
            }

        a.btn2.disabled, fieldset:disabled a.btn2 {
            pointer-events: none;
        }

        .btn-primary2 {
            color: #fff;
            background-color: #0069d9;
            border-color: #0069d9;
        }

            .btn-primary2:hover {
                color: #fff;
                background-color: #0069d9;
                border-color: #0069d9;
            }

            .btn-primary2.focus, .btn-primary2:focus {
                box-shadow: 0 0 0 .2rem rgba(0,123,255,.5);
            }

            .btn-primary2.disabled, .btn-primary2:disabled {
                color: #fff;
                background-color: #0069d9;
                border-color: #0069d9;
            }

            .btn-primary2:not(:disabled):not(.disabled).active, .btn-primary2:not(:disabled):not(.disabled):active, .show > .btn-primary2.dropdown-toggle {
                color: #fff;
                background-color: #0069d9;
                border-color: #0069d9;
            }

                .btn-primary2:not(:disabled):not(.disabled).active:focus, .btn-primary2:not(:disabled):not(.disabled):active:focus, .show > .btn-primary2.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .2rem rgba(0,123,255,.5);
                }

        .mobile-container {
            max-width: 480px;
            margin: auto;
            background-color: #0069d9;
            height: 500px;
            color: white;
            border-radius: 10px;
        }

        .topnav {
            overflow: hidden;
            background-color: #333;
            position: relative;
        }

            .topnav #myLinks {
                display: none;
            }

            .topnav a {
                color: white;
                padding: 14px 16px;
                text-decoration: none;
                font-size: 17px;
                display: block;
            }

                .topnav a.icon {
                    background: black;
                    display: block;
                    position: absolute;
                    right: 0;
                    top: 0;
                }

                .topnav a:hover {
                    background-color: #ddd;
                    color: black;
                }

        .active {
            background-color: #0069d9;
            color: white;
        }
         .loader-wrapper {
            /*top: 0;
            left: 0;*/
            width: 100vw;
            height: 100vh;
            z-index: 1000;
            background-color: rgba(0, 0, 0, 0.3);
            position: absolute;
        }

        .loaders {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index: 1000; /* Ensure it's above other elements */
            background-color: rgba(255, 255, 255, 0.8); /* Optional semi-transparent background */
            padding: 20px;
            border-radius: 10px;
            text-align: center;
        }
        .custom-dropdown {
    width: 80% !important;
    margin-bottom: 15px; /* Add space below the dropdown */
}

/* Ensure dropdown is displayed properly in all screen sizes */
.form-group-sm {
    padding: 5px 10px;
}

/* Style the labels and inputs to be aligned nicely */
/*label {
    font-size: 14px;
    font-weight: bold;
}*/

/* Improve spacing on smaller screens */
@media (max-width: 768px) {
    .custom-dropdown {
        width: 100% !important; /* Full width on smaller screens */
    }
}
    </style>

    <script type="text/javascript" src="/JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var dynamicLabel = '<%= DynamicLabelLiteral.Text %>';
            $('#btnGo').click(function () {
                  var SF = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SF == "---Select---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); $('#loader').hide(); return false; }
                var Name = $('#<%=ddlTerritory.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select " + dynamicLabel + ""); $('#ddlTerritory').focus(); $('#loader').hide(); return false; }
                  var Dr = $('#<%=ddlDr.ClientID%> :selected').text();
                if (Dr == "---Select---") { alert("Select Doctor Name"); $('#ddlDr').focus(); $('#loader').hide(); return false; }
            });
        });
    </script>
    <script>
        function scrollToTop() {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
</script>
    <script>
        function myFunction() {
            var x = document.getElementById("myLinks");
            if (x.style.display === "block") {
                x.style.display = "none";
            } else {
                x.style.display = "block";
            }
        }
        function closeNavbar() {
            document.getElementById("myLinks").style.display = "none";
        }
        function updateNetworkStatus() {
            if (navigator.onLine) {
                var alrt = 'You are back Online..'; showAndHideAlertBox_On(alrt); return false;
            } else {
                var alrt = 'Check Network Connection..'; showAndHideAlertBox(alrt); return false;
            }
        }
        function updateNetworkStatusev() {
            if (navigator.onLine) {
            } else {
                var alrt = 'Check Network Connection..'; showAndHideAlertBox(alrt); return;
            }
        }
        window.addEventListener('online', updateNetworkStatus);
        window.addEventListener('offline', updateNetworkStatus);
        //document.addEventListener('click', updateNetworkStatusev);
        //document.addEventListener('keydown', updateNetworkStatusev);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            
        <asp:UpdatePanel ID="upAdmDashboard" runat="server">

            <ContentTemplate>
                  <div id="alertbox" class="loader-wrapper" style="display: none;">
                                            <div class="loaders d-block d-sm-none" style="background-color: darkgoldenrod; top: 90%; padding: 5px 12px 5px 14px;">
                                                <label style="font-weight: bolder;" class="alertmsg"></label>
                                            </div>
                                            <div class="loaders d-none d-sm-block" style="background-color: darkgoldenrod; top: 90%; padding: 5px 12px 5px 14px;">
                                                <label style="font-weight: bolder;" class="alertmsg"></label>
                                            </div>
                                        </div>
                <nav class="navbar navbar-default">
                 
                <div class="container" style="margin-top: 12px;">
    <div class="row">
        <div class="col-12">
            <div class="row">
                <!-- FieldForce Name Dropdown -->
                <div class="col-12 col-md-4 mb-3">
                    <div class="form-group form-group-sm">
                        <asp:Label ID="Label2" runat="server" Text="FieldForce Name:"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker form-control custom-dropdown" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Cluster Dropdown -->
                <div class="col-12 col-md-4 mb-3">
                    <div class="form-group form-group-sm">
                       <%-- <div class="row">
                            <div class="col-4 col-md-3">--%>
                                <asp:Literal ID="DynamicLabelLiteral" runat="server" Visible="false"></asp:Literal>
                                <asp:Label ID="lblCluster" runat="server" Text="Cluster:"></asp:Label>
                           <%-- </div>
                            <div class="col-8 col-md-9">--%>
                                <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="selectpicker form-control custom-dropdown" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged" AutoPostBack="true" data-live-search="true">
                                </asp:DropDownList>
                         <%--   </div>
                        </div>--%>
                    </div>
                </div>

                <!-- Doctor Name Dropdown -->
                <div class="col-12 col-md-4 mb-3">
                    <div class="form-group form-group-sm">
                       <%-- <div class="row">
                            <div class="col-4 col-md-3">--%>
                                <asp:Label ID="Label3" runat="server" Text="Doctor Name:"></asp:Label>
                         <%--   </div>
                            <div class="col-8 col-md-9">--%>
                                <asp:DropDownList ID="ddlDr" runat="server" CssClass="selectpicker form-control custom-dropdown" OnSelectedIndexChanged="ddlDr_SelectedIndexChanged" AutoPostBack="true" data-live-search="true">
                                </asp:DropDownList>
                           <%-- </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <div class="row">
                                    <div class="form-group form-group-sm">
                                        <asp:Label ID="lblMsgValidation" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="form-group form-group-sm">
                                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="btn2 btn-primary" Text="Go"
                                            OnClientClick="showLoader('Search1')" />
                                        <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" CssClass="btn2 btn-primary" Text="Back"
                                            OnClientClick="showLoader('Search1')" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </nav>

                <div id="main" class="container" runat="server" visible="false">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <%--   <div id="navbar">
                                        <ul class="nav navbar-nav navbar-right">
                                            <li>
                                                <li></li>
                                            </li>
                                        </ul>
                                    </div>--%>
                                    <%--<center><h4 style="font-family:Verdana;color:#0069d9;font-weight:bold"><u>Pre Call Analysis</u></h4><center>--%>
                                    <div class="topnav">
                                        <a href="#home" class="active">Pre Call Analysis</a>
                                        <div id="myLinks">
                                            <a href="#dvp" onclick="closeNavbar()">Personal Information</a>
                                            <a href="#dvact" onclick="closeNavbar()">Activities Involved</a>
                                            <a href="#dvvisit" onclick="closeNavbar()">Visit Details - Datewise</a>
                                            <a href="#dvsample" onclick="closeNavbar()">Product Detailed / Sampled</a>
                                            <a href="#dvinput" onclick="closeNavbar()">Input Given</a>
                                              <a href="#dvLvisit" onclick="closeNavbar()">Last 5 Visit Details</a>
                                              <a href="#dvjoint" onclick="closeNavbar()">Joint Work Details</a>
                                            <a href="#dvrem" onclick="closeNavbar()">Listed Dr-wise Remarks/Feedback</a>
                                            <a href="#dvproduct" onclick="closeNavbar()">Product Priority Visit</a>
                                            <a href="#dvbus" onclick="closeNavbar()">Productwise - Business Details</a>
                                               <a href="#dvrcpa" onclick="closeNavbar()">RCPA Information</a>
                                              <a href="#dvrx" onclick="closeNavbar()">Productwise - POB/Rx Details</a>
                                            <a href="#dvchem" onclick="closeNavbar()">Tagged - Chemist Visits</a>                                          
                                            <a href="#dvcrm" onclick="closeNavbar()">CRM Details</a>
                                            <a href="#dvevent" onclick="closeNavbar()">Event Captures</a>
                                          
                                        </div>
                                        <a href="javascript:void(0);" class="icon" onclick="myFunction()">
                                            <i class="fa fa-bars"></i>
                                        </a>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="row">
                                                <div class="container-fluid" runat="server" id="dvp">
                                                    <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Personal Information</h5>

                                                    <table class="table" id="tblMsgInfo" runat="server" border="1" visible="false">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblvisit" ForeColor="#0069d9" Font-Names="verdana" Font-Size="12px" Font-Bold="true" runat="server">Last Visit Date</asp:Label>



                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblvisitD" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDesign" ForeColor="#0069d9" Font-Names="verdana" Font-Size="12px" Font-Bold="true" runat="server">Address</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbladdr" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEmpCode" ForeColor="#0069d9" Font-Names="verdana" Font-Size="12px" Font-Bold="true" runat="server">Mobile No</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblmob" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblh" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px" runat="server">Hospital Name / Address</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblhosp" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                          
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblWorkType" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Email ID</asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblemail" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Reg No.</asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblreg" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">DOB</asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldob" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">DOW</asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldow" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Speciality</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblspec" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Qualification</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblqua" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label18" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Category</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblcat" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label20" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Class</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblcls" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" ForeColor="#0069d9" Font-Bold="true" Font-Names="verdana" Font-Size="12px">Pincode</asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblpin" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                    <div runat="server" id="dvact">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Activities Involved</h5>

                                                        <table class="table" id="tblact" runat="server" border="1">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" ForeColor="#0069d9" Font-Names="verdana" Font-Size="12px" Font-Bold="true" runat="server">Campaign Name</asp:Label>



                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcamp" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" ForeColor="#0069d9" Font-Names="verdana" Font-Size="12px" Font-Bold="true" runat="server">Core Drs</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcore" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" ForeColor="#0069d9" Font-Names="verdana" Font-Size="12px" Font-Bold="true" runat="server">CRM Drs</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcrm" runat="server" Font-Names="verdana" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>


                                                    </div>

                                                    <div runat="server" id="dvvisit">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Visit Details - Datewise</h5>


                                                        <asp:Literal ID="lcvisit" runat="server"></asp:Literal>


                                                    </div>

                                                    <div id="dvsample" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Product Detailed / Sampled</h5>


                                                        <asp:Literal ID="ltsample" runat="server"></asp:Literal>


                                                    </div>
                                                    <div id="dvinput" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Input Given</h5>


                                                        <asp:Literal ID="ltgift" runat="server"></asp:Literal>


                                                    </div>
                                                      <div id="dvLvisit" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Last 5 Visit Details</h5>
                                                        <asp:Literal ID="ltLastvisit" runat="server"></asp:Literal>
                                                    </div>
                                                    
                                                    <div id="dvjoint" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Joint Work Details</h5>


                                                        <asp:Literal ID="ltjoint" runat="server"></asp:Literal>

                                                    </div>
                                                    <div id="dvrem" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Listed Dr-wise Remarks/Feedback</h5>


                                                        <asp:Literal ID="ltfeed" runat="server"></asp:Literal>

                                                    </div>
                                                    <div id="dvproduct" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Product Priority Visit</h5>


                                                        <asp:Literal ID="ltpvist" runat="server"></asp:Literal>
                                                       
                                                    </div>

                                                    <div id="dvbus" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Productwise - Business Details</h5>


                                                        <asp:Literal ID="ltbus" runat="server"></asp:Literal>

                                                    </div>
                                                       <div id="dvrcpa" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">RCPA Information</h5>

                                                        <asp:Literal ID="ltRCPA" runat="server"></asp:Literal>

                                                    </div>
                                                      <div id="dvrx" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Productwise - POB/Rx Details
                                                        </h5>


                                                        <asp:Literal ID="ltrx" runat="server"></asp:Literal>

                                                    </div>
                                                    <div id="dvchem" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Tagged - Chemist Visits</h5>


                                                        <asp:Literal ID="ltsChem" runat="server"></asp:Literal>

                                                    </div>



                                                  
                                                  
                                                     <div id="dvcrm" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">CRM Details</h5>


                                                        <asp:Literal ID="ltCRM" runat="server"></asp:Literal>

                                                    </div>

                                                    <div id="dvevent" runat="server">
                                                        <h5 style="background-color: #0069d9; color: white; font-family: Verdana; padding: 3px; font-weight: bold">Event Captures</h5>


                                                        <asp:Literal ID="ltevent" runat="server"></asp:Literal>

                                                    </div>

                                                  
                                                    <center>
                                                       <asp:Button ID="btntop" runat="server" Text="Go to Top" OnClientClick="scrollToTop(); return false;" 
                                                       CssClass="btn2 btn-primary" />
                                                    </center>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ddlFieldForce" />
                <%--  <asp:PostBackTrigger ControlID="ddlMonth" />
                <asp:PostBackTrigger ControlID="ddlYear" />>--%>
                <asp:PostBackTrigger ControlID="ddlTerritory" />
                  <asp:PostBackTrigger ControlID="ddldr" />
                <asp:PostBackTrigger ControlID="btnGo" />
                <asp:PostBackTrigger ControlID="btnback" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"
        integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/i18n/defaults-*.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script src="../css/pace/pace.js"></script>
    <script>
        $(document).ready(function () {
            $('.selectpicker').selectpicker();

            $('#tbSSBFS').DataTable({
                dom: 'Bfrtip',
                paging: false,
                searching: false,
                ordering: false,
                processing: true,
                info: false,
                buttons: []
            });
        });

        // Tooltips Initialization
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>
    <script>
        function showLoader(loaderType) {
            if (navigator.onLine) {
                if (loaderType == "Search1") {
                    document.getElementById("loader").style.display = '';
                    $('html').block({
                        message: $('#loader'),
                        centerX: true,
                        centerY: true
                    });
                }
            }
            else {
                var alrt = 'Check Network Connection..'; showAndHideAlertBox(alrt); return false;
            }
        }
    </script>
    <script type="text/javascript">
        var popUpObj;
        debugger
        function showMissedDR(sfcode, fmon, fyr, cmode, vmode, cnt, div_code) {
            popUpObj = window.open("/MasterFiles/AnalysisReports/MissedDocList.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&cMode=" + cmode + "&vMode=" + vmode + "&cnt=" + cnt + "&div_code=" + div_code,
                "_blank",
                "ModalPopUp," +
                "0," +
                "toolbar=no," +
                "scrollbars=yes," +
                "location=no," +
                "statusbar=no," +
                "menubar=no," +
                "addressbar=no," +
                "resizable=yes," +
                "width=800," +
                "height=600," +
                "left = 0," +
                "top = 0"
            );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"
                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }
        function showAndHideAlertBox_On(alrt) {
            const alertBox = document.getElementById('alertbox');
            $('.alertmsg').text(alrt);

            alertBox.style.display = 'none';
            //setTimeout(() => {
            //    alertBox.style.display = 'none';
            //}, 2000);
        }
     
        function showAndHideAlertBox(alrt) {
            const alertBox = document.getElementById('alertbox');
            $('.alertmsg').text(alrt);

            alertBox.style.display = 'block';
            //setTimeout(() => {
            //    alertBox.style.display = 'none';
            //}, 2000);
        }
    </script>
</body>
<div class="container">
    <div class="row">
        <div id="loader" style="display: none;">
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="dot">
            </div>
            <div class="lading">
            </div>
        </div>
    </div>
</div>

</html>
