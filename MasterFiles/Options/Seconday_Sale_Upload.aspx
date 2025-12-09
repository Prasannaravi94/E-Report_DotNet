<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seconday_Sale_Upload.aspx.cs" Inherits="MasterFiles_Options_Seconday_Sale_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sale Upload</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
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
                                <asp:DropDownList ID="ddlMonth" runat="server">
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
                                <asp:DropDownList ID="ddlYear" runat="server" Width="80px">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                <asp:FileUpload ID="FlUploadcsv" CssClass="input" runat="server" Width="100%" />
                            </div>
                               <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkDelete" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                        Text="Delete Existing Secondary List ( if Yes then Check this Option )"
                                         />
                                </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" />

                                                                <asp:Button ID="btnSave" Visible="false" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSave_Click" />

                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblIns" runat="server" CssClass="label" Text="Note:" ForeColor="Red"></asp:Label>
                                <asp:Label ID="Label1" runat="server" CssClass="label"
                                    Text="1) Sheet Name Must be 'UPL_Secondary_Master'"></asp:Label>
                            </div>
                           
                            <div class="single-des clearfix">
                                <asp:Label ID="lblExc" runat="server" CssClass="label" Text="Excel Format File"></asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" CssClass="label" ForeColor="Blue"
                                    Text="Download Here" OnClick="lnkDownload_Click"> 
                                </asp:LinkButton>
                            </div>
                             <div class="single-des clearfix">
                            
                                <asp:LinkButton ID="lnkUploded" runat="server" Font-Size="Large"  ForeColor="Blue"
                                    Text="Uploaded Document Status" OnClick="lnkUploded_Click"> 
                                </asp:LinkButton><br />
                                                                 <asp:Label ID="lblMessage" runat="server" CssClass="label" Font-Bold="true" ForeColor="Green"></asp:Label>

                                 
                            </div>
                          
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
