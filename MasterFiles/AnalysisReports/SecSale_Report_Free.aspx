<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSale_Report_Free.aspx.cs" Inherits="MasterFiles_AnalysisReports_SecSale_Report_Free" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sales Entry Status</title>

    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <%-- <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>--%>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>

    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/SS_Report_Table_CSS.css" rel="stylesheet"
        type="text/css" />
    <script src="../../JsFiles/CommonValidation.js" type="text/javascript"></script>
    <%-- <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/Service_CRM_Entry_Css.css" rel="stylesheet"
        type="text/css" />--%>
    <%--   <script src="../../JScript/Service_CRM/SecSale/SecSale_Stockist_Entry_StatusJS.js"
        type="text/javascript"></script>--%>
    <script src="../../JScript/Service_CRM/SecSale/SecSale_Report.js" type="text/javascript"></script>
    <link href="../../assets/css/Calender_CheckBox.css" rel="stylesheet" />

    <style type="text/css">
        .notIE {
            position: relative;
            display: inline-block;
        }

        select {
            display: inline-block;
            height: 28px;
            width: 150px;
            padding: 2px 10px 2px 2px;
            outline: none;
            color: #74646e;
            border: 1px solid #C8BFC4;
            border-radius: 4px;
            box-shadow: inset 1px 1px 2px #ddd8dc;
            background: #fff;
        }

        /* Select arrow styling */
        .notIE .fancyArrow {
            width: 23px;
            height: 28px;
            position: absolute;
            display: inline-block;
            top: 1px;
            right: 3px;
            background: url(../../Images/loading/ArrowIcon3.png) right / 90% no-repeat #fff;
            pointer-events: none;
        }
        /*target Internet Explorer 9 and Internet Explorer 10:*/
        @media screen and (min-width:0\0) {
            .notIE .fancyArrow {
                display: none;
            }
        }

        .lblText {
            display: inline-block;
            height: 19px;
            width: 100px;
            font-size: 11px;
            color: black;
            font-family: Verdana;
        }
    </style>
    <style type="text/css">
        .btn {
            background: #3498db;
            background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
            background-image: -moz-linear-gradient(top, #3498db, #2980b9);
            background-image: -ms-linear-gradient(top, #3498db, #2980b9);
            background-image: -o-linear-gradient(top, #3498db, #2980b9);
            background-image: linear-gradient(to bottom, #3498db, #2980b9); /*  -webkit-border-radius: 28;
            -moz-border-radius: 28;
            border-radius: 28px;*/
            font-family: Verdana;
            color: #ffffff;
            font-size: 13px;
            padding: 3px 6px 3px 6px;
            text-decoration: none;
        }

            .btn:hover {
                background: #3cb0fd; /* background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
            background-image: linear-gradient(to bottom, #3cb0fd, #3498db);*/
                text-decoration: none;
                color: Black;
            }
    </style>
    <%--    <style type="text/css">
        input[type="radio"] {
            width: 15px;
            height: 15px;
            vertical-align: middle;
            position: relative;
            top: -1px;
            *overflow: hidden;
        }

        .Formatrbtn label {
            margin-right: 30px;
        }

        input[type="radio"]:checked:before {
            content: "";
            display: block;
            position: relative;
            top: 4px;
            left: 4px;
            width: 6px;
            height: 6px;
            border-radius: 50%;
            background: #ad094d;
        }

        /* Base for label styling */
        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: absolute;
            left: -9999px;
        }

            [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label {
                position: relative;
                padding-left: 1.95em;
                cursor: pointer;
                vertical-align: top;
                line-height: 20px;
                margin: 2px 0;
                display: block;
            }

                /* checkbox aspect */
                [type="checkbox"]:not(:checked) + label:before, [type="checkbox"]:checked + label:before {
                    content: '';
                    position: absolute;
                    left: 0;
                    top: 0;
                    width: 1.0em;
                    height: 1.0em;
                    border: 2px solid #ccc;
                    background: #fff;
                    border-radius: 4px;
                    box-shadow: inset 0 1px 3px rgba(0,0,0,.1);
                }
                /* checked mark aspect */
                [type="checkbox"]:not(:checked) + label:after, [type="checkbox"]:checked + label:after {
                    content: '✔';
                    position: absolute;
                    top: .1em;
                    left: .3em;
                    font-size: 1.3em;
                    line-height: 0.3; /* color: #09ad7e;*/
                    color: #d02090;
                    transition: all .2s;
                }
                /* checked mark aspect changes */
                [type="checkbox"]:not(:checked) + label:after {
                    opacity: 0;
                    transform: scale(0);
                }

                [type="checkbox"]:checked + label:after {
                    opacity: 1;
                    transform: scale(1);
                }
        /* disabled checkbox */
        [type="checkbox"]:disabled:not(:checked) + label:before, [type="checkbox"]:disabled:checked + label:before {
            box-shadow: none;
            border-color: #bbb;
            background-color: #ddd;
        }

        [type="checkbox"]:disabled:checked + label:after {
            color: #FF5722;
        }

        [type="checkbox"]:disabled + label {
            color: #2fbdf1;
        }
        /* accessibility */
        [type="checkbox"]:checked:focus + label:before, [type="checkbox"]:not(:checked):focus + label:before {
            border: 2px dotted blue;
        }

        /* hover style just for information */
        label:hover:before {
            border: 2px solid #4778d9 !important;
        }

        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }

        .checkboxes label {
            display: block;
            float: left;
        }
    </style>--%>
    <style type="text/css">
        th {
            background-color: #f1f5f8;
            color: #6c757d;
        }

        a {
            font-size: 16px;
        }

        #tblRate, #tblRate td {
            border: none;
        }
    </style>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">

                    <div class="col-lg-5">
                        <h2 class="text-center" style="border-bottom: none">Secondary Sales Entry Status </h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <span id="lblMode" class="label">Mode </span>
                                <select id="ddlMode" class="custom-select2 nice-select">
                                    <option value="0">--Select--</option>
                                    <option value="1">Product - HQ wise(Sale)</option>
                                    <option value="2">Product - HQ wise (Sale & CB)</option>
                                    <option value="3">Free - All Modes</option>
                                    <option value="4">Manager wise - Stock & Sale</option>
                                    <option value="5">Product - State wise(Sale)</option>
                                    <option value="6">Product - Stock and Sale</option>
                                    <option value="7">Brand wise (Sale & CB)</option>
                                    <option value="8">Product - Stockist wise(Sale & CB)</option>
                                    <option value="9">HQ - Stockist wise(Sale & CB)</option>
                                    <option value="10">SubDivision wise(Sale & CB)</option>
                                    <%--<option value="11">Product wise - Stock (All)</option>--%>
                                    <option value="12">Productwise Stock (All Sale & CB)</option>
                                    <option value="13">Product - Stockist (Console)</option>
                                    <option value="14">Stock & Sale (Consolidate)</option>
                                    <option value="15">Sales Comparison</option>
                                </select>
                            </div>
                            <div class="single-des clearfix">

                                <span id="lblFieldForceName" class="label">
                                    <%-- <span style="color: Red">*</span>--%>Field Force Name </span>
                                <%--<input type="text" id="txtNew" class="lblText" style="width: 100px; font-size: 11px" />--%>
                                <select id="ddlFieldForce" class="custom-select2 nice-select" style="width: 100%">
                                    <option value="0">--Select--</option>
                                </select>
                                <div style="float: right">
                                    <img src="../../Images/ajax_loadinf_3.gif" id="divLoad_1" style="display: none" />
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                        <%--<asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                        <input type="text" runat="server" id="txtFromMonthYear"  class="nice-select" ReadOnly="true"></input>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <%--<asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                        <input type="text" id="txtToMonthYear" runat="server" class="nice-select" ReadOnly="true"></input>
                                    </div>
                                    <%--<div class="col-lg-6">
                                        <span id="lblFMonth" class="label">From Month
                                        </span>
                                        <select id="ddlMonth" class="nice-select">
                                            <option value="0">--Select--</option>
                                            <option value="1">Jan</option>
                                            <option value="2">Feb</option>
                                            <option value="3">Mar</option>
                                            <option value="4">Apr</option>
                                            <option value="5">May</option>
                                            <option value="6">Jun</option>
                                            <option value="7">Jul</option>
                                            <option value="8">Aug</option>
                                            <option value="9">Sep</option>
                                            <option value="10">Oct</option>
                                            <option value="11">Nov</option>
                                            <option value="12">Dec</option>
                                        </select>
                                    </div>
                                    <div class="col-lg-6">
                                        <span id="lblFYear" class="label">From Year
                                        </span><br />
                                        <select id="ddlYear" class="custom-select2 nice-select"  style="width:100%">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>--%>
                                </div>
                            </div>
                            <!-- Bootstrap Datepicker -->
                            <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
                            <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
                            <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
                            <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
                            <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $('[id*=txtFromMonthYear]').datepicker({
                                        changeMonth: true,
                                        changeYear: true,
                                        format: "M-yyyy",
                                        viewMode: "months",
                                        minViewMode: "months",
                                        language: "tr"
                                    });

                                    $('[id*=txtToMonthYear]').datepicker({
                                        changeMonth: true,
                                        changeYear: true,
                                        format: "M-yyyy",
                                        viewMode: "months",
                                        minViewMode: "months",
                                        language: "tr"
                                    });
                                });
                            </script>
                            <%--<div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <span id="lblTMonth" class="label">To Month
                                        </span>
                                        <select id="ddlTMonth" class="nice-select">
                                            <option value="0">--Select--</option>
                                            <option value="1">Jan</option>
                                            <option value="2">Feb</option>
                                            <option value="3">Mar</option>
                                            <option value="4">Apr</option>
                                            <option value="5">May</option>
                                            <option value="6">Jun</option>
                                            <option value="7">Jul</option>
                                            <option value="8">Aug</option>
                                            <option value="9">Sep</option>
                                            <option value="10">Oct</option>
                                            <option value="11">Nov</option>
                                            <option value="12">Dec</option>
                                        </select>
                                    </div>
                                    <div class="col-lg-6">
                                        <span id="lblTYear" class="label">To Year
                                        </span><br />
                                        <select id="ddlTYear" class="custom-select2  nice-select"  style="width:100%">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                            </div>--%>



                            <div class="single-des clearfix">
                                <div class="divchk">
                                    <span id="lblSecSaleType" class="label">Sale Type </span>
                                    <div class="checkboxes">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <input type="checkbox" id="chkSale" value="Sale" name="S_Type" />
                                                <label for="chkSale">Sale</label>
                                            </div>
                                            <div class="col-lg-3">
                                                <input type="checkbox" id="chkClose" value="Closing" name="S_Type" />
                                                <label for="chkClose">
                                                    Closing</label>
                                            </div>
                                            <div class="col-lg-3">
                                                <input type="checkbox" id="chkBoth" value="Both" name="S_Type" />
                                                <label for="chkBoth">
                                                    Both</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="divStock">
                                    <span id="lblStockist" class="label">Stockist Name </span>
                                    <select id="ddlStockist" class="custom-select2  nice-select" style="width: 100%">
                                        <option value="0">--Select--</option>
                                    </select>
                                    <div style="float: right">
                                        <img src="../../Images/ajax_loadinf_3.gif" id="divLoad" style="display: none" />
                                    </div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="divSaleF">
                                    <span id="lblModeType" class="label">Sale Type </span>
                                    <div id="divSale" style="width: 100%"></div>


                                    <div class="divAllF" style="padding-top: 10px">
                                        <div id="divAll" style="background-color: white; width: 70%"></div>
                                    </div>
                                </div>
                            </div>



                            <div class="single-des clearfix">
                                <div class="divBrand">
                                    <span id="lblPrdType" class="label">Field Type </span>
                                    <div id="divBrand"></div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="divField">
                                    <div id="div_F" style="background-color: white; width: 50%"></div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="divQty">
                                    <span id="lblVal" class="label">Field Type </span>
                                    <div id="divVal"></div>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="div_MType">
                                    <span id="lblMType" class="label">Type </span>
                                    <select id="ddlM_type" class="nice-select">
                                        <%--  <option value="0">--Select--</option>--%>
                                        <option value="1">State wise</option>
                                        <option value="2">HQ wise</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <div class="single-des clearfix">
                            <div class="divParam">
                                <%-- <div id="divParamAll" style="background-color:white;border:1px solid black;width:60%"></div>--%>
                                <table id="divParamAll" class="table table-bordered ">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <input type="button" id="btnGo" value="Go" class="savebutton" />
                        <input type="button" id="btnClear" value="Clear" class="resetbutton" />
                    </div>

                </div>
            </div>
        </div>


        <div style="float: right; margin-top: 30px">
            <div id="output_Field_plus">
            </div>
            <div id="overlay_Field_plus" class="web_dialog_overlay">
            </div>
            <div id="dialog_Field_plus" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">Rate 
                        </td>
                        <td class="web_dialog_title align_right">
                            <a href="#" id="btnClose_Field_plus">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tblRate" class="table table-bordered table-striped">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>


    </form>
</body>
</html>
