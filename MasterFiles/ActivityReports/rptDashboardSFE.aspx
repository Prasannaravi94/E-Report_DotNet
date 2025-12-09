<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDashboardSFE.aspx.cs"
    Inherits="MasterFiles_rptDashboardSFE" %>

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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#ffeb95';
            element.style.cursor = 'pointer';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';

        }
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .mGrid th {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .mGrid .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .box {
            background: #FFFFFF;
            border: 2px solid #5f9ea0;
            border-radius: 8px;
        }

        .tableHead {
            background: white;
            color: black;
            border-style: solid;
            border-width: 1px;
            border-color: #a2cd5a;
        }

        .break {
            height: 10px;
        }

        .tableHead1 {
            background: #FFFBEF;
            color: black;
            padding: 10px;
            box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -webkit-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -moz-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -o-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            border-radius: 8px;
        }

        .tableHead2 {
            background: #F4FFFF;
            color: black;
            padding: 10px;
            box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -webkit-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -moz-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            -o-box-shadow: 0 1px 8px rgba(20, 46, 110, 0.1);
            border-radius: 8px;
        }


        .txtbox {
            border-top-left-radius: 0px;
            border-top-right-radius: 0px;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            width: 100%;
            //padding-left: 20px;
            height: 35px;
            margin-bottom: 5px;
            color: black;
            font-weight: 400 !important;
            font-size: 10pt;
        }

        .circle {
            height: 50px;
            width: 50px;
            background-color: #555;
            border-radius: 50%;
        }


        .parallelogram {
            width: 100px;
            height: 50px;
            transform: skew(20deg);
            background: #555;
        }

        .starsix {
            width: 0;
            height: 0;
            border-left: 50px solid transparent;
            border-right: 50px solid transparent;
            border-bottom: 100px solid red;
            position: relative;
        }


        .cone {
            width: 0;
            height: 0;
            border-left: solid transparent;
            border-right: solid transparent;
            border-top: solid Tomato;
            -moz-border-radius: 20%;
            -webkit-border-radius: 20%;
            border-radius: 50%;
        }

        .textbox {
            border-radius: 0px !important;
            border: 1px solid #d1e2ea !important;
            font-size: 14px !important;
            padding-left: 0px !important;
            height: 30px !important;
        }

        .divtextbox {
            width: 16%;
            float: left;
        }

        .divlabel {
            color: Blue;
            font-size: 14px;
            vertical-align: middle;
        }

        .display-table {
            line-height: 1px !important;
        }

            .display-table .table td {
                padding: 15px 20px !important;
                border: 1px solid #dee2e6 !important;
            }

        #addtex_Consol1 span, #addtex_Consol2 span, #addtex_Consol3 span, #addtex_Consol4 span, #addtex_Consol5 span {
            display: contents !important;
            font-size: 14px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" id="tbl" runat="server">
                <tr>
                    <td width="80%"></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <p>
                                        <asp:Label ID="Label10" runat="server" Text="Print" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                    </p>
                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <p>
                                        <asp:Label ID="Label11" runat="server" Text="Excel" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                    </p>
                                </td>
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <p>
                                        <asp:Label ID="Label12" runat="server" Text="Close" CssClass="label" Font-Size="14px" Font-Bold="true"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server">
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div align="center">
                            <asp:Label ID="lblHead" runat="server" Text="SFE - Dash Board (at a Glance) " CssClass="reportheader"></asp:Label>
                            <br /><br />
                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader"></asp:Label>
                            <br /><br />
                            <asp:Panel ID="pnlconsol" runat="server" Visible="false">
                                <table width="95%">
                                    <tr>
                                        <td align="center" class="tableHead2">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="Coverage (Count of fieldforce - Range of %)"></asp:Label>
                                            <div id="addtex_Consol6" runat="server" align="left" style="margin-top: 7px !important;">
                                            </div>
                                        </td>
                                        <td width="20px"></td>
                                        <td align="center" class="tableHead1">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="Call Average (Count of fieldforce - Rangewise)"></asp:Label>
                                            <div id="addtex_Consol7" runat="server" align="left" style="margin-top: 7px !important;">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tableHead1">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="Missed Call (Count of fieldforce - Range of %)"></asp:Label>
                                            <div id="addtex_Consol8" runat="server" align="left" style="margin-top: 7px !important;">
                                            </div>
                                        </td>
                                        <td width="20px"></td>
                                        <td align="center" class="tableHead2">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="No.of Days in Field (Count of fieldforce - Rangewise)"></asp:Label>
                                            <div id="addtex_Consol9" runat="server" align="left" style="margin-top: 7px !important;">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tableHead1">
                                            <asp:Label ID="Label1" runat="server" Text="Drs Visit - Categorywise (Count of fieldforce - Range of %)"
                                                ForeColor="#414D55" Font-Bold="True" Font-Size="15px"></asp:Label>
                                            <div align="left" id="addtex_Consol1" runat="server" style="margin-top: 7px !important;">
                                            </div>
                                        </td>
                                        <td width="20px"></td>
                                        <td align="center" class="tableHead2">
                                            <asp:Label ID="Label2" runat="server" Text="Drs Missed - Categorywise (Count of fieldforce - Range of %)"
                                                ForeColor="#414D55" Font-Bold="True" Font-Size="15px"></asp:Label>
                                            <div align="left" id="addtex_Consol2" runat="server" style="margin-top: 7px !important;">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tableHead2">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="Drs Visit - Frequencywise (Count of fieldforce - Range of %)"></asp:Label>
                                            <div id="addtex_Consol3" runat="server" style="margin-top: 7px !important;" align="left">
                                            </div>
                                        </td>
                                        <td width="20px"></td>
                                        <td align="center" class="tableHead1">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="Drs Missed - Frequencywise (Count of fieldforce - Range of %)"></asp:Label>
                                            <div id="addtex_Consol4" runat="server" style="margin-top: 7px !important;" align="left">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="break"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tableHead1">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="15px"
                                                ForeColor="#414D55" Text="Drs Visit - Classwise (Count of fieldforce - Range of %)"></asp:Label>
                                            <div id="addtex_Consol5" runat="server" style="margin-top: 7px !important;" align="left">
                                            </div>
                                        </td>
                                        <td width="20px"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div align="center" id="addtextbox" runat="server">
                            </div>
                            <div align="center" style="width: 90%;">
                                <div class="divtextbox">
                                    <asp:TextBox ID="Tomatocolor" runat="server" Width="30px" CssClass="textbox" BackColor="Tomato" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="Tomato" runat="server" CssClass="divlabel" OnClick="Tomato_Click"></asp:LinkButton>
                                </div>
                                <div class="divtextbox">
                                    <asp:TextBox ID="Yellowcolor" runat="server" Width="30px" CssClass="textbox" BackColor="Yellow" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="Yellow" runat="server" CssClass="divlabel" OnClick="Yellow_Click"></asp:LinkButton>
                                </div>
                                <div class="divtextbox">
                                    <asp:TextBox ID="LightGreencolor" runat="server" Width="30px" CssClass="textbox" BackColor="LightGreen" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="LightGreen" runat="server" CssClass="divlabel" OnClick="LightGreen_Click"></asp:LinkButton>
                                </div>
                                <div class="divtextbox">
                                    <asp:TextBox ID="LightPinkcolor" runat="server" Width="30px" CssClass="textbox" BackColor="LightPink" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="LightPink" runat="server" CssClass="divlabel" OnClick="LightPink_Click"></asp:LinkButton>
                                </div>
                                <div class="divtextbox">
                                    <asp:TextBox ID="Aquacolor" runat="server" Width="30px" CssClass="textbox" BackColor="Aqua" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="Aqua" runat="server" CssClass="divlabel" OnClick="Aqua_Click"></asp:LinkButton>
                                </div>
                                <div class="divtextbox">
                                    <asp:TextBox ID="SkyBluecolor" runat="server" Width="30px" CssClass="textbox" BackColor="SkyBlue" Enabled="false"></asp:TextBox>
                                    <asp:LinkButton ID="SkyBlue" runat="server" CssClass="divlabel" OnClick="SkyBlue_Click"></asp:LinkButton>
                                </div>
                                <br />
                            </div>
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="designation-reactivation-table-area clearfix">
                                    <br />
                                    <div class="display-table clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                            <asp:GridView ID="grdAppdashboard" runat="server" Width="100%"
                                                OnRowDataBound="grdAppdashboard_Rowdatabound" HorizontalAlign="Center" OnRowCreated="grdAppdashboard_RowCreated"
                                                ShowHeader="false" AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                                AlternatingRowStyle-CssClass="alt" AllowSorting="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsf_name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Desigantion" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Joining" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsf_joining_date" runat="server" Text='<%#Eval("sf_joining_date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvalue" runat="server" Text='<%#Eval("coverage")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No.of Dr" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotdrlist" runat="server" Text='<%#Eval("totdrlist")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drs Met" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDr_mett" runat="server" Text='<%#Eval("Dr_mett")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drs Seen" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDr_seenn" runat="server" Text='<%#Eval("Dr_seenn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No.of FW" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo_FW" runat="server" Text='<%#Eval("No_FW")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="0 - 10" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl1" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="11 - 20" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl2" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="21 - 30" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl3" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="31 - 40" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl4" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="41 - 50" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl5" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="51 - 60" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl6" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="61 - 70" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl7" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="71 - 80" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl8" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="81 - 90" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl9" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="91 - 95" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl10" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="96 - 100" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                        ItemStyle-Wrap="false" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl11" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Color" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif"
                                                                ForeColor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="sf_code" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                                        Visible="false" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsf_code" runat="server" Text='<%#Eval("sf_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
