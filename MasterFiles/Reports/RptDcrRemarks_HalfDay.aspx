<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptDcrRemarks_HalfDay.aspx.cs" Inherits="MasterFiles_Reports_RptDcrRemarks_HalfDay" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--  <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />


    <title></title>
    
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <br />
        
        <br />

        <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <center>
                            <div align="left">
                               
                                <asp:Label ID="lblHead1" runat="server" Text="Remarks View for" CssClass="reportheader"></asp:Label>
                                <asp:Label ID="lblHead" runat="server" Text="Remarks View for" CssClass="reportheader"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <div align="left" style="font-size: 13px">

                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:Label ID="LblCombined" runat="server" Font-Size="14px"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="Remarks" runat="server" Font-Size="14px"></asp:Label>
                                    </div>
                                </div>
                                </div>
                                
                            <br />
                          
                            <%--<div><asp:Label ID="lblFieldForce" runat="server" Font-Underline="True" Font-Size="9pt" Font-Bold="True"></asp:Label></div>--%>
                            
                        </center>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
</body>
</html>
