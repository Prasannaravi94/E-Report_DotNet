<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockiest_Upload.aspx.cs" Inherits="MasterFiles_Options_Stockiest_Upload" %>


<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stockist Upload</title>
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
                                <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" runat="server" CssClass="input" />
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="savebutton" OnClick="btnUpload_Click" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblIns" runat="server" CssClass="label" ForeColor="Red">Note:</asp:Label>
                                <asp:Label ID="Label1" runat="server" CssClass="label">1) Sheet Name Must be 'UPL_Stockist_Master'</asp:Label>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExc" runat="server" CssClass="label">Excel Format File</asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download Here" OnClick="lnkDownload_Click"> 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
