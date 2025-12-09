<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Car_Service.aspx.cs" Inherits="Car_Service" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <%--  <link type="text/css" rel="stylesheet" href="css/style.css" />--%>
    <style type="text/css">
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
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
            <%--<ucl:Menu ID="menu1" runat="server" />--%>

            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />


            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                         <h2 class="text-center">CAR SERVICE</h2>

                        <br />
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin">
                                <asp:GridView ID="grdCarservice" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="False"
                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                    AllowSorting="True">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdCarservice.PageIndex * grdCarservice.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="sf_Code" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White"  Visible="false">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSF" runat="server" Text='<%#Eval("From_Sf_Code")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF" runat="server" Text='<%#Eval("From_Sf_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDG" runat="server" Text='<%#Eval("From_HQ")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("From_Designation")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp Code" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_emp_id")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%#Eval("dt")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:HyperLinkField HeaderText="view" Text="Click View" DataNavigateUrlFormatString="Carservice.aspx?Sl_No={0}&amp;type=1"
                                            DataNavigateUrlFields="Sl_No">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
     
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
