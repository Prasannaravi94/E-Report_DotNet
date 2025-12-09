<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_View.aspx.cs" Inherits="Reports_Rpt_DCR_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR View Report</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />


    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <%--  <script type="text/javascript" src="../../JsFiles/common.js"></script>
     <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>--%>
    <script type="text/javascript">
        function PrintGridData() {
            // alert('test');
            var prtGrid = document.getElementById('<%=pnlContents.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=ddlDate]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-size: 11px;
            font-family: Calibri;
            -webkit-appearance: none;
            width: 300px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            width: 70px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            -webkit-appearance: none;
            width: 190px;
            height: 21px;
            font-weight: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        #effect {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }

        .textbox {
            width: 185px;
            height: 14px;
        }

        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }

        .Holiday {
            color: Red;
            font-size: 10pt;
            /*font-family: Calibri;*/
        }

        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#ExportDiv').html())
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

    <style type="text/css">
        body {
            font-family: 'Roboto', sans-serif;
            font-size: 18px;
            font-weight: 500;
            color: #414d55;
        }

        .container {
            width: 100%;
            padding-right: 15px;
            padding-left: 15px;
            margin-right: auto;
            margin-left: auto;
        }

        .display-reporttable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reporttable .table tr td:first-child a {
            font-size: 12px;
            font-weight: 400;
        }

        .display-reporttable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }

        .dropdown-toggle:after {
            content: none;
        }

        .label1 {
            color: #696d6e;
            font-size: 12px;
        }

        .display-reporttable #pnlRCPA .table tr .no-result-area td {
            border: solid 1px #d1e2ea;
            text-align: center;
            padding: 10px;
            color: #696d6e;
            background-color: white;
        }

        .display-reporttable #pnlRCPA .table tr:first-child td:first-child, .display-reporttable #pnlRCPA .table tr:nth-child(2) td:first-child {
            background-color: #F1F5F8;
            color: #636d73;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reporttable .table tr:first-child td {
            padding: 8px 10px;
        }
    </style>
</head>
<body>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <form id="form1" runat="server">
            <div>
                <center>
                    <br />
                    <asp:Panel ID="pnlbutton" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <div class="row justify-content-center">
                                    <div class="col-lg-9">
                                        <asp:Label ID="lblTitle" runat="server" CssClass="reportheader"></asp:Label>
                                        <span style="color: Red"></span>
                                        <br />
                                        <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for " Font-Underline="True"
                                            Font-Size="Small" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <table width="100%">
                                            <tr>
                                                <td></td>
                                                <td align="right">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 30px">
                                                                <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData();">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblPrint" runat="server" Text="Print" Font-Size="14px"></asp:Label>

                                                            </td>
                                                            <td style="padding-right: 15px">
                                                                <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblExcel" runat="server" Text="Excel" Font-Size="14px"></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" OnClick="btnPDF_Click">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblPDF" runat="server" Text="PDF" Font-Size="14px"></asp:Label>

                                                            </td>
                                                            <td style="padding-right: 50px">
                                                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                                    <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblClose" runat="server" Text="Close" Font-Size="14px"></asp:Label>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                    </asp:Panel>
                    <br />

                    <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                        <div class="row justify-content-center">
                            <div class="col-lg-12">

                                <br />
                                <br />

                                <br />
                                <%--<span style="font-family: Verdana">Field Force Name :</span>--%>
                                <asp:Label ID="lblFieldForceName" Font-Bold="true" Visible="false" Font-Names="Verdana" runat="server"></asp:Label>

                                <div class="row justify-content-center">
                                    <%-- <div class="col-lg-12">--%>
                                    <div class="col-lg-2" style="margin-top: 8px;">
                                        <asp:Label ID="lblDate" CssClass="label1" Text=" Select the Date to View: " runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-1">
                                        <%-- <asp:DropDownList ID="ddlDate" runat="server" 
                                            SkinID="ddlRequired">
                                        </asp:DropDownList>--%>
                                        <asp:ListBox ID="ddlDate" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="col-lg-2" style="margin-top: -3px;">
                                        <asp:Button ID="btnSubmit" runat="server" Width="50px" Text="Go"
                                            CssClass="savebutton" OnClick="btnSubmit_Click" />
                                    </div>
                                    <%--</div>--%>
                                </div>
                                <div class="row justify-content-center">
                                    <asp:Label ID="lblView" Text="Select the Date and Click the Go Button" Style="color: Red; padding-top: 20px; font-size: 16px" Font-Bold="true" runat="server"></asp:Label>
                                </div>
                                <br />
                                <br />

                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">

                                        <asp:GridView ID="gvMyDayPlan" runat="server" Width="99%" HorizontalAlign="Center"
                                            CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false" GridLines="None"
                                            CssClass="table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-Width="50"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity_Date" ItemStyle-Width="70" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Pln_Date" runat="server" Text='<%#Eval("Pln_Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time" ItemStyle-Width="70" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Pln_Time" runat="server" Text='<%#Eval("Pln_Time")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cluster name" ItemStyle-Width="120" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtClustername" runat="server" Text='<%#Eval("ClstrName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WorkType Name" ItemStyle-Width="120" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtWorkTypeName" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="stickyFirstRow"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtremarks" runat="server" Text='<%#Eval("remarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />

                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="display-reportMaintable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="gv" runat="server" Width="50%"
                                            CellPadding="2" RowStyle-HorizontalAlign="Center" EmptyDataText="No Data found for View"
                                            CssClass="mGrid" AutoGenerateColumns="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Capture View" HeaderStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image2" runat="server" Width="200" Height="200" ImageUrl='<%#Eval("imgurl")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>
                                </div>


                                <div class="row justify-content-center">
                                    <div class="col-lg-11">
                                        <div class="display-reporttable clearfix">
                                            <div class="table-responsive" style="scrollbar-width: thin;">
                                                <asp:Panel runat="server" ID="pnlRCPA" Width="100%">
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="display-reporttable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                        <div id="ExportDiv" runat="server">
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                </center>
            </div>
			<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
        </form>
    </asp:Panel>
</body>
</html>
