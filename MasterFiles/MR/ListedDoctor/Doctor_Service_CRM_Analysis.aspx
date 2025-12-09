<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Analysis.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Analysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRM Analysis</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
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
    <script src="../../../JScript/Service_CRM/Crm_Dr_JS/DoctorService_CRM_Analysis_JS.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Sf_Name, fmon, fyr, tmon, tyr, DrType, Mode_Type, ProductType) {

            popUpObj = window.open("rptDrService_CRM_Hospitalwise.aspx?sfcode=" + sfcode + "&Sf_Name=" + Sf_Name + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&TypeVal=" + DrType + "&Mode_Type=" + Mode_Type + "&ProductType=" + ProductType,
                                    "ModalPopUp"//,
                                    //"toolbar=no," +
                                    //"scrollbars=yes," +
                                    //"location=no," +
                                    //"statusbar=no," +
                                    //"menubar=no," +
                                    //"addressbar=no," +
                                    //"resizable=yes," +
                                    ////"width=800," +
                                    ////"height=600," +
                                    //"left = 0," +
                                    //"top=0"
                                    );
            popUpObj.focus();

            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s2.postimg.org/l99kqyrk9/loading_9_k.gif";
                //var ImgSrc = "https://s14.postimg.org/z7zlmgvn5/loading_28_ook.gif";
                // var Text = "http://s9.postimg.org/hyt713i5b/Text_Purple.gif";

                var Text = "";
                $(popUpObj.document.body).append('<div><center><img src="' + Text + '"  alt="" /></center></div><div> <img src="' + ImgSrc + '"  style=" width:150px; height:150px;position: fixed;top: 15%;left:35%;"  alt="" /></div>');

            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnGo").click(function () {

                var Mode_Type = $("#ddlMode").val();
                var ddlFName = $("#ddlFieldForce").val();
                var FieldForce = $("#ddlFieldForce option:selected").text();
                var Year1 = $("#ddlYear").val();
                var Month1 = $("#ddlMonth").val();
                var Year2 = $("#ddlTYear").val();
                var Month2 = $("#ddlTMonth").val();
                // var ProductType = $("#ddlPrdModeType").val();

                var ProductType = "";

                var TypeVal = "";

                //                if (Mode_Type == "1") {
                //                    TypeVal = $("#ddlHospital").val();
                //                    showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2, TypeVal, Mode_Type, ProductType);
                //                }
                //                else 
                if (Mode_Type == "2") {
                    TypeVal = $("#ddlProduct").val();
                    var ProductType = $("#ddlPrdModeType").val();
                    showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2, TypeVal, Mode_Type, ProductType);
                }
                else if (Mode_Type == "3") {
                    TypeVal = $("#ddlDoctors").val();
                    showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2, TypeVal, Mode_Type, ProductType);
                }
                else if (Mode_Type == "4") {
                    TypeVal = $("#ddlChemist").val();
                    ProductType = $("#ddlPrdModeType").val();
                    showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2, TypeVal, Mode_Type, ProductType);
                }

            });
        });
    </script>

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
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" style="border-bottom: none">CRM Analysis</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <span id="lblMode" class="label">Mode<span style="color: Red; padding-left: 3px">*</span></span>
                                <select id="ddlMode" class="nice-select">
                                    <%-- <option value="0">--Select--</option>--%>
                                    <%--  <option value="1">Hospital</option>--%>
                                    <option value="2">Product</option>
                                    <option value="3">Doctors</option>
                                    <option value="4">Chemist/Pharmacy</option>
                                </select>
                            </div>

                            <div class="single-des clearfix">
                                <span id="lblFieldForceName" class="label">Field Force Name <span style="color: Red; padding-left: 2px">*</span></span>
                                <select id="ddlFieldForce" class="custom-select2 nice-select" style="width: 100%">
                                    <option value="0">--Select--</option>
                                </select>
                            </div>

                            <div class="single-des clearfix">
                                <span id="lblType" class="label">Mode Type<span style="color: Red; padding-left: 3px">*</span></span>
                                <div id="PrdModeTypeDD">
                                    <select id="ddlPrdModeType" class="custom-select2 nice-select" style="width: 100%">
                                        <%-- <option value="0">--Select--</option>--%>
                                        <option value="1">Doctors</option>
                                        <option value="2">Chemists</option>
                                    </select>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <%--<span id="lblHospital" class="lblText" style="width: auto; height: 19px"><span style="color: Red">
                                     *</span>Hospital </span>--%><span id="lblProduct" class="label">Product <span style="color: Red; padding-left: 3px">*</span></span>
                                <span id="lblDoctors" class="label">Doctors <span style="color: Red; padding-left: 3px">*</span></span>
                                <span id="lblChemist" class="label">Chemist <span style="color: Red; padding-left: 3px">*</span></span>

                                <%-- <select id="ddlHospital" style="width: auto; font-size: 11px">
                                    <option value="0">--Select--</option>
                                     </select>--%>
                                <div id="ProductDD">
                                    <select id="ddlProduct" class="custom-select2 nice-select" style="width: 100%">
                                        <option value="0">--Select--</option>
                                    </select>
                                </div>
                                <div id="DoctorDD">
                                    <select id="ddlDoctors" class="custom-select2 nice-select" style="width: 100%">
                                        <option value="0">--Select--</option>
                                    </select>
                                </div>
                                <div id="ChemistDD">
                                    <select id="ddlChemist" class="custom-select2 nice-select" style="width: 100%">
                                        <option value="0">--Select--</option>
                                    </select>
                                </div>


                                <div style="float: left">
                                    <img src="../../../Images/ajax_loadinf_3.gif" id="divLoad" style="display: none" />
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
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
                                        </span>
                                        <select id="ddlYear" class="custom-select2 nice-select" style="width: 100%">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="single-des clearfix">
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
                                        </span>
                                        <select id="ddlTYear" class="custom-select2 nice-select" style="width: 100%">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <input type="button" id="btnGo" value="Go" class="savebutton" />
                            <input type="button" id="btnClear" value="Clear" class="resetbutton" />
                        </div>

                    </div>
                </div>
            </div>

        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
