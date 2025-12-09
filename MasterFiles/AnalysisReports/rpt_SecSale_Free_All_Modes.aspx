<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_SecSale_Free_All_Modes.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_rpt_SecSale_Free_All_Modes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />

    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->


    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <link href="../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <%--<link href="../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/Stockist_JS/PrintandExcel_JS.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/SecSale/rpt_Product_Free_All_JS.js"></script>
    <style type="text/css">
        .table tr:nth-child(2) th:first-child, .table tr:nth-child(3) th:first-child {
            background-color: #F1F5F8;
            font-size: 14px;
            font-weight: 400;
            color: #636d73;
            border-left: 1px solid #DCE2E8;
        }
         .table tr:nth-child(4) td:first-child
         {
             background-color:#414d55;
             color:#ffffff;
         }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            /*font-size: 10px;
            white-space: pre;*/
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th {
            border: 1px solid #000;
        }

        .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #696969;
        }


        #lblStock {
            /*color: #0e50ed;
            font-weight: bold;
            font-size: 14px; /*font-family:Cambria;*/
            /*font-family: 'Comic Sans MS';*/
        }

        #lblField {
            color: #ff28f6;
            /*font-weight: bold;
            font-size: 13px;
            font-family: Calibri;*/
        }

        .Service {
            font-size: 14px;
            font-family: Calibri;
            color: #d10091;
            font-weight: bold; /*-webkit-text-stroke: 1px #d10091;
            -webkit-text-fill-color: #d10091;
            -webkit-animation: fillser 0.5s infinite alternate;*/
        }
    </style>
    <style type="text/css">
        @media print {
            .noPrnCtrl {
                display: none;
            }
        }
    </style>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', ''); //height = 400, width = 800
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            });
        });
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <input id="SS_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
            <br />
			<div class="row justify-content-center">
                    <div class="col-lg-12">
					<div class="row justify-content-center">
                    <div class="col-lg-9">
					  <center>
                                 <span id="lblStock" class="reportheader"></span>
                                    <br />
                                 <span id="lblField" class="reportheader"></span>
                                     <br />
                                 </center>
					</div>
					<div class="col-lg-3">
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px" ></asp:Label>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>

                                <td style="padding-right: 100px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
			</div>
			</div>
			</div>
            <br />


            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div id="MainDiv">
                              
                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                        <div id="divpnl">
                                        </div>
                                        <br />
                                        <div id="div_DeAct">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="modal">
                            <img src="../../Images/ICP/Loading_SS_1.gif" style="width: 250px; height: 250px; position: fixed; top: 35%; left: 35%;"
                                alt="" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
		<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
</body>
</html>
