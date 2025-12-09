<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="DoctorBusinessValuewise_Entry.aspx.cs" EnableViewState="true" Inherits="DoctorBusinessEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu3" TagPrefix="ucl2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        .display-table .table th:first-child {
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

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
        }

        .display-table .table tr:nth-child(2) td:first-child {
            color: #636d73 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="Divid" runat="server">
    </div>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
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
    <script type="text/javascript">
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
        }

        $(document).ready(function () {
            $('#<%=btnGo.ClientID%>').click(function () {
                var st1 = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (st1 == "---Select---") { alert("Select Field Force"); $('ddlFieldForce').focus(); return false; }
                <%--var st = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (st == "--Select--") { alert("Select Month"); $('ddlMonth').focus(); return false; }--%>
            });

            <%--$("#<%=testImg.ClientID%>").hide();
            $('#<%=linkcheck.ClientID%>').click(function () {
                window.setTimeout(function () {
                    $("#<%=testImg.ClientID%>").show();
                }, 500);
            });--%>
        });

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1 || (keyCode == 46) && ($(this).indexOf('.') != -1));
            return ret;
        }

        function ValidateEmptyValue() {
            var gridView = document.getElementById("<%=gvDoctorBusiness.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "textbox" && checkBoxes[i].value != '') {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
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
                                <h2 class="text-center">Doctor Business Valuewise - Entry</h2>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="designation-area clearfix">
                    <div class="single-des clearfix">
                        <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                            OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="single-des clearfix">
                        <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                        <input id="txtMonthYear" type="text" runat="server" class="nice-select" readonly="true"></input>
                        <%--   <asp:Label ID="lblMonth" runat="server" Text="Month " CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                        </asp:DropDownList>--%>
                    </div>
                    <%--    <div class="single-des clearfix">
                        <asp:Label ID="lblYear" runat="server" Text="Year " CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </div>--%>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                    <asp:Button ID="btnClear" CssClass="savebutton" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </div>
            </div>
            <div class="col-lg-8">
                <div class="designation-reactivation-table-area clearfix">
                    <br />
                    <div class="display-table clearfix" runat="server">
                        <div class="table-responsive" style="overflow:inherit; scrollbar-width: thin;">
                            <asp:GridView ID="gvDoctorBusiness" runat="server" AlternatingRowStyle-CssClass="alt"
                                AutoGenerateColumns="false" CssClass="table"
                                GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                Width="100%" ShowFooter="true" FooterStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doctor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoctor" runat="server" Text='<%# Eval("ListedDr_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnDoctor" runat="server" Value='<%# Eval("ListedDrCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Speciality">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpeciality" runat="server" Text='<%# Eval("Doc_Spec_ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Doc_Cat_ShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doctor Business Value in Rs.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDocBusVal" runat="server" CssClass="textbox" Width="100px"
                                                onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area" />
                            </asp:GridView>
                        </div>
                        <center>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" Visible="false" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <center>
            <table width="65%">

                <tr>
                    <td align="center" colspan="2"></td>
                </tr>
            </table>
        </center>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

        
            <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
        <script language="javascript" type="text/javascript">
            $(function () {
                $('.custom-select2').hide();
            });
        </script>
</asp:Content>
