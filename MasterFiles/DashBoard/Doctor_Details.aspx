<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Details.aspx.cs" Inherits="MasterFiles_DashBoard_Doctor_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Speciality/Category/Class</title>
    <style type="text/css">
        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #f1f5f8;
            color: #636d73;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
            border-bottom: 10px solid #fff;
        }

        .display-reportMaintable #tbl tr:nth-child(2) td:first-child {
            background-color: white;
            color: #636d73;
        }

        .display-reportMaintable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }
    </style>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'Doctor_Details.aspx/GetData',
                data: '{}',
                success:
                    function (response) {
                        drawVisualization(response.d);

                    }

            });
        })

        function drawVisualization(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value]);
            }

            new google.visualization.PieChart(document.getElementById('visualization')).
                draw(data, { title: "" });
        }

    </script>
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
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <br />
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="heading" runat="server"></h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Field Force"></asp:Label>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                                    SkinID="ddlRequired" Visible="false">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    CssClass="custom-select2  nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblType" runat="server" Text="Type" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="rdoType" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="-1" Text="--Select--" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Category"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Speciality"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Qualification"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Go" OnClick="btnGo_Click" />
                        </div>

                    </div>
                </div>
                <br />
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">

                        <asp:Panel ID="pnlchart" runat="server" Width="100%" BackColor="White" Visible="true">

                            <div align="center">
                                <asp:Label ID="lblCatg" runat="server" CssClass="reportheader"></asp:Label>
                            </div>
                            <br />

                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin">
                                    <asp:Table ID="tbl" runat="server" GridLines="None" Width="100%" CssClass="table"
                                        align="center">
                                    </asp:Table>
                                </div>
                            </div>
                            <br />
                            <div align="center" style="overflow-x: auto; scrollbar-width: thin">
                                <div id="visualization" style="width: 750px; height: 550px;">
                                </div>
                            </div>

                        </asp:Panel>
                    </div>

                </div>
            </div>

            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
