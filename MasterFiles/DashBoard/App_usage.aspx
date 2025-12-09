<%@ Page Language="C#" AutoEventWireup="true" CodeFile="App_usage.aspx.cs" Inherits="MasterFiles_DashBoard_App_usage" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>App Usage</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css">
<link rel="stylesheet" href="../../../assets/css/nice-select.css">
<link rel="stylesheet" href="../../../assets/css/bootstrap.min.css">
<link rel="stylesheet" href="../../../assets/css/style.css">
<link rel="stylesheet" href="../../../assets/css/responsive.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div id="Divid" runat="server">
            </div>
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">App Usage</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <center>
                            <asp:Button ID="btnmgrgo" runat="server" Text="Go" OnClick="btnmgrgo_Click" CssClass="savebutton" />
                                </center>
                        </div>

                    </div>
                </div>
                <asp:Panel ID="pnlContents" runat="server" Visible="false">
                    <br />
                    <div class="row justify-content-center">
                        <div class="col-lg-6">
                            <div class="single-block-area">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="lblloca" runat="server" Text="Location" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>

                                    </tr>
                                    <asp:Repeater ID="rptLocation" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label20" runat="server" Text="Visit Type details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptVisitType" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label15" runat="server" Text="HALFDAY WORK" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label16" runat="server" Text="Base Level" Width="100%" Font-Bold="true" ForeColor="Magenta"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptBaseLevel" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Worktype_Name_B") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label17" runat="server" Text="Managers" Width="100%" Font-Bold="true" ForeColor="Magenta"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptManager" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">

                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Worktype_Name_M") %>' Font-Size="12px"></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label3" runat="server" Text="Chemist Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptChemist" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label5" runat="server" Text="Unlisted Dr Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptUnlisteddr" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>

                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label8" runat="server" Text="Tour Plan Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptTourPlan" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>

                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label11" runat="server" Text="Manager Options" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptManager_Options" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label19" runat="server" Text="Check In - Check Out Options details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptCheckInOut" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />


                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label13" runat="server" Text="Additional Options" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptAdditionalOptions" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="single-block-area">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label1" runat="server" Text="Doctor Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptDoctor" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label4" runat="server" Text="Stockist Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptStockist" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label6" runat="server" Text="CIP Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptCIP" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label7" runat="server" Text="Hospital Details" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptHospital" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label9" runat="server" Text="RCPA" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptRCPA" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label10" runat="server" Text="Missed Date / Leave" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptMissed_Date_Leave" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label12" runat="server" Text="Order Management" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptOrderMgmt" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                          
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="Label14" runat="server" Text="Others" Width="100%"
                                                ForeColor="MediumVioletRed" Font-Bold="True"
                                                Font-Size="14px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptOthers" runat="server">
                                        <ItemTemplate>
                                            <tr class="tblrowcolor">
                                                <td>
                                                    <asp:Label ID="lblmandat" runat="server" Text='<%#Eval("Ordersl_no") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("ColName") %>' Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
