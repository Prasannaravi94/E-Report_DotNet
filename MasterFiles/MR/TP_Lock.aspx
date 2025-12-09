<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Lock.aspx.cs" Inherits="MasterFiles_MR_TP_Lock" %>


<%--<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu1" TagPrefix="m1" %>
<%@ Register Src="~/UserControl/MR_TP_Menu.ascx" TagName="Menu2" TagPrefix="m2" %>--%>
<%--<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu3" TagPrefix="m3" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Entry</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link type="text/css" rel="stylesheet" href="../../css/MR.css" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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

        .c1 {
            width: 280px;
            height: 240px;
        }

        .c1 {
            width: 280px;
            height: 240px;
        }

        .modalDialog {
            position: fixed;
            font-family: Arial, Helvetica, sans-serif;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(0, 0, 0, 0.8);
            z-index: 99999;
            opacity: 0;
            -webkit-transition: opacity 400ms ease-in;
            -moz-transition: opacity 400ms ease-in;
            transition: opacity 400ms ease-in;
            pointer-events: none;
        }

            .modalDialog:target {
                opacity: 1;
                pointer-events: auto;
            }

            .modalDialog > div {
                width: 400px;
                position: relative;
                margin: 10% auto;
                padding: 5px 20px 13px 20px;
                border-radius: 10px;
                background: #fff;
                background: -moz-linear-gradient(#fff, #999);
                background: -webkit-linear-gradient(#fff, #999);
                background: -o-linear-gradient(#fff, #999);
            }

        .close {
            background: #606061;
            color: #FFFFFF;
            line-height: 25px;
            position: absolute;
            right: -12px;
            text-align: center;
            top: -10px;
            width: 24px;
            text-decoration: none;
            font-weight: bold;
            -webkit-border-radius: 12px;
            -moz-border-radius: 12px;
            border-radius: 12px;
            -moz-box-shadow: 1px 1px 3px #000;
            -webkit-box-shadow: 1px 1px 3px #000;
            box-shadow: 1px 1px 3px #000;
        }

            .close:hover {
                background: #00d9ff;
            }

        .p {
            font-family: Calibri;
            font-size: 14px;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .gridheight {
            overflow-y: auto !important;
            height: 500px !important;
        }

        .dropdownlist1 {
            background-position: 184px;
        }

        .DropDown {
            background-color: yellow;
        }

        .display-table .table td {
            padding: 5px 20px;
        }

        .home-section-main-body {
            padding: 5px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <link href="../../css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>


</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div>
            <div class="container home-section-main-body position-relative clearfix" style="width: 1330px; max-width: 1350px !important;">
                <div class="row justify-content-center" style="height: 100%">
                    <div class="col-lg-12">
                        <asp:Panel runat="server" ID="tpmsglock">
                            <br />

                            <asp:Button runat="server" ID="btnbck" CssClass="BUTTON" Width="85px" Height="26px" Text="BACK" Style="float: right" OnClick="btnbck_Click" />
                            <div align="center">
                                <br />
                                <table id="Table1" runat="server" align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbltour" runat="server" Text="Tour Plan for the Month of " Font-Size="Medium"
                                                ForeColor="Green" Font-Names="Verdana"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="65%" align="center" runat="server" id="Table2">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblf" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                                            <asp:Label ID="lblf1" runat="server" Font-Names="Verdana" ForeColor="#A0522D"
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lble" Text="Employee Id :" runat="server" SkinID="lblMand"></asp:Label>
                                            <asp:Label ID="lble1" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbld" Text="DOJ :" runat="server" SkinID="lblMand"></asp:Label>
                                            <asp:Label ID="lbld1" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblr" Text="Reporting To :" runat="server" SkinID="lblMand"></asp:Label>
                                            <asp:Label ID="lblr1" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <asp:Label ID="lblmsg" runat="server" Text="TP should be approve from App only... " Font-Size="Medium" Visible="false"
                                    ForeColor="Red" Font-Names="Verdana"></asp:Label>

                                <asp:Button runat="server" ID="btnAppRej" Text="Click Here to Approve/reject" CssClass="savebutton" Width="250px" OnClick="btnAppRej_Click" />
                                <br />

                                <div runat="server" id="tpdiv" visible="false">
                                    <center>
                                        <table align="center" runat="server">
                                            <tr>
                                                <td>
                                                    <div class="designation-reactivation-table-area clearfix" style="width: 950px">
                                                        <div class="display-table clearfix">
                                                            <div class="table-responsive ">
                                                                <%-- gridheight--%>
                                                                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>--%>
                                                                <asp:GridView ID="grdTPview" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                                    GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("TP_Date") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDay" runat="server" Text='<%#  Eval("TP_Day") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblWork" runat="server" Text='<%#  Eval("Work") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Route Plan 1" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTerr" runat="server" Text='<%#  Eval("Territory") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>

<%--                                                                        <asp:TemplateField HeaderText="Dr Name" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldr_name" runat="server" Text='<%#  Eval("dr_name") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblObjective" runat="server" Text='<%#  Eval("Objective") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                                <%-- </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="center">


                                                    <asp:Button ID="btnApprove" CssClass="savebutton" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                                    &nbsp; 
                        <asp:Button ID="btnReject" CssClass="savebutton" runat="server" Text="Reject" OnClick="btnReject_Click" />


                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblReject" Text="Rejection reason :" runat="server" SkinID="lblMand"  Visible="false"></asp:Label>
                                                    &nbsp;&nbsp;
                                                     <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                                                         runat="server"></asp:TextBox>
                                                    <asp:Button ID="btnSendBack" CssClass="savebutton" Width="140px" runat="server" Visible="false"
                                                        Text="Send for ReEntry" OnClick="btnSendBack_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
