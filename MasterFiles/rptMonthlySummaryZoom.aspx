<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMonthlySummaryZoom.aspx.cs" Inherits="MasterFiles_rptMonthlySummaryZoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
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
    <style type="text/css">
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
            border-style: solid;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        #GrdTimeSt > tbody > tr:nth-child(n) {
            top: 1px;
            position: sticky;
        }

        #GrdTimeSt > tbody > tr > th {
            text-decoration: underline;
        }
    </style>
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

        #GrdFixation > tbody > tr:nth-child(n) > td:nth-child(9) {
            width:700px !important;
        }
         #GrdFixation > tbody > tr:nth-child(n) > td:nth-child(10) {
            width:700px !important;
        }
          #GrdFixation > tbody > tr:nth-child(n) > td:nth-child(11) {
            width:700px !important;
        }

        #GrdFixation {
    width: 700% !important;
    max-width: 700% !important;
}
    </style>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript" src="/JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery-1.10.1.js"></script>
    <script async src="//jsfiddle.net/g9eL6768/2/embed/"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />

            <center>

            <table width="100%">
                <tr>
               
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="window.history.go(-1); return false;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <center>
                <asp:Panel ID="pnlContents" runat="server">
                    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text=" "
                            Font-Underline="True" Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                            <br />
                        <asp:Label ID="lblParameterHead" runat="server" Text=" "
                            Font-Underline="True" Font-Bold="True" Font-Names="Verdana" Font-Size="11pt"></asp:Label>
                            <br />
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
                                    <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWorkType" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
          <%--          <div class="col-lg-12">
                    <div class="display-reportMaintable clearfix">--%>
                           
                                <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="table table-bordered table-hover tbSSBFS"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" Font-Bold="true"  OnRowDataBound="GrdFixation_RowDataBound" 
                                    GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated" Width="300%"
                                   >
                                     <HeaderStyle BackColor="#245884" Font-Italic="false" ForeColor="Snow"  />
                                    <Columns>
                                    
                                       
                                        </Columns>
                                </asp:GridView>

                    <asp:GridView ID="grdCheStk" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="table table-bordered table-hover tbSSBFS"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" Font-Bold="true"  OnRowDataBound="GrdFixation_RowDataBound" 
                                    GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated" Width="100%"
                                   >
                                     <HeaderStyle BackColor="#245884" Font-Italic="false" ForeColor="Snow"  />
                                    <Columns>
                                    
                                       
                                        </Columns>
                                </asp:GridView>
                           
                     <%--   </div>
                         </div>--%>
                </asp:Panel>
            </center>
        </center>
        </div>
    </form>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script src="/css/pace/pace.js"></script>
    <script>
        //function showLoader(loaderType) {
        //    if (loaderType == "Search1") {
        //        document.getElementById("loader").style.display = '';
        //        $('html').block({
        //            message: $('#loader'),
        //            centerX: true,
        //            centerY: true
        //        });
        //    }
        //}

        $(document).ready(function () {
            $("th").click(function () {
                document.getElementById("loader").style.display = '';
                $('html').block({
                    message: $('#loader'),
                    centerX: true,
                    centerY: true
                });
            });
        })
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

