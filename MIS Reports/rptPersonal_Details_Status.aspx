<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptPersonal_Details_Status.aspx.cs"
    Inherits="MIS_Reports_rptPersonal_Details_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title runat="server">Employee Personal Detail Status</title>
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





    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnl').html())
                location.href = url
                return false
            })
        })
    </script>
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }

        .box {
            background: #F3F6ED;
            border: 3px solid #7E8D29;
            border-radius: 8px;
        }

        .box1 {
            border: 2px solid #d1e2ea;
            border-radius: 8px;
            padding: 20px;
        }

        /*.label {
            width: 200px;
            display: inline-block;
            text-align: left;
            color: #f46b42;
        }*/

        .textbox1 {
            width: 200px;
            display: inline-block;
            text-align: left;
        }

        .break {
            height: 10px;
        }

        .mGrid {
            width: 100%;
            background-color: white;
            margin: 5px 0 10px 0;
            border: solid 1px black;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px black;
                color: black;
                border-color: black;
                border-left: solid 1px black;
            }

            .mGrid th {
                padding: 4px 2px;
                color: White;
                background-color: #0097AC;
                border-color: black;
                border-left: solid 1px black;
                font-weight: bold;
                font-size: 11px;
                font-family: Calibri;
            }

            .mGrid .pgr {
                background: white;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px black;
                    font-weight: bold;
                    color: black;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: black;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .normal {
            background-color: white;
        }

        .highlight {
            background-color: white;
        }

        .div_fixed {
            position: fixed;
            top: 400px;
            right: 5px;
        }

        .grdDoc_View_Column {
            color: White;
            height: 20px;
        }

        .grdDoc_View_Row {
            padding-left: 2pt;
            border: 1px solid;
            border-color: #999999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnl" runat="server">
                <center>
                    <br />
                    <table width="100%">
                        <tr>
                            <td></td>

                            <td align="right">
                                <table>
                                    <tr>
                                        <td style="padding-right: 40px">
                                            <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                            <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnPrint_Click" />--%>
                                        </td>
                                        <td style="padding-right: 25px">
                                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                            <%--  <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />--%>
                                        </td>
                                        <td style="padding-right: 50px">
                                            <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                            </asp:LinkButton>
                                            <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                            <%-- <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" OnClick="btnClose_Click" />--%>
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

                                <asp:Label ID="lblHead" Text="Employee Personal Detail Status" CssClass="reportheader"
                                    runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>
                                <asp:Label ID="lblRegionName" runat="server" CssClass="reportheader" ForeColor="#696D6E"></asp:Label>

                                <br />
                                <br />
                                <center>

                                    <asp:Label ID="lblnorecord" Text="No Personal Detail" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                </center>
                                <br />

                                <div class="row justify-content-center ">
                                    <div class="col-lg-6 ">


                                        <asp:Panel ID="pnl2" runat="server" CssClass="box1">
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-11 ">
                                                <div class="designation-area clearfix" align="left">

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblEmpl_name" runat="server" CssClass="label" Text="Employee Name" Font-Size="14px">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtEmpl_name" CssClass="label" runat="server" ForeColor="Red" Font-Size="14px">  </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblDOJ" runat="server" CssClass="label" Font-Size="14px" Text="DOJ">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtDOJ" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lablHQ" runat="server" CssClass="label" Font-Size="14px" Text="HQ">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtHq" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lbldesig" runat="server" CssClass="label" Font-Size="14px" Text="Designation">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtdesig" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblemp_code" runat="server" CssClass="label" Font-Size="14px" Text="Employee Id">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtemp_code" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblstate" runat="server" CssClass="label" Font-Size="14px" Text="State">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtstate" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblperm_addr" runat="server" CssClass="label" Font-Size="14px" Text="Permanent Address">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtperm_addr" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px">  </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblpres_addr" runat="server" CssClass="label" Font-Size="14px" Text="Present Address">
                                                                </asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtpres_addr" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px">  </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblDOB" runat="server" CssClass="label" Font-Size="14px" Text="DOB"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtDOB" runat="server" CssClass="label" ForeColor="Red" Font-Size="14px"> </asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--  <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                           <asp:Label ID="lblDOW" runat="server" CssClass="label" Text="DOW :"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-2" style="font-size: 14px">
                                                        </div>
                                                        <div class="col-lg-6">
                                                           <asp:Label ID="txtDOW" runat="server" CssClass="textbox1"> </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblemail" runat="server" CssClass="label" Font-Size="14px" Text="Email ID"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtemail" runat="server" TabIndex="5" CssClass="label" ForeColor="Red" Font-Size="14px"
                                                                    ToolTip="Enter Email ID"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblmob_no" runat="server" CssClass="label" Font-Size="14px" Text="Mobile No"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtmob_no" runat="server" TabIndex="6" CssClass="label" ForeColor="Red" Font-Size="14px"
                                                                    ToolTip="Enter Mobile No"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblResi_No" runat="server" CssClass="label" Font-Size="14px" Text="Residential No"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtResi_No" TabIndex="7" ToolTip="Enter Residential No" CssClass="label" ForeColor="Red" Font-Size="14px"
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="single-des clearfix">
                                                        <div class="row ">
                                                            <div class="col-lg-4">
                                                                <asp:Label ID="lblemerg_contact" runat="server" CssClass="label" Font-Size="14px" Text="Emergency Contact No"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2" style="font-size: 14px">
                                                                :
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label ID="txtemerg_contact" runat="server" TabIndex="8" ToolTip="Enter Emergency Contact No"
                                                                    CssClass="label" ForeColor="Red" Font-Size="14px"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%-- <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lblpan_no" runat="server" CssClass="label" Text="PAN Card No :"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-2" style="font-size: 14px">
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtpan_no" Width="254px" SkinID="MandTxtBox" TabIndex="9" ToolTip="Enter PAN Card No" CssClass="textbox1"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                                    <%--   <div class="single-des clearfix">
                                                    <div class="row ">
                                                        <div class="col-lg-5">
                                                            <asp:Label ID="lblaadhar" runat="server" CssClass="label" Text="Aadhar No :"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-2" style="font-size: 14px">
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label ID="txtaadhar" Width="154px" runat="server" SkinID="MandTxtBox" TabIndex="10"
                                                                ToolTip="Enter Aadhar No" CssClass="textbox1"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                    </div>
                                </div>

                                <div class="display-table clearfix ">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="grdPersonal" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found" Visible="false"
                                            AutoGenerateColumns="false" GridLines="None"
                                            CssClass="table" AlternatingRowStyle-CssClass="alt" AllowSorting="True">
                                            <HeaderStyle Font-Bold="False" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMP CODE" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemp_id" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FILEDFORCE NAME" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblforce" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESIGNATION" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_HQ")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REPORTING TO" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblreporting" runat="server" Text='<%#Eval("Reporting_To")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Joining" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldatejoin" runat="server" Text='<%#Eval("sf_joining_date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATE" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstate" runat="server" Text='<%#Eval("state")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MOB.NO" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmob" runat="server" Text='<%#Eval("Mob_No")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="POSTAL / RESIDENCIAL / PERMENT ADDRESS" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladress" runat="server" Text='<%#Eval("address")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMAIL ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("sf_email")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOB" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldob" runat="server" Text='<%#Eval("SF_DOB")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Color" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" ForeColor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>

                </center>

            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
