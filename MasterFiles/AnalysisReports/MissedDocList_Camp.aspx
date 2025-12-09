<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissedDocList_Camp.aspx.cs" Inherits="MIS_Reports_MissedDocList_Camp" %>

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
        .display-reporttable .table th:first-child {
            font-size: 14px;
        }
    </style>
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
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Missed Doctor List for the Month of "
                                    ForeColor="#0077FF" CssClass="reportheader"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblsubhead" runat="server" CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e" Visible="false"></asp:Label>
                            </div>
                            <br />

                            <%--   <center>
                <table width="100%" style="text-align:center">
                    <tr>
                        <td style="width: 3.6%" />
                        <td>
                            <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By"></asp:Label>
                            <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                                TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                   <asp:ListItem Text="Doctor Campaign" Value="6"></asp:ListItem>
                               <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                            <%--<asp:ListItem Text="Doctor Name" Value="7" ></asp:ListItem>--%>
                            <%--  </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" CssClass="TEXTAREA"
                                Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" SkinID="ddlRequired"
                                TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="30px" Height="25px"
                                Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
                </center>--%>
                            <br />

                            <div class="display-reporttable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <center>
                                        <asp:Label ID="lblmiss" Text="Missed Listed Doctor" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="lblmisCnt" runat="server" Font-Size="Large" ForeColor="BlueViolet"></asp:Label>
                                    </center>
                                    <div>
                                        <asp:GridView ID="grdDoctor" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                            OnRowDataBound="grdDoctor_RowDataBound" GridLines="None"
                                            HorizontalAlign="Center" AllowSorting="True" OnSorting="grdDoctor_Sorting"
                                            Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                                    <%--  <HeaderTemplate></HeaderTemplate>--%>
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
                                                <asp:TemplateField SortExpression="ListedDr_Name"
                                                    HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="90%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderStyle-BackColor="#0097AC" 
                                HeaderText="Previous Visit" ItemStyle-HorizontalAlign="Left">
                                <ControlStyle Width="90%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblVisit" runat="server" ForeColor="Red"  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
                                                <asp:TemplateField SortExpression="Doc_SubCatName"
                                                    HeaderText="Campaign" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="70%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsub" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
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
                                            EmptyDataText="No Records Found" GridLines="None"
                                            HorizontalAlign="Center"
                                            Width="100%">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">

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
                                                    HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="90%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qual"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="60%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="40%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Specialty" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="40%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="40%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField
                                                    HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="70%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Doc_SubCatName"
                                                    HeaderText="Campaign" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="70%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsub" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
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
                                            Font-Size="8pt" HorizontalAlign="Center"
                                            Width="100%">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">

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
                                                <asp:TemplateField SortExpression="Doc_SubCatName"
                                                    HeaderText="Campaign" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="70%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsub" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
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
                                            EmptyDataText="No Records Found" GridLines="None"
                                            HorizontalAlign="Center"
                                            Width="100%">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
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
                                                <asp:TemplateField SortExpression="Doc_SubCatName"
                                                    HeaderText="Campaign" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="70%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsub" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
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
                                            EmptyDataText="No Records Found"
                                            HorizontalAlign="Center"
                                            Width="100%">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">

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
                                                <asp:TemplateField SortExpression="Doc_SubCatName"
                                                    HeaderText="Campaign" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="70%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsub" runat="server" Text='<%# Bind("Doc_SubCatName") %>'></asp:Label>
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
    </form>
</body>
</html>
