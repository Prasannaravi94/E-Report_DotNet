<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMRStatus.aspx.cs" Inherits="Reports_rptMRStatus" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Doctor and Chemist Master Report</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../../assets/images/logo.png" />
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
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function RefreshParent() {
            //window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <%--<script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>--%>
        <script type="text/javascript">
 function Excel_Download() {
            debugger;
            var myBlob = new Blob([pnlContents.innerHTML], { type: 'vnd.ms-excel' });
            var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
            var a = document.createElement("a");
            document.body.appendChild(a);
            a.href = url;
            a.download = "export.xls";
            a.click();
            setTimeout(function () { window.URL.revokeObjectURL(url); }, 300);
        }

 </script>
</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>

            <center>
                <asp:Panel ID="pnlbutton" runat="server">
                    <br />
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="row justify-content-center">
                                <div class="col-lg-9">
                                    <div align="center">
                                        <asp:Label ID="lblHead" runat="server" Text="FieldForce Status Report" CssClass="reportheader"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblActiveHeader" runat="server" CssClass="reportheader" ForeColor="#696d6e"></asp:Label>
                                        <br />
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
                                                            <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                            </asp:LinkButton>
                                                            <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                            <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="savebutton"
                                                OnClick="btnPrint_Click" />--%>
                                                        </td>
                                                        <td style="padding-right: 15px">
                                                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClientClick="Excel_Download();">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                            </asp:LinkButton>
                                                            <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                            <%--<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="savebutton" />--%>
                                                        </td>
                                                        <td style="padding-right: 15px">
                                                            <asp:LinkButton ID="btnPDF" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf" Width="35px" Style="border-width: 0px;" />
                                                            </asp:LinkButton>

                                                            <%-- <asp:Button ID="btnPDF" runat="server" Text="PDF" CssClass="savebutton"
                                                OnClick="btnPDF_Click" />--%>
                                                        </td>
                                                         <td style="padding-right: 100px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

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

                <div class="container clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">

                            <asp:Panel ID="pnlContents" runat="server" Width="99%">


                                <div class="display-reporttable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                        <table width="90%" align="center">
                                            <tbody>

                                                <tr>
                                                    <td align="center">
                                                        <asp:GridView ID="grdTerritory" runat="server" Width="85%" style="background-color:white"
                                                            AutoGenerateColumns="false" OnRowDataBound="grdTerritory_RowDataBound" GridLines="None"
                                                            CssClass="table">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Territory Code" Visible="false">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerritory_Code" runat="server" Text='<%# Bind("Territory_Code") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Territory Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerritory_SName" runat="server" Text='<%# Bind("Territory_SName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ListedDR_Count" ItemStyle-HorizontalAlign="Center">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblListedDR_Count" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Chemists_Count" ItemStyle-HorizontalAlign="Center">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChemists_Count" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="UnListedDR_Count" ItemStyle-HorizontalAlign="Center" Visible="false">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnListedDR_Count" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                        <asp:GridView ID="grdDoctor" runat="server"  style="background-color:white"
                                                            AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound" GridLines="None"
                                                            CssClass="table">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Right">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Created Date" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDRDate" runat="server" Text='<%# Bind("ListedDr_Created_Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsf_name" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Uni_Code" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldrslno" runat="server" Text='<%# Bind("ListedDr_New_Sl_No") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="SVL No" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldrsvlno" runat="server" Text='<%# Bind("ListedDr_Sl_No") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="First Level Manager" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="150px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstMGR" runat="server" Text='<%# Bind("Reporting_Manager1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Second Level Manager" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                            HeaderStyle-ForeColor="White">
                                            <ControlStyle Width="150px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSecondMGR" runat="server" Text='<%# Bind("Reporting_Manager2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Qual." ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHos" runat="server" Text='<%# Bind("ListedDr_Hospital") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital Address" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHosAddress" runat="server" Text='<%# Bind("Hospital_Address") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital City" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("Hospital_City") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital State" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSt" runat="server" Text='<%# Bind("Hospital_State") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital Country" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Hospital_Country") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Area Cluster Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Town Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DOB" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOB","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DOW" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOW" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ListedDr_DOW","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Right">

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
                                                                        <asp:Label ID="lblBrd1" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Focus Brand 2" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrd2" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Focus Brand 3" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrd3" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Focus Brand 4" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrd4" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                        <asp:GridView ID="grdNonDR" runat="server" Width="85%" HorizontalAlign="Center" style="background-color:white"
                                                            AutoGenerateColumns="false" OnRowDataBound="grdNonDR_RowDataBound" GridLines="None"
                                                            CssClass="table">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UnListed Doctor Created Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnDRDate" runat="server" Text='<%# Bind("UnListedDr_Created_Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Field Force Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsf_name" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HQ">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UnListed Doctor Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnDRName" runat="server" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qual.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnSpeciality" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnCategory" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnClass" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Root Plan">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnMobile" runat="server" Text='<%# Bind("UnListedDr_DOB") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnMobile" runat="server" Text='<%# Bind("UnListedDr_DOW") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnMobile" runat="server" Text='<%# Bind("UnListedDr_Mobile") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EMail">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnEMail" runat="server" Text='<%# Bind("UnListedDr_Email") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdChem" runat="server" Width="85%" HorizontalAlign="Center" style="background-color:white"
                                                            AutoGenerateColumns="false" OnRowDataBound="grdChem_RowDataBound" GridLines="None"
                                                            CssClass="table">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Right">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="chemists_code" ItemStyle-HorizontalAlign="Left" Visible="false">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblchemists_code" runat="server" Text='<%# Bind("chemists_code") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFieldforce" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%# Bind("Chemists_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contact" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChemists_Contact" runat="server" Text='<%# Bind("Chemists_Contact") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Chemists_Address1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="City" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSt" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Country" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PinCode" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPinCode" runat="server" Text='<%# Bind("Chemists_PinCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChemists_Phone" runat="server" Text='<%# Bind("Chemists_Phone") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChemTerritory_Name" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Town Name" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Cluster") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fax" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFax" runat="server" Text='<%# Bind("Chemists_Fax") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email ID" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Chemists_Email") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Chemists_Mobile") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdStok" runat="server" Width="75%" HorizontalAlign="Center" GridLines="None"
                                                            AutoGenerateColumns="false" CssClass="table" style="background-color:white">
                                                            <%-- HeaderStyle-Font-Size="8pt"--%>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stockist_Code" Visible="false">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStockist_Code" runat="server" Text='<%# Bind("Stockist_Code") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stockist Name">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contact">
                                                                    <%-- <ControlStyle Width="90%"></ControlStyle>--%>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStockist_ContactPerson" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
</body>
</html>

