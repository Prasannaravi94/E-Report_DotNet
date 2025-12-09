<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Call_Adh_Zoom.aspx.cs" Inherits="Call_Adh_Zoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAN eReport</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
    <%--  <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
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

        .selected_row {
            background-color: Gray;
            color: White;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', '');
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
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=GrdFixation] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GrdFixation] tr").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                        $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, sfname, cmode) {
            var randomnumber = Math.floor((Math.random() * 100) + 1);
            popUpObj = window.open("MIS Reports/Visit_Fixation_All_In_One_Report.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&TMonth=" + tmon + "&Tyear=" + tyr + "&sf_name=" + sfname + "&cMode=" + cmode,
    "ModalPopUp" + randomnumber,
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/o3z7tptqr/loading19.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;"></p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:650px; height: 300px;position: fixed;top: 20%;left: 20%;"  alt="" /></div>');
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />

            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>

                                <td style="padding-right: 40px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
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
                        <asp:Panel ID="pnlContents" runat="server">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text=" "
                                    CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="label"
                                    Font-Size="12pt"></asp:Label>
                            </div>
                            <br />
                            <br />

                            <div class="display-Approvaltable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated"
                                        ShowHeader="False" Width="90%" OnRowDataBound="GrdFixation_RowDataBound">

                                        <Columns>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <br />

        </div>
    </form>
</body>
</html>
