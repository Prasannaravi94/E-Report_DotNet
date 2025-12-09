<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_NonDespatch_Sample.aspx.cs" Inherits="MIS_Reports_DCR_NonDespatch_Sample" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Non-Despatch Sample</title>
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
   
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                         <h2 class="text-center">DCR Non-Despatch Sample</h2>
                        <br />
                        <div class="designation-area clearfix">
                           
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" SkinID="lblMand" CssClass="label"></asp:Label>

                                    <asp:DropDownList ID="ddlFrmMonth" runat="server" SkinID="ddlRequired">
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
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="From Year" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmYear" runat="server" Width="80px" SkinID="ddlRequired">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <br />
                                <div class="single-des clearfix">
                                    <br />
                                     <br />
                                    <center>
                                    <asp:LinkButton ID="btnSubmit" runat="server" Font-Size="Medium" Font-Bold="true"
                                        Text="Download Excel" OnClick="btnSubmit_Click" />
                                        </center>
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