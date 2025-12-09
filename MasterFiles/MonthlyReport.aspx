<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyReport.aspx.cs" Inherits="MasterFiles_MonthlyReport" %>

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
    <script type="text/javascript">
        function showMonthSummaryZoom(sfcode,sf_name, date,div_code,mode) {
            popUpObj = window.open("rptMonthlySummaryZoom.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&date=" + date + "&div_code=" + div_code + "&mode=" + mode, '_self');
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"
                
                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }
    </script>
    <style>
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

        #tblMsgInfo > tbody > tr > td {
            border: 1px solid #aba3a3;

        }
        .table > tbody > tr > th {
        border:1px solid #ddd; 
            background: #3176b1;
            color:white;
        }
        .table > tbody > tr > td {
        border:1px solid #ddd; }
    </style>
    <script type="text/javascript" src="/JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGo').click(function () {
                $('#<%=lblMsgValidation.ClientID%>').text("");

                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "--Select--") { $('#<%=lblMsgValidation.ClientID%>').text("Select Month."); $('#ddlMonth').focus(); $('.blockUI').hide(); $('#loader').hide(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "--Select--") { $('#<%=lblMsgValidation.ClientID%>').text("Select Year."); $('#ddlYear').focus(); $('.blockUI').hide(); $('#loader').hide(); return false; }

            });
        });
    </script>

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
                                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="selectpicker" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4">
                                        <div class="form-group form-group-sm">
                                            <div class="row">
                                                <div class="col-md-2 col-sm-2">
                                                    <asp:Label ID="lblFrom" runat="server" Text="Month:"></asp:Label>
                                                </div>
                                                <div class="col-md-4 col-sm-4">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control col-md-2 col-sm-2">
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
                                                <div class="col-md-2 col-sm-2">
                                                    <asp:Label ID="Label3" runat="server" Text="Year:"></asp:Label>
                                                </div>
                                                <div class="col-md-4 col-sm-4">
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
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
                                        <asp:Label ID="lblMsgValidation" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
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
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div id="navbar">
                                       <label id="lblmsg" runat="server" visible="false" style="font-size:11px; color:red">* Today Submission Report will not display</label>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="row">
                                                <div class="container-fluid">

                                                    <table class="table" id="tblMsgInfo" runat="server" border="1" visible="false">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFFmsg" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblhq" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDesign" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="lblMonYr" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GrdTimeSt" runat="server" AlternatingRowStyle-CssClass="alt"
                                                            AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="GrdTimeSt_RowDataBound"
                                                          Width="100%">

                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="center"  HeaderText="DCR Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblActivity_Date" runat="server" Text='<% #Eval("day") %>' ></asp:Label>
                                                                        <asp:hiddenfield ID="hdnActivity_Date" runat="server" Value='<% #Eval("Activity_Date") %>' ></asp:hiddenfield>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Work Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWorkType_Name" runat="server" Text='<% #Eval("WorkType_Name") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Sub Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubmission_Date" runat="server" Text='<% #Eval("Submission_Date") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Dr Met" HeaderStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="linkDrCnt" runat="server" Text='<%# Eval("DrCnt") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Chem Met" HeaderStyle-Width="30px" >
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="linkChmCnt" runat="server" Text='<%# Eval("ChmCnt") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Stock Met" HeaderStyle-Width="30px" >
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="linkstkCnt" runat="server" Text='<%# Eval("stkCnt") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Unlist Met" HeaderStyle-Width="30px" >
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="linkunliedDrCnt" runat="server" Text='<%# Eval("unliedDrCnt") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Territory Worked">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerritory_Worked" runat="server" Text='<% #Eval("Territory_Worked") %>' Width="250px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
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

            $('#tbSSBFS').DataTable({
                dom: 'Bfrtip',
                paging: false,
                searching: false,
                ordering: false,
                processing: true,
                info: false,
                buttons: []
            });
        });

        // Tooltips Initialization
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    </script>
    <script>
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
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
