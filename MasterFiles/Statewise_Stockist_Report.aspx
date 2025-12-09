<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statewise_Stockist_Report.aspx.cs"
    Inherits="MasterFiles_Statewise_Stockist_Report" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Statewise Stockist List</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <%--<link href="../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet" type="text/css" />--%>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="../JScript/DateJs/dist/jquery-clockpicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../JScript/DateJs/assets/css/github.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(Month, Year, State) {
            popUpObj = window.open("Statewise_StockistList_MR_Rpt.aspx?Month=" + Month + "&Year=" + Year + "&State=" + State,
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

                // var ImgSrc = "https://s27.postimg.org/b3g2np6oz/loading_23_ook.gif";

                // var ImgSrc = "https://s18.postimg.org/4p9hb6cix/loading_9_k.gif";

                var ImgSrc = "../Images/ICP/loading_9_k.gif"

                var Text = "http://s9.postimg.org/hyt713i5b/Text_Purple.gif";

                $(popUpObj.document.body).append('<div><center><img src="' + Text + '"  alt="" /></center></div><div> <img src="' + ImgSrc + '"  style=" width:150px; height:150px;position: fixed;top: 15%;left:35%;"  alt="" /></div>');

            });
        }

        function Show(State_Name) {

            alert(State_Name);

        }

    </script>

  <%--  <link href="../JScript/Service_CRM/Crm_Dr_Css_Ob/Statewise_Stockist_RptCss.css" rel="stylesheet" />--%>
    <script src="../JScript/Service_CRM/Stockist_JS/Statewise_Stockist_Rpt_JS.js" type="text/javascript"></script>



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
                        <h2 class="text-center" style="border-bottom: none">Statewise Stockist List</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <span id="lblMonth" class="label">Month <span style="color: Red">*</span></span>
                                <span class="fancyArrow"></span>
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
                            <div class="single-des clearfix">
                                <span id="lblYear" class="label">Year <span style="color: Red">*</span></span>
                                <span class="fancyArrow"></span>
                                <select id="ddlYear" class="custom-select2 nice-select" style="width:100%">
                                    <option value="0">--Select--</option>
                                </select>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <input type="button" id="btnGo" value="Go" class="savebutton" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="display-table clearfix">
                    <div class="table-responsive" style="scrollbar-width: thin;">
                        <center>
                            <div>
                                <table id="tblStockist" class="table" style="width: 80%">
                                </table>
                            </div>
                            <div id="divTot">
                                <%-- <span>Grand Total :</span>--%>
                            </div>
                        </center>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
