<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="TargetFixationValue.aspx.cs" Inherits="TargetFixationProduct" %>

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

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
            overflow-x:scroll !important;
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

            .display-table .table tr:nth-child(2) td:first-child {
                color: #636d73 !important;
            }*/
        .display-table .table th {
            font-size: 12px !important;
            font-weight: 401 !important;
        }
        .display-table .table td {
            padding: 5px !important;
        }
    </style>
    <ucl:Menu ID="menu" runat="server" />
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1 || (keyCode == 46) && ($(this).indexOf('.') != -1));
            return ret;
        }

        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
        }

        $(document).ready(function () {
            $('#CmdGo').click(function () {
                var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (st == "---Select Clear---") { alert("Select Field Force Name."); $('ddlFieldForce').focus(); return false; }
                var st = $('#<%=ddlYear.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Year."); $('ddlYear').focus(); return false; }
                var st = $('#<%=ddlmode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Mode."); $('ddlmode').focus(); return false; }
            });
        });

    </script>
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
    <div class=" home-section-main-body position-relative clearfix">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <center>
                    <table>
                        <tr>
                            <td align="center">
                                <h2 class="text-center">Target Fixation Valuewise</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="designation-area clearfix">
                    <div class="single-des clearfix">
                        <asp:Label ID="lblFieldForceName" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
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
                        <div class="table-responsive" style="overflow:inherit; scrollbar-width: thin;">
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
                                        <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_Name" runat="server" Text='<%# Eval("Sf_Name") %>' Width="250px">
                                                </asp:Label>
                                                <asp:HiddenField ID="hdnSf_Code" runat="server" Value='<%# Eval("Sf_Code") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHQ" runat="server" Text='<%# Eval("Sf_HQ") %>' Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("sf_Designation_Short_Name") %>' Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth4" runat="server" Width="50px" class="textbox" Text='<%# Eval("Apr_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth5" runat="server" Width="50px" class="textbox" Text='<%# Eval("May_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth6" runat="server" Width="50px" class="textbox" Text='<%# Eval("Jun_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth7" runat="server" Width="50px" class="textbox" Text='<%# Eval("Jul_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth8" runat="server" Width="50px" class="textbox" Text='<%# Eval("Aug_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth9" runat="server" Width="50px" class="textbox" Text='<%# Eval("Sep_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth10" runat="server" Width="50px" class="textbox" Text='<%# Eval("Oct_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth11" runat="server" Width="50px" class="textbox" Text='<%# Eval("Nov_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth12" runat="server" Width="50px" class="textbox" Text='<%# Eval("Dec_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth1" runat="server" Width="50px" class="textbox" Text='<%# Eval("Jan_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth2" runat="server" Width="50px" class="textbox" Text='<%# Eval("Feb_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth3" runat="server" Width="50px" class="textbox" Text='<%# Eval("Mar_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:GridView ID="gvTarget2" runat="server" AlternatingRowStyle-CssClass="alt"
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
                                        <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_Name" runat="server" Text='<%# Eval("Sf_Name") %>' Width="250px">
                                                </asp:Label>
                                                <asp:HiddenField ID="hdnSf_Code" runat="server" Value='<%# Eval("Sf_Code") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHQ" runat="server" Text='<%# Eval("Sf_HQ") %>' Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("sf_Designation_Short_Name") %>' Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth1" runat="server" Width="50px" class="textbox" Text='<%# Eval("Jan_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth2" runat="server" Width="50px" class="textbox" Text='<%# Eval("Feb_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth3" runat="server" Width="50px" class="textbox" Text='<%# Eval("Mar_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth4" runat="server" Width="50px" class="textbox" Text='<%# Eval("Apr_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth5" runat="server" Width="50px" class="textbox" Text='<%# Eval("May_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth6" runat="server" Width="50px" class="textbox" Text='<%# Eval("Jun_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth7" runat="server" Width="50px" class="textbox" Text='<%# Eval("Jul_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth8" runat="server" Width="50px" class="textbox" Text='<%# Eval("Aug_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth9" runat="server" Width="50px" class="textbox" Text='<%# Eval("Sep_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth10" runat="server" Width="50px" class="textbox" Text='<%# Eval("Oct_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth11" runat="server" Width="50px" class="textbox" Text='<%# Eval("Nov_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMonth12" runat="server" Width="50px" class="textbox" Text='<%# Eval("Dec_Val") %>'
                                                    Style="text-align: right;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
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
