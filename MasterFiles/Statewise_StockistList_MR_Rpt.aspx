<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statewise_StockistList_MR_Rpt.aspx.cs"
    Inherits="MasterFiles_Statewise_StockistList_MR_Rpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Statewise - Status</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../../../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />
    <link rel="stylesheet" href="../../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../../../assets/js/jQuery.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../../assets/js/main.js"></script>




    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
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

    <%--<link href="../JScript/Service_CRM/Crm_Dr_Css_Ob/Statewise_StockistList_MR_RptCss.css" rel="stylesheet" />--%>
    <script src="../JScript/Service_CRM/Stockist_JS/Statewise_Stockist_Rpt_MR_ListJs.js" type="text/javascript"></script>


    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
    <style type="text/css">
      

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" id="hdnState" />
              <br />
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
                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
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
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div align="center">
                            <span id="lblStateName" class="reportheader"></span>
                        </div>
                        <br />

                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                              <%--  <div id="pnlContents" style="width: 1200px; margin: 30px auto;">--%>
                                   <asp:Panel ID="pnlContents" runat="server">
                                    <table id="tblStockList" class="table ">
                                        <tr>
                                            <th>#</th>
                                            <th>Stockist Name</th>
                                            <th>HQ Name &nbsp; :<select id="ddlHq"  class="custom-select2 HQCss" style="width:150px" onchange="return myFunction();"></select></th>
                                            <th>MR Name</th>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <table id="tblStockEnter" class="table" style="width: 100%">
                                        <tr>
                                            <th>#</th>
                                            <th>Stockist Name</th>
                                            <th>HQ Name</th>
                                            <th>MR Name</th>
                                            <th>Submited Date</th>
                                        </tr>
                                    </table>
                                       </asp:Panel>
                                <%--</div>--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <div id="divload">
            <img src="../../Images/ICP/Loading_SS_1.gif" style="width: 150px; height: 150px; position: fixed; top: 15%; left: 35%;" alt="" />
        </div>
         <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
