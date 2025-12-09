<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_Slide_Priority.aspx.cs"
    Inherits="MasterFiles_Options_DD_Slide_Priority" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Slide Priority</title>
    <meta charset="utf-8" />
    <link rel="shortcut icon" href="../../Images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.6/css/buttons.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.1/css/responsive.dataTables.css" />
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://www.plupload.com/css/my.css" media="screen" />
    <link type="text/css" rel="stylesheet" href="https://www.plupload.com/css/prettify.css" media="screen" />
    <link type="text/css" rel="stylesheet" href="https://www.plupload.com/css/shCore.css" media="screen" />
    <link type="text/css" rel="stylesheet" href="https://www.plupload.com/css/shCoreEclipse.css" media="screen" />
    <link type="text/css" rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/themes/smoothness/jquery-ui.css" media="screen" />
    <link type="text/css" rel="stylesheet" href="https://www.plupload.com/plupload/js/jquery.plupload.queue/css/jquery.plupload.queue.css" media="screen" />
    <link type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,400,600,700,300|Bree+Serif" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <link href="../../css/EDetailing/DD_Slide_Upload.css" rel="stylesheet" />
    <script>
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="DD_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="DD_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="DD_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <input id="DD_SubDiv" type="hidden" runat="server" />
        <input id="DD_SubDivTxt" type="hidden" runat="server" />
        <input id="DD_Brand" type="hidden" runat="server" />
        <input id="DD_BrandTxt" type="hidden" runat="server" />
        <input id="DD_Product" type="hidden" runat="server" />
        <input id="DD_Spec" type="hidden" runat="server" />
        <input id="DD_Therapy" type="hidden" runat="server" />
        <input id="DD_Mode" type="hidden" runat="server" />
        <input id="DD_ModeTxt" type="hidden" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <br />
                            <div class="row dvSubDiv">
                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                    <asp:Label ID="lblSDivision" Text="Sub-Division :" runat="server" />
                                </div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                                    <asp:Label ID="lblSDivisionVal" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="row dvMode">
                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                    <asp:Label ID="lblSMode" runat="server" ClientIDMode="Static" />
                                </div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                                    <asp:Label ID="lblSModeVal" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="row dvBrand">
                                <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                    <asp:Label ID="lblSBrand" Text="Brand :" runat="server" />
                                </div>
                                <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                                    <asp:Label ID="lblSBrandVal" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3 col-sm-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-3 col-sm-3">
                                    <button class="btn btn-sm btn-primary float Priority" width="100%" title='Priority Update'>
                                        Update&nbsp;<i class="fa fa-check"></i>
                                    </button>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="grdSlide" style="background-color: white;" class="table table-bordered tbSSBFS" runat="server" width="80%" />
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://www.plupload.com/js/shCore.js" charset="UTF-8"></script>
    <script type="text/javascript" src="https://www.plupload.com/js/shBrushPhp.js" charset="UTF-8"></script>
    <script type="text/javascript" src="https://www.plupload.com/js/shBrushjScript.js" charset="UTF-8"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript" src="https://www.plupload.com/plupload/js/plupload.full.min.js" charset="UTF-8"></script>
    <script type="text/javascript" src="https://www.plupload.com/plupload/js/jquery.plupload.queue/jquery.plupload.queue.js" charset="UTF-8"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.1/js/dataTables.responsive.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>
    <script type="text/javascript" src="../../JScript/Service_CRM/EDetailing/DD_Slide_Priority.js"></script>
    <div class="container">
        <div class="row">
            <div id="loader" style="display: none">
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="dot"></div>
                <div class="lading"></div>
            </div>
        </div>
    </div>
</body>
</html>
