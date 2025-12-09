<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR.aspx.cs" Inherits="MIS_Reports_Rpt_DCR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no" />
    <title>Reoprt | DCR Analysis</title>
    <link rel="icon" href="../../Images/favicon.ico" />
    <link href="../../css/bootstrap-4.3.1-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="//cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/fixedcolumns/3.3.1/css/fixedColumns.dataTables.min.css" rel="stylesheet" />
    <link href="//cdn.datatables.net/responsive/2.2.4/css/responsive.dataTables.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <link href="../../css/Reports/DCR.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <input id="DCR_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="DCR_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="DCR_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <%--<section class="d-flex text-center">--%>
        <div class="container justify-content-center shadow p-3 mb-5 mt-2 bg-white rounded">
            <div class="row align-items-center justify-content-center">
                <div class="col-lg-12 text-center">
                    <h4>DCR Analysis</h4>
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary btn-sm help" data-toggle='modal' data-target='#modelSettings'>Settings &nbsp;<i class="fa fa-cogs fa-lg"></i></button>
                    </div>
                </div>
            </div>
            <div class="row align-items-left justify-content-left">
                <div class="col-lg-3">
                    <div class="designation-area clearfix">
                        <div class="single-des pull-left clearfix">
                            <asp:Label ID="lblFieldForce" CssClass="col-form-label-sm" Text="FieldForce" runat="server" />
                            <asp:DropDownList ID="ddlFieldForce" CssClass="selectpicker" runat="server"
                                data-live-search="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="designation-area clearfix">
                        <div class="single-des pull-left clearfix">
                            <asp:Label ID="lblDate" CssClass="col-form-label-sm" Text="Date Range" runat="server" />
                            <div id="reportrange" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc; width: 100%">
                                <i class="fa fa-calendar"></i>&nbsp;
                                <span id="date"></span>&nbsp;<i class="fa fa-caret-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-1">
                    <div class="designation-area clearfix">
                        <div class="single-des pull-left clearfix">
                            <br />
                            <button id="btnGO" type="button" class="btn btn-primary btn-sm">Go</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row justify-content-center">
                <pre><br /><br /></pre>
                <div class="col-lg-12">
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div id="tblDCR1" class="table-responsive">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="modelSettings" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Report Settings</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlSettings">Saved Settings</label>
                                        <select class="form-control" id="ddlSettings">
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="">&nbsp;</label>
                                        <div class="custom-control custom-checkbox chkDefault">
                                            <input type="checkbox" class="custom-control-input" id="chkDefault" name="chkDefault" />
                                            <label class="custom-control-label" for="chkDefault">Set as Default</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group newSetting">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 Parameters">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="button" id="btnSave" class="btn btn-primary">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
        <%--</section>--%>
        <script src="//code.jquery.com/jquery-3.4.1.js"></script>
        <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script src="../../assets/js/popper.min.js"></script>
        <script src="../../css/bootstrap-4.3.1-dist/js/bootstrap.min.js"></script>
        <script src="//cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>
        <script src="//cdn.datatables.net/fixedcolumns/3.3.1/js/dataTables.fixedColumns.min.js"></script>
        <script src="//cdn.datatables.net/responsive/2.2.4/js/dataTables.responsive.min.js"></script>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>
        <script src="../../JScript/Service_CRM/Reports/DCR.js"></script>
        <div class="container">
            <div class="row">
                <div id="loader" style="display: none">
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
    </form>
</body>
</html>
