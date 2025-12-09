<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory_SFC_Download.aspx.cs" Inherits="MasterFiles_Territory_SFC_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SFC MR (All India)</title>
    
     <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
            </div>
    <br />
   <%-- <div>
   <asp:Panel ID="pnlTerrSFC" Width="90%" runat="server">
        <table width="95%" style="margin-left: 10%">
            <tr>
                <td>
                    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color:White">
                      
                       
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblExc" runat="server" Text="" Font-Size="Medium"></asp:Label>
                                <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana"
                                    Text="Download Here" OnClick="lnkDownload_Click"> 
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                 <td style="width:2%">
                                <br />
                            </td>
                
                
            </tr>
        </table>
    </asp:Panel>  
    </div>--%>
         <div>
            <div id="Div1" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">SFC MR (All India)</h2>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="Medium" Font-Bold="true"
                                Text="Download Here" OnClick="lnkDownload_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
