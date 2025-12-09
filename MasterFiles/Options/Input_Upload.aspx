<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input_Upload.aspx.cs" Inherits="MasterFiles_Options_Input_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Despatch Upload</title>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMoth" runat="server" Text="Month" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblToYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" CssClass="input" runat="server" Width="100%" />
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" />
                            </div>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblIns" runat="server" Text="Note:" CssClass="label" ForeColor="Red"></asp:Label>

                            <asp:Label ID="Label1" runat="server" CssClass="label"
                                Text="1) Sheet Name Must be 'Upl_Input_Master'"></asp:Label>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblExc" runat="server" CssClass="label" Text="Excel Format File"></asp:Label>
                            <asp:LinkButton ID="lnkDownload" runat="server" ForeColor="Blue" CssClass="label"
                                Text="Download Here" OnClick="lnkDownload_Click" />

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
