<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_Master.aspx.cs" Inherits="MasterFiles_Options_Report_Master" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Master</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://code.jquery.com/jquery-3.4.1.slim.js"></script>
    <link href="../../css/Reports/Report_Master.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <input id="Rep_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="Rep_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="Rep_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <div id="Divid" runat="server"></div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <h2 class="text-center" id="hHeading" runat="server"></h2>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <button id="btnAddRep" type="button" class="savebutton">Add</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row justify-content-center divRep">
                <div class="col-lg-5">
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblReportName" runat="server" Width="100%" CssClass="label">Report Name</asp:Label>
                            <asp:TextBox ID="txtReport_Name" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                            <input id="hdnRep_ID" type="hidden" />
                        </div>
                        <div class="single-des clearfix">
                            <asp:CheckBox ID="chk_Rep" runat="server" Text="Active" Checked="true" />
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <button id="btnCancel1" type="button" class="backbutton1 cancel">Cancel</button>
                            <button id="btnSaveRep" type="button" class="savebutton">Save</button>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="row justify-content-center divParam">
                <div class="col-lg-5">
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblParam" runat="server" Width="100%" CssClass="label">Parameter Name</asp:Label>
                            <asp:TextBox ID="txtParameter" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                            <input id="hdnParameterID" type="hidden" />
                        </div>
                        <div class="single-des clearfix">
                            <asp:CheckBox ID="chkParameter" runat="server" Text="Active" Checked="true" />
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <button id="btnCancel2" type="button" class="backbutton1 cancel">Cancel</button>
                            <button id="btnSaveParam" type="button" class="savebutton">Save</button>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="row justify-content-center divSubParam">
                <div class="col-lg-5">
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblSubParam" runat="server" Width="100%" CssClass="label">Sub-Parameter Name</asp:Label>
                            <asp:TextBox ID="txtSubParam" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                            <input id="hdnSubParamID" type="hidden" />
                        </div>
                        <div class="single-des clearfix">
                            <asp:CheckBox ID="chkSubParam" runat="server" Text="Active" Checked="true" />
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <button id="btnCancel3" type="button" class="backbutton1 cancel">Cancel</button>
                            <button id="btnSaveSubParam" type="button" class="savebutton">Save</button>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <table id="tblRep_Master" class="table">
                                </table>
                                <div class="no-result-area" id="div1" runat="server">
                                    No Records Found
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>
        <script src="../../JScript/Service_CRM/Reports/Report_Master.js"></script>
    </form>
</body>
</html>
