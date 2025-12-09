<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePageRest.aspx.cs" Inherits="MasterFiles_Options_HomePageRest" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approval Mandatory Setup</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style type="text/css">
        /*#tblDivisionDtls
        {
            margin-left: 300px;
        }
        #tblLocationDtls
        {
            margin-left: 300px;
        }
        .style2
        {
            width: 92px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }*/
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
        /*.padding
        {
            padding:3px;
        }
        td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
           .chkNew label 
{  
    padding-left: 5px; 
}*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:CheckBoxList ID="chkNew" runat="server" RepeatDirection="Horizontal" RepeatColumns="1">
                                    <asp:ListItem Value="0" Text="DCR"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="TP"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Leave"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Expense"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Listed dr Addition"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Listed dr Deactivation"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Listed dr Addition against Deactivation"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="SS Entry"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Doctor Service Form"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="savebutton" OnClick="btnSave_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
