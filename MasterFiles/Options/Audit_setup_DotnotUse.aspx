<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Audit_setup_DotnotUse.aspx.cs" Inherits="MasterFiles_Options_Audit_setup" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Audit Setup</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../../../assets/css/nice-select.css">
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/css/style.css">
    <link rel="stylesheet" href="../../../assets/css/responsive.css">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').click(function () {
                if ($("#txtSupport").val() == "") { alert("Please enter Support Person Name"); $('#txtSupport').focus(); return false; }
                if ($("#txtremark").val() == "") { alert("Please enter remark"); $('#txtremark').focus(); return false; }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <h2 class="text-center" runat="server">
                            <asp:Label ID="lblhead" runat="server" Text="Audit Setup"></asp:Label>
                        </h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblsupport" runat="server" Text="Support Person Name" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtSupport" runat="server" CssClass="input"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2"></div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblDateTime" runat="server" Text="Date/Time" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtDateTime" runat="server" Enabled="false" CssClass="input"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2"></div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblSetup" runat="server" Text="What Setup changed" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtremark" runat="server" CssClass="input" MaxLength="750" TextMode="MultiLine" Height="130px"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2"></div>
                                </div>
                                <center>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                            </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
