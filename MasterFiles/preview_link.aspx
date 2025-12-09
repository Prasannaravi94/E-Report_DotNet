<%@ Page Language="C#" AutoEventWireup="true" CodeFile="preview_link.aspx.cs" Inherits="preview_link" %>

<!DOCTYPE html>

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
    <link rel="stylesheet" href="../../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>

    <style type="text/css">
        body {
            font-size: 12pt;
        }

        .Grid th {
            color: #fff;
            background-color: green;
        }
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td {
            font-size: small;
            font-family: Calibri;
            font-weight: bold;
            border: 1px solid;
        }

        .GridHeader {
            text-align: center !important;
        }
    </style>
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
     <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>

<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>                         
                                <td style="padding-right: 40px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <asp:Label ID="lblhead" runat="server" Text="Preview Of Notification Message" CssClass="reportheader"></asp:Label>
                </div>
                <div class="row justify-content-center">
                    <div class="display-name-heading text-center clearfix">
                        <div class="d-inline-block division-name">
                            <asp:Label ID="lblyr" runat="server" Text="Select the Year"></asp:Label>
                        </div>
                        <div class="d-inline-block align-middle">
                            <div class="single-des-option">
                                <div style="float: left; width: 75%">
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right; width: 20%;">
                                    <asp:Button ID="btnpgo" runat="server" Text="Go" OnClick="btnlink_goclick" CssClass="savebutton" Width="40px" />
                                </div>

                            </div>
                        </div>
                    </div>
                  
                    <div class="display-reportMaintable clearfix" style="padding-top:50px">
                        <div class="table-responsive" style="scrollbar-width: thin;">

                            <asp:GridView ID="grdnotifymsg" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                GridLines="None" CssClass="table"
                                HeaderStyle-HorizontalAlign="Left" Width="100%" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notification Messages" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmsg" runat="server" Text='<% #Eval("Notification_Message") %>' onkeyup="sync()"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area" />
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>
</body>
</html>
