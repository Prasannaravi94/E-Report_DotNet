<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar_Consolidated.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="MasterFiles_Calendar_Consolidated" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consolidated View</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
        .aclass {
            border: 1px solid lighgray;
        }

        .aclass {
            width: 50%;
        }

            .aclass tr td {
                background: White;
                font-weight: bold;
                color: Black;
                border: 1px solid black;
                border-collapse: collapse;
            }

            .aclass th {
                border: 1px solid black;
                border-collapse: collapse;
                background: LightBlue;
            }

        .BUTTON : hover {
            background-color: #A6A6D2;
        }

        .BUTTON {
            float: right;
            margin-right: 25px;
        }

        .lbl {
            color: Red;
        }

        .table {
            border: 0;
        }
        .Gridlabel
        {
            color: Red;
            font-size: 18px;
            font-weight: 500;
            padding-bottom:20px;
        }
    </style>
    <script type="text/javascript">

        function blinker() {
            $('.blink_me').fadeOut(500);
            $('.blink_me').fadeIn(500);
        }
        setInterval(blinker, 1500);

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Height="25px" Width="70px"
                Text="Back" OnClick="btnBack_Click" Visible="false" />

            <center>
                <asp:Label ID="lblErMsg" CssClass="blink_me" ForeColor="red"
                    Font-Bold="true" Font-Size="Small" Font-Italic="true" Text="ERROR :- If Shows Red Color for the below Tables,<br /> Please Reset Date because Multiple Holiday entered for Same Date!!.. "
                    runat="server" Visible="false" />
            </center>
        </div>

        <div class="container home-section-main-body position-relative clearfix">
          <%--  <br />--%>
            <div class="row justify-content-center">

                <div class="col-lg-11">
                    <h2 class="text-center">Consolidated View</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading text-center clearfix">
                            <div class="d-inline-block division-name">
                                <asp:Label ID="lblyear" runat="server" Text="Year"></asp:Label>
                            </div>
                            <div class="d-inline-block align-middle">
                                <div class="single-des-option">
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:Panel ID="pnl" runat="server" EnableViewState="false" align="Center">
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
             <%--   <asp:Button ID="btnback" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click"/>--%>
            </div>

        </div>
        <br />
        <br />
    </form>
</body>
</html>
