<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Assesment_MGR_View.aspx.cs" Inherits="Assesment_MGR_View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
    <link rel="stylesheet" href="assets/css/responsive.css" />
    <style type="text/css">
        .mGrid {
            /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px Black;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                font-size: small;
                font-family: Calibri;
            }


            .mGrid th {
                padding: 4px 2px;
                color: white;
                background: #A6A6D2;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGrid .pgr {
                background: #A6A6D2;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    text-align: left;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: White;
                    line-height: 12px;
                }

                .mGrid .pgr th {
                    background: #A6A6D2;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
            height: 22px;
        }


        .gridview1 {
            background-color: #666699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview1 a {
                margin: auto 1%;
                border-style: none;
                border-radius: 50%;
                background-color: #444;
                padding: 5px 7px 5px 7px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview1 a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview1 td {
                border-style: none;
            }

            .gridview1 span {
                background-color: #ae2676;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 7px 5px 7px;
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

        .BUTTON {
            background-color: lightblue;
        }

            .BUTTON:hover {
                background-color: #666699;
                color: White;
                border-radius: 10px;
            }
    </style>
    <script language="Javascript" type="text/javascript">
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
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />

            <div class="container home-section-main-body position-relative clearfix" >
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <table align="center">
                            <tr>
                                <td>

                                    <asp:Label ID="lblheader" runat="server" Text="MGR Assessment" CssClass="reportheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="display-reporttable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin">
                                <asp:GridView ID="grdAssessment" runat="server" Width="100%"
                                    AutoGenerateColumns="false" CellPadding="4" CssClass="table" GridLines="None"
                                    EmptyDataText="No Records Found">

                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ControlStyle></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" Width="20px" runat="server" Text='<%# (grdAssessment.PageIndex *grdAssessment.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_id" runat="server" Text='<%#Eval("Sl_No") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Width="200px" Text='<%#Eval("Frm_Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Width="150px" Text='<%#Eval("Frm_Sf_Hq") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Width="80px" Text='<%#Eval("Frm_Sf_Desg") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemp" runat="server" Width="80px" Text='<%#Eval("Emp_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemp" runat="server" Width="80px" Text='<%#Eval("dt") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:HyperLinkField HeaderText="view" Text="Click View" DataNavigateUrlFormatString="Assesment_MGR.aspx?Sl_No={0}&amp;mode=1"
                                            DataNavigateUrlFields="Sl_No">
                                           
                                        </asp:HyperLinkField>

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

        </div>
    </form>
</body>
</html>
