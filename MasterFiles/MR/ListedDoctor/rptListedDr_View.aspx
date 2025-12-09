<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptListedDr_View.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_rptListedDr_View" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor View</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../../assets/css/style.css" />

    <%--<link type="text/css" rel="stylesheet" href="../../../css/Report.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <br />
			<div class="row justify-content-center">
                    <div class="col-lg-12">
					<div class="row justify-content-center">
                    <div class="col-lg-9">
					   <div align="center">
                                <asp:Label ID="lblHead" runat="server" Text="Listed Doctor Details" class="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="Label1" runat="server" class="reportheader" ForeColor="#696d6e"></asp:Label>
                            </div>
					</div>
					<div class="col-lg-3">
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
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
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPDF_Click" Visible="false" />
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

            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">

                         
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow:inherit">

                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" GridLines="None"
                                        AutoGenerateColumns="false" CssClass="table" OnRowDataBound="grdDoctor_RowDataBound" style="background-color:white">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblmode" runat="server" Text='<%# Bind("mode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="SVL No" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblsvlno" runat="server" Text='<%# Bind("ListedDr_Sl_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qual." ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Clinic Name" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHos" runat="server" Text='<%# Bind("ListedDr_Hospital") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Clinic Address" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblHosAddress" runat="server" Text='<%# Bind("Hospital_Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pin Code" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbpincode" runat="server" Text='<%# Bind("listeddr_pincode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Area Cluster Name" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Town Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="DOB" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("ListedDr_DOB") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOW" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOW" runat="server" Text='<%# Bind("ListedDr_DOW") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("ListedDr_Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Tagged" Visible="false" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblProd" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Focus Brand 1" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrd1" runat="server" Text='<%# Bind("Focus_brand1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Focus Brand 2" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrd2" runat="server" Text='<%# Bind("Focus_brand2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Focus Brand 3" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrd3" runat="server" Text='<%# Bind("Focus_brand3") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Focus Brand 4" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrd4" runat="server" Text='<%# Bind("Focus_brand4") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Focus Brand 5" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrd5" runat="server" Text='<%# Bind("Focus_brand5") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Potential" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPot" runat="server" Text='<%# Bind("Dr_Potential") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contribution" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblContr" runat="server" Text='<%# Bind("Dr_Contribution") %>'></asp:Label>
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
        </div>
    </form>
</body>
</html>
