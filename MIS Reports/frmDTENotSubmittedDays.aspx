<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDTENotSubmittedDays.aspx.cs" Inherits="MIS_Reports_frmDTENotSubmittedDays" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Not Submitted Analysis</title>
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <%-- <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {

                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }




            });
        }); 
    </script>--%>

    <style type="text/css">
        .display-callAvgreporttable1 .table tr:first-child td:first-child {
            color: white;
            border-bottom: 10px solid #fff;
            border-radius: 8px 0 0 8px;
        }

        .display-callAvgreporttable1 .table tr:first-child td {
            padding: 1px;
            vertical-align: inherit;
            border-top: none;
            border-bottom: 10px solid #fff;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            border-top: 0px;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            text-transform: uppercase;
        }

        .display-callAvgreporttable1 .table tr:nth-child(2) td:first-child,
        .display-callAvgreporttable1 .table tr:nth-child(2) td {
            color: #636d73;
            /*padding: 20px 5px;*/
            vertical-align: inherit;
            border-top: none;
            border-bottom: 10px solid #fff;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            border-top: 0px;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            text-transform: uppercase;
        }

        .display-callAvgreporttable1 .table tr:nth-child(3) td:first-child {
            color: white;
            background-color: #414D55;
        }

        .display-callAvgreporttable1 {
            color: #636d73;
            font-size: 12px;
            font-weight: 400;
        }

            .display-callAvgreporttable1 .table td {
                padding: 4px 10px;
                border-left: 1px solid #DCE2E8;
                vertical-align: inherit;
            }

            .display-callAvgreporttable1 .table tr td:first-child {
                background-color: #f1f5f8;
                text-align: center;
                border: 0px;
                padding: 2px 10px;
                vertical-align: inherit;
                color: rgb(99, 109, 115);
            }

            .display-callAvgreporttable1 .table tr:first-child td {
                padding: 4px 10px;
                border-bottom: 2px solid #dce2e8;
                position: sticky;
                position: -webkit-sticky;
                top: 0px;
                z-index: 0;
                background: inherit;
            }

            .display-callAvgreporttable1 .table tr:nth-child(2) td:first-child, .display-callAvgreporttable1 .table tr:nth-child(2) td {
                padding: 4px 10px;
                border-bottom: 2px solid #dce2e8;
                position: sticky;
                position: -webkit-sticky;
                top: 28px;
                z-index: 0;
                background: inherit;
            }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body style="overflow-x: scroll;">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <br />
            <br />
            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Not Submitted Analysis</h2>


                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Visible="true" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" Visible="true" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblState" runat="server" Text="Field Force Name" CssClass="label"></asp:Label>

                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                                                CssClass="nice-select" Visible="false">
                                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                                <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                                CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" CssClass="custom-select2 nice-select" Width="100%">
                                    </asp:DropDownList>
                                    <br />

                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    <%--          <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                            </div>

                            <%--              <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>

                        <br />
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server" Text="View"
                                CssClass="savebutton" OnClick="btnSubmit_Click" />&nbsp&nbsp&nbsp
                            <asp:LinkButton ID="btnExcel" Visible="false" Text="Excel" OnClick="btnExcel_Click" runat="server" Style="font-size: 14px;"></asp:LinkButton>
                        </div>

                    </div>
                </div>
                <br />

                <div class="row justify-content-center">
                    <div class="col-lg-12">

                        <div class="display-callAvgreporttable1 clearfix ">
                            <div class="table-responsive" style="overflow: inherit; scrollbar-width: thin;">

                                <asp:GridView ID="GrdDoctor" runat="server" Width="90%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" PageSize="10" GridLines="None"
                                    CssClass="table" OnRowCreated="GrdDoctor_RowCreated" OnRowDataBound="GrdDoctor_OnRowDataBound" ShowHeader="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <%--<ControlStyle Width="10%"></ControlStyle>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left"
                                            Visible="false">
                                            <ControlStyle Width="20%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSfCode" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp_Code" ItemStyle-HorizontalAlign="Left"
                                            Visible="true">
                                            <ControlStyle Width="20%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsrDfd_UserName" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="250px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="110px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_HQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Desig" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="50px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig_color" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="sf_TP_Active_Dt" ItemStyle-HorizontalAlign="Left"
                                            Visible="false">
                                            <ControlStyle Width="70px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_TP_Active_Dt" runat="server" Text='<%# Bind("sf_TP_Active_Dt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Joining_Date" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="70px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOJ" runat="server" Text='<%# Bind("sf_joining_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resigned_Date" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="70px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblResigned_Date" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="State_Name" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="150px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblState_Name" runat="server" Text='<%# Bind("State_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting_Manager1" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="150px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReporting_Manager1" runat="server" Text='<%# Bind("Reporting_Manager1") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting_Manager2" ItemStyle-HorizontalAlign="Left">
                                            <ControlStyle Width="158px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReporting_Manager2" runat="server" Text='<%# Bind("Reporting_Manager2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="DCR Not Submit Days" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="10%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCR_Not_Submit" Visible="true" runat="server" Text='<%# Bind("DCR_Not_Submit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TP Not Submit (Y/N)" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="10%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTP_Not_Submit" Visible="false" runat="server" Text='<%# Bind("TP_Not_Submit") %>'></asp:Label>
                                                <asp:Label ID="imgTP_Not_Submit" Font-Bold="true" Visible="false" Text="Y" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MR" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="10%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplied" Visible="false" runat="server" Text='<%# Bind("MR") %>'></asp:Label>
                                                <asp:Label ID="imgAdmin1" Font-Bold="true" Visible="true" Text="-" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Manager" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="10%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMR" Visible="false" runat="server" Text='<%# Bind("MGR") %>'></asp:Label>
                                                <asp:Label ID="imgAdmin2" Font-Bold="true" Visible="true" Text="-" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Admin" ItemStyle-HorizontalAlign="Center">
                                            <ControlStyle Width="10%"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMGR" Visible="false" runat="server" Text='<%# Bind("Admin_Mgr") %>'></asp:Label>
                                                <asp:Label ID="imgAdmin3" Font-Bold="true" Visible="true" Text="-" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Last_DCR_Date" ItemStyle-HorizontalAlign="Left" Visible="false">
                                            <ControlStyle Width="70px"></ControlStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="Last_DCR_Date" runat="server" Text='<%# Bind("Last_DCR_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </div>
                        </div>

                        <div class="no-result-area" id="div1" runat="server" visible="false">
                            No Records Found
                        </div>

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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>


        <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
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
    </form>
</body>
</html>
