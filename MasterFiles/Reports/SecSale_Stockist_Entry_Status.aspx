<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSale_Stockist_Entry_Status.aspx.cs"
    Inherits="MasterFiles_Reports_SecSale_Stockist_Entry_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Sales Entry Status</title>
    <%--   <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <%-- <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>--%>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
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
    <script src="../../JScript/Service_CRM/SecSale/SecSale_Stockist_Entry_StatusJS.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Sf_Name, fmon, fyr, tmon, tyr,IsVacant) {
            // alert(DrType);

            popUpObj = window.open("rptStockist_SecSale_EntryStatus.aspx?sfcode=" + sfcode + "&Sf_Name=" + Sf_Name + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr+"&IsVacant="+IsVacant,
                "ModalPopUp"//,
                //"toolbar=no," +
                //"scrollbars=yes," +
                //"location=no," +
                //"statusbar=no," +
                //"menubar=no," +
                //"addressbar=no," +
                //"resizable=yes," +
                //"width=800," +
                //"height=600," +
                //"left = 0," +
                //"top=0"
            );
            popUpObj.focus();

            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s2.postimg.org/l99kqyrk9/loading_9_k.gif";

                //var ImgSrc = "https://s14.postimg.org/z7zlmgvn5/loading_28_ook.gif";

                // var Text = "http://s9.postimg.org/hyt713i5b/Text_Purple.gif";

                // var ImgSrc = "https://s1.postimg.org/8dj3wli173/Loading_Img.gif";

                var Text = "";

                $(popUpObj.document.body).append('<div><center><img src="' + Text + '"  alt="" /></center></div><div> <img src="' + ImgSrc + '"  style=" width:150px; height:150px;position: fixed;top: 15%;left:35%;"  alt="" /></div>');

            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnGo").click(function () {

                var ddlFName = $("#ddlFieldForce").val();
                var FieldForce = $("#ddlFieldForce option:selected").text();
                <%--var Year1 = $("#ddlYear").val();
                var Month1 = $("#ddlMonth").val();
                var Year2 = $("#ddlTYear").val();
                var Month2 = $("#ddlTMonth").val();--%>

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year2 = ToMonYear[1];
                var IsVacant = $("#ChkVacant").is(':checked') == true ? "0" : "1";
                //showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2);
                showModalPopUp('admin', 'admin', Month1, Year1, Month2, Year2,IsVacant);

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $("input[id$=txtNew]");
            var $ddl = $("select[id$=ddlFieldForce]");
            var $items = $("select[id$=ddlFieldForce] option");

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    });
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("No Items Found");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }
            }
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
            <div id="Divid" runat="server">
            </div>
            <div>
                <%--<ucl:Menu ID="menu1" runat="server" />--%>
                <div>
                    <div class="container home-section-main-body position-relative clearfix">
                        <div class="row justify-content-center">

                            <div class="col-lg-5">
                                <h2 class="text-center" style="border-bottom: none">Secondary Sales Entry Status </h2>
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <div class="single-des-option">
                                            <span id="lblFieldForceName" class="label">Field Force Name <span
                                                style="color: Red; padding-left: 2px">*</span></span>
                                            <input type="text" id="txtNew" class="lblText" style="width: 100px; font-size: 11px; display: none" />

                                            <select id="ddlFieldForce" class="custom-select2 nice-select" style="width: 100%">
                                                <option value="0">--Select--</option>
                                            </select>

                                        </div>
                                    </div>

                                    <div class="single-des clearfix">
                                        <div class="single-des-option">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                                    <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                                    <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
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
                                    <div class="single-des clearfix">
                                        <div class="single-des-option">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:CheckBox ID="ChkVacant" runat="server" Text="With Vacant" />
                                                </div>
                                               
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
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>


    </form>
</body>
</html>
