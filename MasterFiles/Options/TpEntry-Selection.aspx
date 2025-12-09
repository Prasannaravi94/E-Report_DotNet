<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TpEntry-Selection.aspx.cs" Inherits="TpEntry_Selection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" type="text/css" />
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
</head>
<body>


    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <form id="form1" runat="server">
            <div>
                <center>
                    <br />
                    <asp:Panel ID="pnlbutton" runat="server">
                        <table width="100%">
                            <tr>
                                <td></td>
                                <td align="right">
                                    <table>
                                        <tr>
                                            <td style="padding-right: 50px">
                                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="lblClose" runat="server" Text="Close" Font-Size="14px"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />

                    <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <asp:Label ID="lblTitle" runat="server" CssClass="reportheader"></asp:Label>
                                <span style="color: Red"></span>
                                <br />
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Tp Entry - Selection Option
                                        </h6>
                                    </div>
                                    <div class="card-body">
                                        <asp:RadioButtonList ID="Radio1" runat="server">
                                            <asp:ListItem Value="BSTS" Text="Based on Single Territory selection"></asp:ListItem>
                                            <asp:ListItem Value="BSTSD" Text="Based on Single Territory Selection with Listed drs & Chemists"></asp:ListItem>
                                            <asp:ListItem Value="BMTS" Text="Based on Multiple Territory Selections"></asp:ListItem>
                                            <asp:ListItem Value="BMTSD" Text="Based on Multiple Territory Selection with Listed drs & Chemists"></asp:ListItem>
                                            <asp:ListItem Value="BMSTS" Text="Based on Multiple-Separate Territory Selection"></asp:ListItem>
                                            <asp:ListItem Value="BMSTSD" Text="Based on Multiple-Separate Territory Selection with Listed Drs & Chemists"></asp:ListItem>
                                            <asp:ListItem Value="BSTP" Text="Master Tp Needed"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />

                                    <asp:Button ID="BtnSave" runat="server" CssClass="savebutton" Text="Save" OnClick="BtnSave_Click" />

                                    <asp:Button ID="BtnClear" runat="server" CssClass="savebutton" Text="Clear" OnClick="BtnClear_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </center>
            </div>
        </form>
    </asp:Panel>
</body>
</html>
