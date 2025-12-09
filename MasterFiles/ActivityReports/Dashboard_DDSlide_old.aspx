<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard_DDSlide.aspx.cs" MasterPageFile="~/Admin.master"
    Inherits="MasterFiles_ActivityReports_Dashboard_DDSlide" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
        integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/css/bootstrap-select.min.css" />
    <link href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap.min.css"
        rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />
    <link href="../css/pace/themes/blue/pace-theme-flash.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        .ui-datepicker-calendar {
            display: none;
        }
    </style>
    <style type="text/css">
        input[type="checkbox"], input[type="radio"] {
            position: absolute;
            right: 9000px;
        }

            /*Radio box*/

            input[type="radio"] + label:before {
                content: "\f10c";
                font-family: "FontAwesome";
                speak: none;
                font-style: normal;
                font-weight: normal;
                font-variant: normal;
                text-transform: none;
                line-height: 1;
                -webkit-font-smoothing: antialiased;
                width: 1em;
                display: inline-block;
                margin-right: 5px;
            }

            input[type="radio"]:checked + label:before {
                content: "\f192";
                color: #8e44ad;
                animation: effect 250ms ease-in;
            }

            input[type="radio"]:disabled + label {
                color: #aaa;
            }

                input[type="radio"]:disabled + label:before {
                    content: "\f111";
                    color: #ccc;
                }

        /*Radio Toggle*/

        .toggle input[type="radio"] + label:before {
            content: "\f204";
            font-family: "FontAwesome";
            speak: none;
            font-style: normal;
            font-weight: normal;
            font-variant: normal;
            text-transform: none;
            line-height: 1;
            -webkit-font-smoothing: antialiased;
            width: 1em;
            display: inline-block;
            margin-right: 10px;
        }

        .toggle input[type="radio"]:checked + label:before {
            content: "\f205";
            color: #16a085;
            animation: effect 250ms ease-in;
        }

        .toggle input[type="radio"]:disabled + label {
            color: #aaa;
        }

            .toggle input[type="radio"]:disabled + label:before {
                content: "\f204";
                color: #ccc;
            }


        @keyframes effect {
            0% {
                transform: scale(0);
            }

            25% {
                transform: scale(1.3);
            }

            75% {
                transform: scale(1.4);
            }

            100% {
                transform: scale(1);
            }
        }

        .loading {
            display: none !important;
        }



        .loading {
            display: none;
        }

        .blockUI.blockMsg.blockElement {
            border: none !important;
        }

        #loader {
            position: fixed;
            top: 50%;
            right: 50%;
            margin: auto;
        }

            #loader .dot {
                bottom: 0;
                height: 100%;
                left: 0;
                margin: auto;
                position: absolute;
                right: 0;
                top: 0;
                width: 87.5px;
            }

                #loader .dot::before {
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

                #loader .dot:nth-child(7n+1) {
                    transform: rotate(45deg);
                }

                    #loader .dot:nth-child(7n+1)::before {
                        animation: 0.8s linear 0.1s normal none infinite running load;
                        background: #00ff80 none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+2) {
                    transform: rotate(90deg);
                }

                    #loader .dot:nth-child(7n+2)::before {
                        animation: 0.8s linear 0.2s normal none infinite running load;
                        background: #00ffea none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+3) {
                    transform: rotate(135deg);
                }

                    #loader .dot:nth-child(7n+3)::before {
                        animation: 0.8s linear 0.3s normal none infinite running load;
                        background: #00aaff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+4) {
                    transform: rotate(180deg);
                }

                    #loader .dot:nth-child(7n+4)::before {
                        animation: 0.8s linear 0.4s normal none infinite running load;
                        background: #0040ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+5) {
                    transform: rotate(225deg);
                }

                    #loader .dot:nth-child(7n+5)::before {
                        animation: 0.8s linear 0.5s normal none infinite running load;
                        background: #2a00ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+6) {
                    transform: rotate(270deg);
                }

                    #loader .dot:nth-child(7n+6)::before {
                        animation: 0.8s linear 0.6s normal none infinite running load;
                        background: #9500ff none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+7) {
                    transform: rotate(315deg);
                }

                    #loader .dot:nth-child(7n+7)::before {
                        animation: 0.8s linear 0.7s normal none infinite running load;
                        background: magenta none repeat scroll 0 0;
                    }

                #loader .dot:nth-child(7n+8) {
                    transform: rotate(360deg);
                }

                    #loader .dot:nth-child(7n+8)::before {
                        animation: 0.8s linear 0.8s normal none infinite running load;
                        background: #ff0095 none repeat scroll 0 0;
                    }

            #loader .lading {
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

        @keyframes load {
            100% {
                opacity: 0;
                transform: scale(1);
            }
        }

        @keyframes load {
            100% {
                opacity: 0;
                transform: scale(1);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="Divid" runat="server"></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upAdmDashboard" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSlide" runat="server" ClientIDMode="Static"></asp:HiddenField>
            <div id="main" class="container home-section-main-body position-relative clearfix">
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
                                                            <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="form-control">
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
                                <div class="row justify-content-center">
                                    <div class="col-sm-12">
                                        <div class="form-group form-group-sm">
                                            <center>
                                                <asp:Label ID="lblBasedOn" runat="server" Text="Based-On:"></asp:Label>
                                                <asp:RadioButtonList ID="rblBasedOn" Style="font-size: 18px;" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0" Text="Product &nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Brand"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                                <div class="row justify-content-center">
                                    <div class="col-sm-12">
                                        <div class="w-100 designation-submit-button text-center clearfix">
                                            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" CssClass="savebutton" Text="Go"
                                                OnClientClick="showLoader('Search1')" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-md-4 col-sm-4">
                                                <div class="form-group form-group-sm">

                                                    <div class="row">
                                                        <br />
                                                        <div class="col-md-12">

                                                            <div class="row">
                                                                <div class="col-md-2 col-sm-2">
                                                                    <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Filter"></asp:Label>
                                                                </div>
                                                                <div class="col-md-4 col-sm-4">
                                                                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                                                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged" CssClass="form-control">
                                                                        <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="Product / Brand" Value="7"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-4 col-sm-4">
                                                                    <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" SkinID="ddlRequired"
                                                                        CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-2 col-sm-2">
                                                                    <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <br />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
                                    <div class="container-fluid">
                                        <div class="table-responsive">
                                            <div id="highcontainer1" style="min-width: 310px; height: 400px; margin: 0 auto">
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
            <asp:PostBackTrigger ControlID="ddlSrch" />
            <asp:PostBackTrigger ControlID="ddlSrc2" />
            <asp:PostBackTrigger ControlID="Btnsrc" />
            <asp:PostBackTrigger ControlID="btnGo" />
        </Triggers>
    </asp:UpdatePanel>
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
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"
        integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4"
        crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/i18n/defaults-*.min.js"></script>
    <script type="text/javascript" type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script type="text/javascript" type="text/javascript" src="https://malsup.github.io/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../css/pace/pace.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.btn.dropdown-toggle.btn-default').hide();

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

            $('#<%# btnGo.ClientID%>').click(function () {
                var FMont = Number($('#<%# ddlFMonth.ClientID%> :selected').val());
                var FMonth = $('#<%# ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
                var FYea = Number($('#<%#ddlFYear.ClientID%> :selected').val());
                var TMont = Number($('#<%# ddlTMonth.ClientID%> :selected').val());
                var TMonth = $('#<%# ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }
                var TYea = Number($('#<%# ddlTYear.ClientID%> :selected').val());

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
    <script type="text/javascript">
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
</asp:Content>
