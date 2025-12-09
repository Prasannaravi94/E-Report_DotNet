<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCallAverage_Halfday.aspx.cs" Inherits="MasterFiles_Reports_rptCallAverage_Halfday" %>

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
     <%-- <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
<%--    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
 <%--   <link rel="stylesheet" href="../../assets/css/style.css" />--%>
   <%-- <link rel="stylesheet" href="../../assets/css/responsive.css" />--%>
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

<%--    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>--%>





    <%--  <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
<%--     <style type="text/css">
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <%-- <div>
      <ucl2:Menu2 ID="menu1" runat="server" />
    </div>--%>
        <div>
            <%--<asp:HyperLink Target="_parent"--%>

            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </div>
         <div align="center">
            <asp:Label ID="lblHead" Text="Call Average Sheet" SkinID="lblMand" Font-Underline="true"
                runat="server"></asp:Label>
        </div>



<%--        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-12">--%>
                    <asp:Panel ID="pnlContents" runat="server" Width="99%">
                        <div>
                        
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
                            </div>
                            <br />
<%--                            <div class="display-callAvgreporttable clearfix">
                                <div class="table-responsive" style="overflow:inherit; scrollbar-width: thin;">--%>
                                    <table width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <asp:Table ID="tbl" runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
<%--                                </div>
                            </div>--%>
                        </div>
                    </asp:Panel>
  <%--              </div>
            </div>
        </div>--%>
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