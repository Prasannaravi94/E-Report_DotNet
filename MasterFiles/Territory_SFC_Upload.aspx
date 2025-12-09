<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_SFC_Upload.aspx.cs" Inherits="MasterFiles_Territory_SFC_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SFC Download/Upload</title>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        #divdr {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 130px;
        }

        #detail {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 130px;
            padding: 2px;
        }

        #divcat {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailcat {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }

        #divTerr {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailTerr {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }

        #divBrd {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailBrd {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
    </style>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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
                                <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblIns" runat="server" CssClass="label" ForeColor="Red">Note:</asp:Label>
                                <br />
                                <asp:Label ID="Label1" runat="server" CssClass="label">1) Sheet Name Must be 'UPL_SFC_Master'</asp:Label>
                                <br />
                                <asp:Label ID="Label4" runat="server" CssClass="label">2) Don't Do Any Special Formats in the Excel File</asp:Label>
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
