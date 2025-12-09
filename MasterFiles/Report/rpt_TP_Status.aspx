<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_TP_Status.aspx.cs" Inherits="MasterFiles_Report_rpt_TP_Status" %>

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
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <style type="text/css">
        .auto-style1 {
            height: 19px;
        }
    </style>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <style type="text/css">
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reportMaintable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <center>
          <br />

            <asp:Panel ID="pnlbutton" runat="server">
                            <div class="row justify-content-center">
                <div class="col-lg-12">

            <div class="row justify-content-center">
                <div class="col-lg-9">
                
                </div>
                <div class="col-lg-3">
                <table width="100%">
                    <tr>
                        <td >
                        </td>
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
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server"  OnClick="btnExcel_Click">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:LinkButton ID="btnPDF" ToolTip="PDF" runat="server" Visible="false" OnClick="btnPDF_Click">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="PDF" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="padding-right: 50px">
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
            </asp:Panel>
           <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="TP Status" CssClass="reportheader"></asp:Label>
                </div>
                  <div class="row" align="left" >
                          <div class="col-lg-8">
                                           <asp:Label ID="lblFieldForce" runat="server" CssClass="reportheader" ForeColor="#696d6e" ></asp:Label>
                                    </div>
                                     <div class="col-lg-2">
                                          <asp:Label ID="lblMonth" runat="server" CssClass="reportheader"  ForeColor="#696d6e"></asp:Label>
                                     </div>
                                     <div class="col-lg-2">
                                         <asp:Label ID="lblYear" runat="server" CssClass="reportheader" ForeColor="#696d6e"></asp:Label>
                                     </div>
                                </div>  
                <br />

                  <div class="display-reportMaintable clearfix">
                                   <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">

                                <asp:GridView ID="grdtpstatus" runat="server" AutoGenerateColumns="false" width="100%" CssClass="table" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1"
                                     EmptyDataText="No Data found for View"  OnRowDataBound="grdtpstatus_row_databound">
                              
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="45px" HeaderStyle-CssClass="stickyFirstRow" >
                            <ItemTemplate>
                                <asp:Label ID="lbno" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblparticular1" runat="server" Text='<%#Eval("Sf_Name") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Designation" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblparticular2" runat="server" Text='<%#Eval("sf_Designation_Short_Name") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Hq" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblparticular3" runat="server" Text='<%#Eval("Sf_HQ") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Emp_Id" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblempid" runat="server" Text='<%#Eval("sf_emp_id") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                               
                                          <asp:TemplateField HeaderText="Tp Status" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemStyle  Width="40px"></ItemStyle>
                                                <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Change_Status") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Tp Entry_Date" HeaderStyle-CssClass="stickyFirstRow">
                           <ItemStyle  Width="60px"></ItemStyle>
                                                <ItemTemplate>
                                <asp:Label ID="lblsubdte" runat="server" Text='<%#Eval("submission_date") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                       
                                          <asp:TemplateField HeaderText="Tp Approved_Date" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemStyle  Width="70px"></ItemStyle>
                                               <ItemTemplate>
                                <asp:Label ID="lblconformdate" runat="server" Text='<%#Eval("Confirmed_Date") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
       <%--                                  <asp:TemplateField HeaderStyle-Font-Names="Calibri" HeaderStyle-Font-Size="11px">

                                             
                                             <HeaderTemplate >
                        <table border="1" style="border-collapse:collapse;background-color:#006666" width="100%">
                            <tr>
                                <td >
                                    <table align="center">
                            <tr>
                                <td align="center">
                                 <asp:Label ID="lbltour" runat="server" Text="Tour Plan" ForeColor="White" Font-Names="Calibri" Font-Size="11px"></asp:Label>
                                </td>
                            </tr> 
                             </table>
                                    </td></tr>
                            <tr>
                                <td>
                                    <table border="1" style="border-collapse:collapse" width="100%">
                                        <tr>
                                            <td>
                                    <asp:Label ID="lblstatus" runat="server" Text="Status" Width="55px" BackColor="#006666" ForeColor="white" Font-Names="Calibri" Font-Size="11px"></asp:Label></td><td><asp:Label ID="lblentrydate" runat="server" Text="Entry Date" ForeColor="white" Font-Names="Calibri" width="60px" Font-Size="11px"></asp:Label></td>
                                            <td><asp:Label ID="lblapprvedate" runat="server" Text="Approve Date" Width="60px" BackColor="#006666" ForeColor="White" Font-Names="Calibri" Font-Size="11px"></asp:Label></td>
                                </tr></table>
                            </tr>
                            </table>
                                               
                       
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table border="1" style="border-collapse:collapse" width="100%">
                            <tr>
                               <td Width="55px"><asp:Label ID="lblchangestatus" runat="server" Text='<%# Eval("Change_Status") %>' Font-Names="Calibri" Font-Size="11px"></asp:Label></td>
                             <td width="60px"><asp:Label ID="lblsudte" runat="server" Text='<%# Eval("submission_date") %>' Font-Names="Calibri" Font-Size="11px"></asp:Label></td>
                               <td width="60px"><asp:Label ID="lblconformdte" runat="server" Text='<%# Eval("Confirmed_Date") %>' Font-Names="Calibri" Font-Size="11px"></asp:Label></td>
                            </tr>
                        </table>

                    </ItemTemplate>
                        </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="desigcolor"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Desig_Color") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                     </div>
                                       </div>

             
            </asp:Panel>
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
    <%-- <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script>
        function fnExcelReport() {
            var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
            var textRange; var j = 0;
            tab = document.getElementById('grdtpstatus'); // id of table

            for (j = 0; j < tab.rows.length; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            }
            tab_text = tab_text + "</table>";
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, 'rpt_TP_Status.aspx.xls');
            }
            else                 //other browser not tested on IE 11
                var element = document.createElement('a');
            element.setAttribute('href', 'data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
            element.setAttribute('download', 'rpt_TP_Status.aspx.xls');
            element.style.display = 'none';
            document.body.appendChild(element);
            element.click();
            document.body.removeChild(element);
        }
    </script>--%>
</body>
</html>
