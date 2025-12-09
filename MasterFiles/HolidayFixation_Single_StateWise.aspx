<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayFixation_Single_StateWise.aspx.cs"
    Inherits="MasterFiles_HolidayFixation_Single_StateWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday Fixation</title>
    <link type="text/css" rel="stylesheet" href="../css/font-awesome.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
        .lblHeading {
            color: Maroon;
            text-decoration: underline;
            font-size: 25px;
            font-weight: bolder;
        }
    </style>
    <script type="text/javascript">
        function confirm_Save() {
            if (confirm('Do you want to delete Holiday?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        $(document).ready(function () {
            $('#btnGo').click(function () {
                var st = $('#<%=ddlState.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select State."); $('ddlState').focus(); return false; }
            });
        });

        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= gvSingleSW.ClientID %>');

            if (grid != null) {
                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';

                for (i = 2; i < Incre; i++) {
                    if (Inputs[i].type != '') {

                        if (Inputs[i].type == 'text') {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }

                            var ddlHolidayName = document.getElementById('gvSingleSW_ctl02_ddlHolidayName');
                            var ddlDay = document.getElementById('gvSingleSW_ctl02_ddlDay');
                            var ddlMonth = document.getElementById('gvSingleSW_ctl02_ddlMonth');
                            var txtYear = document.getElementById('gvSingleSW_ctl02_txtYear');

                            if (ddlHolidayName.value == '0') {
                                alert('Select Holiday Name')
                                ddlHolidayName.focus();
                                return false;
                            }
                            if (ddlDay.value == '0') {
                                alert('Select Day')
                                ddlDay.focus();
                                return false;
                            }
                            if (ddlMonth.value == '0') {
                                alert('Select Month')
                                ddlMonth.focus();
                                return false;
                            }
                            if (confirm("Are you sure you want to Update?"))
                                return true;
                            else return false;
                        }
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
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

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Holiday Fixation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblstate" runat="server" Text="Select the State:" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="lblYr" runat="server" Text="Year:" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlYr" runat="server" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton"
                                            OnClick="btnGo_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>

                            <br />
                            <br />
                            <center>
                                <br />
                                <asp:Label ID="lblSelect" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"
                                    Text="Select the State & Year and Press the 'Go' Button"></asp:Label>
                            </center>
                            <%--    <asp:UpdatePanel ID="upStateWiseHoliday" runat="server">
                                        <ContentTemplate>--%>
                            <div id="div1" runat="server" style="width: 100%;">
                                <div id="divSingleSW" runat="server" visible="false">
                                    <div class="display-table clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin;">


                                            <asp:GridView ID="gvSingleSW" runat="server"
                                                AutoGenerateColumns="false" GridLines="None" HorizontalAlign="Center" OnRowDeleting="gvSingleSW_RowDeleting"
                                                Width="100%" CssClass="table" RowStyle-HorizontalAlign="Center">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Holiday Name" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlHolidayName" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlHolidayName_SelectedIndexChanged" DataTextField="Holiday_Name"
                                                                DataValueField="Holiday_Name_Sl_No">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Holiday Date" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div class="row clearfix">
                                                                <div class="col-lg-4">
                                                                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <%--CssClass="day"--%>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                                        <%--CssClass="month" --%>
                                                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                                        <asp:ListItem Value="01">January</asp:ListItem>
                                                                        <asp:ListItem Value="02">February</asp:ListItem>
                                                                        <asp:ListItem Value="03">March</asp:ListItem>
                                                                        <asp:ListItem Value="04">April</asp:ListItem>
                                                                        <asp:ListItem Value="05">May</asp:ListItem>
                                                                        <asp:ListItem Value="06">June</asp:ListItem>
                                                                        <asp:ListItem Value="07">July</asp:ListItem>
                                                                        <asp:ListItem Value="08">August</asp:ListItem>
                                                                        <asp:ListItem Value="09">September</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <asp:TextBox ID="txtYear" runat="server" CssClass="input" MaxLength="4"
                                                                        placeholder="YYYY" Height="38px" Width="100%" Enabled="false"></asp:TextBox>
                                                                    <%--CssClass="year" --%>

                                                                    <asp:TextBox ID="txtDate" runat="server" Text="DD-MM-YYYY" name="hidden" CssClass="input"
                                                                        Width="100px" Visible="false"></asp:TextBox>
                                                                    <asp:TextBox ID="txtOldDate" runat="server" Text="" name="hidden" CssClass="input"
                                                                        Width="100px" Visible="false"></asp:TextBox>
                                                                    <asp:Label ID="lblMulti" runat="server" Visible="false" Text=""></asp:Label>
                                                                </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblbtnDel" runat="server" ToolTip="Delete Row" OnClientClick="return confirm_Save();"
                                                                CommandName="Delete" SkinID="lblMand"><i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <br />
                                    <br />
                                    <div class="row justify-content-center ">
                                        <asp:Button ID="btnSave" CssClass="savebutton" runat="server"  CommandName="Save"
                                            Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" />
                                        <asp:Button ID="btnClear" CssClass="resetbutton" runat="server" 
                                            Text="Clear" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                            <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                        </div>
                    </div>

                      <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>

        </div>
        <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
    <script type="text/javascript" src="http://malsup.github.io/jquery.blockUI.js"></script>
    <script type="text/javascript">
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                $('#<%=divSingleSW.ClientID%>').block({
                    message: '<h1>Please Wait...</h1>',
                    css: {
                        border: '3px solid #a00',
                        padding: '10px',
                        fontWeight: 'bold'
                    }
                });
            }
        }
    </script>
</body>
</html>
