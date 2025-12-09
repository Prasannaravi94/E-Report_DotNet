<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="TargetFixationProduct.aspx.cs" Inherits="TargetFixationProduct" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/responsive.css" />
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .textbox {
            background: white;
            border: 1px double #DDD;
            border-radius: 5px;
            box-shadow: 0 0 1px #333;
            color: #666;
            height: 20px;
            width: 275px;
        }

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
        }


        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .chklabel {
            color: #636d73;
            font-size: 12px;
            font-weight: 401;
            text-transform: uppercase;
            text-align: center;
            padding-left: 10px;
        }

        .textbox {
            border-radius: 8px !important;
            border: 1px solid #d1e2ea !important;
            background-color: #f4f8fa !important;
            color: #90a1ac !important;
            font-size: 14px !important;
            padding-right: 10px !important;
            height: 35px !important;
            text-align: right !important;
        }

        .tbRCPAth {
            background-color: #f1f5f8;
            height: 50px;
            width: 20%;
            text-align: center;
        }

        /*.display-table .table th:first-child {
            border-radius: 0px 0 0 0px !important;
            background-color: #f1f5f8 !important;
            color: #636d73 !important;
            font-size: 12px !important;
            font-weight: 401 !important;
            border-left: 0px solid #F1F5F8 !important;
        }

        .display-table .table th {
            font-size: 12px !important;
            font-weight: 401 !important;
        }

        .display-table .table tr:nth-child(2) td:first-child {
            background-color: #fff !important;
            color: #ffffff !important;
            border: 1px solid #dee2e6 !important;
        }

        .display-table .table tr td:first-child {
            background-color: #fff !important;
            border: 1px solid #dee2e6 !important;
        }

        .display-table .table td {
            border: 1px solid #DCE2E8 !important;
        }

        .display-table .table {
            line-height: 1px !important;
        }

            .display-table .table tr:nth-child(2) td:first-child {
                color: #636d73 !important;
            }*/
        .display-table .table th {
            font-size: 12px !important;
            font-weight: 401 !important;
        }
    </style>
    <script type="text/javascript">
        function Validate() {
            var ddlFieldForce = document.getElementById('<%=ddlFieldForce.ClientID%>');
            var ddlYear = document.getElementById('<%=ddlYear.ClientID%>');
            var ddlmode = document.getElementById('<%=ddlmode.ClientID%>');

            //        if (ddlFieldForce.selectedIndex == '--Select--') {
            //            alert("Please select FieldForce!!");
            //            ddlFieldForce.focus();
            //            return false;
            //        }     
            //        else if (ddlmode.selectedIndex == 0) {
            //            alert("Please select Mode!!");
            //            ddlmode.focus();
            //            return false;
            //        }
        }
    </script>

    <ucl:Menu ID="menu" runat="server" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.on('keyup', function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                   function (n) {
                       return exp.test($(n).text());
                   });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                   );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
    <div class="container home-section-main-body position-relative clearfix">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <center>
                    <table>
                        <tr>
                            <td align="center">
                                <h2 class="text-center">Target Fixation Productwise</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="designation-area clearfix">
                    <div class="single-des clearfix">
                        <asp:Label ID="lblFieldForceName" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="lblYear" runat="server" Text="Month " CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="lblperiod" runat="server" Text="Mode" SkinID="lblMand" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="CmdGo" runat="server" Text="GO" OnClick="CmdGo_Click" OnClientClick="return Validate();" CssClass="savebutton" />
                </div>
            </div>
            <div class="col-lg-12">
                <div class="designation-reactivation-table-area clearfix">
                    <div class="display-table clearfix" runat="server">
                        <div class="table-responsive" style="height: 500px; scrollbar-width: thin;">
                            <br />
                            <center>
                                <asp:GridView ID="gvTarget" runat="server" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" CssClass="table"
                                    GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                    Width="100%" ShowFooter="true" FooterStyle-HorizontalAlign="Center"
                                    OnRowDataBound="gvTarget_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("ProductName") %>' Width="150px">
                                                </asp:Label>
                                                <asp:HiddenField ID="hdnProdCode" runat="server" Value='<%# Eval("ProductCode") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pack" HeaderText="Pack" />
                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth4" runat="server" Width="100px" class="textbox" Text='<%# Eval("Apr") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth5" runat="server" Width="100px" class="textbox" Text='<%# Eval("May") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth6" runat="server" Width="100px" class="textbox" Text='<%# Eval("Jun") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth7" runat="server" Width="100px" class="textbox" Text='<%# Eval("Jul") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth8" runat="server" Width="100px" class="textbox" Text='<%# Eval("Aug") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth9" runat="server" Width="100px" class="textbox" Text='<%# Eval("Sep") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth10" runat="server" Width="100px" class="textbox" Text='<%# Eval("Oct") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth11" runat="server" Width="100px" class="textbox" Text='<%# Eval("Nov") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth12" runat="server" Width="100px" class="textbox" Text='<%# Eval("Dec") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth1" runat="server" Width="100px" class="textbox" Text='<%# Eval("Jan") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth2" runat="server" Width="100px" class="textbox" Text='<%# Eval("Feb") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth3" runat="server" Width="100px" class="textbox" Text='<%# Eval("Mar") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <br />
                                <asp:GridView ID="gvTarget2" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table" OnRowDataBound="gvTarget_RowDataBound" Width="60%">
                                    <HeaderStyle ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("ProductName") %>' Width="150px">
                                                </asp:Label>
                                                <asp:HiddenField ID="hdnProdCode" runat="server" Value='<%# Eval("ProductCode") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Pack" HeaderText="Pack" />
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth1" runat="server" Width="100px" class="textbox" Text='<%# Eval("Jan") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth2" runat="server" Width="100px" class="textbox" Text='<%# Eval("Feb") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth3" runat="server" Width="100px" class="textbox" Text='<%# Eval("Mar") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth4" runat="server" Width="100px" class="textbox" Text='<%# Eval("Apr") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth5" runat="server" Width="100px" class="textbox" Text='<%# Eval("May") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth6" runat="server" Width="100px" class="textbox" Text='<%# Eval("Jun") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth7" runat="server" Width="100px" class="textbox" Text='<%# Eval("Jul") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth8" runat="server" Width="100px" class="textbox" Text='<%# Eval("Aug") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth9" runat="server" Width="100px" class="textbox" Text='<%# Eval("Sep") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth10" runat="server" Width="100px" class="textbox" Text='<%# Eval("Oct") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth11" runat="server" Width="100px" class="textbox" Text='<%# Eval("Nov") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth12" runat="server" Width="100px" class="textbox" Text='<%# Eval("Dec") %>'
                                                    Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </center>
                        </div>
                        <br />
                        <center>
                            <asp:HiddenField ID="hdnTransSlNo" runat="server" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="savebutton" Visible="false" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('.custom-select2').hide();
        });
    </script>
</asp:Content>
