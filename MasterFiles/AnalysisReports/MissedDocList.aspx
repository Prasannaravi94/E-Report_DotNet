<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissedDocList.aspx.cs" Inherits="MIS_Reports_MissedDocList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missed Doctor List</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />

    <script src="../../../assets/js/jQuery.min.js" type="text/javascript"></script>
    <script src="../../../assets/js/popper.min.js" type="text/javascript"></script>
    <script src="../../../assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js" type="text/javascript"></script>
    <script src="../../../assets/js/main.js" type="text/javascript"></script>

    <%--<link type="text/css" rel="stylesheet" href="../../css/Report.css" />--%>
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
    <style type="text/css">
        .nice-select {
            min-width: 170px;
        }

        .display-reporttable .table th:first-child {
            font-size: 14px;
        }
        .display-reporttable .table td , .display-reporttable .table th {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                    </div>
                    <div class="col-lg-3">
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
                                                <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                            </td>
                                            <td style="padding-right: 15px">
                                                <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                            </td>

                                            <td style="padding-right: 40px">
                                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
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
                <br />
                <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlContents" runat="server">
                                <div align="center">
                                    <br />
                                    <asp:Label ID="lblHead" runat="server" Text="Missed Doctor List for the Month of "
                                        ForeColor="#0077FF" CssClass="reportheader"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblsubhead" runat="server" Visible="false" CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label><br />
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                            <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                            <%--<asp:ListItem Text="Doctor Name" Value="7" ></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"
                                            Visible="false"></asp:TextBox>
                                        <div style="padding-top: 18px;">
                                            <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" CssClass="nice-select"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2" style="padding-top: 18px;">
                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px"
                                            Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                                    </div>
                                </div>


                                <div class="display-reporttable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <br />
                                        <center>
                                            <asp:Label ID="lblmiss" Text="Missed Listed Doctor" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lblmisCnt" runat="server" Font-Size="Large" ForeColor="BlueViolet"></asp:Label>
                                        </center>
                                        <div>
                                            <asp:GridView ID="grdDoctor" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                                OnRowDataBound="grdDoctor_RowDataBound" GridLines="Both" BorderWidth="1" BorderColor="WhiteSmoke"
                                                HorizontalAlign="Center" AllowSorting="True" OnSorting="grdDoctor_Sorting"
                                                Width="100%">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">

                                                        <%--<HeaderTemplate></HeaderTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Listed Doctor Code"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField 
                                                        HeaderText="SVL NO" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSVLNO" runat="server" Text='<%# Bind("svl_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="ListedDr_Name"
                                                        HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField
                                                        HeaderText="Previous Visit" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVisit" runat="server" ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_QuaName" HeaderText="Qual"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="60%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_SName"
                                                        HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Special_SName"
                                                        HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_ClsSName"
                                                        HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="territory_Name"
                                                        HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="70%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </div>

                                        <br />
                                        <center>
                                            <asp:Label ID="lblone" Text="One Visit Listed Doctor" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lbloneCnt" runat="server" Font-Size="Large" ForeColor="BlueViolet"></asp:Label>
                                        </center>
                                        <center>
                                            <asp:GridView ID="grdOneVisit" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                EmptyDataText="No Records Found"
                                                HorizontalAlign="Center" GridLines="None" Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                                                        <%-- <ControlStyle Width="40%" />--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Listed Doctor Code"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField 
                                                        HeaderText="SVL NO" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSVLNO" runat="server" Text='<%# Bind("svl_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="ListedDr_Name"
                                                        HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_QuaName" HeaderText="Qual"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="60%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_SName"
                                                        HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Special_SName"
                                                        HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_ClsSName"
                                                        HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="territory_Name"
                                                        HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="70%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </center>
                                        <br />
                                        <center>
                                            <asp:Label ID="lbltwo" Text="Two Visit Listed Doctor" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lbltwoCnt" runat="server" Font-Size="Large" ForeColor="BlueViolet"></asp:Label>
                                        </center>
                                        <center>
                                            <asp:GridView ID="grdTwoVisit" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                EmptyDataText="No Records Found" GridLines="None"
                                                HorizontalAlign="Center"
                                                Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                                                        <%-- <ControlStyle Width="40%" />--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Listed Doctor Code"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField 
                                                        HeaderText="SVL NO" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSVLNO" runat="server" Text='<%# Bind("svl_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="ListedDr_Name"
                                                        HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_QuaName" HeaderText="Qual"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="60%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_SName"
                                                        HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Special_SName"
                                                        HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_ClsSName"
                                                        HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="territory_Name"
                                                        HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="70%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </center>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblthree" Text="Three Visit Listed Doctor" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lblthreecnt" runat="server" Font-Size="Large" ForeColor="BlueViolet"></asp:Label>
                                        </center>
                                        <center>
                                            <asp:GridView ID="grdThreeVisit" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                EmptyDataText="No Records Found"
                                                HorizontalAlign="Center" GridLines="None"
                                                Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                                                        <%--<ControlStyle Width="40%" />--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Listed Doctor Code"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField 
                                                        HeaderText="SVL NO" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSVLNO" runat="server" Text='<%# Bind("svl_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="ListedDr_Name"
                                                        HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_QuaName" HeaderText="Qual"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="60%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_SName"
                                                        HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Special_SName"
                                                        HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_ClsSName"
                                                        HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="territory_Name"
                                                        HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="70%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </center>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblmore" Text="More than Three Visit Listed Doctor" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lblmoreCnt" runat="server" Font-Size="Large" ForeColor="BlueViolet"></asp:Label>
                                        </center>
                                        <center>
                                            <asp:GridView ID="grdMoreVisit" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                EmptyDataText="No Records Found" GridLines="None"
                                                HorizontalAlign="Center"
                                                Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                                                        <%--<ControlStyle Width="40%" />--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Listed Doctor Code"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField 
                                                        HeaderText="SVL NO" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSVLNO" runat="server" Text='<%# Bind("svl_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="ListedDr_Name"
                                                        HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_QuaName" HeaderText="Qual"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="60%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_SName"
                                                        HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Special_SName"
                                                        HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_ClsSName"
                                                        HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="40%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="territory_Name"
                                                        HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="70%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </center>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <br />
                <br />
            </div>

            <%--<script type="text/javascript">
	document.body.style.backgroundColor = '<%= Session["Div_color"].ToString() %>'
</script>--%>
    </form>
</body>
</html>
