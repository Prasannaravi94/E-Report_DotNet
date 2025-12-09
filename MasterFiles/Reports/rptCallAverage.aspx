<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCallAverage.aspx.cs" Inherits="Reports_rptCallAverage" %>

<%--<%@ Register Src ="~/UserControl/ExportData.ascx" TagName ="Menu2" TagPrefix="ucl2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Call Average</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>





    <%--  <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Sf_Name, Division_Code, Mode, FrmDate, ToDate) {
            popUpObj = window.open("Rpt_DCR_View.aspx?sf_code=" + sfcode + "&Sf_Name=" + Sf_Name + "&Division_Code=" + Division_Code + "&Mode=" + Mode + "&FrmDate=" + FrmDate + "&ToDate=" + ToDate,
             "_blank",
    "ModalPopUp", +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
      "fullscreen=yes," +
    //"width=800," +
    //"height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            // LoadModalDiv();
        }
    </script>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("pnlContents");
            var printWindow = window.open('', '', 'height=400,width=800');
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
            //$('#btnExcel').click(function () {
            //    var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
            //    location.href = url
            //    return false
            //})
            //$('td:first-child').addClass("firstChild");
            //$(".table-class tr").each(function () {
            //    $(this).find('td:eq(1)').addClass("secondChild");
            //    $(this).find('td:eq(2)').addClass("thirdChild");
            //});
            $('#btnExcel').click(function () {
                //var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                //location.href = url
                var myBlob = new Blob([pnlContents.innerHTML], { type: 'application/vnd.ms-excel' });
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                var a = document.createElement("a");
                document.body.appendChild(a);
                a.href = url;
                a.download = "Drs_Met_Visit_and_Call_Average.xls";
                a.click();
                setTimeout(function () { window.URL.revokeObjectURL(url); }, 0);
                return false
            })
        })
    </script>
     <%--<style type="text/css">
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
        .stickySecondRow {
            position: sticky;
            top: 0;
            z-index: 1;
            background: blue;
            text-align: left;
        }
        .thead tr.first th, thead tr.first td {
          position: sticky;
          top: 0;
          background: #eee;
        }
        .thead tr.second th, thead tr.second td {
          position: sticky;
          top: 17px;
          background: #eee;
        }
        .table tr.first th, table tr.first td {
          position: sticky;
          top: 0;
          background: #eee;
        }
        .table tr.second th, table tr.second td {
          position: sticky;
          top: 17px;
          background: #eee;
        }
    </style>--%>
    <style type="text/css">

        .sticky {
            position: sticky;
            position: -webkit-sticky;
            top: 0px;
            z-index: 0;
            background: inherit;
        }
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .stickySecondRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0px;
            /*top: 50px;*/
            z-index: 0;
            background: inherit;
        }

        .stickyThirdRow {
            position: sticky;
            position: -webkit-sticky;
            top: 40px;
            z-index: 1;
            background: inherit;
        }

        .display-table3rowspan .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-table3rowspan .table tr:nth-child(n+3) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-table3rowspan .table tr:first-child td:nth-child(2) {
             position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 29px;
            z-index: 2;
            background : inherit;
        }

        .display-table3rowspan .table tr:nth-child(n+3) td:nth-child(2) {
           position: -webkit-sticky;
            position: sticky;
            left: 29px;
            z-index: 1;
            background:inherit;
        }

        /*.display-table3rowspan .table tr:first-child td:nth-child(3) {
             position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 171px;
            z-index: 3;
            background : inherit;
        }

        .display-table3rowspan .table tr:nth-child(n+3) td:nth-child(3) {
           position: -webkit-sticky;
            position: sticky;
            left: 171px;
            z-index: 2;
            background:inherit;
        }*/
                
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <%-- <div>
      <ucl2:Menu2 ID="menu1" runat="server" />
    </div>--%>
        <div>
            <%--<asp:HyperLink Target="_parent"--%>

            <asp:Panel ID="pnlbutton" runat="server">
                <br />
				   <div class="row justify-content-center">
                <div class="col-lg-12">
				   <div class="row justify-content-center">
                <div class="col-lg-9">
				    <div align="center">
                                <asp:Label ID="lblHead" Text="Drs Met/Visit and Call Average" CssClass="reportheader"
                                    runat="server"></asp:Label>
                            </div>
				</div>
				<div class="col-lg-3">
                <table width="100%" style="padding-right: 100px;">
                    <tr>
                        <td></td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td style="padding-right: 50px">
                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                        <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnPrint_Click" />--%>
                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                        <%--<asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />--%>
                                    </td>
                                    <td style="padding-right: 15px">
                                        <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <%--<asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnPDF_Click" />--%>
                                    </td>
                                    <td style="padding-right: 100px">
                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClick="btnClose_Click" OnClientClick="RefreshParent();">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                        <%--<asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" OnClick="btnClose_Click" />--%>
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
            </asp:Panel>
        </div>



        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <asp:Panel ID="pnlContents" runat="server">
                        <div>
                        
                            
                                <table width="100%" align="center">
                                    <tr>
                                        <td width="2.5%"></td>
                                        <td align="left" width="45%">
                                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="14px"></asp:Label>
                                            <asp:Label ID="lblRegionName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="14px"></asp:Label>
                                        </td>

                                        <td align="left">
                                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="14px"></asp:Label>
                                            <asp:Label ID="lblMonth" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="14px"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="14px"></asp:Label>
                                            <asp:Label ID="lblYear" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                           
                            <br />
                            <div class="display-table3rowspan clearfix">
                            <div class="display-callAvgreporttable clearfix">
                                 <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <table width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <asp:Table ID="tbl" runat="server" GridLines="None" Width="100%" CssClass="table-class table">
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                                </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
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
