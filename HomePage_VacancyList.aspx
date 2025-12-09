<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_VacancyList.aspx.cs" Inherits="HomePage_VacancyList" %>

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
                                       <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" Visible="false">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>
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
                    <asp:Label ID="lblHead" runat="server" Text="VACANT LIST" CssClass="reportheader"></asp:Label>
                </div>
                <br />
                  <div class="row" align="left" style="float:left;width:135%;" >
                          <div class="col-lg-8">
                                           <asp:Label ID="lblFieldForce" runat="server" CssClass="reportheader" ForeColor="#696d6e" ></asp:Label>
                                    </div> 
                           
                                          <asp:Button Text="NEXT PAGE" CssClass="savebutton" Width="100px"
                                                ID="Button1" runat="server" OnClick="btnnxt_Click" />
                                     
                                </div>  
                <br />

                  <div class="display-reportMaintable clearfix">
                                   <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">

                                <asp:GridView ID="grdvac" runat="server" AutoGenerateColumns="false" width="100%" CssClass="table" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1"
                                     EmptyDataText="No Data found for View"  OnRowDataBound="grdvac_row_databound">
                              
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="45px" HeaderStyle-CssClass="stickyFirstRow" >
                            <ItemTemplate>
                                <asp:Label ID="lbno" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vacant Name" HeaderStyle-CssClass="stickyFirstRow">
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
                        <asp:TemplateField HeaderText="Vacant From" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblparticular4" runat="server" Text='<%#Eval("Last_DCR_Date") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No: of days (Vacant)" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblparticular5" runat="server" Text='<%#Eval("days") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Emp_Id" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemTemplate>
                                <asp:Label ID="lblempid" runat="server" Text='<%#Eval("sf_emp_id") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                               
                                          <asp:TemplateField HeaderText="Joining Date" HeaderStyle-CssClass="stickyFirstRow">
                            <ItemStyle  Width="40px"></ItemStyle>
                                                <ItemTemplate>
                                <asp:Label ID="lbljoining" runat="server" Text='<%#Eval("sf_joining_date") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="stickyFirstRow">
                           <ItemStyle  Width="60px"></ItemStyle>
                                                <ItemTemplate>
                                <asp:Label ID="lbluser" runat="server" Text='<%#Eval("UsrDfd_UserName") %>' ></asp:Label>
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
                <br />
               <asp:Button Text="NEXT PAGE" CssClass="savebutton" Width="100px"
                                                ID="btnnxt" runat="server" OnClick="btnnxt_Click" />
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
</body>
</html>
