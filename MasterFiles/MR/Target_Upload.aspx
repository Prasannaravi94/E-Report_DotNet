<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Target_Upload.aspx.cs" Inherits="MasterFiles_Options_Target_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Target Upload</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
        .loading {        
        display:none;
         }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById('<%= grdPrimary.ClientID %>').style.display = 'none';
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <center>
                <asp:Panel ID="pnlSalesForce" Width="90%" runat="server">
                    <table cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color: White; height: 350px" width="60%">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="stylespc">
                                <table align="center" width="400px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblToYear" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Year</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlYear" runat="server">
                                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblExcel" runat="server" SkinID="lblMand"><span style="color:Red">*</span>Excel file</asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FlUploadcsv" runat="server" />
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                    <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" /></center>
                            </td>
                        </tr>
                        <tr>
                            <td><div class="single-des clearfix"style="margin-left: 20px">
                <asp:Label ID="lblExc" runat="server" Text="Excel Format File" CssClass="label"></asp:Label>
                <asp:LinkButton ID="lnkDownload" runat="server" CssClass="label" ForeColor="Blue" Text="Download Here" OnClick="lnkDownload_Click"> 
                </asp:LinkButton>
            </div></td>
                        </tr>
                    </table>
                </asp:Panel>
            </center>
            <br />

            
            <div class="single-des clearfix">

                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:Panel ID="pnlprimary" runat="server" Visible="false">
                                        <center>
                                            <img src="../../Images/arrowdown1.gif" height="80px" alt="" />


                                            <h2 style="color: Red; font-weight: bold; font-size: x-large">Not Uploaded List</h2>
                                            <div class="single-des clearfix">
                                                <asp:GridView ID="grdPrimary" runat="server" Width="100%"
                                                    CssClass="table" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="Teal" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                                <asp:Label ID="Label2" runat="server" Text="Download Not Uploaded List" CssClass="label"></asp:Label>
                                                <asp:LinkButton ID="lnlnot" runat="server"
                                                    Text="Download Here" OnClick="lnlnot_Click"> 
                                                </asp:LinkButton>
                                            </div>
                                        </center>
                                    </asp:Panel>


                                    <br />
                                    <br />
                                    <br />

                                    <table style="margin-left: 20px">

                                        <tr>
                                            <td><b><u><font color="Blue">Target Upload Instruction</font></u></b></td>
                                        </tr>
                                        <tr>
                                            <td>1. &nbsp; Click the "Download Here" link in the Screen and Download the Excel file for "Target - Upload".</td>
                                        </tr>
                                        <tr>
                                            <td>2. &nbsp; After Downloading the Excel, the Mandatory Field is marked as "Yellow" Color in the Relevant Column.</td>
                                        </tr>
                                        <tr>
                                            <td>2. &nbsp; The User has to Fill the HQ Code, Sale ERP Code, Month,Target Qty,Target Rate and Target Value (Qty/Rate/Value will be Numeric).</td>
                                        </tr>
                                        <tr>
                                            <td>3. &nbsp; HQ Code is Available in the "Field Force Master" and Sale ERP Code is Available in the "Product Master".</td>
                                        </tr>
                                        <tr>
                                            <td>4. &nbsp; The User should Upload the Targets on Financial Year wise.If user wants to upload in betweeen for a single month then he has to upload all records for the particular Financial Year once again.</td>
                                        </tr>
                                        <tr>
                                            <td>7. &nbsp; Wrong Uploaded Records are downloaded with the link of "Not Uploaded List" in the Bottom of the Screen. </td>
                                        </tr>
                                        <tr>
                                            <td>6. &nbsp; If all the Filled Records are Perfect in the Excel File, then only Upload will happen on a "Successful" basis.</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
