<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Primary_Sales_Entry.aspx.cs" EnableEventValidation="false"
    Inherits="MasterFiles_MR_Primary_Primary_Sales_Entry" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stockist-wise Primary Sale Entry</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <link href="../../../assets/css/style.css" rel="stylesheet" />
    <link id="cssMenu" runat="server" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/fixedheader/3.1.6/css/fixedHeader.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/scroller/2.0.1/css/scroller.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.9/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <link href="../../../css/Primary/PrimarySalesEntry.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.4.1.min.js" integrity="sha256-CSXorXvZcTkaix6Yvo6HppcZGetbYMGWSFlBw8HfCJo=" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="PS_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="PS_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="PS_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <input id="PS_CalRate" type="hidden" runat="server" />
        <div id="Divid" runat="server">
        </div>

        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center" id="hHeading" runat="server"></h2>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFieldForceName" runat="server" Width="100%" CssClass="label">FieldForce Name <span style="color:red">*</span></asp:Label>
                            <asp:DropDownList ID="ddlFieldForce" Width="100%" runat="server" CssClass="selectpicker" data-live-search="true">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblStockist" runat="server" Width="100%" CssClass="label">Stockist Name <span style="color:red">*</span></asp:Label>
                            <asp:DropDownList ID="ddlStockiest" runat="server" Width="100%" CssClass="selectpicker" data-live-search="true">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblMonthYear" CssClass="label" runat="server">Month & Year<span style="color:red"> *</span> </asp:Label>
                            <div class="input-group">
                                <div class="input-group-append" data-date-format="mm-yyyy">
                                    <asp:TextBox ID="txtDate" runat="server" name="txtDate" ReadOnly="true" Width="100%" CssClass="form-control date"></asp:TextBox>
                                    <label class="input-group-text btn" for="txtDate"><span class="fa fa-calendar"></span></label>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" />
                            <asp:Button ID="btnGo" runat="server" Text="New" CssClass="btn btn-primary" />
                            <asp:Button ID="btnClear" CssClass="btn btn-primary" runat="server" Text="Clear" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row justify-content-center invoice">
                <div class="col-lg-6">
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblInvoiceNo" CssClass="label" runat="server">Invoice No. <span style="color:red">*</span></asp:Label>
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" autocomplete="off" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="lblInvoiceDate" CssClass="label" runat="server">Invoice Date <span style="color:red">*</span></asp:Label>
                                    <div class="input-group">
                                        <div class="input-group-append" data-date-format="yyyy-mm-dd">
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" name="txtInvoiceDate" ReadOnly="true" Width="100%" CssClass="form-control date1"></asp:TextBox>
                                            <label class="input-group-text btn" for="txtInvoiceDate"><span class="fa fa-calendar"></span></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="single-des clearfix">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblOrderNo" CssClass="label" runat="server">Order No. <span style="color:red">*</span></asp:Label>
                                    <asp:TextBox ID="txtOrderNo" runat="server" autocomplete="off" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="lblOrderDate" CssClass="label" runat="server">Order Date <span style="color:red">*</span></asp:Label>
                                    <div class="input-group">
                                        <div class="input-group-append" data-date-format="yyyy-mm-dd">
                                            <asp:TextBox ID="txtOrderDate" runat="server" name="txtOrderDate" ReadOnly="true" Width="100%" CssClass="form-control date1"></asp:TextBox>
                                            <label class="input-group-text btn" for="txtOrderDate"><span class="fa fa-calendar"></span></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row justify-content-center invoice">
                <div class="col-lg-11">
                    <table id="grdPrimary" class="display" runat="server" cellspacing="0" width="100%" />
                    <br />
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
    <%--<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.bundle.min.js" integrity="sha384-6khuMg9gaYr5AxOqhkVIODVIvm9ynTT5J4V1cfthmT+emCG6yVmEZsRHdxlotUnm" crossorigin="anonymous"></script>--%>
    <script src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/fixedheader/3.1.6/js/dataTables.fixedHeader.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>
    <script src="https://cdn.datatables.net/scroller/2.0.1/js/dataTables.scroller.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.9/dist/js/bootstrap-select.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>
    <script src="../../../JScript/Service_CRM/Primary/PrimarySalesEntry.js"></script>
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
    <!-- Modal -->
    <div class="modal fade" id="SPS_Modal" tabindex="-1" role="dialog" aria-labelledby="SPS_ModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Stockist-wise Primary Sales Edit</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <br />
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    FieldForce:&nbsp;
                                    <asp:Label ID="mdl_lblSf_Name" CssClass="label" runat="server"></asp:Label>
                                </div>
                                <div class="single-des clearfix">
                                    Stockist:&nbsp;
                                    <asp:Label ID="mdl_lblStockist" CssClass="label" runat="server"></asp:Label>
                                </div>
                                <div class="single-des clearfix">
                                    Month & Year:&nbsp;
                                    <asp:Label ID="mdl_MnthYr" CssClass="label" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <table id="tblPrimaryData" class="display" runat="server" width="100%" />
                    <br />
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            $(".nice-select").remove();
        });
    </script>
</body>
</html>
