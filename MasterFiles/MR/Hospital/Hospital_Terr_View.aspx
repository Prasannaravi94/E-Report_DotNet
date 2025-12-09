<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hospital_Terr_View.aspx.cs" Inherits="MasterFiles_MR_Hospital_Hospital_Terr_View" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital View</title>
    <style>
        .tr_Sno {
            background: #414d55;
            color: white;
            font-weight: 400;
            border-radius: 8px 0 0 8px;
            font-size: 12px;
            border-bottom: 10px solid #fff;
            font-family: Roboto;
            border-left: 0px solid #F1F5F8;
        }

        .tr_th {
            padding: 20px 15px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            text-transform: uppercase;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />

                        <h2 class="text-center">Hospital View</h2>

                        <div class="designation-area clearfix">
                            <asp:Panel ID="pnlSf" runat="server">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblSF" runat="server" Text="Field Force Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="Button1" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />

                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-Approvaltable clearfix">
                            <div class="table-responsive" style="max-height: 700px; scrollbar-width: thin;">
                                <asp:Table ID="tbl" runat="server" CssClass="table" Width="100%">
                                </asp:Table>
                                <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True">No Records Found</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
