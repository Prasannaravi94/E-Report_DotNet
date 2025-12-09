<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard_TargetvsSales.aspx.cs" Inherits="MasterFiles_Dashboard_TargetvsSales"
    EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <style>
        .ui-datepicker-calendar
        {
            display: none;
        }

        /*body
        {
            padding-top: 100px;
        }

        @media (max-width: 767px)
        {
            body
            {
                padding-top: 260px;
            }
        }*/
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upAdmDashboard" runat="server">
            <ContentTemplate>
                <nav class="navbar navbar-default">
                    <div class="container" style="margin-top: 12px;">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-md-4 col-sm-4">
                                        <div class="form-group form-group-sm">
                                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker" data-live-search="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4">
                                        <div class="form-group form-group-sm">
                                            <div class="row">
                                                <div class="col-md-2 col-sm-2">
                                                    <asp:Label ID="lblFrom" runat="server" Text="From:"></asp:Label>
                                                </div>
                                                <div class="col-md-5 col-sm-5">
                                                    <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="form-control col-md-2 col-sm-2">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-5 col-sm-5">
                                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4">
                                        <div class="form-group form-group-sm">
                                            <div class="row">
                                                <div class="col-md-2 col-sm-2">
                                                    <asp:Label ID="Label1" runat="server" Text="To:"></asp:Label>
                                                </div>
                                                <div class="col-md-5 col-sm-5">
                                                    <asp:DropDownList ID="ddlTMonth" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-5 col-sm-5">
                                                    <asp:DropDownList ID="ddlTYear" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
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
                                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="btn btn-primary" Text="Go"
                                            OnClientClick="showLoader('Search1')" />
                                        <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" CssClass="btn btn-primary" Text="Back"
                                            OnClientClick="showLoader('Search1')" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </nav>
                <div id="main" class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-primary" style="margin-top: 12px;">
                                <div class="panel-body">
                                    <div id="navbar">
                                        <ul class="nav navbar-nav navbar-right">
                                            <li>
                                                <li></li>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="row">
                                        <div class="container-fluid">
                                            <div class="table-responsive">
                                                <div id="highcontainer" style="min-width: 310px; height: 400px; margin: 0 auto">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-primary">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="container-fluid">
                                                            <div class="table-responsive">
                                                                <asp:Repeater runat="server" ID="rowRepeaterP" OnItemDataBound="rowRepeaterP_ItemBound">
                                                                    <HeaderTemplate>
                                                                        <table id="tbSSBFS" class="table table-bordered tbSSBFS" style="width: 100%">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th rowspan="3">
                                                                                        <asp:Label runat="server" ID="lblHSl_No" Text="#" />
                                                                                    </th>
                                                                                    <th rowspan="3">
                                                                                        <asp:Label runat="server" ID="lblFieldforce_Name" Text="Fieldforce" />
                                                                                    </th>
                                                                                    <th rowspan="3">
                                                                                        <asp:Label runat="server" ID="lblDesigantion" Text="DES" />
                                                                                    </th>
                                                                                    <th rowspan="3">
                                                                                        <asp:Label runat="server" ID="lblHQ" Text="HQ" />
                                                                                    </th>
                                                                                    <asp:Repeater runat="server" ID="headerRepeater1">
                                                                                        <ItemTemplate>
                                                                                            <th colspan="6" style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblMonthH" Text='<%# Eval("Period") %>' />
                                                                                            </th>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </tr>
                                                                                <tr>
                                                                                    <asp:Repeater runat="server" ID="headerRepeater2">
                                                                                        <ItemTemplate>
                                                                                            <th colspan="2">
                                                                                                <asp:Label runat="server" ID="lblTarget" Text="Target" />
                                                                                            </th>
                                                                                            <th colspan="2">
                                                                                                <asp:Label runat="server" ID="lblSale" Text="Sale" />
                                                                                            </th>
                                                                                            <th colspan="2">
                                                                                                <asp:Label runat="server" ID="lblAchieve" Text="Achieve (%)" />
                                                                                            </th>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </tr>
                                                                                <tr>
                                                                                    <asp:Repeater runat="server" ID="headerRepeater3">
                                                                                        <ItemTemplate>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblTargetQty" Text="Qty" />
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblTargetVal" Text="Val" />
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblSaleQty" Text="Qty" />
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblSaleVal" Text="Val" />
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lbllblAchieveQty" Text="Qty" />
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lbllblAchieveVal" Text="Val" />
                                                                                            </th>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </tr>
                                                                            </thead>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblSl_No" Text='<%# Container.ItemIndex + 1 %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblsf_Code" Text='<%# Eval("sf_Code") %>' Visible="false" />
                                                                                <asp:Label runat="server" ID="lblSf_Name" Text='<%# Eval("Sf_Name") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblsf_Designation_Short_Name" Text='<%# Eval("sf_Designation_Short_Name") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblsf_hq" Text='<%# Eval("sf_hq") %>' />
                                                                            </td>
                                                                            <asp:Repeater runat="server" ID="columnRepeater">
                                                                                <ItemTemplate>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblSec_Sale_Qty" Text='<%# Eval("Target_Qty") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblSec_Sale_Val" Text='<%# Eval("Target_Val") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblLess_Infilt_Qty" Text='<%# Eval("Sale_Qty") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblLess_Infilt_Val" Text='<%# Eval("Sale_Val") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblAdd_Infilt1_Qty" Text='<%# Eval("Achieve_Qty") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label runat="server" ID="lblAdd_Infilt1_Val" Text='<%# Eval("Achieve_Val") %>' />
                                                                                    </td>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
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
                <asp:PostBackTrigger ControlID="ddlFMonth" />
                <asp:PostBackTrigger ControlID="ddlTMonth" />
                <asp:PostBackTrigger ControlID="ddlFieldForce" />
                <asp:PostBackTrigger ControlID="btnback" />
                <asp:PostBackTrigger ControlID="btnGo" />
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"> </script>

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

            $('#<%=btnGo.ClientID%>').click(function () {
                var FMont = Number($('#<%=ddlFMonth.ClientID%> :selected').val());
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYea = Number($('#<%=ddlFYear.ClientID%> :selected').val());
                var TMont = Number($('#<%=ddlTMonth.ClientID%> :selected').val());
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYea = Number($('#<%=ddlTYear.ClientID%> :selected').val());

                if (FMont > TMont && TYea == FYea) {
                    alert('To Month must be greater than From Month');
                    $('#ddlTMonth').focus(); return false;
                }
                if (FYea > TYea) {
                    alert('To Year must be greater than From Year');
                    $('#ddlTMonth').focus(); return false;
                }
            });
        });

        // Tooltips Initialization
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
        $(function () {
            $('.datepicker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'MM yy',
                onClose: function (dateText, inst) {
                    function isDonePressed() {
                        return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
                    }
                    if (isDonePressed()) {

                        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                        $(this).datepicker('setDate', new Date(year, month, 1));
                        console.log('Done is pressed')
                    }
                }
            });
        });
    </script>
    <script>
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                //$('#loader').show();
                document.getElementById("loader").style.display = '';
                $('html').block({
                    message: $('#loader'),
                    centerX: true,
                    centerY: true
                });
            }
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
