<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard_Visit_Monitor.aspx.cs" Inherits="MasterFiles_Dashboard_Visit_Monitor"
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
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group form-group-sm">
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker" data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group form-group-sm">
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
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
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group form-group-sm">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <div class="row">
                                    <div class="form-group form-group-sm">
                                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="btn btn-sm btn-primary" Text="Go"
                                            OnClientClick="showLoader('Search1')" />
                                        <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" CssClass="btn btn-sm btn-primary" Text="Back"
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
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div id="navbar">
                                        <ul class="nav navbar-nav navbar-right">
                                            <li>
                                                <li></li>
                                            </li>
                                        </ul>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <h5>
                                                        <span class="px-4 py-3 white-text z-depth-1-half blue lighten-1" style="border-radius: 5px;">Coverage Analysis</span></h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4">
                                                            <div class="form-group form-group-sm">
                                                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" data-live-search="true"
                                                                    onchange="showLoader('Search1')">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div class="form-group form-group-sm">
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" onchange="showLoader('Search1')">
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
                                                        </div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div class="form-group form-group-sm">
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" onchange="showLoader('Search1')">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
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
                                                                <asp:GridView ID="GrdFixation" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                    OnDataBound="GrdFixation_DataBound" CssClass="table table-bordered table-hover table-condensed"
                                                                    EmptyDataText="No Records Found">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FieldForce">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFieldForce" runat="server" Text='<%#Eval("FieldForce Name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DES">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation Name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TLD">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTLD" runat="server" Text='<%#Eval("Total_Listed_Drs")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DM">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDM" runat="server" Text='<%#Eval("Doctors_Met")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DS">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDS" runat="server" Text='<%#Eval("Doctors_Calls_Seen")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MDC">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMDC" runat="server" Text='<%#Eval("Listed_Drs_Missed")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="NFWD">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNFWD" runat="server" Text='<%#Eval("No_Of_Field_Wrk_Days")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CAV">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCAV" runat="server" Text='<%#Eval("Call_Average")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="COV (%)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCOV" runat="server" Text='<%#Eval("Coverage_Per")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                                        BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                                        VerticalAlign="Middle" />
                                                                </asp:GridView>
                                                                <asp:HiddenField ID="hdnTotal_Listed_Drs" runat="server" />
                                                                <asp:HiddenField ID="hdnDoctors_Met" runat="server" />
                                                                <asp:HiddenField ID="hdnDoctors_Calls_Seen" runat="server" />
                                                                <asp:HiddenField ID="hdnListed_Drs_Missed" runat="server" />
                                                                <asp:HiddenField ID="hdnNo_Of_Field_Wrk_Days" runat="server" />
                                                                <asp:HiddenField ID="hdnCall_Average" runat="server" />
                                                                <asp:HiddenField ID="hdnCoverage_Per" runat="server" />
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
                <asp:PostBackTrigger ControlID="ddlMonth" />
                <asp:PostBackTrigger ControlID="ddlYear" />
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

            $('#<%=GrdFixation.ClientID%>').DataTable({
                //"scrollX": true,
                //"scrollY": "400px",
                //"scrollCollapse": true
                "ordering": false,
                "searching": false,
                "paging": false,
                "info": false,
                "lengthChange": false
            });
            $('#<%=GrdFixation.ClientID%>').append('<caption style="caption-side: bottom"><div class="row"> <div class="col-md-3 col-sm-3">TLD - Total Listed Doctors </div> <div class="col-md-3 col-sm-3">DM - Doctors Met </div> </div> <div class="row"> <div class="col-md-3 col-sm-3">DS - Doctors Seen </div> <div class="col-md-3 col-sm-3">MDC - Missed Doctor Calls </div> </div> <div class="row"> <div class="col-md-3 col-sm-3">NFWD - No.of Field Work Days </div> <div class="col-md-3 col-sm-3">CAV - Call Average </div> </div> <div class="row"> <div class="col-md-3 col-sm-3">COV - Coverage </div> <div class="col-md-3 col-sm-3">DES - Designation</div> </div></caption>');
        });

        // Tooltips Initialization
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
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
