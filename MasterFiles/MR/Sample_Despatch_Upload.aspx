<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample_Despatch_Upload.aspx.cs" Inherits="MasterFiles_Options_Sample_Despatch_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample Despatch Upload</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
        a.mybutton {
    font-size: 20px;
    font-family:Tahoma;
    font-weight:bold;
    font-color:red;
}
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
     <script type="text/javascript">

         $(document).ready(function () {
             document.getElementById('<%= grdPrimary.ClientID %>').style.display = 'none';
         });
        </script>

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
                                <asp:DropDownList ID="ddlYear" runat="server">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" runat="server" Width="100%" CssClass="input" />
                            </div>
                             <div class="single-des clearfix">
                                      
                                        <asp:RadioButtonList ID="rdochksave" runat="server" RepeatDirection="Horizontal">
                                            <%--<asp:ListItem Value="1" Enabled="false">Overwrite with Existing Records</asp:ListItem>--%>
                                            <asp:ListItem Value="0" Selected="true">Only Insert</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" />
                            </div>
                        </div>
                        
                        <div class="single-des clearfix">
                            <asp:Label ID="lblExc" runat="server" Text="Excel Format File" CssClass="label"></asp:Label>
                            <asp:LinkButton ID="lnkDownload" runat="server" cssclass="mybutton" ForeColor="Blue" Text="Download Here" OnClick="lnkDownload_Click"> 
                            </asp:LinkButton>
                        </div>

                          <div class="single-des clearfix">
                                
                            </div>
                    </div>
                </div>
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
                                                                                             <asp:GridView ID="grdPrimary" runat="server"  Width="100%"
                                                CssClass="table" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="Teal" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                            <asp:Label ID="Label2" runat="server" Text="Download Not Uploaded List" CssClass="label"></asp:Label>
                               <asp:LinkButton ID="lnlnot" runat="server" cssclass="mybutton" Text="Download Here" OnClick="lnlnot_Click"> 
                                </asp:LinkButton>
                                                 </div>
                                        </center>
                                    </asp:Panel>

<br /><br /><br />
         <table>
            
                <tr><td><b><u><font color="Blue">Sample Upload Instruction</font></u></b></td></tr>
                <tr><td>1. &nbsp; Click the "Download Here" link in the Screen and Download the Excel file for "Sample - Upload".</td></tr>
                <tr><td>2. &nbsp; After Downloading the Excel, the Mandatory Field is marked as "Yellow" Color in the Relevant Column.</td></tr>
                <tr><td>3. &nbsp; The User has to Fill the Employee ID, Sample ERP Code and Despatch Qty ( Qty. will be Numeric).</td></tr>
                <tr><td>4. &nbsp; Employee Code is Available in the "Field Force Master" and Sample ERP Code is Available in the "Product Master".</td></tr>
                <tr><td>5. &nbsp; The User should Upload the samples on Monthwise. For Single Month Multiple times also can Upload.</td></tr>
                <tr><td>6. &nbsp; For the Particular Month, if User want to Upload again for overwriting the Existing Records, kindly select the Mode as "Overwrite with Existing Records".</td></tr>
                <tr><td>7. &nbsp; For the Particular Month, if User want to Update Extra Records without disturbing the Previous Records, Kindly Select the Mode as "Only Insert".</td></tr>
                <tr><td>8. &nbsp; Wrong Uploaded Records are downloaded with the link of "Not Uploaded List" in the Bottom of the Screen. </td></tr>
                <tr><td>9. &nbsp; If all the Filled Records are Perfect in the Excel File, then only Upload will happen on a "Successful" basis.</td></tr>
        </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
         
        </div>
        
    </form>
</body>
</html>
