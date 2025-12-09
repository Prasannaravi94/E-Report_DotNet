<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard_Menu.aspx.cs" Inherits="MasterFiles_Dashboard_Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <title></title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.2.0/css/all.css" integrity="sha384-hWVjflwFxL6sNzntih27bfxkr27PmbbK/iSvJ+a4+0owXq79v+lsFkW54bOGbiDQ" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous" />
    <style type="text/css">
        html
        {
            background: url(../Images/bg.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        body
        {
            background-color: transparent;
            position: static !important;
        }

        /* display this row with flex and use wrap (= respect columns' widths) */

        .row-flex
        {
            display: flex;
            flex-wrap: wrap;
        }


        /* vertical spacing between columns */

        [class*="col-"]
        {
            margin-bottom: 30px;
        }

        .content
        {
            height: 100%;
            padding: 20px 20px 10px;
            color: #fff;
        }


        /* Demo backgrounds and styling*/

        .colour-1
        {
            background: #483C46;
            color: #fff;
            border-radius: 10px;
        }

        .colour-2
        {
            background: #3C6E71;
            border-radius: 10px;
        }

        .colour-3
        {
            background: #70AE6E;
            border-radius: 10px;
        }

        .colour-4
        {
            background: #82204A;
            border-radius: 10px;
        }

        .colour-5
        {
            background: #558C8C;
            border-radius: 10px;
        }

        .colour-6
        {
            background: #917C78;
            border-radius: 10px;
        }  
        .colour-7
        {
            background: #954f76;
            border-radius: 10px;
        }
         .colour-8
        {
            background:#2e2be2;
            border-radius: 10px;
        }
        body
        {
            padding: 20px 0;
            font-family: Roboto, sans-serif;
        }

        .content h3
        {
            margin-top: 0px;
            font-weight: 300;
        }

        h1
        {
            font-weight: 300;
            margin-bottom: 40px;
        }

        .right
        {
            text-align: right;
            float: right;
        }
    </style>
    <style>
        .blockUI.blockMsg.blockElement
        {
            border: none !important;
        }

        #loader
        {
            position: fixed;
            top: 50%;
            right: 50%;
            margin: auto;
        }

            #loader .dot
            {
                bottom: 0;
                height: 100%;
                left: 0;
                margin: auto;
                position: absolute;
                right: 0;
                top: 0;
                width: 87.5px;
            }

                #loader .dot::before
                {
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

                #loader .dot:nth-child(7n+1)
                {
                    transform: rotate(45deg);
                }

                    #loader .dot:nth-child(7n+1)::before
                    {
                        animation: 0.8s linear 0.1s normal none infinite running load;
                        background: #00ff80 none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+2)
                {
                    transform: rotate(90deg);
                }

                    #loader .dot:nth-child(7n+2)::before
                    {
                        animation: 0.8s linear 0.2s normal none infinite running load;
                        background: #00ffea none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+3)
                {
                    transform: rotate(135deg);
                }

                    #loader .dot:nth-child(7n+3)::before
                    {
                        animation: 0.8s linear 0.3s normal none infinite running load;
                        background: #00aaff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+4)
                {
                    transform: rotate(180deg);
                }

                    #loader .dot:nth-child(7n+4)::before
                    {
                        animation: 0.8s linear 0.4s normal none infinite running load;
                        background: #0040ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+5)
                {
                    transform: rotate(225deg);
                }

                    #loader .dot:nth-child(7n+5)::before
                    {
                        animation: 0.8s linear 0.5s normal none infinite running load;
                        background: #2a00ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+6)
                {
                    transform: rotate(270deg);
                }

                    #loader .dot:nth-child(7n+6)::before
                    {
                        animation: 0.8s linear 0.6s normal none infinite running load;
                        background: #9500ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+7)
                {
                    transform: rotate(315deg);
                }

                    #loader .dot:nth-child(7n+7)::before
                    {
                        animation: 0.8s linear 0.7s normal none infinite running load;
                        background: magenta none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+8)
                {
                    transform: rotate(360deg);
                }

                    #loader .dot:nth-child(7n+8)::before
                    {
                        animation: 0.8s linear 0.8s normal none infinite running load;
                        background: #ff0095 none repeat scroll 0 0;
                    }

            #loader .lading
            {
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

        @keyframes load
        {
            100%
            {
                opacity: 0;
                transform: scale(1);
            }
        }

        @keyframes load
        {
            100%
            {
                opacity: 0;
                transform: scale(1);
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="upAdmDashboard" runat="server">
                <ContentTemplate>
                    <div class="container">
                        <h1>Dashboard Menu</h1>
                        <div class="row row-flex">
						 <asp:LinkButton runat="server" ID="lnkMonthSummary" OnClick="lnkbtnMonthSummary_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                               <div class="content colour-2">
                                    <h3>Monthly Summary</h3>
                                    <h3 class="right"><i class="fa fa-plus-square" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnVisitMonitor" OnClick="lnkbtnVisitMonitor_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-1">
                                    <h3>Visit Monitor</h3>
                                    <h3 class="right"><i class="fa fa-binoculars" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnSalesAnalysis" OnClick="lnkbtnSalesAnalysis_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-2">
                                    <h3>Sales Analysis</h3>
                                    <h3 class="right"><i class="fa fa-chart-line" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <%--<asp:LinkButton runat="server" ID="lnkbtnVisitAnalysisMR" OnClick="lnkbtnVisitAnalysisMR_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-3">
                                    <h3>Visit Analysis MR</h3>
                                    <h3 class="right"><i class="fa fa-chart-area" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnVisitAnalysisManager" OnClick="lnkbtnVisitAnalysisManager_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-4">
                                    <h3>Visit Analysis Manager</h3>
                                    <h3 class="right"><i class="fa fa-chart-area" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>--%>
                            <asp:LinkButton runat="server" ID="lnkbtnMissedCallReport" OnClick="lnkbtnMissedCallReport_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-5">
                                    <h3>Missed Call Report</h3>
                                    <h3 class="right"><i class="fa fa-chart-bar" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnProductExposure" OnClick="lnkbtnProductExposure_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-6">
                                    <h3>Product Exposure</h3>
                                    <h3 class="right"><i class="fa fa-chart-pie" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnReviewReport" OnClick="lnkbtnReviewReport_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-1">
                                    <h3>Review Report</h3>
                                    <h3 class="right"><i class="fa fa-sticky-note" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnAssessmentReport" OnClick="lnkbtnAssessmentReport_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-2">
                                    <h3>Field Assessment Report</h3>
                                    <h3 class="right"><i class="fa fa-compass" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnDrAnalysis" OnClick="lnkbtnDrAnalysis_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-3">
                                    <h3>Doctor Analysis</h3>
                                    <h3 class="right"><i class="fa fa-plus-square" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnSampleDespatch" OnClick="lnkbtnSampleDespatch_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-4">
                                    <h3>Sample Summary</h3>
                                    <h3 class="right"><i class="fa fa-file-export" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnInputDespatch" OnClick="lnkbtnInputDespatch_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-5">
                                    <h3>Gift Summary</h3>
                                    <h3 class="right"><i class="fa fa-file-import" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <%--<asp:LinkButton runat="server" ID="lnkbtnCCP" OnClick="lnkbtnCCP_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-6">
                                    <h3>CCP</h3>
                                    <h3 class="right"><i class="fa fa-calendar-alt" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>--%>
                            <%--<asp:LinkButton runat="server" ID="lnkbtnMyMissedCall" OnClick="lnkbtnMyMissedCall_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-1">
                                    <h3>My Missed Call</h3>
                                    <h3 class="right"><i class="fa fa-level-up-alt" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnCCPDaywise" OnClick="lnkbtnCCPDaywise_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-2">
                                    <h3>CCP Daywise</h3>
                                    <h3 class="right"><i class="fa fa-chart-line" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>--%>
                            <asp:LinkButton runat="server" ID="lnkbtnExpense" OnClick="lnkbtnExpense_Click" Visible="false"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-2">
                                    <h3>Expense</h3>
                                    <h3 class="right"><i class="fa fa-dollar-sign" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkbtnProd_price_lst" OnClick="lnkbtnProd_price_lst_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-3">
                                    <h3>Product Information</h3>
                                    <h3 class="right"><i class="fa fa-chart-area" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnlbtnDashboard" OnClick="lnlbtnDashboard_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-4">
                                    <h3>Dashboard</h3>
                                    <h3 class="right"><i class="fa fa-file-export" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="LnkbtnProductMap" OnClick="LnkbtnProductMap_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-5">
                                    <h3>Listed Doctor - Product Tag</h3>
                                    <h3 class="right"><i class="fa fa-file-import" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>


                            <asp:LinkButton runat="server" ID="lnlbtnDashboard_Sale" OnClick="lnlbtnDashboard_Sale_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-6">
                                    <h3>Sale Dashboard</h3>
                                    <h3 class="right"><i class="fa fa-calendar-alt" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton> 
                             <asp:LinkButton runat="server" ID="lnlbtnDashboard_SFE" OnClick="lnlbtnDashboard_SFE_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-1">
                                    <h3>SFE Dashboard</h3>
                                    <h3 class="right"><i class="fa fa-level-up-alt" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                               <asp:LinkButton runat="server" ID="lnkdcr" OnClick="lnkdcr_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-8">
                                    <h3>Daily Work Report Summary</h3>
                                    <h3 class="right"><i class="fa fa-briefcase-medical" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                              <asp:LinkButton runat="server" ID="lblbtnVisit" OnClick="lblbtnVisit_Click"
                                  CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-4">
                                    <h3>Visit Analysis</h3>
                                    <h3 class="right"><i class="fa fa-binoculars" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                              <asp:LinkButton runat="server" ID="lnkpob" OnClick="lnkpob_Click"
                                 CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-2">
                                    <h3>POB wise</h3>
                                    <h3 class="right"><i class="fa fa-chart-area" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnkcomp" OnClick="lnkcomp_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-6">
                                    <h3>Comprehensive Analysis</h3>
                                    <h3 class="right"><i class="fa fa-chart-pie" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnkSales" OnClick="lnkSales_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                 <div class="divClass content colour-1">
                                    <h3>Sales Analysis</h3>
                                    <h3 class="right"><i class="fa fa-dollar-sign" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkprim" OnClick="lnkprim_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-6">
                                    <h3>Primary Sales</h3>
                                    <h3 class="right"><i class="fa fa-chart-line" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnksprim" OnClick="lnksprim_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-7">
                                    <h3>Slide - Primary Sales</h3>
                                    <h3 class="right"><i class="fa fa-money-bill" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                              <asp:LinkButton runat="server" ID="lnkPstock" OnClick="lnkPstock_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-8">
                                    <h3>Primary Bill - Stockistwise</h3>
                                    <h3 class="right"><i class="fa fa-briefcase-medical" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnkPprod" OnClick="lnkPprod_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-3">
                                    <h3>Primary Bill - Productwise</h3>
                                    <h3 class="right"><i class="fa fa-tablet" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnkcat" OnClick="lnkcat_Click"
                                  CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-1">
                                    <h3>Visit Detail - Categorywise</h3>
                                    <h3 class="right"><i class="fa fa-binoculars" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkdate" OnClick="lnkdate_Click"
                                  CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-4">
                                    <h3>Visit Detail - DateWise</h3>
                                    <h3 class="right"><i class="fa fa-calendar" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnkvmode" OnClick="lnkvmode_Click"
                                  CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-3">
                                    <h3>Visit Detail - Modewise</h3>
                                    <h3 class="right"><i class="fa fa-calendar-plus" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkat" OnClick="lnkat_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                 <div class="divClass content colour-1">
                                    <h3>Visit Detail - at a Glance</h3>
                                    <h3 class="right"><i class="fa fa-notes-medical" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="lnkchem" OnClick="lnkchem_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                 <div class="divClass content colour-2">
                                    <h3>Visit Detail - Chemist & Unlst</h3>
                                    <h3 class="right"><i class="fa fa-medkit" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                              <asp:LinkButton runat="server" ID="lnkpay" OnClick="lnkpay_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-7">
                                    <h3>Payslip View</h3>
                                    <h3 class="right"><i class="fa fa-money-check" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnktimeStatus_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="content colour-1">
                                    <h3>Time Status</h3>
                                    <h3 class="right"><i class="fa fa-clock" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>

                             <asp:LinkButton runat="server" ID="LinkButton4"  OnClick="lnkprecallAnalysis_Click" 
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                   <div class="content colour-5">
                                   <h3>Pre Call Analysis</h3>
                                        <h3 class="right"><i class="fa fa-chart-area" aria-hidden="true"></i></h3>
                                 </div>
                            </asp:LinkButton>
                             <asp:LinkButton runat="server" ID="LinkButton5" OnClick="lnkrcpa_Click"
                                CssClass="col-md-4 col-sm-6 col-xs-12" OnClientClick="showLoader('Search1')">
                                <div class="divClass content colour-4">
                                    <h3>RCPA Analysis</h3>
                                    <h3 class="right"><i class="fa fa-file-export" aria-hidden="true"></i></h3>
                                </div>
                            </asp:LinkButton>

                            
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js" integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script type="text/ecmascript">
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loader").style.display = '';
                $('body').block({
                    message: $('#loader'),
                    centerX: true,
                    centerY: true
                });
            }
        }
    </script>
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
</body>
</html>
